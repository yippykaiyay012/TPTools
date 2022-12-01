using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TPToolsLibrary;
using TPToolsLibrary.BrowserActions;
using TPToolsLibrary.SettingsAndTemplates;

namespace TPToolsLibrary
{
    public class CoHostPortal
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalName = null;
        private static string portalId = null;
        private static string portalURL = null;


        //  this is duplicated evrywhere, need to get all this into NewPortal and reference from demo  and here
        public static void CreateCoHostPortal(string parentCompanyName , string parentCompanyPortalId,  string childCompanyName , bool shareCourses)
        {
            portalName = childCompanyName + " - " + parentCompanyName + " CoHost";

            // 1. create customer
            if (!CreateCompany(portalName))
            {
                return;
            }
            // 2. create portal
            if (!CreatePortal(portalName, true))
            {
                return;
            }
            Thread.Sleep(10000); // need to wait for indexing to complete before searching
                                 // can get portalId after this point to use instead for URLs
            portalId = GetPortalId(portalName);


            //EnableConnectorFileDownloads(portalId);


            if (shareCourses)
            {
                ShareCourses(parentCompanyPortalId, portalName);
            }

            EnableConnectorFileDownloads(portalId);

        }


        private static bool CreateCompany(string customerName)
        {
            try
            {
                browser.Url = @"https://www.trainingportal.no/mintra/474/admin/companies";
                var createNewButton = WebBrowser.wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
                createNewButton.Click();

                var createPageURL = browser.Url;

                var txtCompanyName = wait.Until(driver => driver.FindElement(By.Name("companyName")));
                txtCompanyName.SendKeys(customerName);
                var txtCostCenterName = wait.Until(driver => driver.FindElement(By.Name("costCenterName")));
                txtCostCenterName.SendKeys("xxx");
                var txtCustomerName = wait.Until(driver => driver.FindElement(By.Name("customerName")));
                txtCustomerName.SendKeys(customerName);
                var txtCustomerNumber = wait.Until(driver => driver.FindElement(By.Name("customerNumber")));
                txtCustomerNumber.SendKeys("xxx");
                browser.FindElement(By.Name("_eventId_complete")).Click();

                if (browser.Url == createPageURL)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return false;
            }

        }

        private static bool CreatePortal(string customerName, bool isUK)
        {
            try
            {
                browser.Url = @"https://www.trainingportal.no/mintra/474/admin/portals";
                var btnCreatePortal = wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
                btnCreatePortal.Click();

                var createPageURL = browser.Url;

               // portalName = customerName + " Trainingportal";
                var txtPortalName = wait.Until(driver => driver.FindElement(By.Name("portal.name")));
                txtPortalName.SendKeys(portalName);

                var txtLogicalId = wait.Until(driver => driver.FindElement(By.Name("portal.logicalId")));
                txtLogicalId.SendKeys(portalName.Replace(" ", ""));

                if (isUK)
                {
                    var txtURL = wait.Until(driver => driver.FindElement(By.Name("portal.url")));
                    txtURL.SendKeys("www.trainingportal.co.uk/mintra/p/" + portalName.Replace(" ", ""));

                    var txtContactCompany = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input")));
                    txtContactCompany.Click();
                    txtContactCompany.SendKeys("Mintra Trainingportal Ltd");
                    Thread.Sleep(1000);
                    txtContactCompany.SendKeys(Keys.Tab);

                    var txtServerDomain = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input")));
                    txtServerDomain.Click();
                    txtServerDomain.SendKeys("https://www.trainingportal.co.uk");
                    Thread.Sleep(1000);
                    txtServerDomain.SendKeys(Keys.Tab);
                }
                else
                {
                    var txtURL = wait.Until(driver => driver.FindElement(By.Name("portal.url")));
                    txtURL.SendKeys("www.trainingportal.no/mintra/p/" + portalName.Replace(" ", ""));

                    var txtContactCompany = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input")));
                    txtContactCompany.Click();
                    txtContactCompany.SendKeys("Mintra Trainingportal AS");
                    Thread.Sleep(1000);
                    txtContactCompany.SendKeys(Keys.Tab);

                    var txtServerDomain = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input")));
                    txtServerDomain.Click();
                    txtServerDomain.SendKeys("https://www.trainingportal.no");
                    Thread.Sleep(1000);
                    txtServerDomain.SendKeys(Keys.Tab);

                }

                var txtPortalOwner = wait.Until(driver => driver.FindElement(By.Name("ownerCompany")));
                txtPortalOwner.Click();
                txtPortalOwner.SendKeys(customerName);
                Thread.Sleep(1000);
                txtPortalOwner.SendKeys(Keys.Tab);

                var txtIndustry = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='model.portal.industry']/tbody/tr/td[2]/input")));
                txtIndustry.Click();
                txtIndustry.SendKeys("-");
                txtIndustry.SendKeys(Keys.Tab);

                browser.FindElement(By.Name("_eventId_complete")).Click();

                if (browser.Url == createPageURL)
                {
                    return false;
                }
                return true;

            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return false;
            }



        }


        private static string GetPortalId(string customerName)
        {
            try
            {
                //var portalName = customerName + " Trainingportal";
                browser.Url = "https://www.trainingportal.no/mintra/474/admin/portals?maxResults=20&page=1&criteria%5Bquery%5D.value=" + portalName;

                wait.Until(driver => driver.FindElement(By.LinkText(portalName))).Click();

                portalId = browser.Url.Substring(browser.Url.LastIndexOf('/') + 1);

                // check if valid number but still return as string to be used elsewhere.
                if (int.TryParse(portalId, out int x))
                {
                    portalURL = "https://www.trainingportal.no/mintra/" + portalId + "/admin/portals/show/portal/" + portalId;
                    return portalId;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return null;
            }

        }



        private static void EnableConnectorFileDownloads(string portalId)
        {

            if (portalId != null)
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/portals/show/portal/" + portalId;

                var btnEditPortal = wait.Until(driver => driver.FindElement(By.Id("editPortal")));
                btnEditPortal.Click();

                foreach (var element in CoHostSharingSettings.CoHostPortalSettings)
                {
                    CheckAndSelectElementName(element);
                }


                browser.FindElement(By.Name("_eventId_complete")).Click();
            }

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
        private static void CheckAndSelectElementId(string elementId)
        {
            try
            {
                var element = wait.Until(driver => driver.FindElement(By.Id(elementId)));

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











        private static void ShareCourses(string parentPortalId, string childPortalName)
        {
            if(parentPortalId == "655")
            {
                foreach (var course in CoHostSharingSettings.ControlRisksCourseIds)
                {
                    browser.Url = "https://www.trainingportal.no/mintra/" + parentPortalId + "/admin/courses/course/" + course + "/dashboard/coursesharing/list";

                    var btnNewShare = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='section']/div/div[1]/div/div/span/button")));
                    btnNewShare.Click();

                    var rdioPortalShare = wait.Until(driver => driver.FindElement(By.Id("portalRadioButton")));
                    rdioPortalShare.Click();

                    var txtPortalName = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portal']")));
                    txtPortalName.Click();

                    txtPortalName.SendKeys(childPortalName);
                    Thread.Sleep(1000);
                    txtPortalName.SendKeys(Keys.Tab);

                    browser.FindElement(By.Name("_eventId_complete")).Click();

                }


            }
           }





    }

}
