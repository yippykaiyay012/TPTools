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

        public static void SetCourseExpiry(List<string> courseCodeList, string months, string portalId)
        {
            var browser = WebBrowser.Driver;


            foreach(var course in courseCodeList)
            {

                browser.Url =
                       @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/about";


                browser.FindElementById("courseBasicPropertiesEditMiniIcon").Click();

                browser.FindElementById("repetitionIntervalEdit__setIntervalRadioWrapper").Click();
               
                Thread.Sleep(500);

                browser.FindElementById("repetitionIntervalEdit__intervalSelector").Clear();

                browser.FindElementById("repetitionIntervalEdit__intervalSelector").SendKeys(months);

                browser.FindElementByName("_eventId_complete").Click();

            }


        }
    }
}
