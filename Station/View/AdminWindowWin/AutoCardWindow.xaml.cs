using Microsoft.Win32;
using Station.Model;
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
    /// Логика взаимодействия для AutoCardWindow.xaml
    /// </summary>
    public partial class AutoCardWindow : Window
    {
        private string _selectedImagePath;
        private Auto auto;
        private Administrator administrator;
        public AutoCardWindow(Administrator admin, Client client)
        {
            InitializeComponent();
            administrator = admin;
            auto = client.Auto;
            ImageAuto.Source = new BitmapImage(new Uri($"{auto.Photo}"));
            DataContext = auto;
        }
        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject(DataFormats.FileDrop, new string[] { "путь_к_файлу" });
                DragDrop.DoDragDrop((sender as UIElement), data, DragDropEffects.Copy);
            }
        }
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            string[] line = (string[])e.Data.GetData(DataFormats.FileDrop);

            ImageAuto.Source = new BitmapImage(new Uri(line[0]));
        }
        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void InserImage_Click(object sender, RoutedEventArgs e)
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

        private void AutoPhoto_Click(object sender, RoutedEventArgs e)
        {
            var Photo = new Image();

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
                popupWindow.Width = Photo.Width / 2;
                popupWindow.Height = Photo.Height / 2;
            }

            popupWindow.ShowDialog();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(auto.Mark))
                errors.AppendLine("Heт марки авто");
            if (string.IsNullOrWhiteSpace(auto.Model))
                errors.AppendLine("Heт модели авто");
            if (string.IsNullOrWhiteSpace(auto.VIN))
                errors.AppendLine("He указан VIN авто");
            if (Convert.ToInt32(auto.Year_maid) < 1976  && string.IsNullOrWhiteSpace(auto.Year_maid))
                errors.AppendLine("Неверный формат года изготовления");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                auto.Photo = ImageAuto.Source.ToString();
                StationEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow wadmin = new AdminWindow(administrator);
            wadmin.Show();
            this.Close();
        }
    }
}
