using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class CREnrolmentToElearningCourseWithDirectLink : IEmailTemplate
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
            <p>For technical support, please contact Trainingportal support on the email below:</p>
            <p>&nbsp;</p>
            <table style=""font-size: 18px;"" border=""0"">
            <tbody>
            <tr>
            <td><span style=""font-size: 10pt;"">Email:</span></td>
            <td><span style=""font-size: 10pt;"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
            <td><span style=""font-size: 13.3333px;"">eLearning@controlrisks.com</span></td>
            </tr>
            </tbody>
            </table>
            <p>&nbsp;</p>
            <p>Kind regards,</p>
            <p>Control Risks</p>";

        public string TitleNo =>
            throw new NotImplementedException();

        public string SubjectNo =>
            throw new NotImplementedException();

        public string ContentNo =>
            throw new NotImplementedException();

    }
}
