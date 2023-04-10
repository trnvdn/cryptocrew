using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestTask
{
    public partial class Home : Window
    {
        private APIrequests API = new APIrequests();
        private Currency cur = new Currency();
        private int index = 0;
        public int MaxIndex = 100;


        public Home(int indx)
        {
            index = indx;
            InitializeComponent();
            cur = API.GetCurrentCurrency(index);
            Initialize();
        }
        private void Initialize()
        {
            Currency currency = API.GetCurrentCurrency(index);
            Currency.Text = currency.name;
            Price.Text = currency.priceUsd.ToString() + " USD";
            Code.Text = currency.symbol;
            Rank.Text = currency.rank.ToString();
            if (currency.changePercent24Hr > 0)
            {
                СhangePercent.Foreground = new SolidColorBrush(Colors.Green);
                СhangePercent.Text = currency.changePercent24Hr.ToString() + "%   ↑↑↑";
            }
            else
            {
                СhangePercent.Foreground = new SolidColorBrush(Colors.Crimson);
                СhangePercent.Text = currency.changePercent24Hr.ToString() + "%   ↓↓↓";
            }
        }
        
        private void decrease_Click(object sender, RoutedEventArgs e)
        {
            index = index == 0 ? MaxIndex - 1 : index - 1;
            Initialize();
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            index = index == MaxIndex - 1 ? 0 : index + 1;
            Initialize();
        }

        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await API.LoadData();
            Initialize();
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.LinkButton(API.GetCurrentCurrency(index).explorer,this.WindowState);
        }

        private void Menu1_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.MenuClick(this.MenuGrid, this.Menu);
        }

        private void MenuInside_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.MenuClick(this.MenuGrid, this.Menu);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Details dt = new Details(index,MaxIndex);
            dt.Show();
            Close();
        }
        private void Home_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Menu1_Click(sender, e);
            }
        }
    }
}
