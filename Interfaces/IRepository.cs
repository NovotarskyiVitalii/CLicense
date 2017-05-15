using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLicense.DAL;
using CLicense.Models;

namespace CLicense.Infrastructure
{
    public interface IRepository
    {
        IRGrantLicense GrantLicense { get; }
        IRGrantee Grantee { get; }
        ICurrensy Currensy { get; }
        ICountry Country { get; }
        IPurpose Purpose { get; }
        ICancelReason CancelReason { get; }
        ILicenseStatus LicenseStatus { get; }
        ISex Sex { get; }

        int CancelLicense(int licenseID, int cancelReasonID, int userID);
        int AddLicense(int granteeID, int bankID, int userID);
        int AddGrantee(int userID);
        int CheckLicense(int? granteeID, int bankID, int userID);
        int GetLimit(int? granteeID, int bankID, int userID);
        string GetUser(string userCode, string userTaxCode, string userName, string post, string eMail, out int? userID, out int? bankID);
        string ErrorMessage { set; get; }
        CLicenseModel CLicenseModel { set; get; }
    }
    public interface IRGrantLicense
    {
        IEnumerable<spLicenseSeek_Result> GetLicenses(int userID, string taxCode=null, string DocSeries=null, string DocNum=null,
                                DateTime? startDate=null, DateTime? finalDate=null);
        sprpLicenseList_Result GetLicense(int rowid);
    }

    public interface IRGrantee
    {
        spGetGrantee_Result Grantee { get;}
    }
    public interface ICountry : IBase
    {
    }
    public interface ICurrensy
    {
        spCurrencyListCombo_Result Item { get; }
        IEnumerable<spCurrencyListCombo_Result> All { get; }
    }
    public interface IPurpose : IBase
    {
    }
    public interface ICancelReason : IBase
    {
    }
    public interface ILicenseStatus : IBase
    {
    }
    public interface ISex : IBase
    {
    }
    public interface IBase
    {
        vItemInfo Item { get; }
        IEnumerable<vItemInfo> All { get; }
    }
}

