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

        public AppWrapper(String exeName, String arguments, MainWindow mainParent, TabItem parentTabItem)
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
            this.Loaded += new RoutedEventHandler(OnLoaded);//new RoutedEventHandler(OnVisibleChanged);
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

        private Process _childp;

        /// <summary>
        /// The name of the exe to launch
        /// </summary>
        private string exeName = "";

        public string ExeName
        {
            get { return exeName; }
        }

        private string arguments = "";

        public string Arguments
        {
            get { return arguments; }
        }

        private double ratio = 1.25;

        public double Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }

        private MainWindow mainParent = null;
        private TabItem parentTabItem = null;

        [DllImport("user32.dll", EntryPoint="GetWindowThreadProcessId",  SetLastError=true,
             CharSet=CharSet.Unicode, ExactSpelling=true,
             CallingConvention=CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId); 
            
        [DllImport("user32.dll", SetLastError=true)]
        private static extern IntPtr FindWindow (string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError=true)]
        private static extern long SetParent (IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint="GetWindowLongA", SetLastError=true)]
        private static extern long GetWindowLong (IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint="SetWindowLongA", SetLastError=true)]
        public static extern int SetWindowLongA([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError=true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);
        
        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = -20;
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CAPTION = 0xc00000;
        private const int WS_SYSMENU = 0x80000;
        private const int WS_THICKFRAME = 0x40000;
        private const int WS_MINIMIZE = 0x20000000;
        private const int WS_MAXIMIZEBOX = 0x20000;
        private const int WS_MAXIMIZE = 0x01000000;
        private const int WS_BORDER = 0x800000;
        private const int WS_DLGFRAME = 0x00400000;
        private const int WS_EX_DLGMODALFRAME = 0x00000001;
        private const int WS_MINIMIZEBOX      = 0x00020000;
        private const int WS_OVERLAPPED       = 0x00000000;

        
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

        protected void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // If control needs to be initialized/created
            if (_iscreated == false)
            {

                // Mark that control is created
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
                    Debug.Print("U WOT M8");
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message + "Error");
                }

                // Put it into this form
                var helper = new WindowInteropHelper(Window.GetWindow(this.AppContainer));
                SetParent(_appWin, helper.Handle);
                
                var actualStyle = GetWindowLong(_appWin, GWL_STYLE);

                Debug.Print(actualStyle+"");
                
                actualStyle = actualStyle & ~WS_CAPTION;
                actualStyle = actualStyle & ~WS_SYSMENU;
                actualStyle = actualStyle & ~WS_THICKFRAME;
                actualStyle = actualStyle & ~WS_MINIMIZE;
                actualStyle = actualStyle & ~WS_MAXIMIZEBOX;
                
                Debug.Print(actualStyle + "");
                

                // Remove border and whatnot
                SetWindowLongA(_appWin, GWL_STYLE, (int)actualStyle);
                //SetWindowLongA(_appWin, GWL_STYLE, (int)actualStyle & ~(WS_BORDER | WS_DLGFRAME | WS_THICKFRAME));

                var actualExStyle = GetWindowLong(_appWin, GWL_EXSTYLE);
                SetWindowLongA(_appWin, GWL_EXSTYLE, (int)actualExStyle | WS_EX_DLGMODALFRAME);
    

                // Move the window to overlay it on this window
                //MoveWindow(_appWin, 0, 0, (int)this.ActualWidth, (int)this.ActualHeight, true);
                SetRightMoveWindow();


            }
        }

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

        private void SetRightMoveWindow()
        {
            if (this._appWin != IntPtr.Zero)
            {
                Point relativePoint = this.TransformToAncestor(mainParent)
                          .Transform(new Point(0, 0));
                MoveWindow(_appWin, (int)(relativePoint.X*ratio), (int)(relativePoint.Y*ratio), (int)(this.ActualWidth * ratio), (int)(this.ActualHeight * ratio), true);
            }
        }

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
