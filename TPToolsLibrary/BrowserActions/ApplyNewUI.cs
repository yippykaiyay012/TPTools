﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary.BrowserActions
{



    public class ApplyNewUI
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;

        private static string portalId = null;



        public static async Task<List<string>> Apply(List<string> portals, string primaryColour, string secondaryColour, string headerBGColour, string headerTextColour, bool enableStudentUI, bool enableAdminUI)
        {
            var issuesList = new List<string>();

            foreach(var portal in portals)
            {
                try
                {              
                    browser.Url = @"https://www.trainingportal.no/mintra/" + portal + "/admin/portals/show/portal/" + portal;

                    // so we can check if going to plan or on error page, if error, try next portal
                    // try catch does not catch this...
                    var editPortal = browser.FindElement(By.Id("editPortal"));
                    

                    if (editPortal == null)
                    {
                        continue;
                    }

                    editPortal.Click();

                   //wait.Until(driver => driver.FindElement(By.Id("editPortal"))).Click();

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


                    browser.FindElement(By.Name("_eventId_complete")).Click();
                    

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
                catch(NoSuchElementException e)
                {
                    issuesList.Add(portal);
                }
                catch(Exception e)
                {
                    issuesList.Add(portal);
                }
            }

            return issuesList;



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
