using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using authUserLib2;
using CLicense.DAL;
using CLicense.Infrastructure;
using CLicense.Utils;

namespace CLicense
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(BuildManager.GetGlobalAsaxType().BaseType.Assembly.Location);
            var version = fvi.FileVersion;
            log4net.GlobalContext.Properties["App_Name"] = "CLicense" + version;

            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            //if (!Request.Browser.Type.ToLower().Contains("chrome"))
            //    HttpContext.Current.Response.Redirect("~/CLicense/ErrorBrowser", false);
            //else
            //{
            //    if (Session["Version"] == null)
            //        Session["Version"] = typeof(MvcApplication).Assembly.GetName().Version;
            //}
        }
        protected void Application_PostRequestHandlerExecute(Object sender, EventArgs e)
        {
#if !test
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserID"] == null)
            {
                UserInfo2 MVC_userCred = (UserInfo2)HttpContext.Current.Items["MVC_userCred"];
                //if (MVC_userCred == null)
                //{
                //    HttpContext.Current.Response.Redirect("~/CLicense/ErrorGetUser?message=Помилка зчитування сертифікату", false);
                //    return;
                //}
                int? userID;
                int? BankID;
                int dd = Convert.ToInt32(Session["UserID"]);
                IRepository r = new Repository();
                string errormessage = r.GetUser(
                    userCode: MVC_userCred.certInfo.NBUCertId,
                    userTaxCode: null,
                    userName: MVC_userCred.certInfo.userPIB,
                    post: MVC_userCred.certInfo.Title,
                    eMail: MVC_userCred.certInfo.officialEmail,
                    userID: out userID,
                    bankID: out BankID
                    );
                if (errormessage != null)
                    HttpContext.Current.Response.Redirect("~/CLicense/ErrorGetUser?message=" + errormessage, false);
                else
                {
                    Session["UserID"] = userID;
                    Session["BankID"] = BankID;
                    Session["userPIB"] = MVC_userCred.certInfo.userPIB;
                    Session["Version"] = typeof(MvcApplication).Assembly.GetName().Version;
                    //HttpContext.Current.Response.Redirect("~/CLicense/Index", false);
                }
            }
#else
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserID"] == null)
            {
                Session["UserID"] = 1;
                Session["BankID"] = 16;
                Session["userPIB"] = "Тестовий користувач";
            }
#endif
        }
    }
}
