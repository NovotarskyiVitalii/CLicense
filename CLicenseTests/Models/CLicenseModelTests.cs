using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLicense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLicense.Models.Tests
{
    
    [TestClass()]
    public class CLicenseModelTests
    {
        [TestMethod()]
        public void ValidateTaxCodeTest()
        {
            CLicenseModel m = new CLicenseModel { TaxCode = "2264011816", Birthday = "26.12.1961", SexID = (int)Sex.man };

            Assert.IsTrue(m.ValidateTaxCode((int)Sex.man));

            m = new CLicenseModel { TaxCode = "2264011816", Birthday = "26.12.1961", SexID = (int)Sex.women };

            Assert.IsFalse(m.ValidateTaxCode((int)Sex.women));

            m = new CLicenseModel { TaxCode = "2264011816", Birthday = "25.12.1961", SexID = (int)Sex.women };

            Assert.IsFalse(m.ValidateTaxCode((int)Sex.women));
        }
    }
}