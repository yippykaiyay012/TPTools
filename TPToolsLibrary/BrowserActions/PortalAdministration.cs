using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPToolsLibrary.BrowserActions;
using TPToolsLibrary.SettingsAndTemplates;

namespace TPToolsLibrary
{
    public enum PortalType
    {
        Basic,
        Standard,
        Advanced
    }

    public class PortalAdministration
    {

        public static void CreateFullPortal(string companyName, string portalName, string logicalId, string url)
        {


        }


        public static void CreateDemoPortal(string customerName, bool isUK , PortalType portalType)
        {
            var browser = WebBrowser.Driver;

            // 1. create company
            browser.Url = @"https://www.trainingportal.no/mintra/474/admin/companies";
            browser.FindElementById("generalCreate").Click();
            Thread.Sleep(1000);
            browser.FindElementByXPath("//*[@id='name']").SendKeys(customerName);
            browser.FindElementByXPath("//*[@id='costCenterName']").SendKeys("xxx");
            browser.FindElementByXPath("//*[@id='CrmInfocustomerName']").SendKeys(customerName);
            browser.FindElementByXPath("//*[@id='CrmInfocustomerNumber']").SendKeys("xxx");
            browser.FindElementByName("_eventId_complete").Click();



            // 2. create portal
            browser.Url = @"https://www.trainingportal.no/mintra/474/admin/portals";
            browser.FindElementById("generalCreate").Click();
            var portalName = customerName + " Demo Trainingportal";
            browser.FindElementById("name").SendKeys(portalName);
            browser.FindElementById("logicalid").SendKeys(portalName.Replace(" ", ""));

            if(isUK)
            {
                browser.FindElementById("url").SendKeys("www.trainingportal.co.uk/mintra/p/" + portalName.Replace(" ", ""));

                browser.FindElementByXPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input").Click();
                browser.FindElementByXPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input").SendKeys("Mintra Trainingportal Ltd");
                browser.FindElementByXPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input").SendKeys(Keys.Tab);

                browser.FindElementByXPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input").Click();
                browser.FindElementByXPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input").SendKeys("https://www.trainingportal.co.uk");
                browser.FindElementByXPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input").SendKeys(Keys.Tab);
            }
            else
            {
                browser.FindElementById("url").SendKeys("www.trainingportal.no/mintra/p/" + portalName.Replace(" ", ""));

                browser.FindElementByXPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input").Click();
                browser.FindElementByXPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input").SendKeys("Mintra Trainingportal AS");
                browser.FindElementByXPath("//*[@id='dijit_form_Select_0']/tbody/tr/td[2]/input").SendKeys(Keys.Tab);

                browser.FindElementByXPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input").Click();
                browser.FindElementByXPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input").SendKeys("https://www.trainingportal.no");
                browser.FindElementByXPath("//*[@id='dijit_form_Select_1']/tbody/tr/td[2]/input").SendKeys(Keys.Tab);
            }


            browser.FindElementByXPath("//*[@id='companySelect']").SendKeys(customerName);
            Thread.Sleep(2000);
            browser.FindElementByXPath("//*[@id='companySelect']").SendKeys(Keys.Tab);

            browser.FindElementByXPath("//*[@id='model.portal.industry']/tbody/tr/td[2]/input").Click();
            browser.FindElementByXPath("//*[@id='model.portal.industry']/tbody/tr/td[2]/input").SendKeys("DEMO");
            browser.FindElementByXPath("//*[@id='model.portal.industry']/tbody/tr/td[2]/input").SendKeys(Keys.Tab);


            browser.FindElementByXPath("//*[@id='registerBtn']/button").Click();



            // 3. Customize Portal
            browser.Url = "https://www.trainingportal.no/mintra/474/admin/portals?maxResults=20&page=1&criteria%5Bquery%5D.value=" + portalName;
            browser.FindElementByLinkText(portalName).Click();

            var portalId = browser.Url.Substring(browser.Url.Length - 3);

            browser.FindElementByXPath("//*[@id='editPortal']").Click();


            if (portalType == PortalType.Basic)
            {
                foreach(var element in PortalSettings.basicPortalSettings)
                {
                    CheckAndSelect(element);
                }

            }
            else if(portalType == PortalType.Standard)
            {
                foreach (var element in PortalSettings.standardPortalSettings)
                {
                    CheckAndSelect(element);
                }
            }
            else if(portalType == PortalType.Advanced)
            {
                foreach (var element in PortalSettings.advancedPortalSettings)
                {
                    CheckAndSelect(element);
                }
            }



            // 4. Templates
            browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

            if (portalType == PortalType.Basic)
            {
                    
                foreach(var template in PortalSettings.basicEmailTemplates)
                {
                    AddTemplate(portalId, template);
                }
            }
            else if(portalType == PortalType.Standard)
            {
                foreach (var template in PortalSettings.standardEmailTemplates)
                {
                    AddTemplate(portalId, template);
                }
            }
            else if(portalType == PortalType.Advanced)
            {
                foreach (var template in PortalSettings.advancedEmailTemplates)
                {
                    AddTemplate(portalId, template);
                }
            }

        }


        // if not already checked, check
        public static void CheckAndSelect(string elementName)
        {
            var browser = WebBrowser.Driver;
            var element = browser.FindElementByName(elementName);

            if (!element.Selected)
            {
                element.Click();
            }
        }

        public static void AddTemplate(string portalId, IEmailTemplate template)
        {
            var browser = WebBrowser.Driver;

            browser.Url = "https://www.trainingportal.no/mintra/" + portalId + "/admin/content";

            browser.FindElementByXPath("//*[@id='libraryNewMail']").Click();
            browser.FindElementByXPath("//*[@id='contentDocumentFormTitle']").SendKeys(template.TitleEn);
            browser.FindElementByXPath("//*[@id='subject_en']").SendKeys(template.SubjectEn);
            browser.FindElementByXPath("//*[@id='mceu_28 - open']").Click();
            browser.FindElementByXPath("//*[@id='mceu_47 - text']").Click();
            browser.FindElementByXPath("//*[@id='mceu_50']").SendKeys(template.ContentEn);
            browser.FindElementByXPath("//*[@id='mceu_52']/button").Click();
            browser.FindElementByName("_eventId_complete").Click();
        }

        

    }
}
