using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

namespace CLicense.Utils
{
    [Serializable()]
    public sealed class MyReportServerCredentials : IReportServerCredentials
    {
        public string userName = ConfigurationManager.AppSettings["rvUser"];
        public string password = ConfigurationManager.AppSettings["rvPassword"];
        string reportUser = WebConfigurationManager.AppSettings["ReportUser"].ToString();
        string reportUserPassword = WebConfigurationManager.AppSettings["ReportUserPassword"].ToString();

        public string domain = ConfigurationManager.AppSettings["rvDomain"];
        public WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                //return new NetworkCredential(@"nbu\i06353", "N_827852912v", domain);
                return new NetworkCredential(reportUser, reportUserPassword, domain);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie,
                   out string userName, out string password,
                   out string authority)
        {
            authCookie = null;
            userName = "ULicRpt";
            password = "@Reporter#";

            authority = null;
            return false;
        }
    }

}