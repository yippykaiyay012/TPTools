using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class ConnectorFiles
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void Download(List<string> courseCodeList, string portalId)
        {
            //   progConnector.Value = 0;
            //   progConnector.Maximum = courseCodeList.Length;

            foreach (var course in courseCodeList)
            {
                try
                {
                    var courseUrl =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/courseContent";

                    browser.Url = courseUrl;
                    wait.Until(driver => driver.FindElement(By.Id("courseContent__downloadForCloud_button"))).Click();

                    //  progConnector.Increment(1);
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }


            }
        }
    }
}
