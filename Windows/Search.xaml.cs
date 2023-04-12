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

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        APIrequests api = new APIrequests();
        RootObject ro = new RootObject();
        int index;
        const string FilePath = "storage.json";
        const string link = "http://api.coincap.io/v2/assets";
        public Search(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            var content = TextInput.Text;
            int intContent;
            bool isInt = int.TryParse(content, out intContent);
            ro.data = api.GetData(FilePath);
            for (int i = 0; i < api.GetData(FilePath).Count; i++)
            {
                if (isInt)
                { 
                    if(ro.data[i].rank == intContent)
                    {
                        GoToCurrencyDetails(i, ro.data.Count);
                        success = true;
                        break;
                    }
                }
                else if(ro.data[i].name.ToLower() == content.ToLower() || ro.data[i].symbol.ToLower() == content.ToLower())
                {
                    GoToCurrencyDetails(i, ro.data.Count);
                    success = true;
                    break;
                }
            }
            if (!success)
            {
                MessageBox.Show("There is no currency like typed one");
            }
        }
        private void GoToCurrencyDetails(int i, int max)
        {
            Details d = new Details(i, ro.data.Count);
            d.Show();
            Close();
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuInside_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.MenuClick(this.MenuGrid, this.MenuInside);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.MenuClick(MenuGrid, this.Menu);
        }
        private void Search_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Menu_Click(sender, e);
            }

            if (e.Key == Key.Enter)
            {
                Button_Click(sender,e);
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            var h = new Home(index);
            h.Show();
            Close();
        }
    }
}
