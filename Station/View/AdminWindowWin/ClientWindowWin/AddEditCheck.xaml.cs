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

namespace Station.View.AdminWindowWin.ClientWindowWin
{
    /// <summary>
    /// Логика взаимодействия для AddEditCheck.xaml
    /// </summary>
    public partial class AddEditCheck : Window
    {
        private Check _check = new Check();
        private List<Type_pay> type_Pays = StationEntities.GetContext().Type_pay.ToList();
        public AddEditCheck(Check check)
        {
            InitializeComponent();
            Typepay_CB.ItemsSource = type_Pays;
            Typepay_CB.DisplayMemberPath = "Name";
            Typepay_CB.SelectedValuePath = "Code";
            if (check == null)
            {
                this.Title = "Добавление акта";
                TitleBlock.Text = "Добавление акта";
            }
            else
            {
                _check = check;
                Typepay_CB.SelectedIndex = _check.ID_type_pay;
            }
            DataContext = check;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_check.ID_schet.ToString()))
                errors.AppendLine("Heт счета");
            if (string.IsNullOrWhiteSpace(_check.ID_administrator.ToString()))
                errors.AppendLine("Heт администратора");
            if (Typepay_CB.SelectedItem == null)
                errors.AppendLine("He указан тип оплаты");
            if (string.IsNullOrWhiteSpace(_check.Date.ToString()))
                errors.AppendLine("Не верный формат цены");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_check.ID == 0)
            {
                StationEntities.GetContext().Check.Add(_check);
            }
            try
            {
                _check.Date = DatePicker.SelectedDate.Value;
                _check.ID_type_pay = Typepay_CB.SelectedIndex;
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
