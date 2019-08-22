using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TPToolsLibrary
{
    public static class WebBrowser
    {
        public static ChromeDriver Driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
    }
}
