using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void AddEnrolmentRule(List<string> courseCodeList, string emailAddress, string orgUnit,  string portalId)
        {
            //   progEnrolRules.Value = 0;
            //  progEnrolRules.Maximum = courseCodeList.Length;

            foreach (var course in courseCodeList)
            {
                try
                {
                    browser.Url =
                   @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/enrollmentrules/list";

                    wait.Until(driver => driver.FindElement(By.ClassName("buttonText"))).Click();

                    wait.Until(driver => driver.FindElement(By.Id("targetGroupEditOrgUnitAnchor"))).Click();

                    wait.Until(driver => driver.FindElement(By.Id("singleOrgUnitsSelectedRadioGroup"))).Click();

                    wait.Until(driver => driver.FindElement(By.Id("clickMeyay"))).Click();

                    Thread.Sleep(500);

                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit__TreeNode_1']/div[1]/span[1]"))).Click();

                    Thread.Sleep(500);

                    wait.Until(driver => driver.FindElement(By.XPath("//span[contains(@class,'dijitTreeLabel') and contains(text(), '" + orgUnit + "')]"))).Click();

                    Thread.Sleep(500);

                    wait.Until(driver => driver.FindElement(By.Id("targetGroup__submitOrgUnit_button"))).Click();

                    wait.Until(driver => driver.FindElement(By.Id("APPROVAL_NEEDED"))).Click();

                    wait.Until(driver => driver.FindElement(By.Id("OTHER_APPROVAL"))).Click();

                    wait.Until(driver => driver.FindElement(By.Id("emailAddress"))).SendKeys(emailAddress);

                    browser.FindElementByName("_eventId_complete").Click();

                    //   progEnrolRules.Increment(1);
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }
               
            }
        }
    }
}
