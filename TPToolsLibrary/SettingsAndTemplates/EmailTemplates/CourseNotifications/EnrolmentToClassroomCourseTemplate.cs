using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class EnrolmentToClassroomCourseTemplate : IEmailTemplate
    {
        public string TitleEn =>
            "Enrolment to classroom course";

        public string SubjectEn =>
            "Enrolment to the classroom course #%course.name%# in Trainingportal";

        public string ContentEn =>
            @"<p>Hi #%user.wholename%#,</p>
            <p> &nbsp;</p>
            <p>You are now enroled to the course #%course.name%#.</p>
            <p> Start: #%courseclass.startdate%#</p>
            <p>End: #%courseclass.enddate%#</p>
            <p>Place: #%courseclass.location%#</p>
            <p>Address: #%courseclass.address%#</p>
            <p>&nbsp;</p>
            <p>Good luck!</p>
            <p>&nbsp;</p>
            <p>Kind&nbsp;regards,</p>
            <p>Mintra Group</p>";

        public string TitleNo =>
            "Enrolment to classroom course";

        public string SubjectNo =>
            "Påmelding til klasseromskurset #%course.name%# på Trainingportal";

        public string ContentNo =>
            @"<p>Hei #%user.wholename%#,</p>
            <p>&nbsp;</p>
            <p>Du er n&aring; p&aring;meldt kurset #%course.name%#.</p>
            <p>Start: #%courseclass.startdate%#</p>
            <p>Slutt: #%courseclass.enddate%#</p>
            <p>Sted: #%courseclass.location%#</p>
            <p>Adresse: #%courseclass.address%#</p>
            <p>&nbsp;</p>
            <p>Lykke til!</p>
            <p>&nbsp;</p>
            <p>Vennlig hilsen,</p>
            <p>Mintra Group</p>";
    }
}
