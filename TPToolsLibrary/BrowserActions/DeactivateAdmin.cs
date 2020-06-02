using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{
    public static class DeactivateAdmin
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static List<string> successList = new List<string>();
        private static List<string> failedList = new List<string>();



        public static async Task DeactivateAdminAccount(string userId, string portalId)
        {
            try
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/users/user/" + userId + "/completeProfile";


                var elementCheck = wait.Until(driver => driver.FindElements(By.XPath("//*[@id='userPasswordContent']/strong/a")));
                if(elementCheck.Count == 0)
                {
                    failedList.Add(userId);
                    return;
                }
                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='userPasswordContent']/strong/a"))).Click();

                var role = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='editLoginInfoModel.roleLogicalId']/tbody/tr/td[2]/input")));
                role.Click();
                role.SendKeys("Student");
                Thread.Sleep(500);
                role.SendKeys(Keys.Tab);

                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_RadioButton_1']"))).Click();

                browser.FindElementByName("_eventId_complete").Click();

                successList.Add(userId);

            }
            catch(Exception e)
            {
                failedList.Add(userId);
            }





        }
    }
}
