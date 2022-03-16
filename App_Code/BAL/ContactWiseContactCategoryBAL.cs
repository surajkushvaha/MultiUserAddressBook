using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactWiseContactCategoryBAL
/// </summary>
namespace MultiUserAddressBook
{
    public class ContactWiseContactCategoryBAL
    {
        #region Constructor
        public ContactWiseContactCategoryBAL()
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

        #region SelectForCheckBoxList
        public DataTable SelectForCheckBoxList(SqlInt32 UserID,SqlInt32 ContactID)
        {
            ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();
            DataTable dtContactWiseContactCategory = new DataTable();
            dtContactWiseContactCategory = dalContactWiseContactCategory.SelectForCheckBoxList(UserID,ContactID);
            Message = dalContactWiseContactCategory.Message;
            return dtContactWiseContactCategory; 
        }
        #endregion SelectForCheckBoxList

        #region Insert
        public Boolean Insert(ContactWiseContactCategoryENT entState)
        {
            ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();
            if (dalContactWiseContactCategory.Insert(entState))
            {
                return true;
            }
            else
            {
                Message = dalContactWiseContactCategory.Message;
                return false;
            }
        }
        #endregion Insert

        #region Delete

        public Boolean Delete(SqlInt32 ContactID, SqlInt32 UserID)
        {
            ContactWiseContactCategoryDAL dalContactWiseContactCategory = new ContactWiseContactCategoryDAL();
            if (dalContactWiseContactCategory.Delete(ContactID, UserID))
            {
                return true;
            }
            else
            {
                Message = dalContactWiseContactCategory.Message;
                return false;
            }
        }
        #endregion Delete
    }
}