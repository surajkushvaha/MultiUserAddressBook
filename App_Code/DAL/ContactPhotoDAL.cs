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

        #region  SelctByPKUserID
        public ContactPhotoENT SelctByPKUserID(SqlInt32 UserID, SqlInt32 ContactID)
        {
            using (SqlConnection objCon = new SqlConnection(ConnectionString))
            {
                if (objCon.State != ConnectionState.Open)
                    objCon.Open();

                using (SqlCommand objCmd = objCon.CreateCommand())
                {
                    try
                    {
                        #region Prepare Command

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "PR_Contact_SelectByPKUserID";
                        objCmd.Parameters.AddWithValue("@ContactID", ContactID);
                        objCmd.Parameters.AddWithValue("@UserID", UserID);
                        #endregion Prepare Command

                        #region Read the value and set the controls
                        ContactPhotoENT entContactPhoto = new ContactPhotoENT();
                        SqlDataReader objSDR = objCmd.ExecuteReader();

                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["ContactID"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.ContactID = Convert.ToInt32(objSDR["ContactID"].ToString().Trim());
                                }

                               
                               
                                if (!objSDR["ContactPhotoPath"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.ContactPhotoPath = objSDR["ContactPhotoPath"].ToString().Trim();
                                }
                                if (!objSDR["PhotoFileType"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.PhotoFileType = objSDR["PhotoFileType"].ToString().Trim();
                                }
                                if (!objSDR["PhotoFileExtension"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.PhotoFileExtension = objSDR["PhotoFileExtension"].ToString().Trim();
                                }
                                if (!objSDR["PhotoFileSize"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.PhotoFileSize = objSDR["PhotoFileSize"].ToString().Trim();
                                }
                               
                                if (!objSDR["CreationDate"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.CreationDate = Convert.ToDateTime(objSDR["CreationDate"].ToString().Trim());
                                }
                                if (!objSDR["ModificationDate"].Equals(DBNull.Value))
                                {
                                    entContactPhoto.ModificationDate = Convert.ToDateTime(objSDR["ModificationDate"].ToString().Trim());
                                }
                                break;
                            }
                        }
                        return entContactPhoto;
                        #endregion Read the value and set the controls

                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message;
                        return null;
                    }
                    finally
                    {
                        if (objCon.State == ConnectionState.Open)
                            objCon.Close();
                    }
                }
            }
        }
        #endregion SelctByPKUserID

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
                        objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = entContactPhoto.ContactID;
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
                        Message = ex.Message;
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
                        objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = entContactPhoto.ContactID;
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
                        Message = ex.Message;
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
                        Message = ex.Message;
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