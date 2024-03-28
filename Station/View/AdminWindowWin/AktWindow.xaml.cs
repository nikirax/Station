using Station.Model;
using Station.View.AdminWindowWin.AktWindowWin;
using Station.View.AdminWindowWin.SchetWindowWin;
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
    /// Логика взаимодействия для AktWindow.xaml
    /// </summary>
    public partial class AktWindow : Window
    {
        private Administrator administrator;
        private Client _client;
        public AktWindow(Administrator admin, Client client)
        {
            InitializeComponent();
            administrator = admin;
            _client = client;
            AktDataGrid.ItemsSource = StationEntities.GetContext().Akt.ToList().Where(x => x.ID_client == client.ID);
        }
        private void Schet_Click(object sender, RoutedEventArgs e)
        {
            AddEditAkt addEditw;
            if (AktDataGrid.SelectedItem == null)
                addEditw = new AddEditAkt(null);
            else
                addEditw = new AddEditAkt(AktDataGrid.SelectedItem as Akt);
            addEditw.ShowDialog();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow wadmin = new AdminWindow(administrator);
            wadmin.Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Akt> akts = StationEntities.GetContext().Akt.ToList();
            if (AktDataGrid != null)
            {
                akts = StationEntities.GetContext().Akt.ToList();
                AktDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
                switch (SearchComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            akts = akts.Where(x => x.ID.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 1:
                        {
                            akts = akts.Where(x => x.About.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            akts = akts.Where(x => x.Price.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 3:
                        {
                            akts = akts.Where(x => x.Date_start.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 4:
                        {
                            akts = akts.Where(x => x.Date_end.ToString().ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 5:
                        {
                            akts = akts.Where(x => x.Mechanic.SecondName.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                }
                AktDataGrid.ItemsSource = akts;
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    StationEntities.GetContext().Akt.Remove(AktDataGrid.SelectedItem as Akt);
                    StationEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены");
                    AktDataGrid.ItemsSource = StationEntities.GetContext().Akt.ToList().Where(x => x.ID_client == _client.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            AktDataGrid.ItemsSource = StationEntities.GetContext().Akt.ToList().Where(x => x.ID_client == _client.ID);
        }
    }
}
