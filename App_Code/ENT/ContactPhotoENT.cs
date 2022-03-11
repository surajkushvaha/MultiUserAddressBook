using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactPhotoENT
/// </summary>
namespace MultiUserAddressBook.ENT
{
    public class ContactPhotoENT
    {
        #region Constructor
        public ContactPhotoENT()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion Constructor

        #region ContactPhotoID
        protected SqlInt32 _ContactPhotoID;

        public SqlInt32 ContactPhotoID
        {
            get
            {
                return _ContactPhotoID;
            }
            set
            {
                _ContactPhotoID = value;
            }
        }
        #endregion ContactPhotoID

        #region ContactID
        protected SqlInt32 _ContactID;

        public SqlInt32 ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                _ContactID = value;
            }
        }
        #endregion ContactID

        #region UserID
        protected SqlInt32 _UserID;

        public SqlInt32 UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
        #endregion UserID

        #region FilePath
        protected SqlString _ContactPhotoPath;

        public SqlString ContactPhotoPath
        {
            get
            {
                return _ContactPhotoPath;
            }
            set
            {
                _ContactPhotoPath = value;
            }
        }
        #endregion FilePath

        #region FileType
        protected SqlString _PhotoFileType;

        public SqlString PhotoFileType
        {
            get
            {
                return _PhotoFileType;
            }
            set
            {
                _PhotoFileType = value;
            }
        }
        #endregion FileType

        #region FileExtension
        protected SqlString _PhotoFileExtension;

        public SqlString PhotoFileExtension
        {
            get
            {
                return _PhotoFileExtension;
            }
            set
            {
                _PhotoFileExtension = value;
            }
        }
        #endregion FileType

        #region FileSize
        protected SqlString _PhotoFileSize;

        public SqlString PhotoFileSize
        {
            get
            {
                return _PhotoFileSize;
            }
            set
            {
                _PhotoFileSize = value;
            }
        }
        #endregion FileSize

        #region CreationDate
        protected SqlDateTime _CreationDate;

        public SqlDateTime CreationDate
        {
            get
            {
                return _CreationDate;
            }
            set
            {
                _CreationDate = value;
            }
        }
        #endregion CreationDate

        #region ModificationDate
        protected SqlDateTime _ModificationDate;

        public SqlDateTime ModificationDate
        {
            get
            {
                return _ModificationDate;
            }
            set
            {
                _ModificationDate = value;
            }
        }
        #endregion ModificationDate
    }
}