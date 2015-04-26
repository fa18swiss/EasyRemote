using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EasyRemote.Converters;
using EasyRemote.Impl.Extension;
using EasyRemote.Spec;
using EasyRemote.Spec.Factory;
using EasyRemote.Spec.Settings;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
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
        private readonly IUserSettings userSettings;


        public static RoutedCommand AddGroup = new RoutedCommand();
        public static RoutedCommand AddServer = new RoutedCommand();
        public static RoutedCommand AddProtocol = new RoutedCommand();
        public static RoutedCommand DeleteItem = new RoutedCommand();
        public MainWindow(IConfig config ,IProgramsProtocolsList programsProtocolsList, IUnityContainer container, IUserSettings userSettings)
        {
            this.config = config;
            this.programsProtocolsList = programsProtocolsList;
            this.container = container;
            this.userSettings = userSettings;
            ProtocolPorgramsConverter.ProgramsProtocolsList = programsProtocolsList;
            InitializeComponent();


            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                OpenFile(args[1], userSettings.LastPath);
            }
            else
            {
                OpenFile(userSettings.LastPath);
            }


            // remove
            mainTabControl.Items.Remove(ProgramsTabItem);
            ProgramsDataGrid.ItemsSource = programsProtocolsList.Programs;
            TreeView.ItemsSource = config.RootGroup.Childrens;
            //AddProcessToTabControl(@"C:\Program Files (x86)\PuTTY\putty.exe", "-load \"cuda1\"", "start");
        }
        private void OpenFile(params string[] paths)
        {
            foreach (var path in paths.Where(File.Exists))
            {
                config.Load(path);
                userSettings.LastPath = path;
                return;
            }
        }

        public void RemoveTabItem(TabItem item)
        {
            if (mainTabControl.Items.Contains(item))
            {
                Action action = () =>
                    mainTabControl.Items.Remove(item);
                try
                {
                    Dispatcher.Invoke(action);
                }
                catch (TaskCanceledException)
                {
                    // nothing to do
                }
            }
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
                    AddProcessToTabControl(program.GetPath(), args, string.Format("{0} - {1}", server.Name, protocol.Protocol.Name));
                    // TODO open connection
                }
            }
        }

        private void AddProcessToTabControl(string programPath, string arguments, string name)
        {
            var item = new TabItem();
            var app = new AppWrapper(programPath, arguments, this, item);

            item.Header = name;

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

        private void MenuAddGroup_OnClick(object sender, RoutedEventArgs e)
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
        private void MenuAddServer_OnClick(object sender, RoutedEventArgs e)
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

        private void MenuAddProtocol_OnClick(object sender, RoutedEventArgs e)
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
                    userSettings.LastPath = fileDialog.FileName;
                }
            }
            else
            {
                MessageBox.Show("At least one connection is required", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ProgramsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TreeView.Items.Refresh();
            TreeView.UpdateLayout();
        }

        private void PathButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var program = button.DataContext as IProgram;
            if (program == null)
            {
                return;
            }
            var fileDialog = new OpenFileDialog
            {
                Filter = "Application (*.exe)|*.exe",
                Multiselect = false,
                RestoreDirectory = true,
                FileName = program.GetPath(),
                InitialDirectory = System.IO.Path.GetDirectoryName(program.GetPath()),
            };
            var result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                program.Path = fileDialog.FileName;
            }
            ProgramsDataGrid.Items.Refresh();
        }


        private void EditProgramsMenuItem_OnChecked(object sender, RoutedEventArgs e)
        {
            mainTabControl.Items.Add(ProgramsTabItem);
            mainTabControl.SelectedItem = ProgramsTabItem;
        }

        private void EditProgramsMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            mainTabControl.Items.Remove(ProgramsTabItem);
        }
    }
}
