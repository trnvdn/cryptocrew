using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestTask
{
    static class ButtonsHandler
    {

        public static void MenuClick(Grid menu,Button menuBtn)
        {
            

           if(menu.Visibility == Visibility.Visible)
            {
                menuBtn.Visibility = Visibility.Visible;
                menu.Visibility = Visibility.Collapsed;
            }
           else
            {
                menuBtn.Visibility = Visibility.Collapsed;
                menu.Visibility = Visibility.Visible;
            }
           
        }


        public static void LinkButton(string link,WindowState ws)
        {
            ws = WindowState.Minimized;
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start {link}"
            });
        }
        public static void Search_Click(int index)
        {
            var s = new Search(index);
            s.Show();
        }
    }
}
