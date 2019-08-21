using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPToolsLibrary.BrowserActions;
using TPToolsLibrary.SettingsAndTemplates;
using TPToolsLibrary.SettingsAndTemplates.CertificateTemplates;

namespace TPToolsLibrary
{
    public class PortalAdministration
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static string portalId = "662";

        private static WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(30));




        public static void CreateDemoPortal(string customerName, bool isUK, PortalType portalType)
        {

            // 1. create company
                  CreateCompany(customerName);


            // 2. create portal
                   CreatePortal(customerName, isUK);


            // 3. Customize Portal
                    Thread.Sleep(10000); // need to wait for indexing to complete before searching
                    CustomizePortal(customerName, portalType);


            // 4. Templates
                  EmailTemplates(portalType);


            // 5. Certificate Template
                   AddCertificateTemplate(portalId, new StandardCertificate());


            // 6. Org units
            CreateDemoOrgUnits(portalId);

        

        }



        private static void CreateCompany(string customerName)
        {
            browser.Url = @"https://www.trainingportal.no/mintra/474/admin/companies";
            var createNewButton = wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
            createNewButton.Click();
            var txtCompanyName = wait.Until(driver => driver.FindElement(By.Name("companyName")));
            txtCompanyName.SendKeys(customerName);
            var txtCostCenterName = wait.Until(driver => driver.FindElement(By.Name("costCenterName")));
            txtCostCenterName.SendKeys("xxx");
            var txtCustomerName = wait.Until(driver => driver.FindElement(By.Name("customerName")));
            txtCustomerName.SendKeys(customerName);
            var txtCustomerNumber = wait.Until(driver => driver.FindElement(By.Name("customerNumber")));
            txtCustomerNumber.SendKeys("xxx");
            browser.FindElementByName("_eventId_complete").Click();
        }

        private static void CreatePortal(string customerName, bool isUK)
        {

            browser.Url = @"https://www.trainingportal.no/mintra/474/admin/portals";
            var btnCreatePortal = wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
            btnCreatePortal.Click();

            var portalName = customerName + " Demo Trainingportal";
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
            txtIndustry.SendKeys("DEMO");
            txtIndustry.SendKeys(Keys.Tab);

            browser.FindElementByName("_eventId_complete").Click();

        }


        private static void CustomizePortal(string customerName, PortalType portalType)
        {
            var portalName = customerName + " Demo Trainingportal";
            browser.Url = "https://www.trainingportal.no/mintra/474/admin/portals?maxResults=20&page=1&criteria%5Bquery%5D.value=" + portalName;
            browser.FindElementByLinkText(portalName).Click();

            portalId = browser.Url.Substring(browser.Url.Length - 3);

            var btnEditPortal = wait.Until(driver => driver.FindElement(By.Id("editPortal")));
            btnEditPortal.Click();


            if (portalType == PortalType.Basic)
            {
                foreach (var element in PortalSettings.basicPortalSettings)
                {
                    CheckAndSelect(element);
                }

            }
            else if (portalType == PortalType.Advanced)
            {
                foreach (var element in PortalSettings.advancedPortalSettings)
                {
                    CheckAndSelect(element);
                }
            }

            //     browser.FindElementByName("_eventId_complete").Click();
        }


        private static void EmailTemplates(PortalType portalType)
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
        private static void CheckAndSelect(string elementName)
        {

            var element = wait.Until(driver => driver.FindElement(By.Name(elementName)));

            if (!element.Selected)
            {
                element.Click();
            }
        }

        private static void AddEmailTemplate(string portalId, IEmailTemplate template)
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



        public static void AddCertificateTemplate(string portalId, ICertificateTemplate template)
        {

            if (portalId == null)
            {
                return;
            }
            else
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

        }



        private static void CreateDemoOrgUnits(string portalId)
        {
            var orgUnits = new List<string>()
            {
                "Location 1", "Location 2", "Department 1", "Department 2"
            };

            if (portalId == null)
            {
                return;
            }
            else
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/users/organizationUnitAdmin/show";

                // add main units
                foreach (var unit in orgUnits)
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





            }
        }


    }
}
