using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public enum SCORMType
    {
        SCORM12,
        SCORM20042nd,
        SCORM20043rd,
        SCORM20044th
    }
    public class ConnectorFiles
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void Download(List<string> courseCodeList, string portalId, bool sameWindow, SCORMType scormType)
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

                    if (sameWindow)
                    {
                        wait.Until(driver => driver.FindElement(By.XPath("//*[@id='cloudShellVersion']/tbody/tr/td[2]/input"))).Click();

                        if(wait.Until(driver => driver.FindElements(By.XPath("//*[@id='dijit_MenuItem_1_text']"))).Count() > 0)
                        {
                            wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_MenuItem_1_text']"))).Click();
                        }
                    }

                    //scorm type
                    //wait.Until(driver => driver.FindElement(By.XPath("//*[@id='scormVersion']/tbody/tr/td[2]/input"))).Click();   // expand                 

                    //type
                    //switch (scormType)
                    //{
                    //    case SCORMType.SCORM12:
                    //        wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_MenuItem_0_text']"))).Click();
                    //        break;
                    //    case SCORMType.SCORM20042nd:
                    //        wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_MenuItem_1_text']"))).Click();
                    //        break;
                    //    case SCORMType.SCORM20043rd:
                    //        wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_MenuItem_2_text']"))).Click();
                    //        break;
                    //    case SCORMType.SCORM20044th:
                    //        wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_MenuItem_3_text']"))).Click();                           
                    //        break;
                    //    default:                           
                    //        break;
                    //}


                    wait.Until(driver => driver.FindElement(By.Id("courseContent__downloadForCloud_button"))).Click();
                    Thread.Sleep(1000);

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
