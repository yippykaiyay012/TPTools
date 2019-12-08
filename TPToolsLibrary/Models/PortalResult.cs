using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TPToolsLibrary.BrowserActions;

namespace TPToolsLibrary.Models
{
    public class PortalResult
    {
        public string CompanyName { get; set; }
        public bool SelfReg { get; set; }
        private string SelfRegStatus;
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string URL { get; set; }
        public List<string> Courses { get; set; }
        private string coursesString;


        public PortalResult(string companyName, bool selfReg, string adminEmail, string adminPassword, List<string> courses)
        {
            CompanyName = companyName;
            SelfReg = selfReg;
            AdminEmail = adminEmail;
            AdminPassword = adminPassword;
            Courses = courses;
            SelfRegStatus = SelfReg ? "Enabled" : "Disabled";

            FormatSelectedCourses();
        }


        public void FormatSelectedCourses()
        {

            var courseFormat = new StringBuilder();

            foreach(var course in Courses)
            {
                var courseTitle = PortalSettings.ControlRisksCourses[course];
                courseFormat.AppendLine("\t\u2022" + courseTitle);
            }


            coursesString =  courseFormat.ToString();

        }

        public override string ToString()
        {
            return 
            $@"Hi,

The {CompanyName} Control Risks Trainingportal is now ready.

Courses available for assignment are:
{coursesString}

Self Registration is {SelfRegStatus}

Default portal admin account details are:
Username : {AdminEmail}
Password : {AdminPassword}


The portal can be accessed at the following URL:
www.trainingportal.co.uk/mintra/p/{CompanyName}

Kind regards,
Mintra Support";
        }

    }
}
