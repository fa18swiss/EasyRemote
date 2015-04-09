using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EasyRemote.Converters;
using EasyRemote.Impl.Extension;
using EasyRemote.Spec;
using EasyRemote.Spec.Factory;
using Microsoft.Practices.Unity;
using Microsoft.Win32;


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

        public MainWindow(IConfig config ,IProgramsProtocolsList programsProtocolsList, IUnityContainer container)
        {
            this.config = config;
            this.programsProtocolsList = programsProtocolsList;
            this.container = container;
            ProtocolPorgramsConverter.ProgramsProtocolsList = programsProtocolsList;
            InitializeComponent();
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
                    Debug.Print("program =" + program.Name);
                    Debug.Print("protocol =" + protocol.Protocol.Name);
                    Debug.Print("server =" + server.Name);
                    var args = program.ConnectTo(server, protocol);
                    Debug.Print("args =" + args);
                    // TODO change this
                    Process.Start(program.GetPath(), args);
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

            Debug.Print("Delete {0}", selected);
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
            var fileDialog = new OpenFileDialog
            {
                Filter = "JSON file (*.json)|*.json",
                Multiselect = false,
                RestoreDirectory = true
            };

            var result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                config.Load(fileDialog.FileName);
                TreeView.ItemsSource = null;
                TreeView.ItemsSource = config.RootGroup.Childrens; //refresh view
            }
        }

        private void SaveConnection_OnClick(object sender, RoutedEventArgs e)
        {
            if (config.RootGroup != null)
            {
                var fileDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = "json",
                    Filter = "JSON (*.json)|*.json"
                };
                var result = fileDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    config.Save(fileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("At least one connection is required", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
