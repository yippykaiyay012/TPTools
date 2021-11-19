using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPToolsLibrary.Models;

namespace TPToolsLibrary
{
    public class DynamicAttributes
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static async Task AddAttributes(List<string> attributeList, string txtPortalId, IProgress<ProgressWrapper> progress, CancellationToken cancellation)
        {
            var progressWrapper = new ProgressWrapper
            {
                Current = 0,
                Total = attributeList.Count
            };
            
            try
            {

                bool isAddingExisting = false;

                if (!browser.Url.Contains("admin/portals/portal/dynamicattribute/edit?"))
                {
                    if (!browser.Url.Contains("/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list"))
                    {
                        browser.Url = @"https://www.trainingportal.no/mintra/" + txtPortalId + "/admin/portals/portal/" + txtPortalId + "/dynamicattribute/list";
                    }

                    wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dynamicAttrListaddNewBtn']/button"))).Click();
                }
                else
                {
                    isAddingExisting = true;
                }
                

                //   progDynam.Value = 0;
                //   progDynam.Maximum = attributeList.Length;


                foreach (var attribute in attributeList)
                {
                    if (cancellation.IsCancellationRequested)
                    {

                        cancellation.ThrowIfCancellationRequested();
                    }
                    try
                    {
                        IWebElement txtAttributeEntry;
                        IWebElement btnAddToList;
                        if (isAddingExisting)
                        {
                            txtAttributeEntry = wait.Until(driver => driver.FindElement(By.Id("dynamicAttributeValueOption")));
                            btnAddToList = wait.Until(driver => driver.FindElement(By.Name("_eventId_addNewSelectableValueToDynamicAttribute")));
                        }
                        else
                        {
                            txtAttributeEntry = wait.Until(driver => driver.FindElement(By.Id("newDynamicAttributeValueOption")));
                            btnAddToList = wait.Until(driver => driver.FindElement(By.Name("_eventId_addNewSelectableValueToNewDynamicAttribute")));
                        }


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

                    progressWrapper.Current++;
                    progress.Report(progressWrapper);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }



        }
    }
}
