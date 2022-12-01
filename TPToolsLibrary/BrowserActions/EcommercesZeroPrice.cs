using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{
    public class EcommercesZeroPrice
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static async Task AddZeroPriceEcommerce(List<string> courseCodes, string portalId, string codePrefix)
        {

            foreach (var course in courseCodes)
            {

                browser.Url = $"https://www.trainingportal.no/mintra/{portalId}/admin/courses/course/{course}";


                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dashboardMenuAdd']/button"))).Click();


                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_RadioButton_0']"))).Click();

                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='continue']/button"))).Click();


                var txtCode = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='courseCode']")));
                var txtPrice = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='ecommerceDefaultSettingsModel.priceDTO_amountInputField']")));
                var txtCreditValue = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='dijit_form_TextBox_0']")));

                txtCode.Clear();
                txtCode.SendKeys($"{codePrefix}-{course}");

                txtPrice.Clear();
                txtPrice.SendKeys("0");

                txtCreditValue.Clear();
                txtCreditValue.SendKeys("0");

                Thread.Sleep(1000);


                browser.FindElement(By.Name("_eventId_complete")).Click();
            }
        }

    }
}