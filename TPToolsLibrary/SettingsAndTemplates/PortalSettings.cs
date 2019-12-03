using System;
using System.Collections.Generic;
using System.Text;
using TPToolsLibrary.SettingsAndTemplates;

namespace TPToolsLibrary.BrowserActions
{
    public enum PortalType
    {
        Basic,
        Advanced
    }
    public enum UserRole
    {
        Student,
        User_Administrator,
        Support_Agent,
        Manager,
        Manager_ReadOnly,
        Company_Administrator,
        TMS_Agent,
        System_Administrator,
        Content_Administrator,
        TMS_Administrator,
        Regional_Support_Agent,
        Portal_Administrator,
        Course_Administrator,
        Course_Provider
    }

    public static class PortalSettings
    {


        public static readonly List<string> basicPortalSettings = new List<string>()
        {
            //"portalBooleanProperties[DISABLE_CV_MODULE_EDIT_FOR_STUDENTS]",
            //"portalBooleanProperties[HIDE_CV_MODULE_FOR_STUDENTS]",
            //"portalBooleanProperties[HIDE_COMPETENCE_DESCRIPTIONS_IN_CV]",
            //"portalBooleanProperties[CV_HIDE_EDUCTION_CREDIT]",
            //"portalBooleanProperties[CV_SHOW_DURATION_FOR_COURSES]",
            //"portalBooleanProperties[SHOW_GENERATION_DATE_IN_CV_FOOTER]",
            //"portalBooleanProperties[USE_CV_MODULE]",
            "portalBooleanProperties[COMPANY_PORTAL]",
            //"portalBooleanProperties[USE_SOCIALMEDIA_INTEGRATION]",
            "portalBooleanProperties[SHOW_CHANGELOG]",
            "portalBooleanProperties[SHOW_STUDENT_COURSE_CATALOGUE]",
            //"portalBooleanProperties[SHOW_IN_PORTAL_LIST]",
            //"portalBooleanProperties[USE_SMS_MODULE]",
            //"portalBooleanProperties[USE_CUSTOM_THEME]",
            //"portalBooleanProperties[USE_EXPIRYDATE]",
            "portalBooleanProperties[USE_NEW_STUDENT_UI]",
            "portalBooleanProperties[USE_STUDENT_DASHBOARD]",
            //"portalBooleanProperties[USE_SURVEY]",
            //"portalBooleanProperties[CONNECT_COMPETENCE_ROLES_TO_ORGANIZATIONUNITS]",
            //"portalBooleanProperties[ALLOW_LIMITED_ACCESS_TO_COMPETENCE_CATALOGUES]",
            //"portalBooleanProperties[DENY_MANUAL_APPROVAL_OF_TRAINING_PACKAGES_AND_ROLES]",
            //"portalBooleanProperties[HIDE_COMPETENCE_MODULE_FROM_STUDENT]",
            //"portalBooleanProperties[HIDE_USER_EVIDENCE_LOCKER]",
            //"portalBooleanProperties[DENY_ASSESSORS_FROM_MANUALLY_SETTING_COMPLETION_DATES]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_APPROVING_HIS_OWN_COURSES_AND_COMPETENCES]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_MANUALLY_APPROVE_TRAINING_PACKAGES]",
            //"portalBooleanProperties[REQUIRE_EXPLICIT_ASSESSORS_FOR_COMPETENCEREQUIREMENTS]",
            //"portalBooleanProperties[SHOW_MEDIUM_PRIORITY_CAPTION_IN_UI]",
            //"portalBooleanProperties[USE_COMPETENCE_ASSESSMENT_PLAN]",
            //"portalBooleanProperties[USE_COMPETENCE_CANDIDATES]",
            //"portalBooleanProperties[USE_CHECKLIST]",
            //"portalBooleanProperties[USE_COMPETENCE_MODULE]",
            //"portalBooleanProperties[USE_COMPETENCE_TARGET_NUMBER]",
            //"portalBooleanProperties[USE_COMPETENCE_SELF_ASSESSMENT]",
            //"portalBooleanProperties[USE_COMPETENCE_VERIFIERS]",
            //"portalBooleanProperties[HIDE_STATISTICS_ON_COMPETENCE_HOMEPAGE]",
            //"portalBooleanProperties[CAN_SEE_PUBLISHER_TAB]",
            //"portalBooleanProperties[HAS_PUBLISHER_LICENSE]",
            //"portalBooleanProperties[USE_ASSESSMENTS]",
            //"portalBooleanProperties[ALLOW_AICC_ACCESS_TO_COURSES]",
            //"portalBooleanProperties[COURSE_ALLOW_SCORM_CLOUD]",
            //"portalBooleanProperties[COURSE_ALLOW_APP_ACCESS_TO_OWN_COURSES]",
            //"portalBooleanProperties[ALLOW_COURSE_ENROLLMENT_WITH_COURSCLASS_INFORMATION]",
            //"portalBooleanProperties[COURSE_ALLOW_INVITATION_STUDENTS_TO_COURSE_CLASS]",
            //"portalBooleanProperties[ALLOW_MANAGER_COURSE_APPROVAL]",
            //"portalBooleanProperties[ALLOW_XAPI_ACCESS_TO_COURSES]",
            "portalBooleanProperties[PREVENT_CREATE_COURSE]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_ENROLLING_TO_COURSES]",
            //"portalBooleanProperties[USE_CHECKLISTS_ON_COURSES]",
            //"portalBooleanProperties[USE_INSTRUCTORS_ON_COURSES]",
            //"portalBooleanProperties[COURSE_USE_ORGANIZATION_UNIT_SPECIFIC_REFRESHER_INTERVALS]",
            //"portalBooleanProperties[ALLOW_DELETION_OF_ENROLLMENTS_WITH_COURSE_REGISTRATION_ON_ECOMMERCE_COURSES]",
            //"portalBooleanProperties[ALWAYS_SHOW_INVOICE_INFORMATION]",
            //"portalBooleanProperties[PAYMENTINFORMATION_FOR_ECOMMERCE_ENROLLMENTS_REQUIRED]",
            //"portalBooleanProperties[SEARCH_FOR_BUYABLE_COURSES_BY_DEFAULT_FOR_STUDENT]",
            //"portalBooleanProperties[USES_OCS_INTEGRATION]",
            //"portalBooleanProperties[LL_ACCESS_FROM_TRAININGPORTAL]",
            //"portalBooleanProperties[DO_NOT_SHOW_ENROLLMENTS_FROM_OTHER_PORTALS_IN_THE_COURSE_STATUS_REPORT]",
            //"portalBooleanProperties[SHOW_REPORT_COURSE_STATUS_FOR_COURSE_PROVIDERS]",
            //"portalBooleanProperties[SHOW_MEDIUM_PRIORITY_CAPTION_IN_REPORTS]",
            //"portalBooleanProperties[SSO_USER_DENY_CREATE_NEW]",
            //"portalBooleanProperties[SSO_ENABLED]",
            //"portalBooleanProperties[SSO_USE_SAML_BASED]",
            //"portalBooleanProperties[SPIDER_SYNC_USERS]",
            //"portalBooleanProperties[USE_TMS_MODULE]",
            //"portalBooleanProperties[ALLOW_STUDENTS_TO_REENROLL_TO_ECOMMERCE_COURSES]",
            //"portalBooleanProperties[REMOTE_SITES_USES_ALL_COURSES_AVAILABLE_ON_PORTAL]",
            //"portalBooleanProperties[USE_CHECKLISTS_ON_TRAININGPORTAL_OFFLINE]",
            //"portalBooleanProperties[USE_REMOTE_SITES]",
            //"portalBooleanProperties[USE_COMPETENCE_LEVEL_ON_TPO]",
            //"portalBooleanProperties[ALLOW_LOGIN_TO_USERS_REGISTERED_IN_PORTAL_OWNER_COMPANY]",
            //"portalBooleanProperties[ALLOW_SELF_REGISTRATION]",
            //"portalBooleanProperties[DELETE_USER_WHEN_APPROVAL_IS_DENIED]",
            //"portalBooleanProperties[MERGE_USER_ACCOUNTS_DISABLED]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_CHANGING_USER_MASS]",
            //"portalBooleanProperties[PROFILE_VERIFICATION_ALLOW_BYPASS]",
            //"portalBooleanProperties[PROFILE_VERIFICATION_REQUIRED_FIELDS]",
            "portalBooleanProperties[SHOW_LICENSE_AGREEMENT]",
            //"portalBooleanProperties[USE_PORTAL_SPECIFIC_LICENSE_AGREEMENT]"


        };



