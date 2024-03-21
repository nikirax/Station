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

        public AdminWindow(Administrator admin)
        {
            InitializeComponent();
            TitleBlock.Text = $"Добро пожаловать {admin.FirstName}";
            ClientDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
        }

        private void Graph_Click(object sender, RoutedEventArgs e)
        {
            GraphicWindow wgraphic = new GraphicWindow();
            wgraphic.Show();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FiltrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AktBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SchetBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CardAutoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DownRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
