using Station.Model;
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

        public AdminWindow()
        {
            InitializeComponent();
            MainDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
        }
        private void UpdateView()
        {
            List<Client> clients = new List<Client>();
            //Фильтрация
            if (FiltrComboBox.SelectedIndex < 2)
            {
                string selectedGender = Convert.ToString(FiltrComboBox.SelectedIndex);
                //clients = clients.Where(x => x.GenderCode.ToString() == selectedGender).ToList();
            }
            //Сортировка
            switch (SortComboBox.SelectedIndex)
            {
                case 1:
                    {
                        //if (UpRadioButton.IsChecked.Value)
                        //    clients = clients.OrderBy(x => x.LastName).ToList();
                        //if (DownRadioButton.IsChecked.Value)
                        //    clients = clients.OrderByDescending(x => x.LastName).ToList();
                        break;
                    }
                case 2:
                    {
                        //if (UpRadioButton.IsChecked.Value)
                        //    clients = clients.OrderBy(x => x.RegistrationDate).ToList();
                        //if (DownRadioButton.IsChecked.Value)
                        //    clients = clients.OrderByDescending(x => x.RegistrationDate).ToList();
                        break;
                    }
            }
            //Поиск
            switch (SearchComboBox.SelectedIndex)
            {
                case 0:
                    {
                        //clients = clients.Where(x => x.LastName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                        break;
                    }
                case 1:
                    {
                        clients = clients.Where(x => x.FirstName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                        break;
                    }
                case 2:
                    {
                        //clients = clients.Where(x => x.Patronymic.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
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
            }

            MainDataGrid.ItemsSource = clients.ToList();
            //Странный вывод
            if (clients.Count == 0)
            {
                MessageBox.Show("По вашему запросу ничего не найдено");
                SearchTextBox.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new AddEditPage(null));
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данные?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    //ProgramEntities1.GetContext().Client.Remove(MainDataGrid.SelectedItem as Client);
                    //ProgramEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены");
                    //MainDataGrid.ItemsSource = ProgramEntities1.GetContext().Client.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new AddEditPage(MainDataGrid.SelectedItem as Client));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //var valueClients = LimitDataGrid();
            //foreach (var client in valueClients)
            //{
            //    Gender clientGender = allGenders.FirstOrDefault(x => x.ID == client.GenderCode);

            //    client.Gender = clientGender;

            //    MainGridSourse.Add(client);
            //}

            //ClientCount.Text = $"Всего записей: {MainGridSourse.Count()}";

            //MainDataGrid.ItemsSource = ProgramEntities1.GetContext().Client.ToList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (FiltrComboBox != null)
            //    UpdateClients();
        }

        private void UpRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //if (FiltrComboBox != null)
            //    UpdateClients();
        }

        private void DownRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //if (FiltrComboBox != null)
            //    UpdateClients();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //UpdateClients();
        }

        private void FiltrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateClients();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ClientCard(MainDataGrid.SelectedItem as Client));
        }

        private void BtnPagination(object sender, RoutedEventArgs e)
        {
            //var valueClients = LimitDataGrid();
            //foreach (var client in valueClients)
            //{
            //    Gender clientGender = allGenders.FirstOrDefault(x => x.ID == client.GenderCode);

            //    client.Gender = clientGender;

            //    MainGridSourse.Add(client);
            //}

            //ClientCount.Text = $"Всего записей: {MainGridSourse.Count()}";

            //MainDataGrid.ItemsSource = MainGridSourse;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainDataGrid != null)
            {
                switch (selectData.SelectedIndex)
                {
                    case 0:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
                        RemoveColumsInEnd(4);
                        break;
                    case 1:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Mechanic.ToList();
                        RemoveColumsInEnd(4);
                        break;
                    case 2:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Administrator.ToList();
                        RemoveColumsInEnd(3);
                        break;
                    case 3:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Zapic.ToList();
                        RemoveColumsInEnd(5);
                        break;
                    case 4:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Graphic.ToList();
                        RemoveColumsInEnd(2);
                        break;
                    case 5:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Akt.ToList();
                        RemoveColumsInEnd(3);
                        break;
                    case 6:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Schet.ToList();
                        RemoveColumsInEnd(4);
                        break;
                    case 7:
                        MainDataGrid.ItemsSource = StationEntities.GetContext().Check.ToList();
                        RemoveColumsInEnd(3);
                        break;
                }
            }
        }
        private void RemoveColumsInEnd(int count) //Чистка последних мусорных колонок
        {
            int totalColumns = MainDataGrid.Columns.Count;

            if (totalColumns < count)
                return;

            for (int i = 0; i < count; i++)
                MainDataGrid.Columns.RemoveAt(MainDataGrid.Columns.Count - 1);
        }
    }
}
