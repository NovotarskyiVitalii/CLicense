using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Microsoft.Reporting.WebForms;

namespace CLicense.Controllers
{
    public class TestController : Controller
    {
        [WebMethod]
        public ActionResult Index()
        {
            ReportParameterCollection arr_pp = new ReportParameterCollection();
            ReportParameter pp1 = new ReportParameter("GranteeID", "1");
            ReportParameter pp2 = new ReportParameter("UserID", "1");
            ReportParameter pp3 = new ReportParameter("StartDate", "2001-1-1");
            ReportParameter pp4 = new ReportParameter("FinalDate", "2100-1-1");
            arr_pp.Add(pp1);
            arr_pp.Add(pp2);
            arr_pp.Add(pp3);
            arr_pp.Add(pp4);
            TempData.Add("ReportParams", arr_pp);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}