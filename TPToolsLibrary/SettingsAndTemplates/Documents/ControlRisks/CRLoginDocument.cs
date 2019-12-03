using System;
using System.Collections.Generic;
using System.Text;

namespace TPToolsLibrary.SettingsAndTemplates.Documents.ControlRisks
{
    public class CRLoginDocument
    {

        private string CompanyName;

        public string Title =>
            @"Login Document";
        public string Contents =>
            @"<div style=""width: 530px; float: left;"">
            <h1>Welcome to the " + CompanyName + @" Trainingportal</h1>
            <p>&nbsp;</p>
            <p><strong>Accessing the Portal:<br /></strong><br />If you have an existing account on the system or if you have received login details in an email, you can sign-in using the login box provided. Please note that both the username and password are case sensitive. After you have logged in, please update your profile and change your password.</p>
            <p>&nbsp;</p>
            <p>Please click the link to view Control Risks&nbsp;<a href=""https://www.controlrisks.com/core-cspp"" target=""_blank"" rel=""noopener noreferrer"">Content Standards &amp; Privacy Policy</a></p>
            <p>&nbsp;</p>
            <p><strong>Need some help - contact our technical support team:</strong></p>
            <p><br />If you are having trouble logging in, or experience any technical problems, you can contact Mintra Technical Support on the email below:</p>
            <div><br />
            <table style=""font-size: 18px;"" border=""0"">
            <tbody>
            <tr>
            <td><span style=""font-size: 10pt;"">E-mail:</span></td>
            <td><span style=""font-size: 10pt;"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
            <td><span style=""font-size: 10pt;""><a href=""mailto:support@mintragroup.com"">support@mintragroup.com</a></span></td>
            </tr>
            </tbody>
            </table>
            </div>
            <p style=""text-align: justify;"">&nbsp;</p>
            </div>";


        public CRLoginDocument(string companyName)
        {
            this.CompanyName = companyName;
        }

    }
}
