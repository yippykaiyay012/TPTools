using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.BrowserActions
{



    public class ApplyNewUI
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalId = null;


        public static void Apply(List<string> portals, string primaryColour, string secondaryColour, string headerBGColour, string headerTextColour, bool enableStudentUI, bool enableAdminUI)
        {

            foreach(var portal in portals)
            {
                browser.Url = @"https://www.trainingportal.no/mintra/" + portal + "/admin/portals/show/portal/" + portal;

                wait.Until(driver => driver.FindElement(By.Id("editPortal"))).Click();
                

                if (enableAdminUI)
                {
                    CheckAndSelectElementName("portalBooleanProperties[USE_NEW_ADMIN_UI]");
                }
                else
                {
                    CheckAndUnSelectElementName("portalBooleanProperties[USE_NEW_ADMIN_UI]");
                }



                if (enableStudentUI)
                {
                    CheckAndSelectElementName("portalBooleanProperties[USE_NEW_STUDENT_UI]");
                }
                else
                {
                    CheckAndUnSelectElementName("portalBooleanProperties[USE_NEW_STUDENT_UI]");
                }


                browser.FindElementByName("_eventId_complete").Click();

                browser.Url = @"https://www.trainingportal.no/mintra/" + portal + "/admin/portals/show/portal/" + portal;

                wait.Until(driver => driver.FindElement(By.XPath("//*[@id='infoContainer']/div/a[2]"))).Click();

                wait.Until(driver => driver.FindElement(By.Name("primaryColor"))).Clear();
                wait.Until(driver => driver.FindElement(By.Name("primaryColor"))).SendKeys(primaryColour);
                wait.Until(driver => driver.FindElement(By.Name("secondaryColor"))).Clear();
                wait.Until(driver => driver.FindElement(By.Name("secondaryColor"))).SendKeys(secondaryColour);
                wait.Until(driver => driver.FindElement(By.Name("headerBackgroundColor"))).Clear();
                wait.Until(driver => driver.FindElement(By.Name("headerBackgroundColor"))).SendKeys(headerBGColour);
                wait.Until(driver => driver.FindElement(By.Name("headerTextColor"))).Clear();
                wait.Until(driver => driver.FindElement(By.Name("headerTextColor"))).SendKeys(headerTextColour);

                wait.Until(driver => driver.FindElement(By.Id("portalNewUIThemeShow__save_button"))).Click();
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

    }
}
