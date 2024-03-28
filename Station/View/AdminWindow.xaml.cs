using Station.Model;
using Station.View.AdminWindowWin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        List<Gender> genders = StationEntities.GetContext().Gender.ToList();
        List<Client> clients = new List<Client> ();
        Administrator _admin;

        public AdminWindow(Administrator admin)
        {
            InitializeComponent();
            TitleBlock.Text = $"Добро пожаловать {admin.FirstName}";
            ClientDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();

            _admin = admin;

            genders.Insert(2, new Gender
            {
                Name = "Все типы"
            });
            FiltrComboBox.ItemsSource = genders;
            FiltrComboBox.DisplayMemberPath = "Name";
            FiltrComboBox.SelectedValuePath = "Code";
            FiltrComboBox.SelectedIndex = 2;

        }

        private void UpdateClients()
        {
            if(ClientDataGrid != null)
            {
                clients = StationEntities.GetContext().Client.ToList();
                ClientDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
            }
        }

        private void Graph_Click(object sender, RoutedEventArgs e)
        {
            GraphicWindow wgraphic = new GraphicWindow();
            wgraphic.Show();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortMethod();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ClientDataGrid != null)
            {
                UpdateClients();
                switch (SearchComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            clients = clients.Where(x => x.SecondName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 1:
                        {
                            clients = clients.Where(x => x.FirstName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            clients = clients.Where(x => x.MiddleName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 3:
                        {
                            clients = clients.Where(x => x.Email.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 4:
                        {
                            clients = clients.Where(x => x.Phone.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 5:
                        {
                            clients = clients.Where(x => x.Auto.Mark.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                }
                ClientDataGrid.ItemsSource = clients;
            }
        }

        private void FiltrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateClients();
            if (FiltrComboBox.SelectedIndex < 2)
            {
                string selectedGender = Convert.ToString(FiltrComboBox.SelectedIndex);
                clients = clients.Where(x => x.Passport.ID_gender.ToString() == selectedGender).ToList();
            }
            ClientDataGrid.ItemsSource = clients;
        }

        private void AktBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                AktWindow wact = new AktWindow(_admin, ClientDataGrid.SelectedItem as Client);
                wact.Show();
                this.Close();
            }
            else
                MessageBox.Show("Выберете клиента у которого хотите редактировать счета");
        }

        private void SchetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                SchetWindow wschet = new SchetWindow(_admin, ClientDataGrid.SelectedItem as Client);
                wschet.Show();
                this.Close();
            }
            else
                MessageBox.Show("Выберете клиента у которого хотите редактировать счета");
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            ClientPayWindow wclientpay = new ClientPayWindow();
            wclientpay.Show();
        }

        private void CardAutoBtn_Click(object sender, RoutedEventArgs e)
        {
            AutoCardWindow wautocard = new AutoCardWindow();
            wautocard.Show();
        }
        private void UpRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortMethod();
        }
        private void DownRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortMethod();
        }
        private void SortMethod()
        {
            if(ClientDataGrid != null)
            {
                UpdateClients();
                switch (SortComboBox.SelectedIndex)
                {
                    case 1:
                        {
                            if (UpRadioButton.IsChecked.Value)
                                clients = clients.OrderBy(x => x.SecondName).ToList();
                            if (DownRadioButton.IsChecked.Value)
                                clients = clients.OrderByDescending(x => x.SecondName).ToList();
                            break;
                        }
                    case 2:
                        {
                            if (UpRadioButton.IsChecked.Value)
                                clients = clients.OrderBy(x => x.FirstName).ToList();
                            if (DownRadioButton.IsChecked.Value)
                                clients = clients.OrderByDescending(x => x.FirstName).ToList();
                            break;
                        }
                    case 3:
                        {
                            if (UpRadioButton.IsChecked.Value)
                                clients = clients.OrderBy(x => x.MiddleName).ToList();
                            if (DownRadioButton.IsChecked.Value)
                                clients = clients.OrderByDescending(x => x.MiddleName).ToList();
                            break;
                        }
                }
                ClientDataGrid.ItemsSource = clients;
            }
        }
    }
}
