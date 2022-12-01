using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{

    public enum EvidenceTypesEnum
    {
        OB,
        PW,
        QA,
        TS,
        TA,
        WIT,
        RPL,
        PD,
        OTHER
    }

    public class SetEvidenceType
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static async Task Update(List<string> compCodeList, List<EvidenceTypesEnum> evidenceTypes, string portalID)
        {
            // progComp.Value = 0;
            //  progComp.Maximum = compCodeList.Length;

            foreach (var comp in compCodeList)
            {
                try
                {
                    var compUrl = "https://www.trainingportal.no/mintra/" + portalID + "/admin/competences/competence/dashboard/about/" + comp;
                    browser.Url = compUrl;


         
                    
                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='competenceAboutEdit__showApprovalApplicationDiv']/a"))).Click();



                    //OB
                    var OB = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_0']")));


                    //PW
                    var PW = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_1']")));

                    //QA
                    var QA = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_2']")));

                    //TS
                    var TS = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_3']")));

                    //TA
                    var TA = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_4']")));

                    //WIT
                    var WIT = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_5']")));

                    //RPL
                    var RPL = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_6']")));

                    //PD
                    var PD = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_7']")));

                    //OTHER
                    var OTHER = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_CheckBox_8']")));


                    if (evidenceTypes.Contains(EvidenceTypesEnum.OB))
                    {
                        CheckAndSelectElement(OB);
                    }
                    else
                    {
                        CheckAndUnSelectElement(OB);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.PW))
                    {
                        CheckAndSelectElement(PW);
                    }
                    else
                    {
                        CheckAndUnSelectElement(PW);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.QA))
                    {
                        CheckAndSelectElement(QA);
                    }
                    else
                    {
                        CheckAndUnSelectElement(QA);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.TS))
                    {
                        CheckAndSelectElement(TS);
                    }
                    else
                    {
                        CheckAndUnSelectElement(TS);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.TA))
                    {
                        CheckAndSelectElement(TA);
                    }
                    else
                    {
                        CheckAndUnSelectElement(TA);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.WIT))
                    {
                        CheckAndSelectElement(WIT);
                    }
                    else
                    {
                        CheckAndUnSelectElement(WIT);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.RPL))
                    {
                        CheckAndSelectElement(RPL);
                    }
                    else
                    {
                        CheckAndUnSelectElement(RPL);
                    }

                    if (evidenceTypes.Contains(EvidenceTypesEnum.PD))
                    {
                        CheckAndSelectElement(PD);
                    }
                    else
                    {
                        CheckAndUnSelectElement(PD);
                    }


                    if (evidenceTypes.Contains(EvidenceTypesEnum.OTHER))
                    {
                        CheckAndSelectElement(OTHER);
                    }
                    else
                    {
                        CheckAndUnSelectElement(OTHER);
                    }




                    //var saveButton = browser.FindElementByName("_eventId_editApplicationApprovalSubmit");
                    var saveButton  = browser.FindElement(By.Name("_eventId_editApplicationApprovalSubmit"));


                    saveButton.Click();
                   

                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }

            }
        }

        private static void CheckAndSelectElement(IWebElement element)
        {
            try
            {

                if (!element.Selected)
                {
                    element.Click();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }
        }

        private static void CheckAndUnSelectElement(IWebElement element)
        {
            try
            {      
                if (element.Selected)
                {
                    element.Click();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }

        }
    }
}
