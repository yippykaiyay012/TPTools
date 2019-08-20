using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public interface ITemplate
    {
        string TitleEn { get; }
        string SubjectEn { get; }
        string ContentEn { get; }

        string TitleNo { get; }
        string SubjectNo { get; }
        string ContentNo { get; }


    }
}
