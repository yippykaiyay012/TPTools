using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class EnrolmentToElearningCourseWithDirectLink : IEmailTemplate
    {
        public string TitleEn =>
            "Enrolment to e-learning course with direct link";

        public string SubjectEn =>
            "Enrolment to the course #%course.name%# at #%portal.name%#";

        public string ContentEn =>
            @"<p>Hi #%user.firstname%#,</p>
            <p>&nbsp;</p>
            <p>You have been enrolled to&nbsp;#%course.name%# on Trainingportal.</p>
            <p>To complete the course log on to #%portal.url%#&nbsp;with your username #%user.username%#. If you do not know your password click on ""Forgotten username/password"" to receive a new password. You will find the course under My Training.</p>
            <p>You may also go directly to the course by clicking this link: #%course.directurl%#.</p>
            <p>&nbsp;</p>
            <p>When the course is completed you can print your course certificate under ""My Training""/""Completed courses"".&nbsp;</p>
            <p>For technical support please contact Trainingportal support on the email below:</p>
            <p>&nbsp;</p>

            <table style = ""font-size: 18px;"" border=""0"">
            <tbody>
            <tr>
            <td><span style = ""font-size: 10pt;"" > Email:</span></td>
            <td><span style = ""font-size: 10pt;"" > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
            <td><span style = ""font-size: 10pt;"" >< a href=""mailto:support@mintragroup.com"">support @mintragroup.com</a></span></td>
            </tr>
            </tbody>

            </table>


            <p>&nbsp;</p>
            <p>Kind regards,</p>
            <p>Mintra Group</p>";

        public string TitleNo =>
            "Enrolment to e-learning course with direct link";

        public string SubjectNo =>
            "Påmelding til kurset #%course.name%# på #%portal.name%#";

        public string ContentNo =>
            @"<p>Hei #%user.firstname%#,</p>
            <p>Du er p&aring;meldt kurset #%course.name%# p&aring; Trainingportal.</p>
            <p>For &aring; f&aring; tilgang til kurset logger du deg p&aring; #%portal.url%# med ditt brukernavn #%user.username%#. Dersom du ikke kjenner ditt passord kan du klikke p&aring; ""Glemt passord"" og f&aring; tilsendt et nytt. Kurset finner du under ""Min oppl&aelig;ring"". </p>

            <p>Du kan ogs&aring; g&aring; direkte til kurset ved &aring; klikke p&aring; denne linken: #%course.directurl%#&nbsp;.&nbsp;</p>
            <p>N&aring;r kurset er fullf&oslash;rt kan du skrive ut kursbevis under ""Min oppl&aelig;ring""/""Fullf&oslash;rte kurs"". &nbsp;</p>
            <p>For teknisk st&oslash;tte kan Trainingportal Support kontaktes p&aring; e-posten nedenfor:</p>
            <p>&nbsp;</p>

            <table style=""font-size: 18px;"" border=""0"">

            <tbody>
            <tr>
            <td><span style=""font-size: 10pt;"">Epost:</span></td>
            <td><span style=""font-size: 10pt;"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
            <td><span style=""font-size: 10pt;""><a href=""mailto:support@mintragroup.com"">support@mintragroup.com</a></span></td>
            </tr>
            </tbody>
            </table>
            <p>&nbsp;</p>
            <p>Vennlig hilsen,</p>
            <p>Mintra Group</p>";

    }
}
