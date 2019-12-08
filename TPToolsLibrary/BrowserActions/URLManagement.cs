using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TPToolsLibrary.BrowserActions
{
    public class URLManagement
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        const string username = "trainingportal";
        const string password = "knicPof1";


        public static void RegisterURL(string logicalId)
        {
            Login();
        }


        private static void Login()
        {
            try
            {
                //if (true)
                //{
                    browser.Url = @"https://www.trainingportal.co.uk/administrator";

                 //   Thread.Sleep(10000);

                   // var alert = browser.SwitchTo().Alert();

                var alert = wait.Until(driver => driver.SwitchTo().Alert());

                alert.SetAuthenticationCredentials(username, password);
                    alert.Accept();
         //       }

            }
            catch (Exception e)
            {

            }



        }
    }
}
