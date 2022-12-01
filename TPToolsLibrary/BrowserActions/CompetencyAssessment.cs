using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class CompetencyAssessment
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void UpdateCompetencyStatus(List<string> compCodeList, bool applyAssessment, string portalID)
        {
            // progComp.Value = 0;
            //  progComp.Maximum = compCodeList.Length;

            foreach (var comp in compCodeList)
            {
                try
                {
                    var compUrl = @"https://www.trainingportal.no/mintra/" + portalID + "/admin/competences/competence/dashboard/about/" + comp;

                    browser.Url = compUrl;

                    //var downloadButton = Driver.FindElementById("courseContent__downloadForCloud_button");
                    wait.Until(driver => driver.FindElement(By.XPath("(//a[@title='Edit'])[2]"))).Click();

                    var saveButton = browser.FindElement(By.Name("_eventId_editApplicationApprovalSubmit"));
                    

                    if (applyAssessment)
                    {
                        wait.Until(driver => driver.FindElement(By.Id("enableStudentApprovalApplicationBtn"))).Click();

                        saveButton.Click();
                    }

                    if (!applyAssessment)
                    {
                        wait.Until(driver => driver.FindElement(By.Id("disableStudentApprovalApplicationBtn"))).Click();

                        saveButton.Click();
                    }


                    //progComp.Increment(1);

                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }
             
            }
        }
    }
}
