using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace TPToolsLibrary.BrowserActions
{
    public class PriceSettingComponent
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public enum Currency{
            NOK,
            EUR,
            USD,
            GBP,
            DKK
        }

        public static async Task AddComponent(List<string> courseCodes, string portalId, string price, string portalName, Currency currency)
        {

            foreach(var course in courseCodes)
            {

                browser.Url = $"https://www.trainingportal.no/mintra/{portalId}/admin/courses/course/{course}/dashboard/ecommerce/list";

                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='addNewpriceSettingButton']/button/span"))).Click();

                var txtAttributeEntry = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='ecommercePriceSettingModel.priceDTO_amountInputField']")));
                txtAttributeEntry.Clear();
                txtAttributeEntry.SendKeys(price);

                
                var currencyField = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='priceDTO.currency']")));
                currencyField.Click();
                currencyField.SendKeys(currency.ToString());
                currencyField.SendKeys(Keys.Tab);



                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portalRadioButton']"))).Click();
                var portalNameTxt = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='portal']")));
                portalNameTxt.SendKeys(portalName);
                Thread.Sleep(1000);
                portalNameTxt.SendKeys(Keys.Tab);



                //browser.FindElementByName("_eventId_complete").Click();
                browser.FindElement(By.Name("_eventId_complete"));
            }
        }

    }
}