        public static readonly List<string> advancedPortalSettings = new List<string>()
        {
            // "portalBooleanProperties[DISABLE_CV_MODULE_EDIT_FOR_STUDENTS]",
            //"portalBooleanProperties[HIDE_CV_MODULE_FOR_STUDENTS]",
            //"portalBooleanProperties[HIDE_COMPETENCE_DESCRIPTIONS_IN_CV]",
            //"portalBooleanProperties[CV_HIDE_EDUCTION_CREDIT]",
            //"portalBooleanProperties[CV_SHOW_DURATION_FOR_COURSES]",
            //"portalBooleanProperties[SHOW_GENERATION_DATE_IN_CV_FOOTER]",
            "portalBooleanProperties[USE_CV_MODULE]",
            "portalBooleanProperties[COMPANY_PORTAL]",
            //"portalBooleanProperties[USE_SOCIALMEDIA_INTEGRATION]",
            "portalBooleanProperties[SHOW_CHANGELOG]",
            "portalBooleanProperties[SHOW_STUDENT_COURSE_CATALOGUE]",
            //"portalBooleanProperties[SHOW_IN_PORTAL_LIST]",
            //"portalBooleanProperties[USE_SMS_MODULE]",
            //"portalBooleanProperties[USE_CUSTOM_THEME]",
            //"portalBooleanProperties[USE_EXPIRYDATE]",
            "portalBooleanProperties[USE_NEW_STUDENT_UI]",
            "portalBooleanProperties[USE_STUDENT_DASHBOARD]",
            "portalBooleanProperties[USE_SURVEY]",
            //"portalBooleanProperties[CONNECT_COMPETENCE_ROLES_TO_ORGANIZATIONUNITS]",
            //"portalBooleanProperties[ALLOW_LIMITED_ACCESS_TO_COMPETENCE_CATALOGUES]",
            //"portalBooleanProperties[DENY_MANUAL_APPROVAL_OF_TRAINING_PACKAGES_AND_ROLES]",
            //"portalBooleanProperties[HIDE_COMPETENCE_MODULE_FROM_STUDENT]",
            //"portalBooleanProperties[HIDE_USER_EVIDENCE_LOCKER]",
            //"portalBooleanProperties[DENY_ASSESSORS_FROM_MANUALLY_SETTING_COMPLETION_DATES]",
            "portalBooleanProperties[DENY_MANAGER_FROM_APPROVING_HIS_OWN_COURSES_AND_COMPETENCES]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_MANUALLY_APPROVE_TRAINING_PACKAGES]",
            //"portalBooleanProperties[REQUIRE_EXPLICIT_ASSESSORS_FOR_COMPETENCEREQUIREMENTS]",
            //"portalBooleanProperties[SHOW_MEDIUM_PRIORITY_CAPTION_IN_UI]",
            "portalBooleanProperties[USE_COMPETENCE_ASSESSMENT_PLAN]",
            "portalBooleanProperties[USE_COMPETENCE_CANDIDATES]",
            "portalBooleanProperties[USE_CHECKLIST]",
            "portalBooleanProperties[USE_COMPETENCE_MODULE]",
            //"portalBooleanProperties[USE_COMPETENCE_TARGET_NUMBER]",
            "portalBooleanProperties[USE_COMPETENCE_SELF_ASSESSMENT]",
            "portalBooleanProperties[USE_COMPETENCE_VERIFIERS]",
            //"portalBooleanProperties[HIDE_STATISTICS_ON_COMPETENCE_HOMEPAGE]",
            //"portalBooleanProperties[CAN_SEE_PUBLISHER_TAB]",
            //"portalBooleanProperties[HAS_PUBLISHER_LICENSE]",
            "portalBooleanProperties[USE_ASSESSMENTS]",
            //"portalBooleanProperties[ALLOW_AICC_ACCESS_TO_COURSES]",
            //"portalBooleanProperties[COURSE_ALLOW_SCORM_CLOUD]",
            //"portalBooleanProperties[COURSE_ALLOW_APP_ACCESS_TO_OWN_COURSES]",
            //"portalBooleanProperties[ALLOW_COURSE_ENROLLMENT_WITH_COURSCLASS_INFORMATION]",
            //"portalBooleanProperties[COURSE_ALLOW_INVITATION_STUDENTS_TO_COURSE_CLASS]",
            //"portalBooleanProperties[ALLOW_MANAGER_COURSE_APPROVAL]",
            //"portalBooleanProperties[ALLOW_XAPI_ACCESS_TO_COURSES]",
            //"portalBooleanProperties[PREVENT_CREATE_COURSE]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_ENROLLING_TO_COURSES]",
            "portalBooleanProperties[USE_CHECKLISTS_ON_COURSES]",
            //"portalBooleanProperties[USE_INSTRUCTORS_ON_COURSES]",
            //"portalBooleanProperties[COURSE_USE_ORGANIZATION_UNIT_SPECIFIC_REFRESHER_INTERVALS]",
            //"portalBooleanProperties[ALLOW_DELETION_OF_ENROLLMENTS_WITH_COURSE_REGISTRATION_ON_ECOMMERCE_COURSES]",
            //"portalBooleanProperties[ALWAYS_SHOW_INVOICE_INFORMATION]",
            //"portalBooleanProperties[PAYMENTINFORMATION_FOR_ECOMMERCE_ENROLLMENTS_REQUIRED]",
            //"portalBooleanProperties[SEARCH_FOR_BUYABLE_COURSES_BY_DEFAULT_FOR_STUDENT]",
            //"portalBooleanProperties[USES_OCS_INTEGRATION]",
            //"portalBooleanProperties[LL_ACCESS_FROM_TRAININGPORTAL]",
            //"portalBooleanProperties[DO_NOT_SHOW_ENROLLMENTS_FROM_OTHER_PORTALS_IN_THE_COURSE_STATUS_REPORT]",
            //"portalBooleanProperties[SHOW_REPORT_COURSE_STATUS_FOR_COURSE_PROVIDERS]",
            //"portalBooleanProperties[SHOW_MEDIUM_PRIORITY_CAPTION_IN_REPORTS]",
            //"portalBooleanProperties[SSO_USER_DENY_CREATE_NEW]",
            //"portalBooleanProperties[SSO_ENABLED]",
            //"portalBooleanProperties[SSO_USE_SAML_BASED]",
            //"portalBooleanProperties[SPIDER_SYNC_USERS]",
            //"portalBooleanProperties[USE_TMS_MODULE]",
            //"portalBooleanProperties[ALLOW_STUDENTS_TO_REENROLL_TO_ECOMMERCE_COURSES]",
            //"portalBooleanProperties[REMOTE_SITES_USES_ALL_COURSES_AVAILABLE_ON_PORTAL]",
            //"portalBooleanProperties[USE_CHECKLISTS_ON_TRAININGPORTAL_OFFLINE]",
            //"portalBooleanProperties[USE_REMOTE_SITES]",
            //"portalBooleanProperties[USE_COMPETENCE_LEVEL_ON_TPO]",
            //"portalBooleanProperties[ALLOW_LOGIN_TO_USERS_REGISTERED_IN_PORTAL_OWNER_COMPANY]",
            //"portalBooleanProperties[ALLOW_SELF_REGISTRATION]",
            //"portalBooleanProperties[DELETE_USER_WHEN_APPROVAL_IS_DENIED]",
            //"portalBooleanProperties[MERGE_USER_ACCOUNTS_DISABLED]",
            //"portalBooleanProperties[DENY_MANAGER_FROM_CHANGING_USER_MASS]",
            //"portalBooleanProperties[PROFILE_VERIFICATION_ALLOW_BYPASS]",
            //"portalBooleanProperties[PROFILE_VERIFICATION_REQUIRED_FIELDS]",
            "portalBooleanProperties[SHOW_LICENSE_AGREEMENT]",
            //"portalBooleanProperties[USE_PORTAL_SPECIFIC_LICENSE_AGREEMENT]"
        };


