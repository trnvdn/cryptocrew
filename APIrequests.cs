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
        public int ProgressBarCount = 0;
            public async Task LoadData()
            {
                string url = "http://api.coincap.io/v2/assets";
                HttpClient client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        if (!File.Exists("storage.json"))
                        {
                            File.Create("storage.json");
                        }
                        File.WriteAllText("storage.json", responseData);
                        while(ProgressBarCount != 100)
                        {
                            ProgressBarCount++;
                        }
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
            private RootObject GetData()
            { 
                string json = File.ReadAllText("storage.json");
                return JsonConvert.DeserializeObject<RootObject>(json); 
            }
            public Currency GetCurrentCurrency(int index)
        {
            return GetData().data[index];
        }
            
    }
}
