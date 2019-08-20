using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class CompetencyAssessment
    {
        public static void UpdateCompetencyStatus(List<string> compCodeList, bool applyAssessment, string portalID)
        {
            var browser = WebBrowser.Driver;

            // progComp.Value = 0;
            //  progComp.Maximum = compCodeList.Length;

            foreach (var comp in compCodeList)
            {
                var compUrl = @"https://www.trainingportal.no/mintra/" + portalID + "/admin/competences/competence/dashboard/about/" + comp;

               browser.Url = compUrl;

                //var downloadButton = Driver.FindElementById("courseContent__downloadForCloud_button");
               browser.FindElementByXPath("(//a[@title='Edit'])[3]").Click();

                var saveButton =browser.FindElementByName("_eventId_editApplicationApprovalSubmit");

                if (applyAssessment)
                {
                   browser.FindElementById("enableStudentApprovalApplicationBtn").Click();

                    saveButton.Click();
                }

                if (!applyAssessment)
                {
                   browser.FindElementById("disableStudentApprovalApplicationBtn").Click();

                    saveButton.Click();
                }


                //progComp.Increment(1);

            }
        }
    }
}
