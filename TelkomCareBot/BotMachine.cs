using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TelkomCareBot
{
    public class BotMachine
    {
        public IWebDriver driver;
        public Cookie cookie;
        public ChromeDriverService service;

        public BotMachine(IWebDriver driver)
        {
            this.driver = driver;
            cookie = default(Cookie);
            service = ChromeDriverService.CreateDefaultService();
        }

        public void startLogin()
        {
            driver.Navigate().GoToUrl("https://telkomcare.telkom.co.id/assurance/lapebis/surveiltreg?dir=EBIS");

        }

        public void navigateToTheData()
        {
            WaitForAjax();
            driver.FindElement(By.XPath("//div[@class='labellevel'][3]/span/a")).Click();
            WaitForAjax();

        }

        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                Thread.Sleep(100);
            }
        }



        public void hideBrowser()
        {
            service.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.AddArgument("--window-position=-32000,-32000");
            driver = new ChromeDriver(service, options);
        }

        public string htmlCheck()
        {
            return driver.PageSource;
        }

        public Boolean is_logged_in()
        {
            if (driver.Title == "XPRO")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
