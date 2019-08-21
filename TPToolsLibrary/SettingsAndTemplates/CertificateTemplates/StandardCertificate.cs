using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates.CertificateTemplates
{
    public class StandardCertificate : ICertificateTemplate
    {
        public string TitleEn =>
            "Trainingportal standard certificate";

        public string ContentEn =>
            @"<table style=""margin: 0 0 0 30px; width: 640px; height: 910px; background-image: url('getImage?img=BG_Logo.jpg');"">
            <tbody>
            <tr>
            <td>
            <p align = ""center""><span style = ""color: #177b98; font-size: xx-large;""><strong><span style=""font-family: arial, helvetica, sans-serif;"">CERTIFICATE OF COMPLETION</span></strong></span></p>
            <p align = ""center""><span style = ""font-family: arial, helvetica, sans-serif; color: #595a5c;"">This is to certify that</span></p>
            <p align = ""center""><span style = ""color: #177b98; font-size: xx-large;""><strong><span style=""font-family: arial, helvetica, sans-serif;"">#%user.firstname%# #%user.lastname%#</span></strong></span></p>
            <p align = ""center""><span style = ""font-family: arial, helvetica, sans-serif;""><span style = ""color: #595a5c;"">Born:</span> <span style=""color: #595a5c;"">#%user.birthdate%#</span></span></p>
            <br />
            <p align = ""center""><span style = ""font-family: arial, helvetica, sans-serif; color: #595a5c;"">has completed the e-Learning course</span></p>
            <p align = ""center""><span style = ""font-size: small;"">&nbsp;</span></p>
            <p align = ""center""><span style = ""font-size: x-large; border: 0px solid; margin: 60px; color: #177b98; font-family: arial, helvetica, sans-serif;""><strong>#%course.name%#</strong></span></p>
            <br />
            <p align = ""center""><span style = ""font-family: arial, helvetica, sans-serif;""><span style = ""color: #595a5c;"">Completed:</span> <span style=""color: #177b98;""><strong>#%completion.completeddate%#</strong></span></span></p>
            <p style = ""text-align: center;"" align=""center""><span style = ""font-size: small;""><span style = ""font-size: x-small; font-family: arial, helvetica, sans-serif; color: #595a5c;"">Unique certificate number: #%certificate.number%#</span><br /></span></p>
            </td>
            </tr>
            </tbody>
            </table>";

        public string TitleNo =>
            "Trainingportal standard certificate";

        public string ContentNo =>
            @"<table style=""margin: 0 0 0 30px; width: 640px; height: 910px; background-image: url('getImage?img=BG_Logo.jpg');"">
            <tbody>
            <tr>
            <td>
            <p align=""center""><span style=""color: #177b98; font-size: xx-large;""><strong><span style=""font-family: arial, helvetica, sans-serif;"">KURSBEVIS</span></strong></span></p>
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif; color: #595a5c;"">Herved bekreftes det at</span></p>
            <p align=""center""><span style=""color: #177b98; font-size: xx-large;""><strong><span style=""font-family: arial, helvetica, sans-serif;"">#%user.firstname%# #%user.lastname%#</span></strong></span></p>
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif;""><span style=""color: #595a5c;"">F&oslash;dt:</span> <span style=""color: #595a5c;"">#%user.birthdate%#</span></span></p>
            <br />
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif; color: #595a5c;"">har fullf&oslash;rt e-l&aelig;ringskurset</span></p>
            <p align=""center""><span style=""font-size: small;"">&nbsp;</span></p>
            <p align=""center""><span style=""font-size: x-large; border: 0px solid; margin: 60px; color: #177b98; font-family: arial, helvetica, sans-serif;""><strong>#%course.name%#</strong></span></p>
            <br />
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif;""><span style=""color: #595a5c;"">Fullf&oslash;rt:</span> <span style=""color: #177b98;""><strong>#%completion.completeddate%#</strong></span></span></p>
            <p style=""text-align: center;"" align=""center""><span style=""font-size: small;""><span style=""font-size: x-small; font-family: arial, helvetica, sans-serif; color: #595a5c;"">Unikt nummer for kursbeviset: #%certificate.number%#</span><br /></span></p>
            </td>
            </tr>
            </tbody>
            </table>";
    }
}
