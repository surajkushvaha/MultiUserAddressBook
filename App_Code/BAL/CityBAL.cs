
using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CityBAL
/// </summary>
namespace MultiUserAddressBook.BAL
{
    public class CityBAL
    {
        #region Constructor
        public CityBAL()
        {
            //
            // TODO: Add constructor logic here
            //
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
            CityDAL dalCity = new CityDAL();
            return dalCity.SelectAllByUserID(UserID);
        }

        public DataTable SelectForDropDownList(SqlInt32 UserID)
        {
            CityDAL dalCity = new CityDAL();
            return dalCity.SelectForDropDownList(UserID);
        }

        public CityENT SelectByPKUserID(SqlInt32 UserID, SqlInt32 CityID)
        {
            CityDAL dalCity = new CityDAL();
            return dalCity.SelectByPKUserID(UserID, CityID);
        }

        public DataTable SelectByStateIDUserID(SqlInt32 StateID, SqlInt32 UserID)
        {
            CityDAL dalCity = new CityDAL();
            return dalCity.SelectByStateIDUserID(StateID,UserID);
        }
        #endregion Select Operation

        #region Insert
        public Boolean Insert(CityENT entCity)
        {
            CityDAL dalCity = new CityDAL();
            if(dalCity.Insert(entCity))
            {
                return true;
            }
            else
            {
                Message = dalCity.Message;
                return false;
            }
        }
        #endregion Insert

        #region Delete

        public Boolean Delete(SqlInt32 CityID, SqlInt32 UserID)
        {
            CityDAL dalCity = new CityDAL();
            if (dalCity.Delete(CityID,UserID))
            {
                return true;
            }
            else
            {
                Message = dalCity.Message;
                return false;
            }
        }
        #endregion Delete

        #region Update

        public Boolean Update(CityENT entCity)
        {
            CityDAL dalCity = new CityDAL();
            if(dalCity.Update(entCity))
            {
                  return true;
            }
            else
            {
                Message = dalCity.Message;
                return false;
            }
        }
        #endregion Update

    }
}