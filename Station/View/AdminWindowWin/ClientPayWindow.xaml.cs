using Station.Model;
using Station.View.AdminWindowWin.AktWindowWin;
using Station.View.AdminWindowWin.ClientWindowWin;
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

namespace Station.View.AdminWindowWin
{
    /// <summary>
    /// Логика взаимодействия для ClientPayWindow.xaml
    /// </summary>
    public partial class ClientPayWindow : Window
    {
        private Administrator administrator;
        Client _client = new Client();
        public ClientPayWindow(Administrator admin, Client client)
        {
            InitializeComponent();
            administrator = admin;
            _client = client;
            CheckDataGrid.ItemsSource = StationEntities.GetContext().Check.ToList().Where(x => x.Schet.ID_client == client.ID);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Check> checks = StationEntities.GetContext().Check.ToList();
            if (CheckDataGrid != null)
            {
                checks = StationEntities.GetContext().Check.ToList();
                switch (SearchComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            checks = checks.Where(x => x.ID.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 1:
                        {
                            checks = checks.Where(x => x.Date.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            checks = checks.Where(x => x.ID_administrator.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 3:
                        {
                            checks = checks.Where(x => x.ID_schet.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 4:
                        {
                            checks = checks.Where(x => x.Type_pay.Name.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                }
                CheckDataGrid.ItemsSource = checks;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CheckDataGrid.ItemsSource = StationEntities.GetContext().Check.ToList().Where(x => x.Schet.ID_client == _client.ID);
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            AddEditCheck addEditw;
            if (CheckDataGrid.SelectedItem == null)
                addEditw = new AddEditCheck(null);
            else
                addEditw = new AddEditCheck(CheckDataGrid.SelectedItem as Check);
            addEditw.ShowDialog();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow wadmin = new AdminWindow(administrator);
            wadmin.Show();
            this.Close();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    StationEntities.GetContext().Check.Remove(CheckDataGrid.SelectedItem as Check);
                    StationEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены");
                    CheckDataGrid.ItemsSource = StationEntities.GetContext().Check.ToList().Where(x => x.Schet.ID_client == _client.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
