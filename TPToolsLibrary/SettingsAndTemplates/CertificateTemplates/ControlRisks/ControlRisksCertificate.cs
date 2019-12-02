using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates.CertificateTemplates
{
    class ControlRisksCertificate : ICertificateTemplate
    {
        public string TitleEn =>
            "Control Risks Branded Completion Certificate";


        public string ContentEn =>
            @"<table style=""margin: 0 0 0 30px; width: 640px; height: 910px; background-image: url('getImage?img=Elearning_Certficate(2).jpg');"">
            <tbody>
            <tr>
            <td>
            <p align=""center""><span style=""color: white; font-size: x-large;""><strong><span style=""font-family: arial, helvetica, sans-serif;"">CERTIFICATE OF COMPLETION</span></strong></span></p>
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif; color: white;"">This is to certify that</span></p>
            <p align=""center""><span style=""color: white; font-size: xx-large;""><strong><span style=""font-family: arial, helvetica, sans-serif;"">#%user.firstname%# #%user.lastname%#</span></strong></span></p>
            <p align=""center"">&nbsp;</p>
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif; color: white;"">has completed the e-Learning course</span><span style=""font-size: small;"">&nbsp;</span></p>
            <p align=""center""><span style=""font-size: x-large; border: 0px solid; margin: 60px; color: white; font-family: arial, helvetica, sans-serif;""><strong>#%course.name%#</strong></span></p>
            <br />
            <p align=""center""><span style=""font-family: arial, helvetica, sans-serif;""><span style=""color: white;"">Completed:</span> <span style=""color: white;""><strong>#%completion.completeddate%#</strong></span></span></p>
            <p align=""center""><span style=""font-size: small;""><span style=""font-size: x-small; font-family: arial, helvetica, sans-serif; color: white;"">Unique certificate number: #%certificate.number%#</span></span>&nbsp;</p>
            </td>
            </tr>
            </tbody>
            </table>";


        public string TitleNo => throw new NotImplementedException();

        public string ContentNo => throw new NotImplementedException();
    }
}
