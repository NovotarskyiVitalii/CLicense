using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CLicense.Models
{
    public enum Sex
    {
        man = 1,
        women = 2
    }
    public class CLicenseModel
    {
        const string kirilicpatern = @"[А-Яа-яЇїЄєІі''""]+";
        [Display(Name = "Ідентифікаційний номер")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Невірно заданий ІНН")]
        [StringLength(10, MinimumLength = 10)]
        public string TaxCode { set; get; }
        [StringLength(2,MinimumLength =2)]
        [RegularExpression(@"[А-Яа-я]+", ErrorMessage = "Невірна серія паспорту")]
        [Display(Name = "Серія паспорту")]
        public string DocSeries { set; get; }
        [StringLength(6)]
        [RegularExpression(@"\d{6}", ErrorMessage = "В номері паспорту повинно бути 6 сммволів")]
        [Display(Name = "Номер паспорту")]
        public string DocNum { set; get; }
        [Display(Name = "Прізвище")]
        [Required]
        [RegularExpression(kirilicpatern, ErrorMessage = "Невірна строка")]
        public string LastName { set; get; }
        [RegularExpression(kirilicpatern, ErrorMessage = "Невірна строка")]
        [Display(Name = "Ім'я")]
        [Required]
        public string FirstName { set; get; }
        [Display(Name = "По батькові")]
        [Required]
        [RegularExpression(kirilicpatern, ErrorMessage = "Невірна строка")]
        public string MiddleName { set; get; }
        [Display(Name = "Дата народження")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Required]
        public string Birthday { set; get; }
        [Display(Name = "Стать")]
        public int SexID { set; get; }
        
        [Display(Name = "Код банку одержувача")]
        [Required(ErrorMessage = "Невірна строка")]
        public string CorBankCode { set; get; }
        [Display(Name = "Банк одержувача")]
        [Required(ErrorMessage = "Невірна строка")]
        public string CorBankName { set; get; }
        [Display(Name = "Країна банку одержувача")]
        [Required(ErrorMessage = "Невірна строка")]
        public int CountryID { set; get; }
        [Required(ErrorMessage = "Незаданий реквізит")]
        [Display(Name = "Країна одержувача")]
        public int CorCountryID { set; get; }
        [Display(Name = "Одержувач")]
        [Required(ErrorMessage = "Невірна строка")]
        public string CorName { set; get; }
        [Display(Name = "Призначення платежу")]
        [Required(ErrorMessage = "Незаданий реквізит")]
        public int PurposeID { set; get; }
        [Display(Name = "Код валюти")]
        [Required(ErrorMessage = "Незаданий реквізит")]
        public int CurrencyID { set; get; }
        //[RegularExpression(@"\d+", ErrorMessage = "Незаведена сума")]
        [Range(1, 50000, ErrorMessage = "Невірна сума")]
        [Required]
        //[RequiredIf("Limit > 0", true,ErrorMessage ="okey!")]
        [Display(Name = "Сума")]
        public int Amount { set; get; }
        [Display(Name = "Примітка")]
        public string Note { set; get; }
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public bool SaveController { set; get; }
        public int Limit { set; get; }
        public int? GranteeRowID { set; get; }
        public int? RowID { set; get; }

        public bool ValidateTaxCode(int sex)
        {
            DateTime dateborn;
            int daysfrom1900 = 0;
            if (Int32.TryParse(TaxCode.Substring(0, 5), out daysfrom1900))
                dateborn = new DateTime(1900, 1, 1).AddDays(daysfrom1900 - 1);
            else
                return false;
            if (TaxCode.Length < 10) return false;
            if (Birthday == null) return false;
            int identitySex = Convert.ToInt16(TaxCode.Substring(8, 1)) % 2;
            bool testSex = (identitySex == 0 && sex == (int)Sex.women) || (identitySex != 0 && (int)sex == (int)Sex.man);
            bool testDateBorn = dateborn == Convert.ToDateTime(DateTime.ParseExact(this.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture));

            return testSex && testDateBorn;
        }
        public bool ValidateFirstName()
        {
            return string.IsNullOrEmpty(FirstName);
        }
        public bool ValidateLastName()
        {
            return string.IsNullOrEmpty(LastName);
        }
        public bool ValidateMiddleName()
        {
            return string.IsNullOrEmpty(MiddleName);
        }
    }
    public class ClientIdentity
    {
        public string TaxCode { get; set; }
        public string DocSeries { get; set; }
        public string DocNum { get; set; }
    }
    
}