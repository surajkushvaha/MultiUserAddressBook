using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDAL
/// </summary>
namespace MultiUserAddressBook.DAL
{
    public class UserDAL:DatabaseConfig
    {
        #region Constructor
        public UserDAL()
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
        public Boolean Insert(UserENT entUser)
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
                        objCmd.CommandText = "PR_User_Insert";
                        objCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = entUser.UserName;
                        objCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = entUser.Password;
                        objCmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = entUser.DisplayName;
                        objCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = entUser.MobileNo;
                        objCmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = entUser.Address;


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
        public Boolean Update(UserENT entUser)
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
                        objCmd.CommandText = "PR_User_UpdateByPK";
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entUser.UserID;

                        objCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = entUser.UserName;
                        objCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = entUser.Password;
                        objCmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = entUser.DisplayName;
                        objCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = entUser.MobileNo;
                        objCmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = entUser.Address;

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
        public Boolean Delete(SqlInt32 UserID)
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
                        objCmd.CommandText = "PR_User_DeleteByUserID";
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

        #region Check For Availability UserName
        public Boolean CheckForInsert(SqlString UserName)
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
                        objCmd.CommandText = "PR_User_CheckForInsert";
                        objCmd.Parameters.AddWithValue("@UserName", UserName);

                        #endregion Prepare Command


                        SqlDataReader objSDR = objCmd.ExecuteReader();
                        if (objSDR.HasRows)
                        {
                            Message = "This Username is alredy exist try another username";                          
                            return false;
                        }
                        else
                        {
                            return true;
                        }

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
        #endregion Check For Availability UserName

        #region Check For Password
        public Boolean CheckPassword(SqlInt32 UserID,SqlString Password)
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
                        objCmd.CommandText = "PR_User_CheckPassword";
                        objCmd.Parameters.AddWithValue("@UserID", UserID);

                        #endregion Prepare Command

                        #region Read Data & Set Controls
                        using (SqlDataReader objSDR = objCmd.ExecuteReader())
                        {
                            if (objSDR.HasRows)
                            {
                                while (objSDR.Read())
                                {
                                    if (!objSDR["Password"].Equals(DBNull.Value))
                                    {
                                        if (objSDR["Password"].ToString().Trim() == Password)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            Message = "Please Enter Correct Password";
                                            return false;
                                        }

                                    }
                                }

                            }
                        }
                        return false;
                        #endregion Read Data & Set Controls

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
        #endregion Check For Password

        #region Select By UserID
        public UserENT SelectByUserID(SqlInt32 UserID)
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
                        objCmd.CommandText = "PR_User_SelectByUserID";
                        objCmd.Parameters.AddWithValue("@UserID", UserID);
                        #endregion Prepare Command

                        #region Read the value and set the controls
                        UserENT entUser = new UserENT();

                        SqlDataReader objSDR = objCmd.ExecuteReader();

                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {

                                if (!objSDR["UserName"].Equals(DBNull.Value))
                                {
                                    entUser.UserName = objSDR["UserName"].ToString().Trim();
                                }
                                if (!objSDR["DisplayName"].Equals(DBNull.Value))
                                {
                                    entUser.DisplayName = objSDR["DisplayName"].ToString().Trim();
                                }
                                if (!objSDR["MobileNo"].Equals(DBNull.Value))
                                {
                                    entUser.MobileNo = objSDR["MobileNo"].ToString().Trim();
                                }

                                if (!objSDR["Address"].Equals(DBNull.Value))
                                {
                                    entUser.Address = objSDR["Address"].ToString().Trim();
                                }
                               
                                if (!objSDR["CreationDate"].Equals(DBNull.Value))
                                {
                                    entUser.CreationDate = Convert.ToDateTime(objSDR["CreationDate"].ToString().Trim());
                                }
                                if (!objSDR["ModificationDate"].Equals(DBNull.Value))
                                {
                                    entUser.ModificationDate = Convert.ToDateTime(objSDR["ModificationDate"].ToString().Trim());
                                }
                                break;
                            }
                        }
                        return entUser;
                        #endregion Read the value and set the controls

                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
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
        #endregion Select By UserID

        #region Select By UserName & Password
        public UserENT SelctByUserNamePassword(SqlString UserName, SqlString Password)
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
                        objCmd.CommandText = "PR_User_SelectByUserNamePassword";
                        objCmd.Parameters.AddWithValue("@Password", Password);
                        objCmd.Parameters.AddWithValue("@UserName", UserName);
                        #endregion Prepare Command

                        #region Read Data & Set Controls
                        UserENT entUser = new UserENT();

                        SqlDataReader objSDR = objCmd.ExecuteReader();

                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["UserID"].Equals(DBNull.Value))
                                {
                                    entUser.UserID = Convert.ToInt32(objSDR["UserID"].ToString().Trim());
                                }
                                if (!objSDR["UserName"].Equals(DBNull.Value))
                                {
                                    entUser.UserName = objSDR["UserName"].ToString().Trim();
                                }
                                if (!objSDR["DisplayName"].Equals(DBNull.Value))
                                {
                                    entUser.DisplayName = objSDR["DisplayName"].ToString().Trim();
                                }
                                if (!objSDR["MobileNo"].Equals(DBNull.Value))
                                {
                                    entUser.MobileNo = objSDR["MobileNo"].ToString().Trim();
                                }

                                if (!objSDR["Address"].Equals(DBNull.Value))
                                {
                                    entUser.Address = objSDR["Address"].ToString().Trim();
                                }

                                break;
                            }
                        }
                        else
                        {
                            Message = "Wrong UserName and Passsword";
                            return null;
                        }
                        return entUser;
                        #endregion Read Data & Set Controls

                    }
                    catch (SqlException sqlex)
                    {
                        Message = sqlex.Message;
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Message = ex.InnerException.Message;
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
        #endregion Select By UserName & Password

    }
}