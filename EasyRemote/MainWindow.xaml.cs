using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EasyRemote.Converters;
using EasyRemote.Impl.Extension;
using EasyRemote.ProgramsProtocols.Programs;
using EasyRemote.Spec;
using WpfAppControl;


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
            //config.Save(path);
            //config.Load(path);
            TreeView.ItemsSource = config.RootGroup.Childrens;
            AddProcessToTabControl(@"C:\Program Files (x86)\PuTTY\putty.exe", "-load \"cuda1\"");
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
                    Debug.Print("program =" + program.Name);
                    Debug.Print("protocol =" + protocol.Protocol.Name);
                    Debug.Print("server =" + server.Name);
                    var args = program.ConnectTo(server, protocol);
                    Debug.Print("args =" + args);
                    // TODO change this
                    AddProcessToTabControl(program.GetPath(), args);
                    // TODO open connection
                }
            }
        }

        private void AddProcessToTabControl(string programPath, string arguments)
        {
            TabItem item = new TabItem();
            var app = new AppWrapper(programPath, arguments, this);

            item.Header = programPath;

            //Grid newGrid = new Grid();

            //ColumnDefinition col1 = new ColumnDefinition();
            //RowDefinition row1 = new RowDefinition();



            //Grid.SetColumn(app,0);
            //Grid.SetRow(app, 0);

            //newGrid.ColumnDefinitions.Add(col1);
            //newGrid.RowDefinitions.Add(row1);
            //newGrid.Children.Add(app);
            item.Content = app;
            mainTabControl.Items.Add(item);
            mainTabControl.SelectedItem = item;

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
