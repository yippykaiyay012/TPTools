using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class DynamicAttributes
    {
        public static void AddAttributes(List<string> attributeList, string txtPortalId)
        {
            var browser = WebBrowser.Driver;
            if (!browser.Url.Contains("/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list"))
            {
                browser.Url = @"https://www.trainingportal.no/mintra/" + txtPortalId + "/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list";
            }

            browser.FindElementByXPath("(//a[@name='Edit'])[2]").Click();

         //   progDynam.Value = 0;
         //   progDynam.Maximum = attributeList.Length;


            foreach (var attribute in attributeList)
            {
                var txtAttributeEntry = browser.FindElementById("dynamicAttributeValueOption");
                var btnAddToList = browser.FindElementByName("_eventId_addNewSelectableValueToDynamicAttribute");

                txtAttributeEntry.Clear();
                txtAttributeEntry.SendKeys(attribute.Trim());
                btnAddToList.Click();
                //Thread.Sleep(1000);

                //progDynam.Increment(1);
            }

        }
    }
}
