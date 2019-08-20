using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates
{
    public interface ICertificateTemplate
    {
        string TitleEn { get; }
       
        string ContentEn { get; }

        string TitleNo { get; }
 
        string ContentNo { get; }
    }
}
