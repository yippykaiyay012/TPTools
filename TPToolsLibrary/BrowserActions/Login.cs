using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class Login
    {
        public static void LogIn(string username, string password)
        {
            if (!IsLoggedIn()) {
                WebBrowser.Driver.Url = @"https://www.trainingportal.no/no/olje-og-gass/hjem";
            }
            

            //var tries = 0;
            //while (!IsLoggedIn() && tries <= 3)
            //{
            //    WebBrowser.Driver.Url = @"https://www.trainingportal.no/no/olje-og-gass/hjem";

            //    try
            //    {
            //        var txtUsername = WebBrowser.Driver.FindElementById("username");
            //        var txtPassword = WebBrowser.Driver.FindElementById("passwd");
            //        var btnSubmit = WebBrowser.Driver.FindElementByName("Submit");

            //        txtUsername.SendKeys(username);
            //      //  Thread.Sleep(500);
            //        txtPassword.SendKeys(password);
            //      //  Thread.Sleep(500);

            //        btnSubmit.Click();
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show("Log In Unsuccessful");
            //    }

            //    tries++;
            //}

            //if (tries == 3)
            //{
            //    MessageBox.Show("Log In Unsuccessful");
            //}

        }

        public static bool IsLoggedIn()
        {
            if (WebBrowser.Driver.FindElementsByClassName("loggedInUser").Count == 0)
            {
                //MessageBox.Show("Log In Unsuccessful");
                return false;
            }

            return true;
        }
    }
}
