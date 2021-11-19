using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{
    public class AddFrenchTabToCompetence
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;


        public static async Task AddFrenchTab(List<string> competenceIds, string portalId)
        {

            foreach(var competenceId in competenceIds)
            {
                browser.Url =
                    @"https://www.trainingportal.no/mintra/" + portalId + "/admin/competences/competence/dashboard/about/" + competenceId;

                //click edit
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='aboutContent']/div[1]/strong/a[2]"))).Click();


                //click french Tab
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='competenceAboutEditTabs_tablist']/div[4]/div/div[2]"))).Click();


                //add placeholderText
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='competenceFormAboutTitle_fr']"))).Clear();
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='competenceFormAboutTitle_fr']"))).SendKeys("FrenchPlaceholder");


                //save
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='saveBtn']/button"))).Click();






            }



        }
    }
}
