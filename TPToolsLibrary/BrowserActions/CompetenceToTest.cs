using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.BrowserActions
{
    public class CompetenceToTest
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void SetToTest(string portalId, List<string> competenceIds)
        {



            foreach(var comp in competenceIds)
            {
                browser.Url =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/competences/competence/dashboard/about/" + comp;


                //*[@id="aboutContent"]/div[1]/strong/a[2]
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='aboutContent']/div[1]/strong/a[2]"))).Click();
                //*[@id="dijit_form_RadioButton_4"]
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_RadioButton_4']"))).Click();

                
                browser.FindElement(By.Name("_eventId_save")).Click();

            }

        }
    }
}
