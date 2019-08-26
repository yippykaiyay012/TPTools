using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TPToolsLibrary.SettingsAndTemplates;
using TPToolsLibrary.SettingsAndTemplates.CertificateTemplates;

namespace TPToolsLibrary.BrowserActions
{
    public class NewPortal
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalId = null;
        private static string portalName = null;
        private static string portalURL = null;


        public static void CreateNewPortal(string existingCompanyName, string customerName, bool isUK, string industry, PortalType portalType, string contractType, List<string> orgUnitList)
        {

            // 1. create company
            if (string.IsNullOrEmpty(existingCompanyName))
            {
                if (!CreateCompany(customerName))
                {
                    return;
                }
            }
           

            // 2. create portal
            if (!CreatePortal(customerName, isUK, industry))
            {
                return;
            }
            Thread.Sleep(10000); // need to wait for indexing to complete before searching
            // can get portalId after this point to use instead for URLs
            portalId = GetPortalId(customerName);


            // 3. Customize Portal          
            CustomizePortal(portalId, portalType, contractType);


            // 4. Templates
            EmailTemplates(portalId, portalType);


            // 5. Certificate Template
            AddCertificateTemplate(portalId, new StandardCertificate());


            // 6. Org units
            if ((orgUnitList != null) && (!orgUnitList.Any()))
            {
                CreateOrgUnits(portalId, orgUnitList);
            }           


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
                browser.FindElementByName("_eventId_complete").Click();

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

        private static bool CreatePortal(string customerName, bool isUK, string industry)
        {
            try
            {
                browser.Url = @"https://www.trainingportal.no/mintra/474/admin/portals";
                var btnCreatePortal = wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
                btnCreatePortal.Click();

                var createPageURL = browser.Url;

                portalName = customerName + " Trainingportal";
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
                txtIndustry.SendKeys(industry);
                txtIndustry.SendKeys(Keys.Tab);

                browser.FindElementByName("_eventId_complete").Click();

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
                var portalName = customerName + " Trainingportal";
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


        private static void CustomizePortal(string portalId, PortalType portalType, string contractType)
        {
            try
            {
                if (portalId != null)
                {
                    browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/portals/show/portal/" + portalId;

                    var btnEditPortal = wait.Until(driver => driver.FindElement(By.Id("editPortal")));
                    btnEditPortal.Click();


                    if (portalType == PortalType.Basic)
                    {
                        foreach (var element in PortalSettings.basicPortalSettings)
                        {
                            CheckAndSelectElementName(element);
                        }

                    }
                    else if (portalType == PortalType.Advanced)
                    {
                        foreach (var element in PortalSettings.advancedPortalSettings)
                        {
                            CheckAndSelectElementName(element);
                        }
                    }

                    var contractTypeChoice = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='CONTRACT_TYPE']/tbody/tr/td[2]/input")));
                    contractTypeChoice.Click();
                    contractTypeChoice.SendKeys(contractType);
                    contractTypeChoice.SendKeys(Keys.Tab);


                    browser.FindElementByName("_eventId_complete").Click();
                }


            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }

        }


        private static void EmailTemplates(string portalId, PortalType portalType)
        {
            if (portalId == null)
            {
                return;
            }
            else
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

                if (portalType == PortalType.Basic)
                {

                    foreach (var template in PortalSettings.basicEmailTemplates)
                    {
                        AddEmailTemplate(portalId, template);
                    }
                }
                else if (portalType == PortalType.Advanced)
                {
                    foreach (var template in PortalSettings.advancedEmailTemplates)
                    {
                        AddEmailTemplate(portalId, template);
                    }
                }


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

        private static void CheckAndUnSelectElementId(string elementId)
        {
            try
            {
                var element = wait.Until(driver => driver.FindElement(By.Id(elementId)));

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

        private static void AddEmailTemplate(string portalId, IEmailTemplate template)
        {
            try
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

                var btnNewEmail = wait.Until(driver => driver.FindElement(By.Id("libraryNewMail")));
                btnNewEmail.Click();
                var txtTitle = wait.Until(driver => driver.FindElement(By.Name("contentDocument.title")));
                txtTitle.SendKeys(template.TitleEn);
                var txtSubject = wait.Until(driver => driver.FindElement(By.Name("contentDocument.localeSubjectProperties[en]")));
                txtSubject.SendKeys(template.SubjectEn);
                var btnTools = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Tools')]")));
                btnTools.Click();
                Thread.Sleep(500);
                var btnSourceCode = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Source code')]")));
                btnSourceCode.Click();
                var txtSourceCode = wait.Until(driver => driver.FindElement(By.XPath("//textarea[@class='mce-textbox mce-multiline mce-abs-layout-item mce-first mce-last']")));
                txtSourceCode.Click();
                txtSourceCode.SendKeys(template.ContentEn);

                var btnOK = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Ok')]")));
                btnOK.Click();

                browser.FindElementByName("_eventId_complete").Click();

            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }

        }



        private static void AddCertificateTemplate(string portalId, ICertificateTemplate template)
        {

            if (portalId == null)
            {
                return;
            }
            else
            {
                try
                {
                    browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

                    var btnNewCert = wait.Until(driver => driver.FindElement(By.Id("libraryNewCourseCertificate")));
                    btnNewCert.Click();

                    var txtTitle = wait.Until(driver => driver.FindElement(By.Name("contentDocument.title")));
                    txtTitle.SendKeys(template.TitleEn);

                    var btnTools = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Tools')]")));
                    btnTools.Click();
                    Thread.Sleep(500);
                    var btnSourceCode = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Source code')]")));
                    btnSourceCode.Click();
                    var txtSourceCode = wait.Until(driver => driver.FindElement(By.XPath("//textarea[@class='mce-textbox mce-multiline mce-abs-layout-item mce-first mce-last']")));
                    txtSourceCode.Click();
                    txtSourceCode.SendKeys(template.ContentEn);

                    var btnOK = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Ok')]")));
                    btnOK.Click();

                    browser.FindElementByName("_eventId_complete").Click();

                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }



            }

        }



        private static bool CreateOrgUnits(string portalId, List<string> orgUnitList)
        {
            if (portalId == null)
            {
                return false;
            }
            else
            {
                try
                {
                    browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/users/organizationUnitAdmin/show";


                    bool mainUnitSuccess = true;
                    // add main units
                    foreach (var unit in orgUnitList)
                    {
                        try
                        {
                            Thread.Sleep(1000);
                            var rootOrg = wait.Until(driver => driver.FindElement(By.XPath("//span[@id='dijit__TreeNode_1_label']")));
                            rootOrg.Click();

                            var btnCreateNew = wait.Until(driver => driver.FindElement(By.Id("orgUnitCreate")));
                            btnCreateNew.Click();

                            var txtTitle = wait.Until(driver => driver.FindElement(By.Name("OrganizationUnitDTO.name")));
                            txtTitle.Click();
                            txtTitle.SendKeys(unit);

                            browser.FindElementByName("_eventId_complete").Click();
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(e.ToString());
                            mainUnitSuccess = false;
                        }

                    }

                    return true;

                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                    return false;
                }

            }
        }

        
    }
}
