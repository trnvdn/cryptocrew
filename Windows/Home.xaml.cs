using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        const string FilePath = "storage.json";
        bool FilterState = false;


        public Home(int indx)
        {
            index = indx;
            InitializeComponent();
            cur = API.GetCurrentCurrency(FilePath, index);
            Initialize();
        }
        private void Initialize()
        {
            if(index >= MaxIndex)
            {
                index = MaxIndex - 1;
            }
            Currency currency = API.GetCurrentCurrency(FilePath, index);
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
        
        private void Decrease_Click(object sender, RoutedEventArgs e)
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
            await API.LoadData("http://api.coincap.io/v2/assets",FilePath);
            Initialize();
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.LinkButton(API.GetCurrentCurrency(FilePath, index).explorer,this.WindowState);
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
            if (e.Key == Key.Escape || e.Key == Key.Tab)
            {
                Menu1_Click(sender, e);
            }
            if(e.Key == Key.Left)
            {
                Decrease_Click(sender, e);
            }
            if(e.Key == Key.Right)
            {
                Increase_Click(sender, e);
            }
            if(e.Key == Key.Up)
            {
                Details_Click(sender, e);
            }
            if(e.Key == Key.Down)
            {
                Link_Click(sender, e);
            }
            if(Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Left)
            {
                index = 0;
                Initialize();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Right)
            {
                index = MaxIndex - 1;
                Initialize();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.Search_Click(index);
            Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(ValueInput.Text,out int v))
            {
                if(v <= 100 && v >= 1)
                {
                    MaxIndex = v;
                    this.Filter.Visibility = Visibility.Collapsed;
                    Initialize();
                }
                else
                {
                    MessageBox.Show("The maximum value is 100.");
                }
            }
            else
            {
                MessageBox.Show("The input number should be an integer.");
            }

        }

        private void FilterB_Click(object sender, RoutedEventArgs e)
        {
            if(FilterState == false)
            {
                this.Filter.Visibility = Visibility.Visible;
            }
            else
            {
                this.Filter.Visibility = Visibility.Collapsed;
            }
            FilterState = !FilterState;
        }
    }
}
