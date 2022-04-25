using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace XproBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BotMachine bot;
        Scraper scraper;
        IWebDriver driver = new ChromeDriver();
        TelegramBotConnect tbotcon;
        String path = "D:\\ProyekBotTelkom\\bot_project_2\\web static 2\\scraped page\\XPRO - Detail Order.html";
        public MainWindow()
        {
            InitializeComponent();
            bot = new BotMachine(driver);
            tbotcon = new TelegramBotConnect();
            //SendData.IsEnabled = true;
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bot.startLogin();
                SendData.IsEnabled = true;
                DisplayName.Content = "Starting...";
                DisplayName.Foreground = Brushes.Green;
                MessageBox.Show("Please Login to The Website !!!");
            }
            catch (Exception ex)
            {
                DisplayName.Content = "Error !!!";
                DisplayName.Foreground = Brushes.Red;
                MessageBox.Show(ex.ToString());
            }
        }

        private async void SendDataMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SendData.Toggled1 == true)
            {
                try
                {
                    //Trace.WriteLine(scraper.htmlCheck());
                    scraper = new Scraper(bot);
                    SleepInputBox.Text = TelegramBotConnect.sleep.ToString();
                    DisplayName.Content = "Sending...";
                    DisplayName.Foreground = Brushes.Green;
                    MessageBox.Show("Running " + tbotcon.username + " 💻🤖");

                    //scraper.getMsgsTextAsync();

                    await Task.Run(() => tbotcon.startBotWork(scraper));
                }
                catch (Exception ex)
                {
                    DisplayName.Content = "Error !!!";
                    DisplayName.Foreground = Brushes.Red;
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                if (tbotcon != null)
                {
                    DisplayName.Content = "Stopping...";
                    DisplayName.Foreground = Brushes.Red;
                    tbotcon.stopBotWork();
                    bot.driver.Quit();

                }
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tbotcon != null)
            {
                tbotcon.stopBotWork();
            }
            DisplayName.Content = "Stopping...";
            DisplayName.Foreground = Brushes.Red;
            bot.driver.Quit();
        }

        private void TextBoxKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    TelegramBotConnect.sleep = int.Parse(SleepInputBox.Text);
                    MessageBox.Show("Sleep value Has Set \n Sleep Value : " + TelegramBotConnect.sleep.ToString());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }
    }
}
