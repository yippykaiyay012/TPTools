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
    public class CourseCodes
    {

        private static ChromeDriver browser = WebBrowser.Driver;
        private static WebDriverWait wait = WebBrowser.wait;


        public static void AddCourseCodes(List<string> courseCodeList)
        {

            if (!browser.Url.Contains("/support/purchase/create"))
            {
                browser.Url = @"https://www.trainingportal.no/mintra/474/admin/ecommerce/support/customeraccounts";
                wait.Until(driver => driver.FindElement(By.Id("createPurchaseButton"))).Click();
                
            }



            var txtCourseCode = wait.Until(driver => driver.FindElement(By.Id("addCourseCodesInput")));
            var btnAddCourse = wait.Until(driver => driver.FindElement(By.Id("addCourseCodesButton")));


            //   progCourseCodes.Value = 0;
            //   progCourseCodes.Maximum = courseCodeList.Length;


            foreach (var code in courseCodeList)
            {
                txtCourseCode.Clear();
                txtCourseCode.SendKeys(code.Trim());
                btnAddCourse.Click();

               // progCourseCodes.Increment(1);
            }
        }
    }
}
