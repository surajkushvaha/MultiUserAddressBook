using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactCategoryDAL
/// </summary>
namespace MultiUserAddressBook.DAL
{
    public class ContactCategoryDAL : DatabaseConfig
    {
        #region Constructor
        public ContactCategoryDAL()
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

        #region Select All
        public DataTable SelectAllByUserID(SqlInt32 UserID)
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
                        objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";
                        objCmd.Parameters.AddWithValue("@UserID", UserID);
                        #endregion Prepare Command

                        #region Read Data & Set Controls
                        DataTable dt = new DataTable();
                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            dt.Load(objSDR);
                        }
                        return dt;
                        #endregion Read Data & Set Controls
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
        #endregion Select All

        #region Select By PK
        public ContactCategoryENT SelectByPKUserID(SqlInt32 UserID, SqlInt32 ContactCategoryID)
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
                        objCmd.CommandText = "PR_ContactCategory_SelectByPKUserID";
                        objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
                        objCmd.Parameters.AddWithValue("@UserID", UserID);
                        #endregion Prepare Command

                        #region Read the value and set the controls
                        ContactCategoryENT entContactCategory = new ContactCategoryENT();

                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {

                            if (objSDR.HasRows)
                            {
                                while (objSDR.Read())
                                {
                                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                                    {
                                        entContactCategory.ContactCategoryID = Convert.ToInt32(objSDR["ContactCategoryID"].ToString().Trim());
                                    }


                                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                                    {
                                        entContactCategory.ContactCategoryName = objSDR["ContactCategoryName"].ToString().Trim();
                                    }


                                    if (!objSDR["CreationDate"].Equals(DBNull.Value))
                                    {
                                        entContactCategory.CreationDate = Convert.ToDateTime(objSDR["CreationDate"].ToString().Trim());
                                    }
                                    if (!objSDR["ModificationDate"].Equals(DBNull.Value))
                                    {
                                        entContactCategory.ModificationDate = Convert.ToDateTime(objSDR["ModificationDate"].ToString().Trim());
                                    }
                                    break;
                                }
                            }
                        }
                        return entContactCategory;
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
        #endregion Select By PK

        #region SelectForDropDown
        public DataTable SelectForDropDownList(SqlInt32 UserID)
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
                        objCmd.CommandText = "PR_ContactCategory_SelectForDropDownListByUserID";
                        objCmd.Parameters.AddWithValue("@UserID", UserID);
                        #endregion Prepare Command

                        #region Read Data & Set Controls
                        DataTable dt = new DataTable();
                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            dt.Load(objSDR);
                        }
                        return dt;
                        #endregion Read Data & Set Controls
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
                        if (objConn.State == ConnectionState.Open)
                            objConn.Close();
                    }
                }
            }
        }
        #endregion SelectForDropDown

        #region Insert Operation
        public Boolean Insert(ContactCategoryENT entContactCategory)
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
                        objCmd.CommandText = "PR_ContactCategory_InsertByUserID";
                        objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entContactCategory.UserID;
                        objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar).Value = entContactCategory.ContactCategoryName;
                       

                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();
                        entContactCategory.ContactCategoryID = Convert.ToInt32(objCmd.Parameters["@ContactCategoryID"].Value);

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
        public Boolean Update(ContactCategoryENT entContactCategory)
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
                        objCmd.CommandText = "PR_ContactCategory_UpdateByPKUserID";
                        objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = entContactCategory.ContactCategoryID;
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entContactCategory.UserID;
                        objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar).Value = entContactCategory.ContactCategoryName;
                      

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
        public Boolean Delete(SqlInt32 ContactCategoryID, SqlInt32 UserID)
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
                        objCmd.CommandText = "PR_ContactCategory_DeleteByPKUserID";
                        objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
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