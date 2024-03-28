using Station.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для GraphicWindow.xaml
    /// </summary>
    public partial class GraphicWindow : Window
    {

        public GraphicWindow()
        {
            InitializeComponent();
            ZapicDataGrid.ItemsSource = StationEntities.GetContext().Zapic.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Zapic> zapics = StationEntities.GetContext().Zapic.ToList();
            if (ZapicDataGrid != null)
            {
                zapics = StationEntities.GetContext().Zapic.ToList();
                ZapicDataGrid.ItemsSource = StationEntities.GetContext().Client.ToList();
                switch (SearchComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            zapics = zapics.Where(x => x.Client.SecondName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 1:
                        {
                            zapics = zapics.Where(x => x.Mechanic.SecondName.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            zapics = zapics.Where(x => x.DateTime.ToString().ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 3:
                        {
                            zapics = zapics.Where(x => x.Client.Phone.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 4:
                        {
                            zapics = zapics.Where(x => x.Client.Email.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                    case 5:
                        {
                            zapics = zapics.Where(x => x.Client.Auto.Mark.ToLower().StartsWith(SearchTextBox.Text.ToLower())).ToList();
                            break;
                        }
                }
                ZapicDataGrid.ItemsSource = zapics;
            }
        }
    }
}

