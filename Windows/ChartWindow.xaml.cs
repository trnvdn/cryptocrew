using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestTask.Classes;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    /// 

    public partial class ChartWindow : Window
    {
        List<CurrencyHistory> history = new List<CurrencyHistory>();
        APIrequests API = new APIrequests();
        const string FilePath = "history.json";
        string link;
        int index;
        int maxindex;
        DateTimeOffset start;
        DateTimeOffset end;
        public ChartWindow(string id, DateTimeOffset start, DateTimeOffset end,int index,int maxindex)
        {
            this.index = index;
            this.maxindex = maxindex;
            this.start = start;
            this.end = end;
            link = "http://api.coincap.io/v2/assets/" + id + "/history?interval=h1";
            InitializeComponent();
            Loaded += async (sender, e) =>
            {
                var correctValues = await GetHistoryData();
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = id,
                        Values = new ChartValues<decimal>(correctValues.Select(x=>x.priceUsd)),
                        StrokeThickness = 3
                    }
                };
                Labels = correctValues.Select(x => x.date.ToString("f")).ToArray();
                YFormatter = value => value.ToString("N2") + "$";
                DataContext = this;
            };
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private async Task<List<CurrencyHistory>> GetHistoryData()
        {
            await API.LoadData(link, FilePath);
            history = API.GetHistory(FilePath);

            return history.Where(x => x.date.ToLocalTime() < end && x.date.ToLocalTime() > start).ToList();
        }

        private void Chart_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Escape)
            {
                var d = new Details(index,maxindex);
                d.Show();
                Close();
            }
        }
    }
}