using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EasyRemote.Spec;

namespace EasyRemote
{
    /// <summary>
    /// Logique d'interaction pour AskProtocol.xaml
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
