using OpenQA.Selenium;
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
        public static void ShareCourses(List<string> courseCodeList, List<string> companyList, string portalId)
        {
            var browser = WebBrowser.Driver;

            foreach (var course in courseCodeList)
            {
                browser.Url =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/coursesharing/list";



                foreach(var company in companyList)
                {
                    browser.FindElementByXPath("//*[@id='section']/div/div[1]/div/div/span").Click();

                    browser.FindElementByXPath("//*[@id='companyRadioButton']").Click();

                    browser.FindElementByXPath("//*[@id='company']").SendKeys(company);
                    Thread.Sleep(2000);
                    browser.FindElementByXPath("//*[@id='company']").SendKeys(Keys.Tab);
                    Thread.Sleep(1000);
                    browser.FindElementByName("_eventId_complete").Click();

                }

                

            }
        }
    }
}
