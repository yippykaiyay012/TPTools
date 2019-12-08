using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TPToolsLibrary;
using TPToolsLibrary.BrowserActions;
using System.Collections.Generic;
using TPToolsLibrary.SettingsAndTemplates;
using System.Threading.Tasks;

namespace TPTools
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();

        }


        protected override void OnShown(EventArgs e)
        {
            StartUp();
        }


        private void StartUp()
        {
            if (Properties.Settings.Default.userName != string.Empty)
            {
                txtUsernameAdmin.Text = Properties.Settings.Default.userName;
                txtPasswordAdmin.Text = Properties.Settings.Default.userPass;
                chkRememberDetails.Checked = true;
            }


            // initialise list of CoHost Clients
            CoHostClientDropDown.DataSource = new BindingSource(CoHostSharingSettings.ClientDetails, null);
            CoHostClientDropDown.DisplayMember = "Value";
            CoHostClientDropDown.ValueMember = "Key";

            // initialise list of Control Risks Courses
            foreach(var course in PortalSettings.ControlRisksCourses)
            {
                chkListControlRiskCourses.Items.Add(course);
            }

            MessageBox.Show("Cancel Buttons No Worky, Close App To Cancel Processes.", "Info");


            

        }


        private void ChkRememberDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRememberDetails.Checked)
            {
                Properties.Settings.Default.userName = txtUsernameAdmin.Text;
                Properties.Settings.Default.userPass = txtPasswordAdmin.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Reset();
            }

        }


        private void BtnLogIn_Click(object sender, EventArgs e)
        {

                Thread thread = new Thread(() => Login.LogIn(txtUsernameAdmin.Text, txtPasswordAdmin.Text));
                thread.Start();
        }


        private void BtnAddCourses_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            var courseCodes = txtCourseCodes.Text;
            var courseCodeList = courseCodes.Split(',').ToList();

            Thread thread = new Thread(() => TPToolsLibrary.CourseCodes.AddCourseCodes(courseCodeList));
            thread.Start();

        }

        private void BtnDownloadConnectors_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalID.Text))
            {
                MessageBox.Show("Enter Portal ID");
                return;
            }

            var courseCodeList = txtConnectorCourseCodes.Text.Split(',').ToList();
            var portalId = txtPortalID.Text;

            Thread thread = new Thread(() => TPToolsLibrary.ConnectorFiles.Download(courseCodeList, portalId));
            thread.Start();

        }

        private void BtnUpdateComp_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalIDComp.Text))
            {
                MessageBox.Show("Enter Portal ID");
                return;
            }

            if (!radioNo.Checked && !radioYes.Checked)
            {
                MessageBox.Show("Choose Status");
                return;
            }


            var compCodes = txtCompetencyUnits.Text;
            var compCodeList = compCodes.Split(',').ToList();


            var applyAssessment = !radioNo.Checked;

            Thread thread = new Thread(() => CompetencyAssessment.UpdateCompetencyStatus(compCodeList, applyAssessment, txtPortalIDComp.Text));
            thread.Start();

        }

        private void BtnAddAttributes_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            var attributes = txtAttributes.Text;
            var attributeList = attributes.Split(',').ToList();

            Thread thread = new Thread(() => DynamicAttributes.AddAttributes(attributeList, txtPortalIDDynamic.Text));
            thread.Start();


        }

        private void BtnStartEnrolRules_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalIdEnrolRules.Text))
            {
                MessageBox.Show("Enter Portal ID");
                return;
            }


            var courseCodes = txtCourseCodesEnrolRules.Text;
            var courseCodeList = courseCodes.Split(',').ToList();

            //EnrolmentRules.AddEnrolmentRule(courseCodeList, txtAdminEmailEnrolRules.Text.Trim(),
            //    txtOrgUnitEnrolRules.Text.Trim(), txtPortalIdEnrolRules.Text.Trim());


            Thread thread = new Thread(() => EnrolmentRules.AddEnrolmentRule(courseCodeList, txtAdminEmailEnrolRules.Text.Trim(),
                                                                    txtOrgUnitEnrolRules.Text.Trim(), txtPortalIdEnrolRules.Text.Trim()));
            thread.Start();
        }



        private void BtnShareCourses_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalIdCompanyShare.Text))
            {
                MessageBox.Show("Enter Portal ID to Share From");
                return;
            }

            var courseCodes = txtCourseIdShare.Text;
            var courseCodeList = courseCodes.Split(',').ToList();

            var companies = txtCompanyShare.Text;
            var companyList = companies.Split(',').ToList();

            Thread thread = new Thread(() => TPToolsLibrary.CompanyShare.ShareCourses(courseCodeList, companyList, txtPortalIdCompanyShare.Text));
            thread.Start();
        }

        private void BtnSetExpiry_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalIDExpiry.Text))
            {
                MessageBox.Show("Enter Portal ID to Share From");
                return;
            }
            else if (string.IsNullOrEmpty(txtExpiryMonths.Text))
            {
                MessageBox.Show("Enter Portal ID to Share From");
                return;
            }

            var courseCodes = txtCourseCodesExpiry.Text;
            var courseCodeList = courseCodes.Split(',').ToList();

            var months = txtExpiryMonths.Text;

            Thread thread = new Thread(() => TPToolsLibrary.CourseExpiry.SetCourseExpiry(courseCodeList, months, txtPortalIDExpiry.Text));

            thread.Start();
        }

        private void BtnCreateDemoPortal_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDemoCompanyName.Text))
            {
                MessageBox.Show("Enter Company Name");
                return;
            }
            else
            {
                PortalType portalType = PortalType.Basic;

                if (rdioDemoBasic.Checked)
                {
                    portalType = PortalType.Basic;
                }
                else if (rdioDemoAdvanced.Checked)
                {
                    portalType = PortalType.Advanced;
                }



                Thread thread = new Thread(() =>
                    TPToolsLibrary.DemoPortal.CreateDemoPortal(txtDemoCompanyName.Text, rdioUK.Checked, portalType, chkAddDemoUsers.Checked));

                thread.Start();

            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1prz7bB3qG9m9h1kZR81ECLTC3BosoE_YvyRmnGli8dU/edit?usp=sharing");
        }

        private void TxtDemoCompanyName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDemoCompanyName.Text))
            {
                lblNewDemoPortalName.Text = "__________";
            }
            else
            {
                lblNewDemoPortalName.Text = txtDemoCompanyName.Text + " Trainingportal";
            }

        }



        private void BtnCreateNewPortal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCoHostName.Text))
            {
                lblCoHostName.Text = "__________";
            }
            else
            {
                lblCoHostName.Text = txtCoHostName.Text + " - " + ((KeyValuePair<string, string>)CoHostClientDropDown.SelectedItem).Key + " CoHost";
            }

        }

        private void btnCreatCohostPortal_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCoHostName.Text))
            {
                MessageBox.Show("Enter Client Name");
                return;
            }
            else
            {

                var parentCompanyName = ((KeyValuePair<string, string>)CoHostClientDropDown.SelectedItem).Key;
                var parentCompanyPortalId = ((KeyValuePair<string, string>)CoHostClientDropDown.SelectedItem).Value;

                Thread thread = new Thread(() =>
                    TPToolsLibrary.CoHostPortal.CreateCoHostPortal(parentCompanyName, parentCompanyPortalId, txtCoHostName.Text, chkShareCourses.Checked));

                thread.Start();
            }
        }

        private void btnCompTestStart_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }
            else
            {

                var portalId = txtPortalIdCompTest.Text;
                var compList = txtCompIds.Text.Split(',').ToList();

                Thread thread = new Thread(() =>
                    CompetenceToTest.SetToTest(portalId, compList));

                thread.Start();
            }
        }

        private void btnSetProgLog_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }
            else
            {
                var portalId = txtPortalIdProgLog.Text;
                var courseCodeList = txtCourseCodesProgLog.Text.Split(',').ToList();
                bool desiredValue = false;
                if (rdioProgYes.Checked)
                {
                    desiredValue = true;
                }
                else if (rdioProgNo.Checked)
                {
                    desiredValue = false;
                }


                Thread thread = new Thread(() =>
                    AllowProgressionLog.Set(courseCodeList, portalId, desiredValue));

                thread.Start();

            }
        }

        private void btnUpdateEcommerceValidity_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }
            else
            {
                var portalId = txtPortalIdEcommerce.Text;
                var courseCodeList = txtCourseCodesEcommerce.Text.Split(',').ToList();
                var validity = txtExpiryMonthsEcommerce.Text;

                Thread thread = new Thread(() =>
                EcommercePurchaseValidity.SetPurchaseValidity(courseCodeList, validity, portalId));

                thread.Start();
            }
        }

        private void txtControlRisksPortalCompanyName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtControlRisksPortalCompanyName.Text))
            {
                lblControlRisksPortalName.Text = "__________";
                lblControlRisksCompanyName.Text = "__________";
            }
            else
            {
                lblControlRisksPortalName.Text = txtControlRisksPortalCompanyName.Text + " - Control Risks Trainingportal";
                lblControlRisksCompanyName.Text = txtControlRisksPortalCompanyName.Text + " - Control Risks";
            }
        }

        private void btnCreateControlRisksPortal_Click(object sender, EventArgs e)
        {
            //if (!Login.IsLoggedIn())
            //{
            //    MessageBox.Show("Log In First");
            //    return;
            //}
            //else
            //{

                var courses = new List<string>();
                foreach (var item in chkListControlRiskCourses.CheckedItems)
                {
                    courses.Add(((KeyValuePair<string, string>)item).Key);
                }

                var companyName = txtControlRisksPortalCompanyName.Text;
                bool selfReg = false;

                if (rdioCRSelfRegOn.Checked)
                {
                    selfReg = true;
                }
                else if (rdioCRSelfRegOff.Checked)
                {
                    selfReg = false;
                }

                //Thread thread = new Thread(() =>
                //ControlRisksPortal.CreateCRPortal(companyName, selfReg, courses));

                //thread.Start();



                Task<string> task = ControlRisksPortal.CreateCRPortal(companyName, selfReg, courses);

                task.Wait();

                txtCRPortalResult.Text = task.Result;
           // }
        }
    }
}