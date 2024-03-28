using Station.Model;
using Station.View;
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
using System.Xml;

namespace Station
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Client> clients = StationEntities.GetContext().Client.ToList();
        private List<Administrator> administrators = StationEntities.GetContext().Administrator.ToList();
        private List<Mechanic> mechanics = StationEntities.GetContext().Mechanic.ToList();
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement xRoot;
        public MainWindow()
        {
            InitializeComponent();
            HelloTXT.Text = TimeTextBox();
            xmlDoc.Load("C:\\Users\\nikit.DESKTOP-3K8HP99\\source\\repos\\Station\\Station\\Data\\user.xml");
            xRoot = xmlDoc.DocumentElement;

            foreach (XmlNode childnode in xRoot)
            {
                if (childnode.Name == "Name")
                    Login.Text = childnode.InnerText;
                if (childnode.Name == "Password")
                    Password.Text = childnode.InnerText;
            }
        }

        public static string TimeTextBox()
        {
            DateTime time = DateTime.Now;
            time.ToString("HH:mm:ss"); 
            if (time.Hour >= 6 && time.Hour < 12)
                return "Доброе утро";
            else if (time.Hour >= 12 && time.Hour < 18)
                return "Добрый день";
            else if (time.Hour >= 18 && time.Hour < 24)
                return "Добрый вечер";
            else
                return "Доброй ночи";
        }

        public static bool CheckVoid(string text)
        {
            if (text == null || text == "" || text == " ") return true;
            return false;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _Login();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                _Login();
        }
        private void _Login()
        {
            if (CheckVoid(Login.Text) && CheckVoid(Password.Text))
                MessageBox.Show("Логин или пароль пустует братиш");
            else
            {
                if (Check.IsChecked == true)
                {
                    foreach (XmlNode childnode in xRoot)
                    {
                        if (childnode.Name == "Name")
                            childnode.InnerText = Login.Text;
                        if (childnode.Name == "Password")
                            childnode.InnerText = Password.Text;
                    }
                    xmlDoc.Save("C:\\Users\\nikit.DESKTOP-3K8HP99\\source\\repos\\Station\\Station\\Data\\user.xml");
                }
                var client = clients.FirstOrDefault(x => x.Email.ToLower() == Login.Text.ToLower() && x.Password == Password.Text);
                var admin = administrators.FirstOrDefault(x => x.Email.ToLower() == Login.Text.ToLower() && x.Password == Password.Text);
                var mechanic = mechanics.FirstOrDefault(x => x.Email.ToLower() == Login.Text.ToLower() && x.Password == Password.Text);
                if (client != null)
                {
                    ClientWindow wclient = new ClientWindow(client);
                    wclient.Show();
                    this.Close();
                }
                else if (admin != null)
                {
                    AdminWindow wadmin = new AdminWindow(admin);
                    wadmin.Show();
                    this.Close();
                }
                else if (mechanic != null)
                {
                    MechanicWindow wmech = new MechanicWindow();
                    wmech.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Дальше не пущу :(");
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
