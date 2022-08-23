using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using CefSharp.Wpf;
using Browser.Properties;

namespace Browser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public List<string> WebPages; //список веб-страниц, посещенных с момента открытия браузера
        int Current = 0;

        public MainWindow()
        {
            InitializeComponent();
        } 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebPages = new List<string>();

            GoHome();
        }
        void GoHome()
        {
            area.Text = "www.yandex.ru";
            Chrome.Address = "www.yandex.ru";
            WebPages.Add("www.yandex.ru");
        }
        void LoadWebPages(string Link, bool addToList=true)
        {
            area.Text = Link;
            Chrome.Address = Link;
            MenuItem Item = new MenuItem();
            Item.Click += MenuClicked;
            Item.Header = Link;
            Item.Width = 184;
            Menu.Items.Add(Item);

            if(addToList)
            {
                Current++;
                WebPages.Add(Link);
            }
        }
        void ToggleWebPages(string Option)
        {
            if (Option == "→")
            {
                if ((WebPages.Count - Current - 1) != 0)
                { Current++; LoadWebPages(WebPages[Current], false); }
            }
            else
            {
                if((WebPages.Count + Current-1) >= WebPages.Count)
                {
                    Current--; LoadWebPages(WebPages[Current], false);
                }
            }
           
        }
        //кнопки назад вперед
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ToggleWebPages(btn.Content.ToString());
        }

        //перезагрузка страницы
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[Current]);
        }

        // кнопка home 
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[0]);
        }
        private void MenuClicked(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            LoadWebPages(item.Header.ToString());
        }

        private void area_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoadWebPages(area.Text);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(WebPages.Count !=0)
            { 
             Menu.PlacementTarget = historyBtn;
             Menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
             Menu.HorizontalOffset = -155;
             Menu.IsOpen = true;
            }
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true; //отключить в случае нажатия правой кнопкой мыши
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            /*foreach(var page in WebPages)
            Settings.Default.WebPageS.Add(page);
            Settings.Default.Save();*/
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
