using System.Linq;
using System.Windows;
using System.Windows.Input;
using EasyRemote.Spec;

namespace EasyRemote
{
    /// <summary>
    /// Dialog window for ask protocol to add
    /// </summary>
    public partial class AskProtocol : Window
    {
        private readonly IProgramsProtocolsList programsProtocolsList;
        public IProtocol SelectedProtocol { get; private set; }
        public AskProtocol(IProgramsProtocolsList programsProtocolsList)
        {
            InitializeComponent();
            this.programsProtocolsList = programsProtocolsList;
            ListBox.ItemsSource = programsProtocolsList.Protocols;
            SelectedProtocol = null;
        }

        private void SelectAndClose()
        {
            SelectedProtocol = ListBox.SelectedItem as IProtocol;
            DialogResult = true;
            Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectAndClose();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectAndClose();
        }

        public void Filter(IServer server)
        {
            ListBox.ItemsSource = programsProtocolsList.Protocols.Except(server.Protocols.Select(p => p.Protocol));
        }
    }
}
