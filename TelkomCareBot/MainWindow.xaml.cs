using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TelkomCareBot
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
       // String path = "D:\\ProyekBotTelkom\\bot_project_2\\web static 3\\TelkomCare-2021Telkomcare-2021.html";
        public MainWindow()
        {
            InitializeComponent();
            bot = new BotMachine(driver);
            tbotcon = new TelegramBotConnect();
            SendData.IsEnabled = true;
            LimitInputBox.IsEnabled = false;
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
                    ///*** Change constructor parameter with 'bot' if we want online data from website
                    bot.navigateToTheData();
                    scraper = new Scraper(bot);
                    SleepInputBox.Text = TelegramBotConnect.sleep.ToString();
                    LimitInputBox.Text = TelegramBotConnect.msgLimit.ToString();
                    DisplayName.Content = "Sending...";
                    DisplayName.Foreground = Brushes.Green;
                    MessageBox.Show("Running " + tbotcon.username + " 💻🤖");
                    //var scrapeMsg = Task.Run(async () => await scraper.getMsgsText()).Result;
                    //String[] testMsg = { "test", "test2", "test3" };
                    //String[] msg = new String[10];
                    //for (int i = 0; i < msg.Length; i++)
                    //{
                    //    msg[i] = scrapeMsg[i];
                    //}
                    //Trace.WriteLine(msg[3]);
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
                }
            }

        }

        private void SleepValueTextBoxKeyDownEvent(object sender, KeyEventArgs e)
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

        private void LimitValueTextBoxKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (TelegramBotConnect.messages?.Length != null)
                    {
                        itemLabel.Content = "(" + TelegramBotConnect.messages.Length + " items)";
                    }
                    else
                    {
                        itemLabel.Content = "(Press Enter Again)";
                    }
                    TelegramBotConnect.msgLimit = int.Parse(LimitInputBox.Text);
                    MessageBox.Show("Limit value Has Set \n Limit Value : " + TelegramBotConnect.msgLimit.ToString());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
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

    }
}
