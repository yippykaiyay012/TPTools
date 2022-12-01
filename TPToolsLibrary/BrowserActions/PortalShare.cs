using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public static class PortalShare
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static async Task ShareCourses(List<string> courseCodeList, List<string> portalNameList, string portalId, string payingCompany)
        {

            foreach (var course in courseCodeList)
            {
                try
                {
                    browser.Url =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/coursesharing/list";


                    foreach (var portal in portalNameList)
                    {
                        try
                        {
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='section']/div/div[1]/div/div/span"))).Click();

                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portalRadioButton']"))).Click();

                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portal']"))).SendKeys(portal);
                            Thread.Sleep(2000);
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portal']"))).SendKeys(Keys.Tab);
                            Thread.Sleep(1000);
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='payingCompany']"))).SendKeys(payingCompany);
                            Thread.Sleep(1000);
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='payingCompany']"))).SendKeys(Keys.Tab);
                            Thread.Sleep(1000);


                            browser.FindElement(By.Name("_eventId_complete")).Click();
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(e.ToString());
                        }

                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }

            }
        }


    }
}