        public static readonly List<string> AllPortalSettings = new List<string>()
        {
            "portalBooleanProperties[DISABLE_CV_MODULE_EDIT_FOR_STUDENTS]",
            "portalBooleanProperties[HIDE_CV_MODULE_FOR_STUDENTS]",
            "portalBooleanProperties[HIDE_COMPETENCE_DESCRIPTIONS_IN_CV]",
            "portalBooleanProperties[CV_HIDE_EDUCTION_CREDIT]",
            "portalBooleanProperties[CV_SHOW_DURATION_FOR_COURSES]",
            "portalBooleanProperties[SHOW_GENERATION_DATE_IN_CV_FOOTER]",
            "portalBooleanProperties[USE_CV_MODULE]",
            "portalBooleanProperties[COMPANY_PORTAL]",
            "portalBooleanProperties[USE_SOCIALMEDIA_INTEGRATION]",
            "portalBooleanProperties[SHOW_CHANGELOG]",
            "portalBooleanProperties[SHOW_STUDENT_COURSE_CATALOGUE]",
            "portalBooleanProperties[SHOW_IN_PORTAL_LIST]",
            "portalBooleanProperties[USE_SMS_MODULE]",
            "portalBooleanProperties[USE_CUSTOM_THEME]",
            "portalBooleanProperties[USE_EXPIRYDATE]",
            "portalBooleanProperties[USE_NEW_STUDENT_UI]",
            "portalBooleanProperties[USE_STUDENT_DASHBOARD]",
            "portalBooleanProperties[USE_SURVEY]",
            "portalBooleanProperties[CONNECT_COMPETENCE_ROLES_TO_ORGANIZATIONUNITS]",
            "portalBooleanProperties[ALLOW_LIMITED_ACCESS_TO_COMPETENCE_CATALOGUES]",
            "portalBooleanProperties[DENY_MANUAL_APPROVAL_OF_TRAINING_PACKAGES_AND_ROLES]",
            "portalBooleanProperties[HIDE_COMPETENCE_MODULE_FROM_STUDENT]",
            "portalBooleanProperties[HIDE_USER_EVIDENCE_LOCKER]",
            "portalBooleanProperties[DENY_ASSESSORS_FROM_MANUALLY_SETTING_COMPLETION_DATES]",
            "portalBooleanProperties[DENY_MANAGER_FROM_APPROVING_HIS_OWN_COURSES_AND_COMPETENCES]",
            "portalBooleanProperties[DENY_MANAGER_FROM_MANUALLY_APPROVE_TRAINING_PACKAGES]",
            "portalBooleanProperties[REQUIRE_EXPLICIT_ASSESSORS_FOR_COMPETENCEREQUIREMENTS]",
            "portalBooleanProperties[SHOW_MEDIUM_PRIORITY_CAPTION_IN_UI]",
            "portalBooleanProperties[USE_COMPETENCE_ASSESSMENT_PLAN]",
            "portalBooleanProperties[USE_COMPETENCE_CANDIDATES]",
            "portalBooleanProperties[USE_CHECKLIST]",
            "portalBooleanProperties[USE_COMPETENCE_MODULE]",
            "portalBooleanProperties[USE_COMPETENCE_TARGET_NUMBER]",
            "portalBooleanProperties[USE_COMPETENCE_SELF_ASSESSMENT]",
            "portalBooleanProperties[USE_COMPETENCE_VERIFIERS]",
            "portalBooleanProperties[HIDE_STATISTICS_ON_COMPETENCE_HOMEPAGE]",
            "portalBooleanProperties[CAN_SEE_PUBLISHER_TAB]",
            "portalBooleanProperties[HAS_PUBLISHER_LICENSE]",
            "portalBooleanProperties[USE_ASSESSMENTS]",
            "portalBooleanProperties[ALLOW_AICC_ACCESS_TO_COURSES]",
            "portalBooleanProperties[COURSE_ALLOW_SCORM_CLOUD]",
            "portalBooleanProperties[COURSE_ALLOW_APP_ACCESS_TO_OWN_COURSES]",
            "portalBooleanProperties[ALLOW_COURSE_ENROLLMENT_WITH_COURSCLASS_INFORMATION]",
            "portalBooleanProperties[COURSE_ALLOW_INVITATION_STUDENTS_TO_COURSE_CLASS]",
            "portalBooleanProperties[ALLOW_MANAGER_COURSE_APPROVAL]",
            "portalBooleanProperties[ALLOW_XAPI_ACCESS_TO_COURSES]",
            "portalBooleanProperties[PREVENT_CREATE_COURSE]",
            "portalBooleanProperties[DENY_MANAGER_FROM_ENROLLING_TO_COURSES]",
            "portalBooleanProperties[USE_CHECKLISTS_ON_COURSES]",
            "portalBooleanProperties[USE_INSTRUCTORS_ON_COURSES]",
            "portalBooleanProperties[COURSE_USE_ORGANIZATION_UNIT_SPECIFIC_REFRESHER_INTERVALS]",
            "portalBooleanProperties[ALLOW_DELETION_OF_ENROLLMENTS_WITH_COURSE_REGISTRATION_ON_ECOMMERCE_COURSES]",
            "portalBooleanProperties[ALWAYS_SHOW_INVOICE_INFORMATION]",
            "portalBooleanProperties[PAYMENTINFORMATION_FOR_ECOMMERCE_ENROLLMENTS_REQUIRED]",
            "portalBooleanProperties[SEARCH_FOR_BUYABLE_COURSES_BY_DEFAULT_FOR_STUDENT]",
            "portalBooleanProperties[USES_OCS_INTEGRATION]",
            "portalBooleanProperties[LL_ACCESS_FROM_TRAININGPORTAL]",
            "portalBooleanProperties[DO_NOT_SHOW_ENROLLMENTS_FROM_OTHER_PORTALS_IN_THE_COURSE_STATUS_REPORT]",
            "portalBooleanProperties[SHOW_REPORT_COURSE_STATUS_FOR_COURSE_PROVIDERS]",
            "portalBooleanProperties[SHOW_MEDIUM_PRIORITY_CAPTION_IN_REPORTS]",
            "portalBooleanProperties[SSO_USER_DENY_CREATE_NEW]",
            "portalBooleanProperties[SSO_ENABLED]",
            "portalBooleanProperties[SSO_USE_SAML_BASED]",
            "portalBooleanProperties[SPIDER_SYNC_USERS]",
            "portalBooleanProperties[USE_TMS_MODULE]",
            "portalBooleanProperties[ALLOW_STUDENTS_TO_REENROLL_TO_ECOMMERCE_COURSES]",
            "portalBooleanProperties[REMOTE_SITES_USES_ALL_COURSES_AVAILABLE_ON_PORTAL]",
            "portalBooleanProperties[USE_CHECKLISTS_ON_TRAININGPORTAL_OFFLINE]",
            "portalBooleanProperties[USE_REMOTE_SITES]",
            "portalBooleanProperties[USE_COMPETENCE_LEVEL_ON_TPO]",
            "portalBooleanProperties[ALLOW_LOGIN_TO_USERS_REGISTERED_IN_PORTAL_OWNER_COMPANY]",
            "portalBooleanProperties[ALLOW_SELF_REGISTRATION]",
            "portalBooleanProperties[DELETE_USER_WHEN_APPROVAL_IS_DENIED]",
            "portalBooleanProperties[MERGE_USER_ACCOUNTS_DISABLED]",
            "portalBooleanProperties[DENY_MANAGER_FROM_CHANGING_USER_MASS]",
            "portalBooleanProperties[PROFILE_VERIFICATION_ALLOW_BYPASS]",
            "portalBooleanProperties[PROFILE_VERIFICATION_REQUIRED_FIELDS]",
            "portalBooleanProperties[SHOW_LICENSE_AGREEMENT]",
            "portalBooleanProperties[USE_PORTAL_SPECIFIC_LICENSE_AGREEMENT]"


        };






