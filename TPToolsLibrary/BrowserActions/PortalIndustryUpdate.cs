using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{
    public class PortalIndustryUpdate
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;


        public static async Task Update(List<string> portalList, string industry)
        {
           
            foreach(var portal in portalList)
            {
                try
                {
                    browser.Url = "https://www.trainingportal.no/mintra/33/admin/portals/show/portal/" + portal;
                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='editPortal']"))).Click();

                    var industryField = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portalEditModel.portal.industry']/tbody/tr/td[1]/div[1]/span")));

                    
                    industryField.Click();
                    //industryField.SendKeys(industry);
                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_MenuItem_16_text']"))).Click();
                    Thread.Sleep(1000);

                    //industryField.SendKeys(Keys.Enter);


                    browser.FindElement(By.Name("_eventId_complete")).Click();
                }           
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
