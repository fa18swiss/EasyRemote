using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppControl;

namespace EasyRemote.WinMgmt
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppControl app = new AppControl();
            //app.ExeName = @"C:\Program Files (x86)\PuTTY\putty.exe";
            app.ExeName = @"notepad.exe";
            //Button app = new Button();
            
            Grid.SetColumn(app,0);
            Grid.SetRow(app, 0);
            mainGrid.Children.Add(app);
            this.Unloaded += new RoutedEventHandler((s, e) => { app.Dispose(); });
        }
    }
}
