﻿using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TPToolsLibrary;
using TPToolsLibrary.BrowserActions;
using System.Collections.Generic;
using TPToolsLibrary.SettingsAndTemplates;
using System.Threading.Tasks;
using TPToolsLibrary.Models;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.IO;

namespace TPTools
{
    public partial class Form1 : Form
    {

        CancellationTokenSource tokenSource;
        CancellationToken token;



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
            CoHostClientDropDown.DisplayMember = "Key";
            CoHostClientDropDown.ValueMember = "Value";


            //initialise evidence types


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

            var sameWindow = rdioSameWindow.Checked;


            SCORMType scormType = SCORMType.SCORM12;

            if (rdioSCORM12.Checked)
            {
                scormType = SCORMType.SCORM12;
            }
            else if (rdioSCORM20042nd.Checked)
            {
                scormType = SCORMType.SCORM20042nd;
            }
            else if (rdioSCORM20043rd.Checked)
            {
                scormType = SCORMType.SCORM20043rd;
            }
            else if (rdioSCORM20044th.Checked)
            {
                scormType = SCORMType.SCORM20044th;
            }

            Thread thread = new Thread(() => TPToolsLibrary.ConnectorFiles.Download(courseCodeList, portalId, sameWindow, scormType));

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
            progDynam.Value = 0;

            var attributes = txtAttributes.Text;
            var attributeList = attributes.Split(',').ToList();

            var progress = new Progress<ProgressWrapper>(UpdateProgressDynamAtt);
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

            var task = Task.Run(() =>   DynamicAttributes.AddAttributes(attributeList, txtPortalIDDynamic.Text, progress, token));

        }
        private void UpdateProgressDynamAtt(ProgressWrapper progress)
        {
            var progressBar = progDynam;
            progressBar.Maximum = progress.Total;
            progressBar.Value = progress.Current;
        }
        private void btnCancelAttributes_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
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



            var admin1 = new User();
            var admin2 = new User();
            //Admin 1
            if (!string.IsNullOrWhiteSpace(txtAdmin1FirstNameCR.Text) && !string.IsNullOrWhiteSpace(txtAdmin1LastNameCR.Text) && !string.IsNullOrWhiteSpace(txtAdmin1EmailCR.Text))
            {
                admin1.Firstname = txtAdmin1FirstNameCR.Text;
                admin1.Lastname = txtAdmin1LastNameCR.Text;
                admin1.Email = txtAdmin1EmailCR.Text;
                admin1.SendEmail = chkAdmin1SendEmail.Checked;
                admin1.Username = txtAdmin1EmailCR.Text;
                admin1.Password = "Welcome123!";
                admin1.UserRole = UserRole.Portal_Administrator;
                admin1.OrgUnit = "";
                
            }
            //Admin 2
            if (!string.IsNullOrWhiteSpace(txtAdmin2FirstNameCR.Text) && !string.IsNullOrWhiteSpace(txtAdmin2LastNameCR.Text) && !string.IsNullOrWhiteSpace(txtAdmin2EmailCR.Text))
            {
                admin2.Firstname = txtAdmin2FirstNameCR.Text;
                admin2.Lastname = txtAdmin2LastNameCR.Text;
                admin2.Email = txtAdmin2EmailCR.Text;
                admin2.SendEmail = chkAdmin2SendEmail.Checked;
                admin2.Username = txtAdmin2EmailCR.Text;
                admin2.Password = "Welcome123!";
                admin2.UserRole = UserRole.Portal_Administrator;
                admin2.OrgUnit = "";
            }
            //Thread thread = new Thread(() =>
            //ControlRisksPortal.CreateCRPortal(companyName, selfReg, courses));

            //thread.Start();



            Task<string> task = ControlRisksPortal.CreateCRPortal(companyName, selfReg, courses, admin1, admin2);

            task.Wait();

