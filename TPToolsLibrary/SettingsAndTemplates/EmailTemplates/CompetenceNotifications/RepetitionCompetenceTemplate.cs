using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class RepetitionCompetenceTemplate : IEmailTemplate
    {
        public string TitleEn =>
            "Repetition requirement for competence";

        public string SubjectEn =>
            "Repetition requirement for #%competence.name%# at #%portal.name%#";

        public string ContentEn =>
            @"<p>Hi #%user.firstname%#,<br /><br /></p>
            <p>Your competence in&nbsp;#%competence.name%#&nbsp;is valid until #%completion.validuntildate%#.</p>
            <p>&nbsp;</p>
            <p>Please contact your manager to arrange repetition training.</p>
            <p>&nbsp;</p>
            <p>Kind regards,</p>
            <p>Mintra Group</p>";

        public string TitleNo =>
            "Repetition requirement for competence";

        public string SubjectNo =>
            "Repetisjonskrav for #%competence.name%# på #%portal.name%#";

        public string ContentNo =>
            @"<p>Hei #%user.firstname%#,<br /><br /><br />Din kompetanse i&nbsp;#%competence.name%# er gyldig til #%completion.validuntildate%#.</p>
            <p><br />Ta kontakt med din n&aelig;rmeste leder for &aring; arrangere repetisjonstrening.</p>
            <p>&nbsp;</p>
            <p>Med vennlig hilsen,</p>
            <p>Mintra Group</p>";
    }
}
