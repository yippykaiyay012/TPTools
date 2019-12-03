﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace TPToolsLibrary
{
    public class Login
    {
        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(30));

        public static bool LogIn(string username, string password)
        {
            try
            {
                if (!IsLoggedIn())
                {
                    WebBrowser.Driver.Url = @"https://www.trainingportal.no/en/oil-and-gas/home";


                    var txtUsername = wait.Until(driver => driver.FindElement(By.Id("username")));
                    txtUsername.Click();
                    txtUsername.SendKeys(username);

                    var txtPassword = wait.Until(driver => driver.FindElement(By.Id("passwd")));
                    txtPassword.Click();
                    txtPassword.SendKeys(password);

                    var btnSubmit = wait.Until(driver => driver.FindElement(By.Name("Submit")));
                    btnSubmit.Click();

                    if (!IsLoggedIn())
                    {
                        return false;
                       // MessageBox.Show("Login Unsuccessful");
                    }

                }
  
                // MessageBox.Show("Already Logged In m8");
                return true;
                
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                return false;
               // MessageBox.Show("Login Unsuccessful");
            }

            

        }

        public static bool IsLoggedIn()
        {
            if (browser.FindElementsByClassName("loggedInUser").Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
