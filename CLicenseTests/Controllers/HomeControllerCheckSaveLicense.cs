using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLicense.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLicense.Models;
using System.Web.Mvc;
using CLicense.DAL;
using CLicense.Infrastructure;
using Moq;
using Newtonsoft.Json;
using System.Web;

namespace CLicense.Controllers.Tests
{
    public class sex : ISex
    {
        vItemInfo _item;
        public IEnumerable<vItemInfo> All
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public vItemInfo Item
        {
            get
            {
                if (_item == null)
                    _item = new vItemInfo { IID = 0 };
                return _item;
            }
        }
    }
    public class item
    {

    }
    public class granty : IRGrantee
    {
        public spGetGrantee_Result Grantee
        {
            get
            {
                return new spGetGrantee_Result();
            }
        }
    }

    [TestClass()]
    public class HomeControllerCheckSaveLicense
    {
        [TestMethod()]
        public void CheckSaveLicenseCheckSaveLicense()
        {

            Mock<HttpSessionStateBase> session = new Mock<HttpSessionStateBase>();
            session.SetupGet(s => s["BankID"]).Returns(99999999);
            session.SetupGet(s => s["UserID"]).Returns(9999);
            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(c1 => c1.Session).Returns(session.Object);

            Mock<IRepository> mockRepository = new Mock<IRepository>();

            CLicenseController c = new CLicenseController(mockRepository.Object);
            ControllerContext cc = new ControllerContext();
            c.ControllerContext = cc;
            c.ControllerContext.HttpContext = httpContext.Object;

            mockRepository.Setup(m => m.Sex)
           .Returns(new sex());
            CLicenseModel model = new CLicenseModel {TaxCode= "2364011816",Birthday= "1961-12-26" };
            ActionResult ar = c.CheckSaveLicense(model);
            JsonResult js = ar as JsonResult;
            var js1 = JsonConvert.SerializeObject(new { Code = -1, ErrorMessage = "Невідповідність дати народжепння або року народженння з ідентифікаційним номером" });
            var js2 = JsonConvert.SerializeObject(js.Data);
            Assert.AreEqual(js1, js2);

            model = new CLicenseModel { TaxCode = "2264011816", Birthday = "1962-12-26" };
            ar = c.CheckSaveLicense(model);
            js = ar as JsonResult;
            js1 = JsonConvert.SerializeObject(new { Code = -1, ErrorMessage = "Невідповідність дати народжепння або року народженння з ідентифікаційним номером" });
            js2 = JsonConvert.SerializeObject(js.Data);
            Assert.AreEqual(js1, js2);

            mockRepository.Setup(m => m.Grantee)
            .Returns(new granty());
            mockRepository.Setup(m => m.CheckLicense(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(1);
            model = new CLicenseModel { TaxCode = "2264011816", Birthday = "1961-12-26" };
            ar = c.CheckSaveLicense(model);
            js = ar as JsonResult;
            js1 = JsonConvert.SerializeObject(new { Code = 0, ErrorMessage = "" });
            js2 = JsonConvert.SerializeObject(js.Data);
            Assert.AreEqual(js1, js2);

            mockRepository.Setup(m => m.CheckLicense(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(-1);

            mockRepository.Setup(m => m.ErrorMessage)
            .Returns("test");
            ar = c.CheckSaveLicense(model);
            js = ar as JsonResult;
            js1 = JsonConvert.SerializeObject(new { Code = -1, ErrorMessage = "test" });
            js2 = JsonConvert.SerializeObject(js.Data);
            Assert.AreEqual(js1, js2);

            string strreturn = null;
            mockRepository.Setup(m => m.ErrorMessage)
            .Returns(strreturn);
            ar = c.CheckSaveLicense(model);
            js = ar as JsonResult;
            js1 = JsonConvert.SerializeObject(new { Code = -1, ErrorMessage = "Перевищений розмір ліміту" });
            js2 = JsonConvert.SerializeObject(js.Data);
            Assert.AreEqual(js1, js2);

            //Assert.Fail();
        }
    }
}