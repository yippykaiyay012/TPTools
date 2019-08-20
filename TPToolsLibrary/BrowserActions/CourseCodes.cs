using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPToolsLibrary
{
    public class CourseCodes
    {




        public static void AddCourseCodes(List<string> courseCodeList)
        {
            var browser = WebBrowser.Driver;
            //Login.LogIn();

            if (!browser.Url.Contains("/support/purchase/create"))
            {
                browser.Url = @"https://www.trainingportal.no/mintra/474/admin/ecommerce/support/customeraccounts";
                browser.FindElementById("createPurchaseButton").Click();
                
            }



            var txtCourseCode = browser.FindElementById("addCourseCodesInput");
            var btnAddCourse = browser.FindElementById("addCourseCodesButton");


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