        // email templates for each type of portal
        public static readonly List<IEmailTemplate> basicEmailTemplates = new List<IEmailTemplate>()
        {
            new CourseCompletionWithCertTemplate(),
            new EnrolmentToBlendedCourseTemplate(),
            new EnrolmentToClassroomCourseTemplate(),
            new EnrolmentToCourseTemplate(),
            new EnrolmentToElearningCourseWithDirectLink(),
            new ReminderToCompleteElearningCourseTemplate(),
            new RepetitionRequirementCourseTemplate()
           
        };


        public static readonly List<IEmailTemplate> advancedEmailTemplates = new List<IEmailTemplate>()
        {
            new CourseCompletionWithCertTemplate(),
            new EnrolmentToBlendedCourseTemplate(),
            new EnrolmentToClassroomCourseTemplate(),
            new EnrolmentToCourseTemplate(),
            new EnrolmentToElearningCourseWithDirectLink(),
            new ReminderToCompleteElearningCourseTemplate(),
            new RepetitionRequirementCourseTemplate(),

            new CompetenceRequirementsTemplate(),
            new RepetitionCompetenceTemplate(),
            new RepetitionRequirementsForAdminTemplate()
        };


        public static readonly List<IEmailTemplate> controlRisksEmailTemplates = new List<IEmailTemplate>()
        {
            new CREnrolmentToElearningCourseWithDirectLink(),
            new CRReminderToCompleteElearningCourseTemplate()

        };


        public static readonly List<string> orgUnits = new List<string>()
        {
            "Location 1", "Location 2", "Department 1", "Department 2"
        };


        public static readonly Dictionary<string, string> ControlRisksCourses = new Dictionary<string, string>()
        {
            { "Active Shooter","32039" },
            { "Advanced Security Training","32612" },
            { "Introduction to Kidnap","32614" },
            { "Virtual Kidnap","32038" }

        };




    }
}
