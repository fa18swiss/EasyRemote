using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasyRemote.Spec;

namespace EasyRemote
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Event
        {
            Click,
            DoubleClick
        }

        private readonly IConfig config;

        public MainWindow(IConfig config)
        {
            this.config = config;
            InitializeComponent();

            TreeView.ItemsSource = config.RootGroup.Childrens;
        }

        private void TreeViewItem_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item != null)
            {
                HandleEvent(item.Header, Event.DoubleClick);
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = sender as TreeView;
            if (tree != null)
            {
                HandleEvent(tree.SelectedItem, Event.Click);
            }
        }

        private void HandleEvent(object ob, Event event_)
        {
            if (ob == null)
            {
                return;
            }
            if (event_ == Event.Click)
            {
                LoadProperty(ob);
                return;
            }
            if (ob is IServer)
            {
            }
            else if (ob is IServerProtocol)
            {
                var protocol = ob as IServerProtocol;
                // TODO open connection
            }
            else if (ob is IServerGroup)
            {
            }
        }

        private void LoadProperty(object ob)
        {
            // TODO load property of object
            Debug.Print("Load ob " + ob);
        }
    }
}