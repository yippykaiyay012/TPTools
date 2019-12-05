using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TPToolsLibrary.Models;
using TPToolsLibrary.SettingsAndTemplates;
using TPToolsLibrary.SettingsAndTemplates.CertificateTemplates;
using TPToolsLibrary.SettingsAndTemplates.Documents.ControlRisks;

namespace TPToolsLibrary.BrowserActions
{
    public class ControlRisksPortal
    {


        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalId = null;
        private static string portalName = null;
        private static string portalURL = null;


        public static void CreateCRPortal(string customerName, bool selfReg, List<string> coursesToShare)
        {

            //customerName += " - Control Risks";

            // 1. create company
            if (!CreateCompany(customerName + " - Control Risks"))
            {
                return;
            }

            // 2. create portal
            if (!CreatePortal(customerName))
            {
                return;
            }
            Thread.Sleep(10000); // need to wait for indexing to complete before searching
            //                     // can get portalId after this point to use instead for URLs
            portalId = GetPortalId(customerName);



            // 3. Add Template
            AddDocumentTemplate(portalId, new CRLoginDocument(customerName));

            // 4. Settings
            CustomizePortal(portalId, selfReg);

            // 5. Add Sample Learner Admin
            var portalAdmin = new User()
            {
                Firstname = "Learner",
                Lastname = customerName,
                Email = $"Learner.{customerName}@ControlRisks.com",
                Username = $"Learner.{customerName}@ControlRisks.com",
                Password = "Welcome123!",
                OrgUnit = "",
                UserRole = UserRole.Portal_Administrator,
                SendEmail = false

            };

            AddUser(portalAdmin);

            //// 4. Templates
            ////      EmailTemplates(portalId);

            //// 5. Certificate Template
            ////     AddCertificateTemplate(portalId, new ControlRisksCertificate());


            //// 6 . Share Courses
            if (!ShareCourses("655", portalName, coursesToShare))
            {
                return;
            }

            // 7. activate Courses
            ActivateCourses(portalId);


            // to do
            //  Add eLearning@controlrisks.com  as an admin to the portal? from the main CR portal
            // add notifications to courses.

         
            
            //  AddNotifications();





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
                txtCostCenterName.SendKeys(customerName);
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

        private static bool CreatePortal(string customerName)
        {
            try
            {
                browser.Url = @"https://www.trainingportal.no/mintra/474/admin/portals";
                var btnCreatePortal = wait.Until(driver => driver.FindElement(By.Id("generalCreate")));
                btnCreatePortal.Click();

                var createPageURL = browser.Url;

                portalName = customerName + " - Control Risks Trainingportal";
                var txtPortalName = wait.Until(driver => driver.FindElement(By.Name("portal.name")));
                txtPortalName.SendKeys(portalName);

                var txtLogicalId = wait.Until(driver => driver.FindElement(By.Name("portal.logicalId")));
                txtLogicalId.SendKeys(customerName);




                var txtURL = wait.Until(driver => driver.FindElement(By.Name("portal.url")));
                txtURL.SendKeys("www.trainingportal.co.uk/mintra/p/" + customerName);

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




                var txtPortalOwner = wait.Until(driver => driver.FindElement(By.Name("ownerCompany")));
                txtPortalOwner.Click();
                txtPortalOwner.SendKeys(customerName + " - Control Risks");
                Thread.Sleep(1000);
                txtPortalOwner.SendKeys(Keys.Tab);

                var txtIndustry = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='model.portal.industry']/tbody/tr/td[2]/input")));
                txtIndustry.Click();
                txtIndustry.SendKeys("-");
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
                var portalName = customerName + " - Control Risks Trainingportal";
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


        private static void AddDocument(string portalId)
        {
            try
            {
                if (portalId != null)
                {
                    if(!browser.Url.Contains(portalId + "/admin/portals/portal/edit?"))
                    {
                        browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/portals/show/portal/" + portalId;

                        var btnEditPortal = wait.Until(driver => driver.FindElement(By.Id("editPortal")));
                        btnEditPortal.Click();
                    }
                   

                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='loginPageDocumentBtn']/button/span"))).Click();

                    Thread.Sleep(1000);

                    //*[@id="contentList"]/tbody/tr/td[1]/a
                    var loginDoc = wait.Until(driver => driver.FindElement(By.XPath("//a[contains(text(),'Login Document')]")));
                    loginDoc.Click();

                    //browser.FindElementByName("_eventId_complete").Click();

                }
            }
            catch(Exception e)
            {
                Logger.LogError(e.ToString());
            }
        }


        private static void CustomizePortal(string portalId, bool selfReg)
        {
            try
            {
                if (portalId != null)
                {
                    browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/portals/show/portal/" + portalId;

                    var btnEditPortal = wait.Until(driver => driver.FindElement(By.Id("editPortal")));
                    btnEditPortal.Click();

                    AddDocument(portalId);

                    //untick all
                    foreach (var element in PortalSettings.AllPortalSettings)
                    {
                        CheckAndUnSelectElementName(element);
                    }


                    // tick required
                    foreach (var element in PortalSettings.basicPortalSettings)
                    {
                        CheckAndSelectElementName(element);
                    }

                    if (selfReg)
                    {
                        CheckAndSelectElementName("portalBooleanProperties[ALLOW_SELF_REGISTRATION]");
                    }

                    


                    var contractTypeChoice = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='CONTRACT_TYPE']/tbody/tr/td[2]/input")));
                    contractTypeChoice.Click();
                    contractTypeChoice.SendKeys("PER USER");
                    contractTypeChoice.SendKeys(Keys.Tab);


                    browser.FindElementByName("_eventId_complete").Click();
                }


            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }

        }


