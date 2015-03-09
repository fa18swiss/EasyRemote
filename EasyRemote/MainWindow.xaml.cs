using System.Windows;
using EasyRemote.Spec;

namespace EasyRemote
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfig config;

        public MainWindow(IConfig config)
        {
            this.config = config;
            InitializeComponent();
        }
    }
}