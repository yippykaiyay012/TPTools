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
    public class CompanyShare
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void ShareCourses(List<string> courseCodeList, List<string> companyList, string portalId)
        {

            foreach (var course in courseCodeList)
            {
                try
                {
                    browser.Url =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/coursesharing/list";


                    foreach (var company in companyList)
                    {
                        try
                        {
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='section']/div/div[1]/div/div/span"))).Click();

                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='companyRadioButton']"))).Click();

                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='company']"))).SendKeys(company);
                            Thread.Sleep(2000);
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='company']"))).SendKeys(Keys.Tab);
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
