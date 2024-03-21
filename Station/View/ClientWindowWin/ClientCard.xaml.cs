using System;
using Station.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Station.View
{
    /// <summary>
    /// Логика взаимодействия для ClientCard.xaml
    /// </summary>
    public partial class ClientCard : Page
    {
        private ClientWindow _window;
        public ClientCard(Client client, ClientWindow window)
        {
            InitializeComponent();
            ClientName.Text = client.FirstName;
            _window = window;
        }

        private void AutoBtn_Click(object sender, RoutedEventArgs e)
        {
            if(_window.GridAuto.Visibility == Visibility.Visible)
                _window.GridAuto.Visibility = Visibility.Hidden;
            else if (_window.ZapicGrid.Visibility == Visibility.Visible)
            {
                _window.ZapicGrid.Visibility = Visibility.Hidden;
                _window.GridAuto.Visibility = Visibility.Visible;
                _window.ChangeDataContext(_window.auto, null);
            }
            else
            {
                _window.GridAuto.Visibility = Visibility.Visible;
                _window.ChangeDataContext(_window.auto, null);
            }
        }

        private void ZapicBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_window.ZapicGrid.Visibility == Visibility.Visible)
                _window.ZapicGrid.Visibility = Visibility.Hidden;
            else if(_window.GridAuto.Visibility == Visibility.Visible)
            {
                _window.GridAuto.Visibility = Visibility.Hidden;
                _window.ZapicGrid.Visibility = Visibility.Visible;
                _window.ChangeDataContext(null, _window.zapic);
            }
            else
            {
                _window.ZapicGrid.Visibility = Visibility.Visible;
                _window.ChangeDataContext(null, _window.zapic);
            }
        }
    }
}
