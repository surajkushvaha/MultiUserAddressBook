
using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactCategoryBAL
/// </summary>
namespace MultiUserAddressBook.BAL
{
    public class ContactCategoryBAL
    {
        #region Constructor
        public ContactCategoryBAL()
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
            ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
            DataTable dtContactCategory = new DataTable();
            dtContactCategory = dalContactCategory.SelectAllByUserID(UserID);
            Message = dalContactCategory.Message;
            return dtContactCategory;
        }

        public DataTable SelectForDropDownList(SqlInt32 UserID)
        {
            ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
            DataTable dtContactCategory = new DataTable();
            dtContactCategory = dalContactCategory.SelectForDropDownList(UserID);
            Message = dalContactCategory.Message;
            return dtContactCategory;
        }

        public ContactCategoryENT SelectByPKUserID(SqlInt32 UserID,SqlInt32 ContactCategoryID)
        {
            ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
            ContactCategoryENT entContactCategory = new ContactCategoryENT();
            entContactCategory = dalContactCategory.SelectByPKUserID(UserID,ContactCategoryID);
            Message = dalContactCategory.Message;
            return entContactCategory;
        }
        #endregion Select Operation

        #region Insert
        public Boolean Insert(ContactCategoryENT entContactCategory)
        {
            ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
            if (dalContactCategory.Insert(entContactCategory))
            {
                return true;
            }
            else
            {
                Message = dalContactCategory.Message;
                return false;
            }
        }
        #endregion Insert

        #region Delete

        public Boolean Delete(SqlInt32 ContactCategoryID,SqlInt32 UserID)
        {
            ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
            if (dalContactCategory.Delete(ContactCategoryID,UserID))
            {
                return true;
            }
            else
            {
                Message = dalContactCategory.Message;
                return false;
            }
        }
        #endregion Delete

        #region Update

        public Boolean Update(ContactCategoryENT entContactCategory)
        {
            ContactCategoryDAL dalContactCategory = new ContactCategoryDAL();
            if (dalContactCategory.Update(entContactCategory))
            {
                return true;
            }
            else
            {
                Message = dalContactCategory.Message;
                return false;
            }
        }
        #endregion Update
    }
}