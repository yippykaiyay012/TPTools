using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class IndustryShare
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void ShareCourses(List<string> courseCodeList, List<string> industries, string portalId)
        {

            foreach (var course in courseCodeList)
            {
                try
                {
                    browser.Url =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/coursesharing/list";


                    foreach (var industry in industries)
                    {
                        try
                        {
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='section']/div/div[1]/div/div/span"))).Click();

                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='industryRadioButton']"))).Click();

                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='industry']"))).SendKeys(industry);
                            Thread.Sleep(1000);

                            if (industry == "Maritime")
                                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='industry']"))).SendKeys(Keys.Backspace);

                            Thread.Sleep(1000);
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='industry']"))).SendKeys(Keys.Tab);
                            Thread.Sleep(1000);
                            browser.FindElementByName("_eventId_complete").Click();
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
