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
        private static string portalId = "663";

        private static WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(30));




        public static void CreateDemoPortal(string customerName, bool isUK, PortalType portalType)
        {

            // 1. create company
            if (!CreateCompany(customerName))
            {             
                return;
            }

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

            // 7. Add Demo Users
            AddDemoUsers(portalId, customerName);
        

        }



        private static bool CreateCompany(string customerName)
        {
            browser.Url = @"https://www.trainingportal.no/mintra/474/admin/companies";
            var createNewButton = wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
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

            if(browser.Url == createPageURL)
            {
                return false;
            }
            return true;
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

            var contractType = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='CONTRACT_TYPE']/tbody/tr/td[2]/input")));
            contractType.Click();
            contractType.SendKeys("DEMO");
            contractType.SendKeys(Keys.Tab);
            

            browser.FindElementByName("_eventId_complete").Click();
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
        private static void CheckAndSelectElementName(string elementName)
        {

            var element = wait.Until(driver => driver.FindElement(By.Name(elementName)));

            if (!element.Selected)
            {
                element.Click();
            }
        }
        private static void CheckAndSelectElementId(string elementId)
        {

            var element = wait.Until(driver => driver.FindElement(By.Id(elementId)));

            if (!element.Selected)
            {
                element.Click();
            }
        }

        private static void CheckAndUnSelectElementName(string elementName)
        {

            var element = wait.Until(driver => driver.FindElement(By.Name(elementName)));

            if (element.Selected)
            {
                element.Click();
            }
        }
        private static void CheckAndUnSelectElementId(string elementId)
        {

            var element = wait.Until(driver => driver.FindElement(By.Id(elementId)));

            if (element.Selected)
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
            if (portalId == null)
            {
                return;
            }
            else
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/users/organizationUnitAdmin/show";

                // add main units
                foreach (var unit in PortalSettings.orgUnits)
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

                Thread.Sleep(1000);

                // add sub units
                // Sub department
                var btnDepartment = wait.Until(driver => driver.FindElement(By.XPath("//span[@id='dijit__TreeNode_3_label']")));
                btnDepartment.Click();
                var btnCreateNewDep = wait.Until(driver => driver.FindElement(By.Id("orgUnitCreate")));
                btnCreateNewDep.Click();
                var txtTitleDep = wait.Until(driver => driver.FindElement(By.Name("OrganizationUnitDTO.name")));
                txtTitleDep.Click();
                txtTitleDep.SendKeys("Sub Department");
                browser.FindElementByName("_eventId_complete").Click();

                Thread.Sleep(1000);

                // Sub Location
                var btnLocation = wait.Until(driver => driver.FindElement(By.XPath("//span[@id='dijit__TreeNode_6_label']")));
                btnLocation.Click();
                var btnCreateNewLoc = wait.Until(driver => driver.FindElement(By.Id("orgUnitCreate")));
                btnCreateNewLoc.Click();
                var txtTitleLoc = wait.Until(driver => driver.FindElement(By.Name("OrganizationUnitDTO.name")));
                txtTitleLoc.Click();
                txtTitleLoc.SendKeys("Sub Location");
                browser.FindElementByName("_eventId_complete").Click();



            }
        }

        public static void AddDemoUsers(string portalId, string companyName)
        {

            // 15 users   ->  3 managers + 2 portal admin + 10 normal
            if (portalId == null)
            {
                return;
            }
            else
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/users";



                // user details
                string firstName;
                string lastName;
                string email;
                string username;
                string password = "Welcome1";
                string orgUnit;
                UserRole userRole;
                bool sendEmail;

               
                // add 3 managers
                for (int i = 1; i <= 3; i++)
                {
                    firstName = companyName;
                    lastName = "Manager" + i;
                    email = firstName + lastName + "@MintraDemo.com";
                    username = email;
                    orgUnit = PortalSettings.orgUnits[i-1];
                    userRole = UserRole.Manager;
                    sendEmail = false;
                    AddUser(portalId, firstName, lastName, email, username, password, orgUnit, userRole,sendEmail);
                }

                // add 2 portal Admins
                for (int i = 1; i <= 2; i++)
                {
                    firstName = companyName;
                    lastName = "PortalAdmin" + i;
                    email = firstName + lastName + "@MintraDemo.com";
                    username = email;
                    orgUnit = PortalSettings.orgUnits[i-2];
                    userRole = UserRole.Portal_Administrator;
                    sendEmail = false;
                    AddUser(portalId, firstName, lastName, email, username, password, orgUnit, userRole, sendEmail);
                }

                // add 10 Students
                for (int i = 1; i <= 10; i++)
                {
                    var rnd = new Random();

                    firstName = companyName;
                    lastName = "Student" + i;
                    email = firstName + lastName + "@MintraDemo.com";
                    username = email;
                    orgUnit = PortalSettings.orgUnits[rnd.Next(0, PortalSettings.orgUnits.Count)];
                    userRole = UserRole.Student;
                    sendEmail = false;
                    AddUser(portalId, firstName, lastName, email, username, password, orgUnit, userRole, sendEmail);
                }



            }
        }

        public static void AddUser(string portalId, string firstName, string lastName, 
            string email, string username, string password, string orgUnit, UserRole userRole, bool sendEmail)
        {
            if (portalId == null)
            {
                return;
            }
            else
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/users";
                var btnCreateNewUser = wait.Until(driver => driver.FindElement(By.Id("usersCreate")));
                btnCreateNewUser.Click();

                var txtFirstName = wait.Until(driver => driver.FindElement(By.Id("firstName")));
                txtFirstName.Click();
                txtFirstName.SendKeys(firstName);
                var txtLatName = wait.Until(driver => driver.FindElement(By.Id("lastName")));
                txtLatName.Click();
                txtLatName.SendKeys(lastName);

                var txtEmail = wait.Until(driver => driver.FindElement(By.Name("user.emailAddress")));
                txtEmail.Click();
                txtEmail.SendKeys(email);

                var txtUsername = wait.Until(driver => driver.FindElement(By.Name("user.username")));
                txtUsername.Click();
                txtUsername.SendKeys(username);

                var txtPassword = wait.Until(driver => driver.FindElement(By.Name("password")));
                txtPassword.Click();
                txtPassword.SendKeys(password);

                var btnShowOrgUnits = wait.Until(driver => driver.FindElement(By.Id("clickMeyay")));
                btnShowOrgUnits.Click();
                Thread.Sleep(1000);

                var btnExpandOrgUnits = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit__TreeNode_1']/div[1]/span[1]")));
                btnExpandOrgUnits.Click();
                Thread.Sleep(1000);


                var orgUnitChoice = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(@class,'dijitTreeLabel') and contains(text(), '" + orgUnit + "')]")));
                orgUnitChoice.Click();

                var userRoleChoice = wait.Until(driver => driver.FindElement(By.XPath("//table[@id='userCreateModel.roleLogicalId']//input[@class='dijitReset dijitInputField dijitArrowButtonInner']")));
                userRoleChoice.Click();
                userRoleChoice.SendKeys(userRole.ToString().Replace('_', ' '));
                userRoleChoice.SendKeys(Keys.Tab);

                if (sendEmail)
                {
                    CheckAndSelectElementId("userCreateSendSendLoginInfoOnMail");
                }
                else
                {
                    CheckAndUnSelectElementId("userCreateSendSendLoginInfoOnMail");
                }

                browser.FindElementByName("_eventId_complete").Click();

            }



        }


    }
}
