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
        private string SelfRegStatus => SelfReg ? "Enabled" : "Disabled";
        public User DefaultAdmin { get; set; }
        public string URL { get; set; }
        public List<string> Courses { get; set; }
        private string coursesString;
        public User Admin1 { get; set; }
        public User Admin2 { get; set; }



        public PortalResult(string companyName, bool selfReg, string url, User defaultAdmin, List<string> courses)
        {
            CompanyName = companyName;
            SelfReg = selfReg;
            DefaultAdmin = defaultAdmin;
            Courses = courses;
            //SelfRegStatus = SelfReg ? "Enabled" : "Disabled";
            URL = url;


            FormatSelectedCourses();
        }
        public PortalResult()
        {

        }

        private string AdminDetails()
        {
            var adminDetails = new StringBuilder().Append("");

            if(Admin1 != null || Admin2 != null)
            {
                adminDetails.AppendLine("Registered company administrator details are:");
            }
            if(Admin1 != null)
            {
                adminDetails.AppendLine($"\t\u2022" + $"{Admin1.Firstname} {Admin1.Lastname}");
                adminDetails.AppendLine($"Username:{Admin1.Username}");
                adminDetails.AppendLine($"Password:{Admin1.Password}");
                adminDetails.AppendLine("");
            }
            if (Admin2 != null)
            {
                adminDetails.AppendLine($"\t\u2022" + $"{Admin2.Firstname} {Admin2.Lastname}");
                adminDetails.AppendLine($"Username:{Admin2.Username}");
                adminDetails.AppendLine($"Password:{Admin2.Password}");
            }

            return adminDetails.ToString();

        }


        private string FormatSelectedCourses()
        {

            var courseFormat = new StringBuilder();

            foreach(var course in Courses)
            {
                var courseTitle = PortalSettings.ControlRisksCourses[course];
                courseFormat.AppendLine("\t\u2022" + courseTitle);
            }


            //coursesString =  courseFormat.ToString();
            return courseFormat.ToString();

        }

        public override string ToString()
        {
            return 
            $@"Hi,

The {CompanyName} Control Risks Trainingportal is now ready.

Courses available for assignment are:
{FormatSelectedCourses()}

Self Registration is {SelfRegStatus}

Default portal admin account details are:
Username: {DefaultAdmin.Username}
Password: {DefaultAdmin.Password}

{AdminDetails()}


The portal can be accessed at the following URL:
www.trainingportal.co.uk/mintra/p/{URL}

Kind regards,
Mintra Support";
        }

    }
}
