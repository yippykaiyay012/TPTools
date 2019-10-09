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
            try
            {
                if (!browser.Url.Contains("/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list"))
                {
                    browser.Url = @"https://www.trainingportal.no/mintra/" + txtPortalId + "/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list";
                }

                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dynamicAttrListaddNewBtn']/button"))).Click();

                //   progDynam.Value = 0;
                //   progDynam.Maximum = attributeList.Length;


                foreach (var attribute in attributeList)
                {
                    try
                    {
                        var txtAttributeEntry = wait.Until(driver => driver.FindElement(By.Id("newDynamicAttributeValueOption")));
                        var btnAddToList = wait.Until(driver => driver.FindElement(By.Name("_eventId_addNewSelectableValueToNewDynamicAttribute")));

                        txtAttributeEntry.Clear();
                        txtAttributeEntry.SendKeys(attribute.Trim());
                        btnAddToList.Click();
                        //Thread.Sleep(1000);

                        //progDynam.Increment(1);
                    }
                    catch (Exception e)
                    {
                        Logger.LogError(e.ToString());
                    }


                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }



        }
    }
}
