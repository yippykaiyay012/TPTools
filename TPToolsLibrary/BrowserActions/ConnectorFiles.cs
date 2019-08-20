using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class ConnectorFiles
    {
        public static void Download(List<string> courseCodeList, string portalId)
        {
            var browser = WebBrowser.Driver;

            //   progConnector.Value = 0;
            //   progConnector.Maximum = courseCodeList.Length;

            foreach (var course in courseCodeList)
            {
                var courseUrl =
                    @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/courseContent";

                browser.Url = courseUrl;
                browser.FindElementById("courseContent__downloadForCloud_button").Click();

              //  progConnector.Increment(1);
            }
        }
    }
}
