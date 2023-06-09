﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public  partial class MainWindow : Window
    {
        const string FilePath = "storage.json";
        const string link = "http://api.coincap.io/v2/assets";
        private APIrequests api = new APIrequests();
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }
        
        public async Task InitializeAsync()
        {
            await api.LoadData(link,FilePath);
            var newWindow = new Home(0);
            newWindow.Show();
            Close();
        }
    }
}
