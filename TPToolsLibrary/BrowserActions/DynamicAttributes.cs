using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class DynamicAttributes
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void AddAttributes(List<string> attributeList, string txtPortalId)
        {

            if (!browser.Url.Contains("/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list"))
            {
                browser.Url = @"https://www.trainingportal.no/mintra/" + txtPortalId + "/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list";
            }

            wait.Until(driver => driver.FindElement(By.XPath("(//a[@name='Edit'])[2]"))).Click();

         //   progDynam.Value = 0;
         //   progDynam.Maximum = attributeList.Length;


            foreach (var attribute in attributeList)
            {
                var txtAttributeEntry = wait.Until(driver => driver.FindElement(By.Id("dynamicAttributeValueOption")));
                var btnAddToList = wait.Until(driver => driver.FindElement(By.Name("_eventId_addNewSelectableValueToDynamicAttribute")));

                txtAttributeEntry.Clear();
                txtAttributeEntry.SendKeys(attribute.Trim());
                btnAddToList.Click();
                //Thread.Sleep(1000);

                //progDynam.Increment(1);
            }

        }
    }
}