            txtCRPortalResult.Text = task.Result;
           // }
        }

        private async void btnApplyNewUI_Click(object sender, EventArgs e)
        {

            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }
            else
            {

                if (string.IsNullOrWhiteSpace(txtNewUIPortalIds.Text))
                {
                    MessageBox.Show("Enter Portals");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPrimaryColour.Text) || string.IsNullOrWhiteSpace(txtSecondaryColour.Text) || string.IsNullOrWhiteSpace(txtHeaderBackgrounColour.Text) || string.IsNullOrWhiteSpace(txtHeaderTextColour.Text))
                {
                    MessageBox.Show("Missing Colour Choice");
                    return;
                }

                var portals = txtNewUIPortalIds.Text.Split(',').ToList();



                var newUIErrors  = await ApplyNewUI.Apply(portals, txtPrimaryColour.Text, txtSecondaryColour.Text, txtHeaderBackgrounColour.Text, txtHeaderTextColour.Text, chkEnableStudentUI.Checked, chkEnableAdminUI.Checked);

                if(newUIErrors.Count == 0)
                {
                    txtNewUIErrors.Text = "No Errors Detected, No One Will Ever See This Message.";
                }
                else
                {
                    txtNewUIErrors.Text = string.Join(",", newUIErrors);
                }
            }

        }
        private void btnShareToIndustry_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalIdIndustryShare.Text))
            {
                MessageBox.Show("Enter Portal ID to Share From");
                return;
            }

            var courseCodes = txtCourseIdIndustryShare.Text;
            var courseCodeList = courseCodes.Split(',').ToList();

            var industries = txtIndustryList.Text;
            var industryList = industries.Split(',').ToList();

            Thread thread = new Thread(() => TPToolsLibrary.IndustryShare.ShareCourses(courseCodeList, industryList, txtPortalIdIndustryShare.Text));
            thread.Start();
        }

        private void btnSelectFileAdminDeactivate_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            dgAdminDeactivate.DataSource = ReadCsv(file);

        }

        private async void btnDeactivateAdmins_Click(object sender, EventArgs e)
        {

            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (dgAdminDeactivate.Rows.Count == 0)
            {
                MessageBox.Show("No Data");
                return;
            }
            foreach(DataGridViewRow row in dgAdminDeactivate.Rows)
            {
                var userId = row.Cells[0].Value.ToString();
                var portalId = row.Cells[1].Value.ToString();

                if(!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(portalId))
                {
                    await DeactivateAdmin.DeactivateAdminAccount(userId, portalId);
                }
            }
                
        }

        public DataTable ReadCsv(string filePath)
        {
            DataTable dt = new DataTable("Data");
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                Path.GetDirectoryName(filePath) + "\";Extended Properties='text;HDR=yes;FMT=Delimited(,)';"))
            {
                
                //Execute select query
                using (OleDbCommand cmd = new OleDbCommand(string.Format("select *from [{0}]", new FileInfo(Path.GetFileName(filePath)).Name), cn))
                {
                    cn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        private async void btnPortalShare_ClickAsync(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            if (string.IsNullOrEmpty(txtPortalSharePortalId.Text))
            {
                MessageBox.Show("Enter Portal ID to Share From");
                return;
            }
            if (string.IsNullOrEmpty(txtPortalSharePayingCompany.Text))
            {
                MessageBox.Show("Enter paying company");
                return;
            }


            var courseCodes = txtPortalShareCourses.Text;
            var courseCodeList = courseCodes.Split(',').ToList();

            var portals = txtPortalSharePortals.Text;
            var portalList = portals.Split(',').ToList();

            await TPToolsLibrary.PortalShare.ShareCourses(courseCodeList, portalList, txtPortalSharePortalId.Text, txtPortalSharePayingCompany.Text);
        }

        private void btnUpdatePortalIndustry_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }
           

            var portals = txtPortalNumbersIndustry.Text;
            var portalList = portals.Split(',').ToList();

            var industry = comboBox1.SelectedItem.ToString();



            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

            var task = Task.Run(() => PortalIndustryUpdate.Update(portalList, industry));
        }

        private async void btnAddPriceComponent_ClickAsync(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            Enum.TryParse(priceComponentCurrency.SelectedItem.ToString(), out PriceSettingComponent.Currency currency);
            

            var courses = txtCourseCodesPriceComponent.Text.Split(',').ToList();


            await PriceSettingComponent.AddComponent(courses, txtPriceComponentPortalId.Text, txtPriceComponentPrice.Text, txtPriceComponentPortalName.Text, currency);
        }

        private async void btnZeroExommerceStart_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            var courses = txtZeroEcommerceCourseCodes.Text.Split(',').ToList();


            await EcommercesZeroPrice.AddZeroPriceEcommerce(courses, txtZeroEcommercePortalId.Text, txtZeroExommercePrefix.Text);
        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private async void btnSetEvidenceTypes_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            var evidenceTypes = new List<EvidenceTypesEnum>();

            if (chk_OB.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.OB);
            }
            if (chk_PW.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.PW);
            }
            if (chk_QA.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.QA);
            }
            if (chk_TS.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.TS);
            }
            if (chk_TA.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.TA);
            }
            if (chk_WIT.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.WIT);
            }
            if (chk_RPL.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.RPL);
            }
            if (chk_PD.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.PD);
            }
            if (chk_OTHER.Checked)
            {
                evidenceTypes.Add(EvidenceTypesEnum.OTHER);
            }


            var competences = evidenceTypesCompIds.Text.Split(',').ToList();


            await SetEvidenceType.Update(competences, evidenceTypes, evidenceTypePortalId.Text);


        }

        private async void btnAddFrenchTab_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }

            var competences = txtCompIdsFrenchTab.Text.Split(',').ToList();
            var portalId = txtFrenchTabPortlId.Text;

            await AddFrenchTabToCompetence.AddFrenchTab(competences, portalId);

            
        }

        private async void btnApplyNewAdminUI_Click(object sender, EventArgs e)
        {
            if (!Login.IsLoggedIn())
            {
                MessageBox.Show("Log In First");
                return;
            }


            var portals = txtNewAdminPortalIds.Text.Split(',').ToList();



            var newAdminUIErrors = await ApplyNewAdminUI.Apply(portals);

            if (newAdminUIErrors.Count == 0)
            {
                txtNewAdminUIIssues.Text = "No Errors Detected, No One Will Ever See This Message.";
            }
            else
            {
                txtNewAdminUIIssues.Text = string.Join(",", newAdminUIErrors);
            }

        }
    }
}