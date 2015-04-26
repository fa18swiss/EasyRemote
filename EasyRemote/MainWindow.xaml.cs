using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasyRemote.Converters;
using EasyRemote.Impl.Extension;
using EasyRemote.Spec;
using EasyRemote.Spec.Factory;
using EasyRemote.Spec.Settings;
using EasyRemote.Tools;
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

        public MainWindow(IConfig config, IProgramsProtocolsList programsProtocolsList, IUnityContainer container,
            IUserSettings userSettings)
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

            // remove tab program
            mainTabControl.Items.Remove(ProgramsTabItem);
            ProgramsDataGrid.ItemsSource = programsProtocolsList.Programs;
            TreeView.ItemsSource = config.RootGroup.Childrens;
            //AddProcessToTabControl(@"C:\Program Files (x86)\PuTTY\putty.exe", "-load \"cuda1\"", "start");
        }

        /// <summary>
        /// Load object property in grid 
        /// </summary>
        /// <param name="ob">object</param>
        private void LoadProperty(object ob)
        {
            //Can't modifiy program
            if (ob is IProgram)
            {
                _propertyGrid.SelectedObject = null;
            }
            else
            {
                _propertyGrid.SelectedObject = ob;
            }
        }

        #region Tabs management

        /// <summary>
        /// Add program to tabs
        /// </summary>
        /// <param name="programPath">path of program</param>
        /// <param name="arguments">Args</param>
        /// <param name="name">Name of tab</param>
        private void AddProcessToTabControl(string programPath, string arguments, string name)
        {
            var item = new TabItem();
            var app = new AppWrapper(programPath, arguments, this, item);

            item.Header = name;

            item.Content = app;
            mainTabControl.Items.Add(item);
            mainTabControl.SelectedItem = item;
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

        #endregion

        #region Listeners

        #region TreeView

        /// <summary>
        /// on double click in treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (ob is IProgram)
                {
                    var serverProtocolItem = item.GetParent<TreeViewItem>();
                    var serverItem = serverProtocolItem.GetParent<TreeViewItem>();
                    var server = serverItem.Header as IServer;
                    var protocol = serverProtocolItem.Header as IServerProtocol;
                    var program = ob as IProgram;

                    var args = program.ConnectTo(server, protocol);
                    //Debug.Print("args =" + args);
                    AddProcessToTabControl(program.GetPath(), args,
                        string.Format("{0} - {1}", server.Name, protocol.Protocol.Name));
                }
            }
        }

        /// <summary>
        /// On click on item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = sender as TreeView;
            if (tree != null)
            {
                LoadProperty(tree.SelectedItem);
            }
        }

        #endregion

        #region tab program

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
                InitialDirectory = Path.GetDirectoryName(program.GetPath()),
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

        #endregion

        #region Menu listeners

        /// <summary>
        /// On add new group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAddGroup_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = TreeView.SelectedItem;
            // if not item selected, add into root group
            if (selected == null)
            {
                AddNewGroup(config.RootGroup);
            }
            // else add to group
            if (selected is IServerGroup)
            {
                AddNewGroup(selected as IServerGroup);
            }
        }

        /// <summary>
        /// on click on new server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// On click on new protocol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAddProtocol_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = TreeView.SelectedItem;
            if (selected is IServer)
            {
                AddNewProtocol(selected as IServer);
            }
        }

        /// <summary>
        /// On click on delete item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

        #endregion

        #region add/delete item

        #region add

        /// <summary>
        /// Add new group into group
        /// </summary>
        /// <param name="group">Parent for new group</param>
        private void AddNewGroup(IServerGroup group)
        {
            var newGroup = container.Resolve<IFactory<IServerGroup>>().Create();
            newGroup.Name = "[New]";
            group.Childrens.Add(newGroup);
        }

        /// <summary>
        /// Add new server into group
        /// </summary>
        /// <param name="group">Parent for new server</param>
        private void AddNewServer(IServerGroup group)
        {
            var newServer = container.Resolve<IFactory<IServer>>().Create();
            newServer.Name = "[New]";
            group.Childrens.Add(newServer);
        }

        /// <summary>
        /// Add new protocol to server
        /// </summary>
        /// <param name="server">Server</param>
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

        #endregion

        #region delete

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

        #endregion

        #endregion

        #region open / save file

        /// <summary>
        /// Try to open file in order of array
        /// </summary>
        /// <param name="paths"></param>
        private void OpenFile(params string[] paths)
        {
            foreach (var path in paths.Where(File.Exists))
            {
                config.Load(path);
                userSettings.LastPath = path;
                return;
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

        #endregion
    }
}