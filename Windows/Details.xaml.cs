using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Логика взаимодействия для Details.xaml
    /// </summary>
    public partial class Details : Window 
    {
        Currency cur = new Currency();
        APIrequests API = new APIrequests();
        Currency ro = new Currency();
        int index;
        int MaxIndex;
        const string FilePath = "storage.json";
        const string link = "http://api.coincap.io/v2/assets";
        public Details(int i,int maxindex)
        {
            MaxIndex = maxindex;
            index = i;
            ro = API.GetCurrentCurrency(FilePath, index);
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            cur = API.GetCurrentCurrency(FilePath,index);
            Symbol.Text = "Symbol: " + cur.symbol;
            Currency.Text = "Currency: " + cur.name;
            Price.Text = "Price: " + cur.priceUsd.ToString() + "$";
            VolumePerDay.Text = "Volume per day: " + cur.volumeUsd24Hr.ToString() + "$";
            MarketCap.Text = "Market capitalization: " + cur.marketCapUsd.ToString() + "$";
            VWAP.Text = "VWAP: " + cur.vwap24Hr.ToString() + "$";
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Home h = new Home(index);
            h.Show();
            Close();
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.LinkButton(API.GetCurrentCurrency(FilePath, index).explorer, this.WindowState);
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
        private void Details_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Tab)
            {
                Menu1_Click(sender, e);
                this.ValueForChart.Visibility = Visibility.Collapsed;
            }
            if (e.Key == Key.Left)
            {
                Decrease_Click(sender, e);
            }
            if (e.Key == Key.Right)
            {
                Increase_Click(sender, e);
            }
            if (e.Key == Key.Up)
            {
                Return_Click(sender, e);
            }
            if (e.Key == Key.Down)
            {
                Link_Click(sender, e);
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

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            await API.LoadData(link, FilePath);
            Initialize();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ButtonsHandler.Search_Click(index);
            Close();
        }

        private void Chart_Click(object sender, RoutedEventArgs e)
        {
            ValueForChart.Visibility = Visibility.Visible;
            Increase.Visibility = Visibility.Collapsed;
            Decrease.Visibility = Visibility.Collapsed;
        }
        public DateTime EndDate
        {
            get { return DateTime.Now; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            if (Start.SelectedDate.HasValue && End.SelectedDate.HasValue)
            {
                DateTimeOffset start = new DateTimeOffset(Start.SelectedDate.Value.ToUniversalTime());
                DateTimeOffset end = new DateTimeOffset(End.SelectedDate.Value.ToUniversalTime());
                ChartWindow cw = new ChartWindow(cur.id, start, end, index, MaxIndex);
                cw.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            ValueForChart.Visibility = Visibility.Collapsed;
            Increase.Visibility = Visibility.Visible;
            Decrease.Visibility = Visibility.Visible;
        }
    }
}
