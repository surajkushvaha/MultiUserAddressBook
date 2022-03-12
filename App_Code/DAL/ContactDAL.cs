using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactDAL
/// </summary>
namespace MultiUserAddressBook.DAL
{
    public class ContactDAL : DatabaseConfig
    {
        #region Constructor
        public ContactDAL()
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
                        objCmd.CommandText = "PR_Contact_SelectAllByUserID";
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
        #endregion Select All

        #region Select By PK
        public ContactENT SelctByPKUserID(SqlInt32 UserID, SqlInt32 ContactID)
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
                        ContactENT entContact = new ContactENT();

                        SqlDataReader objSDR = objCmd.ExecuteReader();

                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["ContactID"].Equals(DBNull.Value))
                                {
                                    entContact.ContactID = Convert.ToInt32(objSDR["ContactID"].ToString().Trim());
                                }

                                if (!objSDR["UserID"].Equals(DBNull.Value))
                                {
                                    entContact.UserID = Convert.ToInt32(objSDR["UserID"].ToString().Trim());
                                }
                                if (!objSDR["CountryID"].Equals(DBNull.Value))
                                {
                                    entContact.CountryID = Convert.ToInt32(objSDR["CountryID"].ToString().Trim());
                                }
                                if (!objSDR["StateID"].Equals(DBNull.Value))
                                {
                                    entContact.StateID = Convert.ToInt32(objSDR["StateID"].ToString().Trim());
                                }
                                if (!objSDR["CityID"].Equals(DBNull.Value))
                                {
                                    entContact.CityID = Convert.ToInt32(objSDR["CityID"].ToString().Trim());
                                }
                                if (!objSDR["ContactName"].Equals(DBNull.Value))
                                {
                                    entContact.ContactName = objSDR["ContactName"].ToString().Trim();
                                }
                                if (!objSDR["ContactNo"].Equals(DBNull.Value))
                                {
                                    entContact.ContactNo = objSDR["ContactNo"].ToString().Trim();
                                }
                                if (!objSDR["WhatsappNo"].Equals(DBNull.Value))
                                {
                                    entContact.WhatsappNo = objSDR["WhatsappNo"].ToString().Trim();
                                }
                                
                                if (!objSDR["BirthDate"].Equals(DBNull.Value))
                                {
                                    entContact.BirthDate = Convert.ToDateTime(objSDR["BirthDate"].ToString().Trim());
                                }
                                if (!objSDR["Email"].Equals(DBNull.Value))
                                {
                                    entContact.Email = objSDR["Email"].ToString().Trim();
                                }
                                if (!objSDR["Address"].Equals(DBNull.Value))
                                {
                                    entContact.Address = objSDR["Address"].ToString().Trim();
                                }
                                if (!objSDR["Age"].Equals(DBNull.Value))
                                {
                                    entContact.Age = Convert.ToInt32(objSDR["Age"].ToString().Trim());
                                }
                                if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                                {
                                    entContact.BloodGroup = objSDR["BloodGroup"].ToString().Trim();
                                }
                                if (!objSDR["FacebookID"].Equals(DBNull.Value))
                                {
                                    entContact.FacebookID = objSDR["FacebookID"].ToString().Trim();
                                }
                                if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                                {
                                    entContact.LinkedINID = objSDR["LinkedINID"].ToString().Trim();
                                }
                                if (!objSDR["CreationDate"].Equals(DBNull.Value))
                                {
                                    entContact.CreationDate = Convert.ToDateTime(objSDR["CreationDate"].ToString().Trim());
                                }
                                if (!objSDR["ModificationDate"].Equals(DBNull.Value))
                                {
                                    entContact.ModificationDate = Convert.ToDateTime(objSDR["ModificationDate"].ToString().Trim());
                                }
                                break;
                            }
                        }
                        return entContact;
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
        #endregion Select By PK

        #region Insert Operation
        public Boolean Insert(ContactENT entContact)
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
                        objCmd.CommandText = "PR_Contact_InsertByUserID";
                        objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entContact.UserID;
                        objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = entContact.CountryID;
                        objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = entContact.StateID;
                        objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = entContact.CityID;

                        objCmd.Parameters.Add("@ContactName", SqlDbType.VarChar).Value = entContact.ContactName;
                        objCmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = entContact.ContactNo;
                        objCmd.Parameters.Add("@WhatsappNo", SqlDbType.VarChar).Value = entContact.WhatsappNo;
                        objCmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = entContact.BirthDate;
                        objCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = entContact.Email;
                        objCmd.Parameters.Add("@Age", SqlDbType.Int).Value = entContact.Age;
                        objCmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = entContact.Address;
                        objCmd.Parameters.Add("@BloodGroup", SqlDbType.VarChar).Value = entContact.BloodGroup;
                        objCmd.Parameters.Add("@FacebookID", SqlDbType.VarChar).Value = entContact.FacebookID;
                        objCmd.Parameters.Add("@LinkedINID", SqlDbType.VarChar).Value = entContact.LinkedINID;
                        


                        #endregion Prepare Command

                        objCmd.ExecuteNonQuery();
                        entContact.ContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);

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
        public Boolean Update(ContactENT entContact)
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
                        objCmd.CommandText = "PR_Contact_UpdateByPKUserID";
                        objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = entContact.ContactID;
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = entContact.UserID;
                        objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = entContact.CountryID;
                        objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = entContact.StateID;
                        objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = entContact.CityID;

                        objCmd.Parameters.Add("@ContactName", SqlDbType.VarChar).Value = entContact.ContactName;
                        objCmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = entContact.ContactNo;
                        objCmd.Parameters.Add("@WhatsappNo", SqlDbType.VarChar).Value = entContact.WhatsappNo;
                        objCmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = entContact.BirthDate;
                        objCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = entContact.Email;
                        objCmd.Parameters.Add("@Age", SqlDbType.Int).Value = entContact.Age;
                        objCmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = entContact.Address;
                        objCmd.Parameters.Add("@BloodGroup", SqlDbType.VarChar).Value = entContact.BloodGroup;
                        objCmd.Parameters.Add("@FacebookID", SqlDbType.VarChar).Value = entContact.FacebookID;
                        objCmd.Parameters.Add("@LinkedINID", SqlDbType.VarChar).Value = entContact.LinkedINID;

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
                        objCmd.CommandText = "PR_Contact_DeleteByPKUserID";
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