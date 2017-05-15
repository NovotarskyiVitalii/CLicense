using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLicense.Infrastructure;
using CLicense.Models;

namespace CLicense.DAL
{
    public class Repository : IRepository
    {
        IRGrantLicense _grantLicense;
        IRGrantee _grantee;
        ICountry _country;
        ICurrensy _currensy;
        IPurpose _purpose;
        ISex _sex;
        ICancelReason _cancelReason;
        ILicenseStatus _licenseStatus;
        public CLicenseModel CLicenseModel { set; get; }
        CLicenseEntities1 _db = new CLicenseEntities1();
        public string ErrorMessage { set; get; }
        public Repository()
        {
            CLicenseModel = new CLicenseModel();
            _db = new CLicenseEntities1();
        }
        public IRGrantee Grantee
        {
            get
            {
                if (_grantee == null)
                    _grantee = new RGrantee(_db, CLicenseModel);
                return _grantee;
            }
        }
        public IRGrantLicense GrantLicense
        {
            get
            {
                if (_grantLicense == null)
                    _grantLicense = new RGrantLicense(_db);
                return _grantLicense;
            }
        }
        public ICountry Country
        {
            get
            {
                if (_country == null)
                    _country = new RCountry(_db);
                return _country;
            }
        }
        public ICurrensy Currensy
        {
            get
            {
                if (_currensy == null)
                    _currensy = new RCurrensy(_db);
                return _currensy;
            }
        }
        public ISex Sex
        //ISex IRepository.Sex
        {
            get
            {
                if (_sex == null)
                    _sex = new RSex(_db,itemcode:"M");
                return _sex;
            }
        }
        public IPurpose Purpose
        {
            get
            {
                if (_purpose == null)
                    _purpose = new RPurpose(_db);
                return _purpose;
            }
        }

        public ICancelReason CancelReason
        {
            get
            {
                if (_cancelReason == null)
                    _cancelReason = new RCancelReason(_db);
                return _cancelReason;
            }
        }

        public ILicenseStatus LicenseStatus
        {
            get
            {
                if (_licenseStatus == null)
                    _licenseStatus = new RLicenseStatus(_db);
                return _licenseStatus;
            }
        }

        public int AddLicense(int granteeID, int bankID, int userID)
        {
            ObjectParameter licenseID = new ObjectParameter("licenseID", typeof(int));
            try
            {
                _db.spLicenseSet(
                      granteeID: granteeID,
                      bankID: bankID,
                      currencyID: CLicenseModel.CurrencyID,
                      amount: CLicenseModel.Amount,
                      countryID: CLicenseModel.CountryID,
                      corBankCode: CLicenseModel.CorBankCode,
                      corBankName: CLicenseModel.CorBankName,
                      corName: CLicenseModel.CorName,
                      corCountryID: CLicenseModel.CorCountryID,
                      purposeID: CLicenseModel.PurposeID,
                      note: CLicenseModel.Note,
                      userID: userID,
                      licenseID: licenseID
                      );
                return Convert.ToInt32(licenseID.Value);

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.InnerException.Message;
                return -1;
            }
            
        }

