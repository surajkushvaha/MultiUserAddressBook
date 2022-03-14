
using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CountryBAL
/// </summary>
namespace MultiUserAddressBook.BAL
{
    public class CountryBAL
    {
        #region Constructor
        public CountryBAL()
        {
            //
            // TODO: Add constructor logic here
            
        }
        #endregion Constructor

        #region Local Variable

        protected string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }

        }
        #endregion Local Variable

        #region Select Operation
        public DataTable SelectAllByUserID(SqlInt32 UserID)
        {
            CountryDAL dalCountry = new CountryDAL();
            DataTable dtCountry = new DataTable();
            dtCountry = dalCountry.SelectAllByUserID(UserID);
            Message = dalCountry.Message;
            return dtCountry;
        }

        public DataTable SelectForDropDownList(SqlInt32 UserID)
        {
            CountryDAL dalCountry = new CountryDAL();
            DataTable dtCountry = new DataTable();
            dtCountry = dalCountry.SelectForDropDownList(UserID);
            Message = dalCountry.Message;
            return dtCountry;
        }

        public CountryENT SelectByPKUserID(SqlInt32 UserID, SqlInt32 CountryID)
        {
            CountryDAL dalCountry = new CountryDAL();
            CountryENT entCountry = new CountryENT();
            entCountry = dalCountry.SelectByPKUserID(UserID,CountryID);
            Message = dalCountry.Message;
            return entCountry;
        }
        #endregion Select Operation

        #region Insert
        public Boolean Insert(CountryENT entCountry)
        {
            CountryDAL dalCountry = new CountryDAL();
            if (dalCountry.Insert(entCountry))
            {
                return true;
            }
            else
            {
                Message = dalCountry.Message;
                return false;
            }
        }
        #endregion Insert

        #region Delete

        public Boolean Delete(SqlInt32 CountryID, SqlInt32 UserID)
        {
            CountryDAL dalCountry = new CountryDAL();
            if (dalCountry.Delete(CountryID,UserID))
            {
                return true;
            }
            else
            {
                Message = dalCountry.Message;
                return false;
            }
        }
        #endregion Delete

        #region Update

        public Boolean Update(CountryENT entCountry)
        {
            CountryDAL dalCountry = new CountryDAL();
            if (dalCountry.Update(entCountry))
            {
                return true;
            }
            else
            {
                Message = dalCountry.Message;
                return false;
            }
        }
        #endregion Update

    }
}