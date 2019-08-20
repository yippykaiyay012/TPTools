using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class RepetitionRequirementCourseTemplate : IEmailTemplate
    {
        public string TitleEn =>
            "Repetition requirement course";

        public string SubjectEn =>
            "Repetition requirement for the course #%course.name%# in Trainingportal";

        public string ContentEn =>
            @"<p>Hi #%user.firstname%#,</p>
            <p><br />The course #%course.name%# is valid until #%completion.validuntildate%#. Please contact your manager to arrange repetition training.</p>";

        public string TitleNo =>
            "Repetition requirement course";

        public string SubjectNo =>
            "Repetisjonskrav for kurset #%course.name%# på Trainingportal";

        public string ContentNo =>
            @"<p>Hei #%user.firstname%#,</p>
            <p><br />Kurset #%course.name%# er gyldig til #%completion.validuntildate%#. Ta kontakt med din n&aelig;rmeste leder for &aring; arrangere repetisjonstrening.</p>
            <p>&nbsp;</p>";
    }
}
