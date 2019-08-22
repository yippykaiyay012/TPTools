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
    public class CourseExpiry
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void SetCourseExpiry(List<string> courseCodeList, string months, string portalId)
        {

            foreach(var course in courseCodeList)
            {

                browser.Url =
                       @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/about";


                wait.Until(driver => driver.FindElement(By.Id("courseBasicPropertiesEditMiniIcon"))).Click();

                wait.Until(driver => driver.FindElement(By.Id("repetitionIntervalEdit__setIntervalRadioWrapper"))).Click();
               
                Thread.Sleep(500);

                wait.Until(driver => driver.FindElement(By.Id("repetitionIntervalEdit__intervalSelector"))).Clear();

                wait.Until(driver => driver.FindElement(By.Id("repetitionIntervalEdit__intervalSelector"))).SendKeys(months);

                browser.FindElementByName("_eventId_complete").Click();

            }


        }
    }
}
