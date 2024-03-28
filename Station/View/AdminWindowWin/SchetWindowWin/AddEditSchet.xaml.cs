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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Station.View.AdminWindowWin.SchetWindowWin
{
    /// <summary>
    /// Логика взаимодействия для AddEditSchet.xaml
    /// </summary>
    public partial class AddEditSchet : Window
    {
        private Schet _schet = new Schet();
        public AddEditSchet(Schet schet)
        {
            InitializeComponent();
            if (schet == null)
            {
                this.Title = "Добавление счета";
                TitleBlock.Text = "Добавление счета";
            }
            else
            {
                _schet = schet;
            }
            DataContext = _schet;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ID_Akt.Text))
                errors.AppendLine("He указан акт");
            if (string.IsNullOrWhiteSpace(ID_Client.Text))
                errors.AppendLine("He указан клиент");
            if (string.IsNullOrWhiteSpace(ID_Zapzhast.Text))
                errors.AppendLine("He указана запчасть");
            if (string.IsNullOrWhiteSpace(DatePicker.Text))
                errors.AppendLine("Не указана дата");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_schet.ID == 0)
            {
                StationEntities.GetContext().Schet.Add(_schet);
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