        private static void EmailTemplates(string portalId)
        {
            if (portalId == null)
            {
                return;
            }
            else
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

                foreach (var template in PortalSettings.controlRisksEmailTemplates)
                {
                    AddEmailTemplate(portalId, template);
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

        private static void AddDocumentTemplate(string portalId, CRLoginDocument template)
        {
            try
            {
                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

                var btnNewDocument = wait.Until(driver => driver.FindElement(By.Id("libraryNewDocument")));
                btnNewDocument.Click();
                var txtTitle = wait.Until(driver => driver.FindElement(By.Name("contentDocument.title")));
                txtTitle.SendKeys(template.Title);
    
                var btnTools = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Tools')]")));
                btnTools.Click();
                Thread.Sleep(500);
                var btnSourceCode = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(text(),'Source code')]")));
                btnSourceCode.Click();
                var txtSourceCode = wait.Until(driver => driver.FindElement(By.XPath("//textarea[@class='mce-textbox mce-multiline mce-abs-layout-item mce-first mce-last']")));
                txtSourceCode.Click();
                txtSourceCode.SendKeys(template.Contents);

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


        private static void AddUser(User user)
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
                    txtFirstName.SendKeys(user.Firstname);
                    var txtLatName = wait.Until(driver => driver.FindElement(By.Id("lastName")));
                    txtLatName.Click();
                    txtLatName.SendKeys(user.Lastname);

                    var txtEmail = wait.Until(driver => driver.FindElement(By.Name("user.emailAddress")));
                    txtEmail.Click();
                    txtEmail.SendKeys(user.Email);

                    var txtUsername = wait.Until(driver => driver.FindElement(By.Name("user.username")));
                    txtUsername.Click();
                    txtUsername.SendKeys(user.Username);

                    var txtPassword = wait.Until(driver => driver.FindElement(By.Name("password")));
                    txtPassword.Click();
                    txtPassword.SendKeys(user.Password);

                    var btnShowOrgUnits = wait.Until(driver => driver.FindElement(By.Id("clickMeyay")));
                    btnShowOrgUnits.Click();
                    Thread.Sleep(1000);

                    var btnExpandOrgUnits = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit__TreeNode_1']/div[1]/span[1]")));
                    btnExpandOrgUnits.Click();
                    Thread.Sleep(1000);


                    //var orgUnitChoice = wait.Until(driver => driver.FindElement(By.XPath("//span[contains(@class,'dijitTreeLabel') and contains(text(), '" + user.OrgUnit + "')]")));
                    //orgUnitChoice.Click();

                    var userRoleChoice = wait.Until(driver => driver.FindElement(By.XPath("//table[@id='userCreateModel.roleLogicalId']//input[@class='dijitReset dijitInputField dijitArrowButtonInner']")));
                    userRoleChoice.Click();
                    userRoleChoice.SendKeys(user.UserRole.ToString().Replace('_', ' '));
                    userRoleChoice.SendKeys(Keys.Tab);

                    if (user.SendEmail)
                    {
                        CheckAndSelectElementId("userCreateSendSendLoginInfoOnMail");
                    }
                    else
                    {
                        CheckAndUnSelectElementId("userCreateSendSendLoginInfoOnMail");
                    }

                    browser.FindElementByName("_eventId_complete").Click();
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                }


            }

        }

        private static bool ShareCourses(string parentPortalId, string childPortalName, List<string> coursesToShare)
        {
            if (parentPortalId == "655")
            {
                foreach (var course in coursesToShare)
                {
                    try
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

                        browser.FindElementByName("_eventId_complete").Click();

                    }
                    catch (Exception e)
                    {
                        Logger.LogError(e.ToString());
                        return false;
                    }


                }

            }

            return true;
        }

        private static bool ActivateCourses(string portalId)
        {

            try
            {


                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/support/home";


                var actAsPortalAdmin = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='actAsPortalAdminButton']/button")));
                actAsPortalAdmin.Click();

                browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/all";




                //var activateButtons = wait.Until(driver => driver.FindElements(By.XPath("//span[text()='Activate']")));

                while (wait.Until(driver => driver.FindElements(By.XPath("//span[text()='Activate']"))).Count > 0)
                {
                    var activateButtons = wait.Until(driver => driver.FindElements(By.XPath("//span[text()='Activate']")));
                    activateButtons[0].Click();

                    var checkBox = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='acceptcheckbox']")));
                    checkBox.Click();

                    browser.FindElementByName("_eventId_complete").Click();
                    browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/all";
                }

            }
            catch(Exception e)
            {
                return false;
            }
            return true;

           
        }



        private static void AddNotifications(string portalId, string courseId)
        {
        
            browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + courseId + "/dashboard/notification/list";


        }


    }




}
