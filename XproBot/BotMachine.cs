using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace XproBot
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
            driver.Navigate().GoToUrl("https://xpro.telkom.co.id/login.php?redir=%2Fdetail_task.php%3Ftipe%3Dtsq%26source%3Dtask.php%26prev%3Dtask.php%26reg%3DREG-1%26witel%3DSUMATERA%2BSELATAN%2B%2528PALEMBANG%2529%26viewproduk%3D%26produk%3Dall%26status%3D%26approval%3D%26startdate%3D2019-01-01%26end%3D2021-08-30/https://xpro.telkom.co.id/login.php?redir=%2Fdetail_task.php%3Ftipe%3Dtsq%26source%3Dtask.php%26prev%3Dtask.php%26reg%3DREG-1%26witel%3DSUMATERA%2BSELATAN%2B%2528PALEMBANG%2529%26viewproduk%3D%26produk%3Dall%26status%3D%26approval%3D%26startdate%3D2019-01-01%26end%3D2021-08-30/https://xpro.telkom.co.id/login.php?redir=%2Fdetail_task.php%3Ftipe%3Dtsq%26source%3Dtask.php%26prev%3Dtask.php%26reg%3DREG-1%26witel%3DSUMATERA%2BSELATAN%2B%2528PALEMBANG%2529%26viewproduk%3D%26produk%3Dall%26status%3D%26approval%3D%26startdate%3D2019-01-01%26end%3D2021-08-30/");
               
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
            if(driver.Title == "XPRO")
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
