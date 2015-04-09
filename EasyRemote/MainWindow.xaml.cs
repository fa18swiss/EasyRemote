using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EasyRemote.Converters;
using EasyRemote.Impl.Extension;
using EasyRemote.ProgramsProtocols.Programs;
using EasyRemote.Spec;
using EasyRemote.Spec.Factory;
using Microsoft.Practices.Unity;
using WpfAppControl;


namespace EasyRemote
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfig config;
        private readonly IProgramsProtocolsList programsProtocolsList;
        private readonly IUnityContainer container;

        public static RoutedCommand AddGroup = new RoutedCommand();
        public static RoutedCommand AddServer = new RoutedCommand();
        public static RoutedCommand AddProtocol = new RoutedCommand();
        public static RoutedCommand DeleteItem = new RoutedCommand();

        public MainWindow(IConfig config ,IProgramsProtocolsList programsProtocolsList, IUnityContainer container)
        {
            this.config = config;
            this.programsProtocolsList = programsProtocolsList;
            this.container = container;
            ProtocolPorgramsConverter.ProgramsProtocolsList = programsProtocolsList;
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
            if (ob is IProgram)
            {
                _propertyGrid.SelectedObject = null;
            }
            else
            {
                _propertyGrid.SelectedObject = ob;
            }
        }

        private void MenuAddGroup_Click(object sender, RoutedEventArgs e)
        {
            var selected = TreeView.SelectedItem;
            if (selected == null)
            {
                AddNewGroup(config.RootGroup);
            }
            if (selected is IServerGroup)
            {
                AddNewGroup(selected as IServerGroup);
            }
        }

        private void AddNewGroup(IServerGroup group)
        {
            var newGroup = container.Resolve<IFactory<IServerGroup>>().Create();
            newGroup.Name = "[New]";
            group.Childrens.Add(newGroup);
        }
        private void AddNewServer(IServerGroup group)
        {
            var newGroup = container.Resolve<IFactory<IServer>>().Create();
            newGroup.Name = "[New]";
            group.Childrens.Add(newGroup);
        }
        private void AddNewProtocol(IServer server)
        {
            var ask = container.Resolve<AskProtocol>();
            ask.Filter(server);
            ask.ShowDialog();
            if (ask.SelectedProtocol != null)
            {
                var serverProtocol = container.Resolve<IFactory<IServerProtocol>>().Create();
                serverProtocol.Protocol = ask.SelectedProtocol;
                server.Protocols.Add(serverProtocol);
            }
        }
        private void MenuAddServer_Click(object sender, RoutedEventArgs e)
        {
            var selected = TreeView.SelectedItem;
            if (selected == null)
            {
                AddNewServer(config.RootGroup);
            }
            if (selected is IServerGroup)
            {
                AddNewServer(selected as IServerGroup);
            }
        }

        private void MenuAddProtocol_Click(object sender, RoutedEventArgs e)
        {
            var selected = TreeView.SelectedItem;
            if (selected is IServer)
            {
                AddNewProtocol(selected as IServer);
            }
        }


        private void MenuDeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = TreeView.SelectedItem;
            if (selected == null || selected is IProgram)
            {
                return;
            }
            var result = MessageBox.Show(this, "Are you sure ?", "Delete", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            if (selected is IServer)
            {
                Delete(selected as IServer, config.RootGroup);
            }
            else if (selected is IServerProtocol)
            {
                Delete(selected as IServerProtocol, config.RootGroup);
            }
            else if (selected is IServerGroup)
            {
                Delete(selected as IServerGroup, config.RootGroup);
            }
        }

        private void Delete(IServerBase server, IServerGroup group)
        {
            foreach (var c in group.Childrens)
            {
                if (server.Equals(c))
                {
                    group.Childrens.Remove(c);
                    return;
                }
                if (c is IServerGroup)
                {
                    Delete(server, c as IServerGroup);
                }
            }
        }

        private void Delete(IServerProtocol serverProtocol, IServerGroup group)
        {
            foreach (var c in group.Childrens)
            {
                
                if (c is IServerGroup)
                {
                    Delete(serverProtocol, c as IServerGroup);
                }
                else if (c is IServer)
                {
                    Delete(serverProtocol, c as IServer);
                }
            }
        }
        private void Delete(IServerProtocol serverProtocol, IServer server)
        {
            foreach (var c in server.Protocols)
            {
                if (serverProtocol.Equals(c))
                {
                    server.Protocols.Remove(c);
                    return;
                }
            }
        }

        private void OpenConnectionFromFile_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
