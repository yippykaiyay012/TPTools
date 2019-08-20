using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public class CourseCompletionWithCertTemplate : IEmailTemplate
    {
        public string TitleEn => 
            "Course completion with Certificate";

        public string SubjectEn =>
            "Course completion #%course.name%# at #%portal.name%#";

        public string ContentEn =>
            @"<p>#%user.wholename%# has today, #%completion.completeddate%#, completed the course #%course.name%#.</p>
            <p>Course certificated is attached.</p>
            <p>#%attach:coursecertificate%#</p>";

        public string TitleNo =>
            "Course completion with Certificate";

        public string SubjectNo => 
            "Kursfullføring #%course.name%# på #%portal.name%#";

        public string ContentNo =>
            @"<p>#%user.wholename%# har i dag, #%completion.completeddate%#, fullf&oslash;rt kurset #%course.name%#.</p>
            <p> Kursbevis er vedlagt.</p>
            <p>#%attach:coursecertificate%#</p>";
    }
}
