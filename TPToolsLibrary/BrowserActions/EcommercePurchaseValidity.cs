using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TPToolsLibrary.BrowserActions
{
    public class EcommercePurchaseValidity
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void SetPurchaseValidity(List<string> courseCodeList, string months, string portalId)
        {
            if(months == "24")
            {
                months = "2 years";
            }
            else if(months == "36")
            {
                months = "3 years";
            }

            foreach (var course in courseCodeList)
            {
                try
                {
                    browser.Url =
                      @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/ecommerce/list";


                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='ecommercesList']/tbody/tr[2]/td[8]/a"))).Click();

                    var validityDropdown = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='validityMonths']/tbody/tr/td[1]/div[1]/span")));

                    validityDropdown.Click();
                    var validityDropdownText = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='validityMonths']/tbody/tr/td[1]/div[1]/span")));
                    validityDropdownText.SendKeys(months);
                    Thread.Sleep(500);
                    validityDropdownText.SendKeys(Keys.Tab);



                    browser.FindElement(By.Name("_eventId_complete")).Click();
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }

            }
        }
    }
}
