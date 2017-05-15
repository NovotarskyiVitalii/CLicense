﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CLicense.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CLicenseEntities1 : DbContext
    {
        public CLicenseEntities1()
            : base("name=CLicenseEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<vItemInfo> vItemInfo { get; set; }
    
        public virtual ObjectResult<spGetGrantee_Result> spGetGrantee(string taxCode, string docSeries, string docNum)
        {
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            var docSeriesParameter = docSeries != null ?
                new ObjectParameter("DocSeries", docSeries) :
                new ObjectParameter("DocSeries", typeof(string));
    
            var docNumParameter = docNum != null ?
                new ObjectParameter("DocNum", docNum) :
                new ObjectParameter("DocNum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetGrantee_Result>("spGetGrantee", taxCodeParameter, docSeriesParameter, docNumParameter);
        }
    
        public virtual int spGranteeAdd(string lastName, string firstName, string middleName, string nameInt, string taxCode, string docSeries, string docNum, Nullable<int> sexID, Nullable<System.DateTime> birthday, string note, Nullable<int> userID, ObjectParameter rowID)
        {
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var middleNameParameter = middleName != null ?
                new ObjectParameter("MiddleName", middleName) :
                new ObjectParameter("MiddleName", typeof(string));
    
            var nameIntParameter = nameInt != null ?
                new ObjectParameter("NameInt", nameInt) :
                new ObjectParameter("NameInt", typeof(string));
    
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            var docSeriesParameter = docSeries != null ?
                new ObjectParameter("DocSeries", docSeries) :
                new ObjectParameter("DocSeries", typeof(string));
    
            var docNumParameter = docNum != null ?
                new ObjectParameter("DocNum", docNum) :
                new ObjectParameter("DocNum", typeof(string));
    
            var sexIDParameter = sexID.HasValue ?
                new ObjectParameter("SexID", sexID) :
                new ObjectParameter("SexID", typeof(int));
    
            var birthdayParameter = birthday.HasValue ?
                new ObjectParameter("Birthday", birthday) :
                new ObjectParameter("Birthday", typeof(System.DateTime));
    
            var noteParameter = note != null ?
                new ObjectParameter("Note", note) :
                new ObjectParameter("Note", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spGranteeAdd", lastNameParameter, firstNameParameter, middleNameParameter, nameIntParameter, taxCodeParameter, docSeriesParameter, docNumParameter, sexIDParameter, birthdayParameter, noteParameter, userIDParameter, rowID);
        }
    
        public virtual int spLicenseSet(Nullable<int> granteeID, Nullable<int> bankID, Nullable<int> currencyID, Nullable<decimal> amount, Nullable<int> countryID, string corBankCode, string corBankName, string corName, Nullable<int> corCountryID, Nullable<int> purposeID, string note, Nullable<int> userID, ObjectParameter licenseID)
        {
            var granteeIDParameter = granteeID.HasValue ?
                new ObjectParameter("GranteeID", granteeID) :
                new ObjectParameter("GranteeID", typeof(int));
    
            var bankIDParameter = bankID.HasValue ?
                new ObjectParameter("BankID", bankID) :
                new ObjectParameter("BankID", typeof(int));
    
            var currencyIDParameter = currencyID.HasValue ?
                new ObjectParameter("CurrencyID", currencyID) :
                new ObjectParameter("CurrencyID", typeof(int));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(decimal));
    
            var countryIDParameter = countryID.HasValue ?
                new ObjectParameter("CountryID", countryID) :
                new ObjectParameter("CountryID", typeof(int));
    
            var corBankCodeParameter = corBankCode != null ?
                new ObjectParameter("CorBankCode", corBankCode) :
                new ObjectParameter("CorBankCode", typeof(string));
    
            var corBankNameParameter = corBankName != null ?
                new ObjectParameter("CorBankName", corBankName) :
                new ObjectParameter("CorBankName", typeof(string));
    
            var corNameParameter = corName != null ?
                new ObjectParameter("CorName", corName) :
                new ObjectParameter("CorName", typeof(string));
    
            var corCountryIDParameter = corCountryID.HasValue ?
                new ObjectParameter("CorCountryID", corCountryID) :
                new ObjectParameter("CorCountryID", typeof(int));
    
            var purposeIDParameter = purposeID.HasValue ?
                new ObjectParameter("PurposeID", purposeID) :
                new ObjectParameter("PurposeID", typeof(int));
    
            var noteParameter = note != null ?
                new ObjectParameter("Note", note) :
                new ObjectParameter("Note", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spLicenseSet", granteeIDParameter, bankIDParameter, currencyIDParameter, amountParameter, countryIDParameter, corBankCodeParameter, corBankNameParameter, corNameParameter, corCountryIDParameter, purposeIDParameter, noteParameter, userIDParameter, licenseID);
        }
    
        public virtual ObjectResult<spLicenseSeek_Result> spLicenseSeek(Nullable<int> userID, string taxCode, string docSeries, string docNum, Nullable<bool> legalOnly, Nullable<System.DateTime> startDate, Nullable<System.DateTime> finalDate)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var taxCodeParameter = taxCode != null ?
                new ObjectParameter("TaxCode", taxCode) :
                new ObjectParameter("TaxCode", typeof(string));
    
            var docSeriesParameter = docSeries != null ?
                new ObjectParameter("DocSeries", docSeries) :
                new ObjectParameter("DocSeries", typeof(string));
    
            var docNumParameter = docNum != null ?
                new ObjectParameter("DocNum", docNum) :
                new ObjectParameter("DocNum", typeof(string));
    
            var legalOnlyParameter = legalOnly.HasValue ?
                new ObjectParameter("LegalOnly", legalOnly) :
                new ObjectParameter("LegalOnly", typeof(bool));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var finalDateParameter = finalDate.HasValue ?
                new ObjectParameter("FinalDate", finalDate) :
                new ObjectParameter("FinalDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLicenseSeek_Result>("spLicenseSeek", userIDParameter, taxCodeParameter, docSeriesParameter, docNumParameter, legalOnlyParameter, startDateParameter, finalDateParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> spGetLimitSum(Nullable<int> granteeID, Nullable<int> userID, Nullable<int> currencyID, Nullable<decimal> amount, ObjectParameter limitSaldo)
        {
            var granteeIDParameter = granteeID.HasValue ?
                new ObjectParameter("GranteeID", granteeID) :
                new ObjectParameter("GranteeID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var currencyIDParameter = currencyID.HasValue ?
                new ObjectParameter("CurrencyID", currencyID) :
                new ObjectParameter("CurrencyID", typeof(int));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("spGetLimitSum", granteeIDParameter, userIDParameter, currencyIDParameter, amountParameter, limitSaldo);
        }
    
        public virtual int spLicenseCancel(Nullable<int> licenseID, Nullable<int> userID, Nullable<int> cancelReasonID)
        {
            var licenseIDParameter = licenseID.HasValue ?
                new ObjectParameter("LicenseID", licenseID) :
                new ObjectParameter("LicenseID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var cancelReasonIDParameter = cancelReasonID.HasValue ?
                new ObjectParameter("CancelReasonID", cancelReasonID) :
                new ObjectParameter("CancelReasonID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spLicenseCancel", licenseIDParameter, userIDParameter, cancelReasonIDParameter);
        }
    
        public virtual ObjectResult<sprpLicenseList_Result> sprpLicenseList(Nullable<int> rowID, Nullable<System.DateTime> startDate, Nullable<System.DateTime> finalDate)
        {
            var rowIDParameter = rowID.HasValue ?
                new ObjectParameter("RowID", rowID) :
                new ObjectParameter("RowID", typeof(int));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var finalDateParameter = finalDate.HasValue ?
                new ObjectParameter("FinalDate", finalDate) :
                new ObjectParameter("FinalDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sprpLicenseList_Result>("sprpLicenseList", rowIDParameter, startDateParameter, finalDateParameter);
        }
    
        public virtual ObjectResult<spGetUser_Result> spGetUser(string userCode, string userTaxCode, string userName, string post, string eMail)
        {
            var userCodeParameter = userCode != null ?
                new ObjectParameter("UserCode", userCode) :
                new ObjectParameter("UserCode", typeof(string));
    
            var userTaxCodeParameter = userTaxCode != null ?
                new ObjectParameter("UserTaxCode", userTaxCode) :
                new ObjectParameter("UserTaxCode", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var postParameter = post != null ?
                new ObjectParameter("Post", post) :
                new ObjectParameter("Post", typeof(string));
    
            var eMailParameter = eMail != null ?
                new ObjectParameter("EMail", eMail) :
                new ObjectParameter("EMail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetUser_Result>("spGetUser", userCodeParameter, userTaxCodeParameter, userNameParameter, postParameter, eMailParameter);
        }
    
        public virtual int spGetLimit(Nullable<int> granteeID, Nullable<int> userID, ObjectParameter limitSaldo)
        {
            var granteeIDParameter = granteeID.HasValue ?
                new ObjectParameter("GranteeID", granteeID) :
                new ObjectParameter("GranteeID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spGetLimit", granteeIDParameter, userIDParameter, limitSaldo);
        }
    
        public virtual ObjectResult<spCurrencyListCombo_Result> spCurrencyListCombo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spCurrencyListCombo_Result>("spCurrencyListCombo");
        }
    }
}
