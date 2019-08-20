using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class CompetenceRequirementsTemplate : IEmailTemplate
    {
        public string TitleEn =>
            "Competence requirements must be completed";

        public string SubjectEn =>
            "Competence requirements in #%competence.name%# must be completed";

        public string ContentEn =>
            @"<p>Hi #%user.wholename%#,<br /><br /></p>
            <p>Competence requirements in #%competence.name%# must be completed.</p>
            <p><br />To get information about how to complete and achieve the competence, log on to #%portal.url%# with your username #%user.username%#.</p>
            <p><br />Do you not know your password? Click on ""Forgotten username and/or password"" to receive a new password.</p>
            <p>&nbsp;</p>
            <p>You can find your competence requirements in ""My Training"" and ""My Competence"".</p>
            <p>&nbsp;</p>
            <p>Kind regards,</p>
            <p>Mintra Group</p>";

        public string TitleNo =>
            "Competence requirements must be completed";

        public string SubjectNo =>
            "Kompetanse må fullføres i #%competence.name%#";

        public string ContentNo =>
            @"<p>Hei, #%user.wholename%#,</p>
            <p>&nbsp;</p>
            <p>Kompetanse m&aring; fullf&oslash;res i #%competence.name%#.</p>
            <p>&nbsp;</p>
            <p>For informasjon om hvordan du kan gjennomf&oslash;re og oppn&aring; kompetansen, logg deg p&aring; #%portal.url%# med ditt brukernavn #%user.username%#.</p>
            <p>&nbsp;</p>
            <p>Kjenner du ikke ditt passord? Klikk p&aring; ""Glemt brukernavn og/eller passord"" s&aring; f&aring;r du tilsendt et nytt.</p>
            <p>&nbsp;</p>
            <p>Du kan lese om kompetansekravene som stilles til deg ved &aring; g&aring; til ""Min oppl&aelig;ring"" og ""Min Kompetanse"".</p>
            <p>&nbsp;</p>
            <p>Med vennlig hilsen,</p>
            <p>Mintra Group</p>";
    }
}
