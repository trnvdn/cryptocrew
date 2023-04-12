using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTask.Classes;

namespace TestTask
{
    internal class APIrequests
    {
        CurrencyHistoryList ch = new CurrencyHistoryList();
        RootObject ro = new RootObject();
        HttpClient client = new HttpClient();
        public async Task LoadData(string link,string file)
        {
            
            try
            {
                HttpResponseMessage response = await client.GetAsync(link);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (!File.Exists(file))
                    {
                        var fileStream = File.Create(file);
                        fileStream.Close();
                    }
                    File.WriteAllText(file, responseData);
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
        public List<Currency> GetData(string FilePath)
        {
            string json = File.ReadAllText(FilePath);
            ro = JsonConvert.DeserializeObject<RootObject>(json);
            return ro.data;
        }
        public List<CurrencyHistory> GetHistory(string FilePath)
        {
            string json = File.ReadAllText(FilePath);
            ch = JsonConvert.DeserializeObject<CurrencyHistoryList>(json);
            return ch.data;
        }
        public Currency GetCurrentCurrency(string FilePath,int index)
        {
            return GetData(FilePath)[index];
        }
    }
}
