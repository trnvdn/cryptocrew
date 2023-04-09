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
        RootObject? ro = new RootObject();
        private int index = 0;


        public Home()
        {
            InitializeComponent();
            LoadData();
        }

        private async Task LoadData()
        {
            string url = "http://api.coincap.io/v2/assets";
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    File.WriteAllText("storage.json", responseData);

                    string json = File.ReadAllText("storage.json");
                    ro = JsonConvert.DeserializeObject<RootObject>(json);

                    Initialize(index);
                }
                else
                {
                    MessageBox.Show($"An error occurred while retrieving data: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving data: {ex.Message}");
            }
        }


        private void Initialize(int i)
        {
            if (ro?.data != null && ro.data.Any())
            {
                Currency currency = ro.data[i];
                Currency.Text = currency.id;
                Price.Text = currency.priceUsd.ToString();
                Code.Text = currency.symbol;
                Rank.Text = currency.rank.ToString();
                СhangePercent.Text = currency.changePercent24Hr.ToString();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }
        
        private void decrease_Click(object sender, RoutedEventArgs e)
        {
            index = index == 0 ? ro.data.Count - 1 : index - 1;
            Initialize(index);
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            index = index == ro?.data.Count - 1 ? 0 : index + 1;
            Initialize(index);
        }

        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start {ro?.data[index].explorer}"
            });
        }

        private void Menu1_Click(object sender, RoutedEventArgs e)
        {
            MenuJPEG.Visibility = Visibility.Visible;
            MenuInside.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Collapsed;
        }

        private void MenuInside_Click(object sender, RoutedEventArgs e)
        {
            MainJPG.Visibility = Visibility.Visible;
            MenuJPEG.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Visible;
            MenuInside.Visibility = Visibility.Collapsed;
        }
    }
}
