﻿using OpenQA.Selenium;
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
    public class DemoPortal
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalId = null;
        private static string portalName = null;
        private static string portalURL = null;


        public static void CreateDemoPortal(string customerName, bool isUK, PortalType portalType, bool addDemoUsers)
        {

            // 1. create company
            if (!CreateCompany(customerName))
            {
                return;
            }

            // 2. create portal
            if (!CreatePortal(customerName, isUK))
            {
                return;
            }
            Thread.Sleep(10000); // need to wait for indexing to complete before searching
                                    // can get portalId after this point to use instead for URLs
            portalId = GetDemoPortalId(customerName);


            // 3. Customize Portal          
            CustomizePortal(portalId, portalType);


            // 4. Templates
            EmailTemplates(portalId, portalType);


            // 5. Certificate Template
            AddCertificateTemplate(portalId, new StandardCertificate());


            // 6. Org units
            if (CreateDemoOrgUnits(portalId))
            {
                // 7. Add Demo Users - dont add if org units failed
                if (addDemoUsers)
                {
                    AddDemoUsers(portalId, customerName);
                }
            }

            // 8. Register portal in Sheet
            GSheets.CreateEntry(portalName, portalId, portalURL, DateTime.UtcNow.ToString());


                  
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
                txtIndustry.SendKeys("DEMO");
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

        private static string GetDemoPortalId(string customerName)
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


        private static void CustomizePortal(string portalId, PortalType portalType)
        {
            try
            {        
                if(portalId != null)
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
                    contractTypeChoice.SendKeys("DEMO");
                    contractTypeChoice.SendKeys(Keys.Tab);


                    browser.FindElement(By.Name("_eventId_complete")).Click();
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

                browser.FindElement(By.Name("_eventId_complete")).Click();

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

                    browser.FindElement(By.Name("_eventId_complete")).Click();

                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }



            }

        }



        private static bool CreateDemoOrgUnits(string portalId)
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
                    foreach (var unit in PortalSettings.orgUnits)
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

                            browser.FindElement(By.Name("_eventId_complete")).Click();
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(e.ToString());
                            mainUnitSuccess = false;
                        }
                        
                    }

                    // only add sub units if main units added successfully
                    if (mainUnitSuccess)
                    {
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
                        browser.FindElement(By.Name("_eventId_complete")).Click();

                        Thread.Sleep(1000);

                        // Sub Location
                        var btnLocation = wait.Until(driver => driver.FindElement(By.XPath("//span[@id='dijit__TreeNode_6_label']")));
                        btnLocation.Click();
                        var btnCreateNewLoc = wait.Until(driver => driver.FindElement(By.Id("orgUnitCreate")));
                        btnCreateNewLoc.Click();
                        var txtTitleLoc = wait.Until(driver => driver.FindElement(By.Name("OrganizationUnitDTO.name")));
                        txtTitleLoc.Click();
                        txtTitleLoc.SendKeys("Sub Location");
                        browser.FindElement(By.Name("_eventId_complete")).Click();

                        return true;
                    }
                    return false;

                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                    return false;
                }             

            }
        }

        private static void AddDemoUsers(string portalId, string companyName)
        {

            // 15 users   ->  3 managers + 2 portal admin + 10 normal
            if (portalId == null)
            {
                return;
            }
            else
            {
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
                    AddUser(portalId, firstName, lastName, email.Replace(" ", ""), username.Replace(" ", ""), password, orgUnit, userRole, sendEmail);
                }

                // add 2 portal Admins
                for (int i = 1; i <= 2; i++)
                {
                    firstName = companyName;
                    lastName = "PortalAdmin" + i;
                    email = firstName + lastName + "@MintraDemo.com";
                    username = email;
                    orgUnit = PortalSettings.orgUnits[i];
                    userRole = UserRole.Portal_Administrator;
                    sendEmail = false;
                    AddUser(portalId, firstName, lastName, email.Replace(" ", ""), username.Replace(" ", ""), password, orgUnit, userRole, sendEmail);
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
                    AddUser(portalId, firstName, lastName, email.Replace(" ", ""), username.Replace(" ", ""), password, orgUnit, userRole, sendEmail);
                }



            }
        }

        private static void AddUser(string portalId, string firstName, string lastName, 
            string email, string username, string password, string orgUnit, UserRole userRole, bool sendEmail)
        {
            if (portalId == null)
            {
                return;
            }
            else
            {
                try
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

                    browser.FindElement(By.Name("_eventId_complete")).Click();
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());                    
                }


            }



        }


    }
}
