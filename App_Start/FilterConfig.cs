﻿//#define test
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using libWebFilters2.WebMVCRequestFilters;
namespace CLicense
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
#if !test
            string domain = "extern";
            string container = "OU=IND_VALLIC,OU=ReestrLic,OU=!CA_Servers,DC=extern,DC=bank,DC=gov,DC=ua";
            //string login = "certuser";
            //string pass = "123456q!";
            string login = WebConfigurationManager.AppSettings["Certuser"].ToString();
            string pass = WebConfigurationManager.AppSettings["PasswordCertuser"].ToString();

            //filters.Add(new MVCUserAuthenticationAttribute(domain, container, login, pass, true, true));
            filters.Add(new MVCUserAuthenticationL4NAttribute(domain, container, login, pass));
            filters.Add(new MVCExceptionL4NAttribute());


#endif
            //filters.Add(new MVCUserAuthenticationL4NAttribute(domain, container, login, pass, true, true));
            //filters.Add(new MVCExceptionL4NAttribute());
        }
    }
}
