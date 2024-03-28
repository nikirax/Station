using Station.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Station.View.AdminWindowWin.AktWindowWin
{
    /// <summary>
    /// Логика взаимодействия для AddEditAktxaml.xaml
    /// </summary>
    public partial class AddEditAkt : Window
    {
        private Akt _akt = new Akt();
        public AddEditAkt(Akt akt)
        {
            InitializeComponent();
            if (akt == null)
            {
                this.Title = "Добавление акта";
                TitleBlock.Text = "Добавление акта";
            }
            else
            {
                _akt = akt;
            }
            DataContext = _akt;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_akt.Date_start.ToString()))
                errors.AppendLine("Heт даты начала");
            if (string.IsNullOrWhiteSpace(_akt.Date_end.ToString()))
                errors.AppendLine("Heт даты окончания");
            if (string.IsNullOrWhiteSpace(_akt.About))
                errors.AppendLine("He указаны проведенные работы");
            if (_akt.Price < 0 && string.IsNullOrWhiteSpace(_akt.Price.ToString()))
                errors.AppendLine("Не верный формат цены");
            if (_akt.ID_mechanic < 0 && string.IsNullOrWhiteSpace(_akt.ID_mechanic.ToString()))
                errors.AppendLine("Не указан номер механика");
            if (_akt.ID_client < 0 && string.IsNullOrWhiteSpace(_akt.ID_client.ToString()))
                errors.AppendLine("Не указан номер клиента");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_akt.ID == 0)
            {
                StationEntities.GetContext().Akt.Add(_akt);
            }
            try
            {
                StationEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
