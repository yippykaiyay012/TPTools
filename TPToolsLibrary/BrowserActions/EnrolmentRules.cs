using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class EnrolmentRules
    {
        public static void AddEnrolmentRule(List<string> courseCodeList, string emailAddress, string orgUnit,  string portalId)
        {
            var browser = WebBrowser.Driver;
            //   progEnrolRules.Value = 0;
            //  progEnrolRules.Maximum = courseCodeList.Length;

            foreach (var course in courseCodeList)
            {
                browser.Url =
                    @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/enrollmentrules/list";

                browser.FindElementByClassName("buttonText").Click();

                browser.FindElementById("targetGroupEditOrgUnitAnchor").Click();

                browser.FindElementById("singleOrgUnitsSelectedRadioGroup").Click();

                browser.FindElementById("clickMeyay").Click();

                Thread.Sleep(500);

                browser.FindElementByXPath("//*[@id='dijit__TreeNode_1']/div[1]/span[1]").Click();

                Thread.Sleep(500);

                browser.FindElementByXPath("//span[contains(@class,'dijitTreeLabel') and contains(text(), '" + orgUnit + "')]").Click();

                Thread.Sleep(500);

                browser.FindElementById("targetGroup__submitOrgUnit_button").Click();

                browser.FindElementById("APPROVAL_NEEDED").Click();

                browser.FindElementById("OTHER_APPROVAL").Click();

                browser.FindElementById("emailAddress").SendKeys(emailAddress);

                browser.FindElementByName("_eventId_complete").Click();

             //   progEnrolRules.Increment(1);
            }
        }
    }
}
