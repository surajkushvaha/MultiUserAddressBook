using MultiUserAddressBook.DAL;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactPhotoBAL
/// </summary>
namespace MultiUserAddressBook
{
    public class ContactPhotoBAL
    {
        #region Constructor
        public ContactPhotoBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion  Constructor

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

        #region Select 
        public ContactPhotoENT SelctByPKUserID(SqlInt32 UserID, SqlInt32 ContactID)
        {
            ContactPhotoDAL dalContactPhoto = new ContactPhotoDAL();
            ContactPhotoENT entContactPhoto = new ContactPhotoENT();
            entContactPhoto = dalContactPhoto.SelctByPKUserID(UserID, ContactID);
            Message = dalContactPhoto.Message;
            return entContactPhoto;
          }
        #endregion Select

        #region Insert
        public Boolean Insert(ContactPhotoENT entContactPhoto)
        {
            ContactPhotoDAL dalContact = new ContactPhotoDAL();
            if (dalContact.Insert(entContactPhoto))
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

        public Boolean Delete(SqlInt32 ContactID, SqlInt32 UserID)
        {
            ContactPhotoDAL dalContactPhoto = new ContactPhotoDAL();
            if (dalContactPhoto.Delete(ContactID, UserID))
            {
                return true;
            }
            else
            {
                Message = dalContactPhoto.Message;
                return false;
            }
        }
        #endregion Delete

        #region Update

        public Boolean Update(ContactPhotoENT entContactPhoto)
        {
            ContactPhotoDAL dalContactPhoto = new ContactPhotoDAL();
            if (dalContactPhoto.Update(entContactPhoto))
            {
                return true;
            }
            else
            {
                Message = dalContactPhoto.Message;
                return false;
            }
        }
        #endregion Update
    }
}
