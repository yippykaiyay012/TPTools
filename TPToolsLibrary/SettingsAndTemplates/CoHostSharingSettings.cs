using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public static class CoHostSharingSettings
    {


        //lol


        public static readonly Dictionary<string, string> ClientDetails = new Dictionary<string, string>()
        {
            { "Control Risks","655" }
        };


        public static readonly List<string> CoHostPortalSettings = new List<string>()
        {
            "portalBooleanProperties[ALLOW_AICC_ACCESS_TO_COURSES]",
            "portalBooleanProperties[COURSE_ALLOW_SCORM_CLOUD]"
        };



        public static readonly List<string> ControlRisksCourseIds = new List<string>()
        {
            "32039",
            "32612",
            "32614",
            "32038",
            "33371",
            "34845"
        };


    }
}
