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
            Debug.Print("Selected {0}", TreeView.SelectedItem);
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

        private void MenuAddServer_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
