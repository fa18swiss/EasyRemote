using System;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Interop;
using EasyRemote;

namespace WpfAppControl
{
    /// <summary>
    /// AppWrapper.xaml 
    /// </summary>
    public partial class AppWrapper : UserControl, IDisposable
    {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HWND__ {
    
            /// int
            public int unused;
        }

        /// <summary>
        /// creates a new AppWrapper
        /// </summary>
        /// <param name="exeName">path to the executable to wrap</param>
        /// <param name="arguments">command lind arguments to pass to the executable</param>
        /// <param name="mainParent">parent</param>
        /// <param name="parentTabItem">tab item that contains the application</param>
        public AppWrapper(String exeName, String arguments, MainWindow mainParent, TabItem parentTabItem)
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
            this.Loaded += new RoutedEventHandler(OnLoaded);
            this.SizeChanged += new SizeChangedEventHandler(OnResize);

            this.exeName = exeName;
            this.arguments = arguments;
            this.mainParent = mainParent;
            this.parentTabItem = parentTabItem;
        }

        ~AppWrapper()
        {
            this.Dispose();
        }

        /// <summary>
        /// everything is magic, and this ratio is everything
        /// </summary>
        private const double MAGIC_RATIO = 1.25;

        /// <summary>
        /// Track if the application has been created
        /// </summary>
        private bool _iscreated = false;
        
        /// <summary>
        /// Track if the control is disposed
        /// </summary>
        private bool _isdisposed = false;

        /// <summary>
        /// Handle to the application Window
        /// </summary>
        IntPtr _appWin;

        /// <summary>
        /// child process
        /// </summary>
        private Process _childp;

        /// <summary>
        /// The name of the exe to launch
        /// </summary>
        private string exeName = "";

        /// <summary>
        /// The name of the exe to launch
        /// </summary>
        public string ExeName
        {
            get { return exeName; }
        }

        /// <summary>
        /// the arguments to pass to the application
        /// </summary>
        private string arguments = "";

        /// <summary>
        /// the arguments to pass to the application
        /// </summary>
        public string Arguments
        {
            get { return arguments; }
        }

        private MainWindow mainParent = null;
        private TabItem parentTabItem = null;

        [DllImport("user32.dll", SetLastError=true)]
        private static extern long SetParent (IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint="GetWindowLongA", SetLastError=true)]
        private static extern long GetWindowLong (IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint="SetWindowLongA", SetLastError=true)]
        public static extern int SetWindowLongA([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nIndex, int dwNewLong);
        
        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);


        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = -20;
        private const int WS_CAPTION = 0xc00000;
        private const int WS_SYSMENU = 0x80000;
        private const int WS_THICKFRAME = 0x40000;
        private const int WS_MINIMIZE = 0x20000000;
        private const int WS_MAXIMIZEBOX = 0x20000;
        private const int WS_EX_DLGMODALFRAME = 0x00000001;

        
        /// <summary>
        /// Force redraw of control when size changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnSizeChanged(object s, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }


        /// <summary>
        /// Create control when visibility changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnVisibleChanged(object s, RoutedEventArgs e)
        {
           
        }

        /// <summary>
        /// when the form is fully loaded, this function is called and the application is created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        protected void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // If control needs to be initialized/created
            if (_iscreated == false)
            {
                // Mark that control as created
                _iscreated = true;

                // Initialize handle value to invalid
                _appWin = IntPtr.Zero;
                try
                {
                    var procInfo = new ProcessStartInfo(this.exeName, this.arguments);
                    procInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(this.exeName);
                    // Start the process
                    _childp = Process.Start(procInfo);

                    // Wait for process to be created and enter idle condition
                    _childp.WaitForInputIdle();

                    // Get the main handle
                    _appWin = _childp.MainWindowHandle;

                    _childp.EnableRaisingEvents = true;
                    _childp.Exited += ProcessExited;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message + "Error");
                }

                // Put it into this form
                var helper = new WindowInteropHelper(Window.GetWindow(this.AppContainer));
                SetParent(_appWin, helper.Handle);
                
                //get the actual style
                var actualStyle = GetWindowLong(_appWin, GWL_STYLE);
                
                //modify the style
                actualStyle = actualStyle & ~WS_CAPTION;
                actualStyle = actualStyle & ~WS_SYSMENU;
                actualStyle = actualStyle & ~WS_THICKFRAME;
                actualStyle = actualStyle & ~WS_MINIMIZE;
                actualStyle = actualStyle & ~WS_MAXIMIZEBOX;

                // set the modified style as the actual style
                SetWindowLongA(_appWin, GWL_STYLE, (int)actualStyle);

                //modify exstyle
                var actualExStyle = GetWindowLong(_appWin, GWL_EXSTYLE);
                SetWindowLongA(_appWin, GWL_EXSTYLE, (int)actualExStyle | WS_EX_DLGMODALFRAME);
    
                // Move the window to overlay it on this window
                SetRightMoveWindow();
            }
        }

        /// <summary>
        /// removes the tab item from the parent window when the process is killed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProcessExited(object sender, EventArgs e)
        {
            mainParent.RemoveTabItem(parentTabItem);
        }

        /// <summary>
        /// Update display of the executable
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnResize(object s, SizeChangedEventArgs e)
        {
            SetRightMoveWindow();
        }

        /// <summary>
        /// resizes the application window using a MAGIC_RATIO
        /// (Needed because of reasons)
        /// </summary>
        private void SetRightMoveWindow()
        {
            //in case the application has started
            if (this._appWin != IntPtr.Zero)
            {
                Point relativePoint = this.TransformToAncestor(mainParent)
                          .Transform(new Point(0, 0));
                MoveWindow(_appWin, (int)(relativePoint.X*MAGIC_RATIO), (int)(relativePoint.Y*MAGIC_RATIO), (int)(this.ActualWidth * MAGIC_RATIO), (int)(this.ActualHeight * MAGIC_RATIO), true);
            }
        }

        /// <summary>
        /// kills the application
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isdisposed)
            {
                if (disposing)
                {
                    if (_iscreated && _appWin != IntPtr.Zero && !_childp.HasExited)
                    {
                        // Stop the application
                        _childp.Kill();

                        // Clear internal handle
                        _appWin = IntPtr.Zero;
                    }
                }
                _isdisposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
