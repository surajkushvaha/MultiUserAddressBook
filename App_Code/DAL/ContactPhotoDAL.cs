using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactPhotoDAL
/// </summary>
namespace MultiUserAddressBook.DAL
{
    public class ContactPhotoDAL : DatabaseConfig
    {
        #region Constructor
        public ContactPhotoDAL()
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

        #region Insert Operation
        public Boolean Insert(ContactPhotoENT entContactPhoto)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_ContactPhoto_InsertByContactIdUserID";
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entContactPhoto.UserID;
                        objCmd.Parameters.Add("@ContactID", SqlDbType.VarChar).Value = entContactPhoto.ContactID;
                        objCmd.Parameters.Add("@ContactPhotoPath", SqlDbType.VarChar).Value = entContactPhoto.ContactPhotoPath;
                        objCmd.Parameters.Add("@PhotoFileType", SqlDbType.VarChar).Value = entContactPhoto.PhotoFileType;
                        objCmd.Parameters.Add("@PhotoFileExtension", SqlDbType.VarChar).Value = entContactPhoto.PhotoFileExtension;
                        objCmd.Parameters.Add("@PhotoFileSize", SqlDbType.VarChar).Value = entContactPhoto.PhotoFileSize;


                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();

                        return true;

                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }

        #endregion Insert Operation

        #region Update Operation
        public Boolean Update(ContactPhotoENT entContactPhoto)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_ContactPhoto_UpdateByContactIdUserID";
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entContactPhoto.UserID;
                        objCmd.Parameters.Add("@ContactID", SqlDbType.VarChar).Value = entContactPhoto.ContactID;
                        objCmd.Parameters.Add("@ContactPhotoPath", SqlDbType.VarChar).Value = entContactPhoto.ContactPhotoPath;
                        objCmd.Parameters.Add("@PhotoFileType", SqlDbType.VarChar).Value = entContactPhoto.PhotoFileType;
                        objCmd.Parameters.Add("@PhotoFileExtension", SqlDbType.VarChar).Value = entContactPhoto.PhotoFileExtension;
                        objCmd.Parameters.Add("@PhotoFileSize", SqlDbType.VarChar).Value = entContactPhoto.PhotoFileSize;

                        #endregion Prepare Command


                        objCmd.ExecuteNonQuery();

                        return true;

                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion Update Operation

        #region Delete Operation
        public Boolean Delete(SqlInt32 ContactID, SqlInt32 UserID)
        {
            using (SqlConnection objConn = new SqlConnection(ConnectionString))
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_ContactPhoto_DeleteByContactIdUserID";
                        objCmd.Parameters.AddWithValue("@ContactID", ContactID);
                        objCmd.Parameters.AddWithValue("@UserID", UserID);

                        #endregion Prepare Command


                        objCmd.ExecuteNonQuery();

                        return true;

                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
                        return false;
                    }
                    finally
                    {
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion Delete Operation
    }
}