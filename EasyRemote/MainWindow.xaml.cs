using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EasyRemote.Convertes;
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
            ProtocolPorgramsConverter.Config = config;
            InitializeComponent();
            string path = @"C:\zgeg.json";
            config.Save(path);
            //config.Load(path);
            TreeView.ItemsSource = config.RootGroup.Childrens;
        }

        private static T GetParent<T>(DependencyObject ob)
            where T : DependencyObject
        {
            do
            {
                ob = VisualTreeHelper.GetParent(ob);
                if (ob is T)
                {
                    return (T) ob;
                }
            } while (ob != null);
            return default(T);
        }

        private void TreeViewItem_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item != null)
            {
                var ob = item.Header;
                if (ob == null)
                {
                    return;
                }
                if (ob is IServer)
                {
                }
                else if (ob is IServerProtocol)
                {
                    
                }
                else if (ob is IServerGroup)
                {
                }
                else if (ob is IProgram)
                {;
                    var serverProtocolItem = GetParent<TreeViewItem>(item);
                    var serverItem = GetParent<TreeViewItem>(serverProtocolItem);
                    Debug.Print("item =" + item);
                    Debug.Print("serverProtocolItem =" + serverProtocolItem);
                    Debug.Print("serverItem =" + serverItem);
                    var server = serverItem.Header as IServer;
                    var protocol = serverProtocolItem.Header as IServerProtocol;
                    var program = ob as IProgram;
                    Debug.Print("program =" + program);
                    Debug.Print("protocol =" + protocol);
                    Debug.Print("server =" + server);
                    // TODO open connection
                }
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = sender as TreeView;
            if (tree != null)
            {
                LoadProperty(tree.SelectedItem);
            }
        }

        private void LoadProperty(object ob)
        {
            _propertyGrid.SelectedObject = ob;
            Debug.Print("Load ob " + ob);
        }
    }
}
