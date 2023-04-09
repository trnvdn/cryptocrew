using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestTask
{
    internal class APIrequests
    {
            RootObject ro = new RootObject();
            public async Task LoadData()
            {
                const string url = "https://api.coincap.io/v2/assets";
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

        public async void Update()
        {
            if (File.Exists("storage.json"))
            {
                await LoadData();
                string json = File.ReadAllText("storage.json");
                ro = JsonConvert.DeserializeObject<RootObject>(json);
                /*Initialize(index);*/
            }
            else
            {
                MessageBox.Show("ERROR.FILE ISN`T FINDED");
            }
        }
        public RootObject ReturnCurrentData()
        {
            Update();
            return ro;
        }
    }
}
