using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CLicense.DAL;
using System.Text;
using CLicense.Models;
using CLicense.Infrastructure;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using authUserLib2;

namespace CLicense.Controllers
{
    public class CLicenseController : Controller
    {
        IRepository _r;
        public CLicenseController(IRepository r)
        {
            _r = r;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            ViewBag.Message = "Опис системи";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакти.";

            return View();
        }

        public ActionResult CreateLicense()
        {
            CLicenseModel model;
            if (TempData["Model"] != null) model = (CLicenseModel)TempData["Model"];
            else model = new CLicenseModel();
            _r.CLicenseModel = model;

            ViewBag.PurposeList = new SelectList(_r.Purpose.All.OrderBy(x => x.ItemName), "IID", "ItemName");
            ViewBag.SexList = new SelectList(_r.Sex.All.OrderBy(x => x.ItemName), "IID", "ItemName");
            ViewBag.CountryList = new SelectList(_r.Country.All.OrderBy(x => x.ItemName), "IID", "ItemName");
            ViewBag.CurrencyList = new SelectList(_r.Currensy.All.OrderBy(x => x.ItemName), "IID", "ItemName");
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckSaveLicense(CLicenseModel model)
        {
            _r.CLicenseModel = model;

            if (model.SaveController)
            {
                int rowid = _r.AddLicense(_r.Grantee.Grantee.RowID, Convert.ToInt32(Session["BankID"]), Convert.ToInt32(Session["UserID"]));
                if (rowid == -1)
                    return Json(new { Code = -1, ErrorMessage = _r.ErrorMessage });
                else
                {
                    TempData["RowID"] = rowid;
                    model.RowID = rowid;
                    return Json(new { Code = -999, ErrorMessage = _r.ErrorMessage, RowID = rowid });
                }
            }
            else
            {
                int sexMan = _r.Sex.Item.IID;
                if (!model.ValidateTaxCode(model.SexID == sexMan ? (int)Sex.man : (int)Sex.women))
                {
                    return Json(new { Code = -1, ErrorMessage = "Невідповідність дати народжепння або року народженння з ідентифікаційним номером" });
                }

                if (_r.Grantee.Grantee == null)
                {
                    model.GranteeRowID = _r.AddGrantee(Convert.ToInt32(Session["UserID"]));
                    if (model.GranteeRowID == -1)
                    {
                        return Json(new { Code = -1, ErrorMessage = _r.ErrorMessage });
                    }
                }
                else
                    model.GranteeRowID = _r.Grantee.Grantee.RowID;
                if (_r.CheckLicense(model.GranteeRowID, Convert.ToInt32(Session["BankID"]), Convert.ToInt32(Session["UserID"])) < 0)
                    if (_r.ErrorMessage == null)
                        return Json(new { Code = -1, ErrorMessage = "Перевищений розмір ліміту" });
                    else
                        return Json(new { Code = -1, ErrorMessage = _r.ErrorMessage });
                else
                    return Json(new { Code = 0, ErrorMessage = "" });
            }
        }
        [HttpPost]
        public JsonResult GetGrantee(string taxCode)
        {
            CLicenseModel model = new CLicenseModel();
            model.TaxCode = taxCode;
            _r.CLicenseModel = model;

            spGetGrantee_Result g = _r.Grantee.Grantee;
            if (g != null)
            {
                model.FirstName = g.FirstName;
                model.LastName = g.LastName;
                model.MiddleName = g.MiddleName;
                if (g.Birthday != null)
                {
                    DateTime b = Convert.ToDateTime(g.Birthday);
                    model.Birthday = new DateTime(b.Year, b.Month, b.Day).ToString("yyyy-MM-dd"); ;
                }
                model.DocNum = g.DocNum;
                model.DocSeries = g.DocSeries;
                model.SexID = (int)g.SexID;
                model.GranteeRowID = g.RowID;

                model.Limit = _r.GetLimit(g.RowID, Convert.ToInt32(Session["BankID"]), Convert.ToInt32(Session["UserID"]));
            }

            return Json(model);
        }
        public JsonResult GetLimit(int? granteeRowID, int currencyID)
        {
            CLicenseModel model = new CLicenseModel();
            _r.CLicenseModel = model;

            model.CurrencyID = currencyID;
            if (granteeRowID != null)
            {
                //model.Limit = _r.CheckLicense(granteeRowID, Convert.ToInt32(Session["BankID"]), Convert.ToInt32(Session["UserID"]));
                model.Limit = _r.GetLimit(granteeRowID, Convert.ToInt32(Session["BankID"]), Convert.ToInt32(Session["UserID"]));
                if ((int)model.Limit < 0)
                    if (_r.ErrorMessage == null)
                        return Json(new { Code = -1, ErrorMessage = "Перевищений розмір ліміту" });
                    else
                        return Json(new { Code = -1, ErrorMessage = _r.ErrorMessage });
                else
                    return Json(model);
            }
            return Json(model);
        }


        [HttpGet]
        public ActionResult CancelLicense()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CancelLicense(string taxCode, string docSeries, string docNum, DateTime? startDate, DateTime? finalDate)
        {
            return View(_r.GrantLicense.GetLicenses(
                userID: Convert.ToInt32(Session["UserID"]),
                taxCode: taxCode,
                DocNum: docNum,
                DocSeries: docSeries,
                startDate: startDate,
                finalDate: finalDate
                )
                );
        }

        [HttpGet]
        public ActionResult CancelClientLicense(int rowid)
        {
            TempData["RowID"] = rowid;
            ViewBag.CancelReasonList = new SelectList(_r.CancelReason.All.OrderBy(x => x.ItemName), "IID", "ItemName");
            sprpLicenseList_Result model = _r.GrantLicense.GetLicense(rowid);
            TempData["TaxCode"] = model.TaxCode;
            return View(model);
        }
        [HttpPost]
        public ActionResult CancelClientLicense(string cancelReason, string rowid, string TaxCode)
        {
            if (string.IsNullOrEmpty(cancelReason)) return View();
            _r.CancelLicense(Convert.ToInt32(rowid), Convert.ToInt32(cancelReason), Convert.ToInt32(Session["UserID"]));
            TempData["TaxCode"] = TaxCode;
            return RedirectToAction("CancelLicense");
        }
        public ActionResult ErrorGetUser(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        public ActionResult LicenseOfUser()
        {
            return View();
        }
        public ActionResult ReportLicensePersonel()
        {
            if (TempData["RowID"] == null) return RedirectToAction("Index");
            ReportParameterCollection arr_pp = new ReportParameterCollection();
            ReportParameter pp = new ReportParameter("RowID", TempData["RowID"].ToString());
            arr_pp.Add(pp);
            TempData.Add("ReportParams", arr_pp);
            TempData.Add("ReportName", "LicenseInfo");
            return View("Reports");
        }
        [HttpPost]
        public ActionResult ReportLicensesPersonel(string taxCode, string docSeries, string docNum, DateTime? startDate, DateTime? finalDate)
        {
            CLicenseModel model = new CLicenseModel();
            model.TaxCode = taxCode;
            model.DocNum = docNum;
            model.DocSeries = docSeries;
            _r.CLicenseModel = model;
            spGetGrantee_Result g = _r.Grantee.Grantee;
            if (g != null)
            {
                ReportParameterCollection arr_pp = new ReportParameterCollection();
                ReportParameter pp1 = new ReportParameter("GranteeID", g.RowID.ToString());
                ReportParameter pp2 = new ReportParameter("UserID", Session["UserID"].ToString());
                ReportParameter pp3 = new ReportParameter("StartDate", (startDate!=null ?  startDate.ToString() : null));
                ReportParameter pp4 = new ReportParameter("FinalDate", (finalDate != null ? finalDate.ToString() : null));
                arr_pp.Add(pp1);
                arr_pp.Add(pp2);
                arr_pp.Add(pp3);
                arr_pp.Add(pp4);
                TempData.Clear();
                TempData.Add("ReportParams", arr_pp);
                TempData.Add("ReportName", "LicenseOfUser");
                return View("Reports");
            }
            else
            {
                return Redirect("NotFoundClient");
            }
        }
        public ActionResult NotFoundClient(string taxCode)
        {
            return View();
        }
        public ActionResult ErrorBrowser(string taxCode)
        {
            return View();
        }
    }
}