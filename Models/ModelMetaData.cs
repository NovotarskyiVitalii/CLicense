using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CLicense.Models
{
    [MetadataType(typeof(GrantLicenseMeta))]
    public partial class GrantLicense
    {
    }
    public partial class GrantLicenseMeta
    {
        [Key]
        public int RowID { get; set; }
    }

    [MetadataType(typeof(spLicenseSeek_ResultMeta))]
    public partial class spLicenseSeek_Result
    {
    }
    public partial class spLicenseSeek_ResultMeta
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Дата ліцензії")]
        public DateTime OperDate { get; set; }
        [Display(Name = "Номер ліцензії")]
        public int RowID { get; set; }
        [Display(Name = "ПІБ клієнта")]
        public string ClientName { get; set; }
        [Display(Name = "Ідентифікаційгий номер")]
        public string TaxCode { get; set; }
        [Display(Name = "Серія паспорту")]
        public string DocSeries { set; get; }
        [Display(Name = "Номер паспорту")]
        public string DocNum { set; get; }

        [Display(Name = "Код банку одержувача")]
        public string CorBankCode { set; get; }
        [Display(Name = "Банк одержувача")]
        public string CorBankName { set; get; }
        [Display(Name = "Одержувач")]
        public string CorName { set; get; }
        [Display(Name = "Країна")]
        public string CountryName { set; get; }

        [Display(Name = "Код валюти")]
        public string CurrencyCode { set; get; }
        [Display(Name = "Назва валюти")]
        public string CurrencyName { set; get; }
        [Display(Name = "Сума")]
        public int Amount { set; get; }
        [Display(Name = "Призначення")]
        public string Purpose { set; get; }
        [Display(Name = "Створений")]
        public string CreateUser { set; get; }
        [Display(Name = "Примітка")]
        public string Note { set; get; }

    }

    [MetadataType(typeof(sprpLicenseList_ResultMeta))]
    public partial class sprpLicenseList_Result
    {
    }
    public partial class sprpLicenseList_ResultMeta
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Дата")]
        public DateTime OperDate { get; set; }
        [Display(Name = "ПІБ клієнта")]
        public string GranteeName { get; set; }
        [Display(Name = "Ідентифікаційний номер")]
        public string TaxCode { get; set; }
        //[Display(Name = "Серія паспорту")]
        //public string DocSeries { set; get; }
        [Display(Name = "Номер паспорту")]
        public string DocNum { set; get; }
        [Display(Name = "Дата народження")]
        public string Birthday { set; get; }
        [Display(Name = "Код банку одержувача")]
        public string CorBankCode { set; get; }
        [Display(Name = "Уповноважений банк")]
        public string BankName_Sender { set; get; }
        [Display(Name = "Країна банку одержувача")]
        public int CorBankCountry { set; get; }
        [Display(Name = "Країна одержувача")]
        public int CorCountry { set; get; }
        [Display(Name = "Одержувач")]
        public string CorName { set; get; }
        [Display(Name = "Банк одержувача")]
        public int CorBankName { set; get; }
        [Display(Name = "Призначення платежу")]
        public int PurposeName { set; get; }
        [Display(Name = "Код валюти")]
        public int CurrencyName { set; get; }
        [Display(Name = "Сума")]
        public int Amount { set; get; }
        //[Display(Name = "Примітка")]
        //public string Note { set; get; }

    }
}