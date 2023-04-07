using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Security.Policy;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace TestTask
{


    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        RootObject? ro = new RootObject();
        private int index = 0;


        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private async Task LoadData()
        {
            string url = "https://api.coincap.io/v2/assets";
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    ro = JsonConvert.DeserializeObject<RootObject>(responseData);
                    File.WriteAllText("storage.json", responseData);
                    Update();
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

        private void Update()
        {
            if (File.Exists("storage.json"))
            {
                string json = File.ReadAllText("storage.json");
                ro = JsonConvert.DeserializeObject<RootObject>(json);
                Initialize(index);
            }
            else
            {
                MessageBox.Show("ERROR.FILE ISN`T FINDED");
            }
        }

        private void Initialize(int i)
        {
            if (ro?.data != null && ro.data.Any())
            {
                Currency currency = ro.data[i];
                ID.Text = currency.name;
                Price.Text = $"{currency.priceUsd} USD";
                Tag.Text = currency.symbol;
                Rank.Text = currency.rank.ToString();
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
            index = index == ro.data.Count - 1 ? 0 : index + 1;
            Initialize(index);
        }

        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private void ButtonURL_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start {ro.data[index].explorer}"
            });
        }
    }
}
