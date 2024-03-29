﻿
using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StateBAL
/// </summary>
namespace MultiUserAddressBook.BAL
{
    public class StateBAL
    {
        #region Constructor
        public StateBAL()
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
            StateDAL dalState = new StateDAL();
            DataTable dtState = new DataTable();
            dtState = dalState.SelectAllByUserID(UserID);
            Message = dalState.Message;
            return dtState;
        }

        public DataTable SelectForDropDownList(SqlInt32 UserID)
        {
            StateDAL dalState = new StateDAL();
            DataTable dtState = new DataTable();
            dtState = dalState.SelectForDropDownList(UserID);
            Message = dalState.Message;
            return dtState;
        }

        public StateENT SelctByPKUserID(SqlInt32 UserID,SqlInt32 StateID)
        {
            StateDAL dalState = new StateDAL();
            StateENT entState = new StateENT();
            entState = dalState.SelctByPKUserID(UserID, StateID);
            Message = dalState.Message;
            return entState;
        }

        public DataTable SelectByCountryIDUserID(SqlInt32 CountryID, SqlInt32 UserID)
        {
            StateDAL dalState = new StateDAL();
            DataTable dtState = new DataTable();
            dtState = dalState.SelectByCountryIDUserID(CountryID, UserID);
            Message = dalState.Message;
            return dtState;
        }
        #endregion Select Operation

        #region Insert
        public Boolean Insert(StateENT entState)
        {
            StateDAL dalState = new StateDAL();
            if (dalState.Insert(entState))
            {
                return true;
            }
            else
            {
                Message = dalState.Message;
                return false;
            }
        }
        #endregion Insert

        #region Delete

        public Boolean Delete(SqlInt32 StateID, SqlInt32 UserID)
        {
            StateDAL dalState = new StateDAL();
            if (dalState.Delete(StateID,UserID))
            {
                return true;
            }
            else
            {
                Message = dalState.Message;
                return false;
            }
        }
        #endregion Delete

        #region Update

        public Boolean Update(StateENT entState)
        {
            StateDAL dalState = new StateDAL();
            if (dalState.Update(entState))
            {
                return true;
            }
            else
            {
                Message = dalState.Message;
                return false;
            }
        }
        #endregion Update
    }
}