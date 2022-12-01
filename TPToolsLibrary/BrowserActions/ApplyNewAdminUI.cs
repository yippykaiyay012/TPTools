using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{



    public class ApplyNewAdminUI
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalId = null;

        //Common: Activate appcues  -on
        //Common: Use new administrator UI   -on
        //User administration: identity verification enabled -off

        private static List<string> PortalSettingsOn = new List<string>()
        {
            "portalBooleanProperties[ACTIVATE_APPCUES]",
            "portalBooleanProperties[USE_NEW_ADMIN_UI]",
           
        };
        private static List<string> PortalSettingsOff = new List<string>()
        {
            "portalBooleanProperties[USER_IDENTITY_VERIFICATION_REQUIRED]"

        };

        public static async Task<List<string>> Apply(List<string> portals)
        {
            var issuesList = new List<string>();

            foreach(var portal in portals)
            {
                try
                {              
                    browser.Url = @"https://www.trainingportal.no/mintra/" + portal + "/admin/portals/show/portal/" + portal;

                    // so we can check if going to plan or on error page, if error, try next portal
                    // try catch does not catch this...
                    var editPortal = browser.FindElement(By.Id("editPortal"));
                    

                    if (editPortal == null)
                    {
                        continue;
                    }

                    editPortal.Click();

                   //wait.Until(driver => driver.FindElement(By.Id("editPortal"))).Click();

                    foreach(var enableSetting in PortalSettingsOn)
                    {
                        CheckAndSelectElementName(enableSetting);
                    }
                    foreach (var disableSetting in PortalSettingsOff)
                    {
                        CheckAndUnSelectElementName(disableSetting);
                    }    

                    browser.FindElement(By.Name("_eventId_complete")).Click();
                    
                }
                catch(NoSuchElementException e)
                {
                    issuesList.Add(portal);
                }
                catch(Exception e)
                {
                    issuesList.Add(portal);
                }
            }

            return issuesList;



        }


        // if not already checked, check
        private static void CheckAndSelectElementName(string elementName)
        {
            try
            {
                var element = wait.Until(driver => driver.FindElement(By.Name(elementName)));

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

        private static void CheckAndUnSelectElementName(string elementName)
        {
            try
            {
                var element = wait.Until(driver => driver.FindElement(By.Name(elementName)));

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
