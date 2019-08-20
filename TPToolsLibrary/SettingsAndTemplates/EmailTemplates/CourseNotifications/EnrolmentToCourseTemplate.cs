using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class EnrolmentToCourseTemplate : IEmailTemplate
    {
        public string TitleEn => 
            "Enrolment to e-learning course";

        public string SubjectEn => 
            "Enrolment to the course #%course.name%# at #%portal.name%#";

        public string ContentEn =>
            @"<p>Hi #%user.wholename%#,</p>
            <p> &nbsp;</p>
            <p>You are now enrolled to the course #%course.name%#.</p>
            <p> Log on to #%portal.url%# with your username #%user.username%# to complete the course.</p>
            <p>Do you not know your password? Click on ""Forgotten username and/or password"" to receive a new password.</p>
            <p>&nbsp;</p>
            <p>After login, you can start the course from My Training.</p>
            <p>Good luck!</p>
            <p>&nbsp;</p>
            <p>Kind regards,</p>
            <p>Mintra Group</p>";


        public string TitleNo => 
            "Enrolment to e-learning course";

        public string SubjectNo => 
            "Påmelding til kurset #%course.name%# på #%portal.name%#";

        public string ContentNo =>
            @"<p>Hei #%user.wholename%#,</p>
            <p> &nbsp;</p>
            <p>Du er n&aring; p&aring;meldt kurset #%course.name%#.</p>
            <p>Logg inn p&aring; #%portal.url%# med ditt brukernavn #%user.username%# for &aring; gjennomf&oslash;re kurset.</p>
            <p>Kjenner du ikke ditt passord? Klikk p&aring; ""Glemt brukernavn og/eller passord"" s&aring; f&aring;r du tilsendt et nytt.</p>
            <p>&nbsp;</p>
            <p>Kurset finner du under Min oppl&aelig;ring.</p>
            <p>&nbsp;</p>
            <p>Lykke til!</p>
            <p>&nbsp;</p>
            <p>Vennlig hilsen,</p>
            <p>Mintra Group</p>";
    }

}
