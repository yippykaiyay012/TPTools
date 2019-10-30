using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.BrowserActions
{
    public class AllowProgressionLog
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        public static void Set(List<string> courseCodeList, string portalId, bool desiredValue)
        {

            foreach (var course in courseCodeList)
            {
                browser.Url =
                    @"https://www.trainingportal.no/mintra/" + portalId + "/admin/courses/course/" + course + "/dashboard/about";

                var currentValue = browser.FindElement(By.XPath("//*[@id='allowAdminStatus']")).Text;

                if(currentValue == "No" && desiredValue == true)
                {
                    //wait.Until(driver => driver.FindElement(By.XPath("//*[@class='miniIconAnchor miniIconChangeStatus']"))).Click();
                    wait.Until(driver => driver.FindElement(By.XPath("//html//body//div[2]//div//main//div//div[1]//div[3]//div[2]//ul//li[4]//div[2]//a"))).Click();
                    //*[@id="courseBasicPropertiesEditMiniIcon"]



                }
                else if(currentValue == "Yes" && desiredValue == false)
                {
                    //wait.Until(driver => driver.FindElement(By.XPath("//*[@class='miniIconAnchor miniIconChangeStatus']"))).Click();
                    wait.Until(driver => driver.FindElement(By.XPath("//*[@class='miniIconAnchor miniIconChangeStatus']"))).Click();


                }

            }


        }



    }
}