        public int AddGrantee(int userID)
        {
            ObjectParameter rowid = new ObjectParameter("rowid", typeof(int));
            try
            {
                _db.spGranteeAdd(
                      lastName: CLicenseModel.LastName,
                      firstName: CLicenseModel.FirstName,
                      middleName: CLicenseModel.MiddleName,
                      nameInt: "",
                      taxCode: CLicenseModel.TaxCode,
                      docSeries: CLicenseModel.DocSeries,
                      docNum: CLicenseModel.DocNum,
                      sexID: CLicenseModel.SexID,
                      birthday: Convert.ToDateTime(CLicenseModel.Birthday),
                      note: CLicenseModel.Note,
                      userID: userID,
                      rowID: rowid
                      );
                return Convert.ToInt32(rowid.Value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.InnerException.Message;
                return -1;
            }
            
        }

        public int CheckLicense(int? granteeID, int bankID, int userID)
        {
            ObjectParameter limitSaldo = new ObjectParameter("LimitSaldo", typeof(int));
            try
            {
                _db.spGetLimitSum(
                      granteeID: granteeID,
                      userID: userID,
                      currencyID: CLicenseModel.CurrencyID,
                      amount: CLicenseModel.Amount,
                      limitSaldo: limitSaldo
                      );
                return Convert.ToInt32(limitSaldo.Value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.InnerException.Message;
                return -1;
            }
        }
        public int GetLimit(int? granteeID, int bankID, int userID)
        {
            ObjectParameter limitSaldo = new ObjectParameter("LimitSaldo", typeof(int));
            try
            {
                _db.spGetLimit(
                      granteeID: granteeID,
                      userID: userID,
                      limitSaldo: limitSaldo
                      );
                return Convert.ToInt32(limitSaldo.Value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.InnerException.Message;
                return -1;
            }
        }
        public int CancelLicense(int licenseID, int cancelReasonID, int userID)
        {
            ObjectParameter limitSaldo = new ObjectParameter("LimitSaldo", typeof(int));
            try
            {
                _db.spLicenseCancel(
                      licenseID: licenseID,
                      userID: userID,
                      cancelReasonID: cancelReasonID
                      );
                return Convert.ToInt32(limitSaldo.Value);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.InnerException.Message;
                return -1;
            }
        }

        public string GetUser(string userCode, string userTaxCode, string userName, string post, string eMail, out int? userID, out int? bankID)
        {
            string returnstring;
            userID = null;
            bankID = null;
            try
            {
                spGetUser_Result user = _db.spGetUser(
                    userCode: userCode,
                    userTaxCode: userTaxCode,
                    userName:userName,
                    post: post,
                    eMail:eMail
                      ).SingleOrDefault();
                userID = user.UserID;
                bankID = user.BankID;
                returnstring = null;
            }
            catch (Exception ex)
            {
                returnstring = ex.InnerException.Message;
            }
            return returnstring;
        }
    }
    public class RGrantee : IRGrantee
    {
        spGetGrantee_Result _grantee;
        CLicenseEntities1 _db;
        public RGrantee(CLicenseEntities1 db, CLicenseModel model)
        {
            _db = db;
            _grantee = GetGrantee(model);
        }

        spGetGrantee_Result GetGrantee(CLicenseModel model)
        {
            return _db.spGetGrantee(model.TaxCode, model.DocSeries, model.DocNum).SingleOrDefault();
        }

        public spGetGrantee_Result Grantee
        {
            get
            {
                return _grantee;
            }
        }
    }
    public class RGrantLicense : IRGrantLicense
    {
        CLicenseEntities1 _db;
        public RGrantLicense(CLicenseEntities1 db)
        {
            _db = db;
            //_licenseStatus = licenseStatus;
        }

        public sprpLicenseList_Result GetLicense(int rowid)
        {
            return
                _db.sprpLicenseList(
                    rowID: rowid,
                    startDate: null,
                    finalDate: null
                    ).SingleOrDefault();
        }

        public IEnumerable<spLicenseSeek_Result> GetLicenses(int userID, string taxCode=null,string DocSeries=null, string DocNum=null, 
                                DateTime? startDate=null, DateTime? finalDate=null)
        {
            return
                _db.spLicenseSeek(
                    userID: userID,
                    taxCode: taxCode,
                    docSeries: DocSeries,
                    docNum: DocNum,
                    startDate: startDate,
                    finalDate: finalDate,
                    legalOnly: true
                    );
        }
    }
    public class RCountry : BaseClass, ICountry
    {
        public RCountry(CLicenseEntities1 db, string classcode= "CL_CountryList", string itemcode=null) : base (db, classcode, itemcode)
        {
        }
    }
    public class RLicenseStatus : BaseClass, ILicenseStatus
    {
        public RLicenseStatus(CLicenseEntities1 db, string classcode = "CL_LicenseStatus", string itemcode = null) : base(db, classcode, itemcode)
        {
        }
    }
    public class RCancelReason : BaseClass, ICancelReason
    {
        public RCancelReason(CLicenseEntities1 db, string classcode = "CL_CancelReason", string itemcode = null) : base(db, classcode, itemcode)
        {
        }
    }
    public class RCurrensy  : ICurrensy
    {
        IEnumerable<spCurrencyListCombo_Result> _all;
        spCurrencyListCombo_Result _item;
        CLicenseEntities1 _db;
        string _itemcode;
        public RCurrensy(CLicenseEntities1 db, string itemcode = null) 
        {
            _itemcode = itemcode;
            _db = db;
        }
        public IEnumerable<spCurrencyListCombo_Result> All
        {
            get
            {
                if (_all == null)
                    _all = _db.spCurrencyListCombo();
                return _all;
            }
        }

        public spCurrencyListCombo_Result Item
        {
            get
            {
                if (_item == null && _itemcode != null)
                    _item = _all.Where(x => x.ItemCode ==_itemcode).SingleOrDefault();
                return _item;
            }
        }
    }

    public class RPurpose : BaseClass, IPurpose
    {
        public RPurpose(CLicenseEntities1 db, string classcode = "CL_PayPurpose", string itemcode = null) : base(db, classcode, itemcode)
        {
        }
    }

    public class RSex : BaseClass, ISex
    {
        public RSex(CLicenseEntities1 db, string classcode = "CL_Sex", string itemcode = null) : base(db, classcode, itemcode)
        {
        }
    }
    public class BaseClass
    {
        IEnumerable<vItemInfo> _all;
        vItemInfo _item;
        CLicenseEntities1 _db;
        string _itemcode;
        string _classcode;
        public BaseClass(CLicenseEntities1 db, string classcode, string itemcode = null)
        {
            _classcode = classcode;
            _itemcode = itemcode;
            _db = db;
        }
        public IEnumerable<vItemInfo> All
        {
            get
            {
                if (_all == null)
                    _all = _db.vItemInfo.Where(x => x.ClassCode == _classcode);
                return _all;
            }
        }

        public vItemInfo Item
        {
            get
            {
                if (_item == null && _itemcode != null)
                    _item = _db.vItemInfo.Where(x => x.ItemCode == _itemcode).SingleOrDefault();
                return _item;
            }
        }
    }

}

