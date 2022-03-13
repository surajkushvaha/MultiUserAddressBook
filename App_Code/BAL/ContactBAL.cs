
using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactBAL
/// </summary>
namespace MultiUserAddressBook.BAL
{
    public class ContactBAL
    {
        #region Constructor
        public ContactBAL()
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
            ContactDAL dalContact = new ContactDAL();
            return dalContact.SelectAllByUserID(UserID);
        }


        public ContactENT SelctByPKUserID(SqlInt32 UserID,SqlInt32 ContactID)
        {
            ContactDAL dalContact = new ContactDAL();
            return dalContact.SelctByPKUserID(UserID,ContactID);
        }
        #endregion Select Operation

        #region Insert
        public Boolean Insert(ContactENT entContact)
        {
            ContactDAL dalContact = new ContactDAL();
            if (dalContact.Insert(entContact))
            {
                return true;
            }
            else
            {
                Message = dalContact.Message;
                return false;
            }
        }
        #endregion Insert

        #region Delete

        public Boolean Delete(SqlInt32 ContactID,SqlInt32 UserID)
        {
            ContactDAL dalContact = new ContactDAL();
            if (dalContact.Delete(ContactID,UserID))
            {
                return true;
            }
            else
            {
                Message = dalContact.Message;
                return false;
            }
        }
        #endregion Delete

        #region Update

        public Boolean Update(ContactENT entContact)
        {
            ContactDAL dalContact = new ContactDAL();
            if (dalContact.Update(entContact))
            {
                return true;
            }
            else
            {
                Message = dalContact.Message;
                return false;
            }
        }
        #endregion Update

    }
}