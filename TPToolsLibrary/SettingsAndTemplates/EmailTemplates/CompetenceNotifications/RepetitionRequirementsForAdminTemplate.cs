using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class RepetitionRequirementsForAdminTemplate : IEmailTemplate
    {
        public string TitleEn =>
            "Repetition requirement for competence for an administrator";

        public string SubjectEn =>
            "#%competence.name%# will expire for #%user.wholename%#";

        public string ContentEn =>
            @"<p>#%competence.name%# for #%user.wholename%# is valid until #%completion.validuntildate%#.<br /><br />Please arrange for repetition training.</p>";

        public string TitleNo =>
            "Repetition requirement for competence for an administrator";

        public string SubjectNo =>
            "#%competence.name%# utløper for #%user.wholename%#";

        public string ContentNo =>
            @"<p>#%competence.name%# for #%user.wholename%# er gyldig til #%completion.validuntildate%#.<br /><br />Vennligst arranger repetisjonstrening.</p>";
    }
}
