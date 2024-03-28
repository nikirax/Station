using Station.Model;
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
    /// Логика взаимодействия для SchetWindow.xaml
    /// </summary>
    public partial class SchetWindow : Window
    {
        private Administrator administrator;
        private Client _client;
        public SchetWindow(Administrator admin, Client client)
        {
            InitializeComponent();
            administrator = admin;
            _client = client;
            SchetDataGrid.ItemsSource = StationEntities.GetContext().Schet.ToList().Where(x => x.ID_client == client.ID);
        }

        private void Schet_Click(object sender, RoutedEventArgs e)
        {
            AddEditSchet addEditw;
            if (SchetDataGrid.SelectedItem == null)
                addEditw = new AddEditSchet(null);
            else
                addEditw = new AddEditSchet(SchetDataGrid.SelectedItem as Schet);
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
            List<Schet> schet = StationEntities.GetContext().Schet.ToList();
            if (SchetDataGrid != null)
            {
                schet = StationEntities.GetContext().Schet.ToList();
                SchetDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
                switch (SearchComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            schet = schet.Where(x => x.ID.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 1:
                        {
                            schet = schet.Where(x => x.Akt.About.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            schet = schet.Where(x => x.Akt.Price.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 3:
                        {
                            schet = schet.Where(x => x.Zapchast.Name.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 4:
                        {
                            schet = schet.Where(x => x.Zapchast.Price_opt.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 5:
                        {
                            schet = schet.Where(x => x.Date.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                }
                SchetDataGrid.ItemsSource = schet;
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    StationEntities.GetContext().Schet.Remove(SchetDataGrid.SelectedItem as Schet);
                    StationEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены");
                    SchetDataGrid.ItemsSource = StationEntities.GetContext().Schet.ToList().Where(x => x.ID_client == _client.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            SchetDataGrid.ItemsSource = StationEntities.GetContext().Schet.ToList().Where(x => x.ID_client == _client.ID);
        }
    }
}
