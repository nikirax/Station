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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Station.View
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private string _selectedImagePath;
        private List<Passport> passports = StationEntities.GetContext().Passport.ToList();
        private List<Auto> autos = StationEntities.GetContext().Auto.ToList();
        private Auto auto = new Auto();
        private List<Zapic> zapici = StationEntities.GetContext().Zapic.ToList();
        private Type currentPageType;
        private Client thisClient;
        public ClientWindow(Client client)
        {
            InitializeComponent();
            //Image
            var passport = passports.FirstOrDefault(x => x.ID == client.ID_passport);
            if (passport.Photo != null)
                Avatar.Source = new BitmapImage(new Uri($"{passport.Photo}"));
            auto = autos.FirstOrDefault(x => x.ID == client.ID_auto);
            if (auto.Photo != null)
                ImageAuto.Source = new BitmapImage(new Uri($"{auto.Photo}"));
            thisClient = client;
            DataContext = auto;
        }
        private void AvatarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageType == typeof(ClientCard))
            {
                pageFrame.Navigate(null);
                currentPageType = null;
            }
            else
            {
                ClientCard page2 = new ClientCard(thisClient, this);
                pageFrame.Navigate(page2);
                currentPageType = typeof(ClientCard);
            }
        }

        private void AutoPhoto_Click(object sender, RoutedEventArgs e)
        {
            var Photo = new Image();

            var auto = autos.FirstOrDefault(x => x.ID == thisClient.ID_auto);
            if (auto != null)
                Photo.Source = new BitmapImage(new Uri($"{auto.Photo}"));

            var container = new Grid();
            container.Children.Add(Photo);
            container.HorizontalAlignment = HorizontalAlignment.Center;
            container.VerticalAlignment = VerticalAlignment.Center;

            var popupWindow = new Window
            {
                Content = container,
                Width = 300,
                Height = 300,
                WindowStyle = WindowStyle.ToolWindow
            };

            if (auto != null)
            {
                popupWindow.Width = Photo.Width/2;
                popupWindow.Height = Photo.Height/2;
            }

            popupWindow.ShowDialog();
        }

        private void ChossePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберете фото";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedImagePath = openFileDialog.FileName;
                ImageAuto.Source = new BitmapImage(new Uri(_selectedImagePath));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(auto.Mark))
                errors.AppendLine("He задана марка автомобиля");
            if (string.IsNullOrWhiteSpace(auto.Model))
                errors.AppendLine("He задана модель автомобиля");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (auto.ID == 0)
            {
                StationEntities.GetContext().Auto.Add(auto);
            }
            try
            {
                auto.Photo = _selectedImagePath;
                StationEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
