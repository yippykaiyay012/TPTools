using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class EnrolmentToBlendedCourseTemplate : IEmailTemplate
    {
        public string TitleEn => 
            "Enrolment to blended course";

        public string SubjectEn => 
            "Enrolment to blended learning course #%course.name%# in Trainingportal";

        public string ContentEn =>
            @"<p>Hi #%user.wholename%#,</p>

            <p> You are now enroled to the course #%course.name%#.</p>
            <p>This course consist of an e-learning module and a class room session.</p>
            <p>Please log on to #%portal.url%# with your username #%user.username%# to complete the e-learning module.</p>
            <p>&nbsp;</p>
            <p><strong>Class room session:</strong>&nbsp;</p>
            <p>Start: #%courseclass.startdate%#</p>
            <p>End: #%courseclass.enddate%#</p>
            <p>Place: #%courseclass.location%#</p>
            <p>Address: #%courseclass.address%#</p>
            <p>&nbsp;</p>
            <p>Good luck!</p>
            <p>&nbsp;</p>
            <p>Kind regards,</p>
            <p>Mintra Group</p>";

        public string TitleNo =>
            "Enrolment to blended course";

        public string SubjectNo =>
            "Påmelding til kombinert kurs #%course.name%# på Trainingportal";

        public string ContentNo =>
            @"<p>Hei #%user.wholename%#,</p>

            <p>Du er n&aring; p&aring;meldt kurset#%course.name%#.</p>
            <p>Dette kurset best&aring;r av en e-l&aelig;ringsmodul og en klasseroms sesjon.</p>
            <p>Logg inn p&aring; #%portal.url%# med ditt brukernavn #%user.username%# for &aring; fullf&oslash;re e-l&aelig;ringsmodulen.</p>
            <p>&nbsp;</p>
            <p><strong>Klasseromssesjon:</strong></p>
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
