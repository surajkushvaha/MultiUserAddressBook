using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonDropDownFillMethods
/// </summary>
public static class CommonDropDownFillMethods
{
    #region FillDropDownListCountry
    public static void FillDropDownListCountry(DropDownList  ddl,SqlInt32 UserID,Label lblErrMsg, Panel lblMsgDiv)
    {
        #region Local Variable

        //define connnection string
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        
        #endregion Local Variable

        try
        {
            #region Set Connection & Command Object

            //open connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            //create command
            SqlCommand objCmd = objConn.CreateCommand();

            //define command tyoe
            objCmd.CommandType = CommandType.StoredProcedure;

            //define command text
            objCmd.CommandText = "PR_Country_SelectForDropDownListbyUserID";
            
            //passing values
            objCmd.Parameters.AddWithValue("@UserID", UserID);
            
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            //execute command
            SqlDataReader objSDR = objCmd.ExecuteReader();


            if (objSDR.HasRows == true)
            {
                //define source
                ddl.DataSource = objSDR;

                //pass value and field
                ddl.DataValueField = "CountryID";
                ddl.DataTextField = "CountryName";

                //bind data
                ddl.DataBind();

            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in Country First.";
            }

            //Insert in dropdown as pseudo data with -1 value
            ddl.Items.Insert(0, new ListItem("Select Country", "-1"));

            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            #endregion Read the value and set the controls

        }
        catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;
        }
        finally
        {
            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }
    }

    #endregion FillDropDownListCountry

    #region FillDropDownState
    public static void FillDropDownState(DropDownList ddlCountryID,SqlInt32 UserID,Label lblErrMsg,Panel lblMsgDiv)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variabes
        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";
           
                objCmd.Parameters.AddWithValue("@UserID",UserID);
            

            #endregion Set Connection & Command Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlCountryID.DataSource = objSDR;
                ddlCountryID.DataValueField = "CountryID";
                ddlCountryID.DataTextField = "CountryName";
                ddlCountryID.DataBind();
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Country First.";
            }

            ddlCountryID.Items.Insert(0, new ListItem("Select Your Country", "-1"));

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Read the value and set the controls

        } catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;

        } finally {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }

    }
    #endregion FillDropDownState

    #region FillDropDownListStateByCountryID
    public static void FillDropDownListStateByCountryID(DropDownList ddlStateID, SqlInt32 CountryID, SqlInt32 UserID, DropDownList ddlCityID,Label lblErrMsg,Panel lblMsgDiv)
    {
        #region Local Variable

        //define connnection string
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        lblErrMsg.Visible = false;
        lblMsgDiv.Visible = false;
        #endregion Local Variable

        try
        {

            #region Set Connection & Command Object

            //open connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            //create command
            SqlCommand objCmd = objConn.CreateCommand();

            //define command type
            objCmd.CommandType = CommandType.StoredProcedure;

            //define command text
            objCmd.CommandText = "PR_State_SelectByCountryIDUserID";

            //passing values
            objCmd.Parameters.AddWithValue("@CountryID", CountryID);           
            objCmd.Parameters.AddWithValue("@UserID", UserID);
            
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            //execute command
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                //define source
                ddlStateID.DataSource = objSDR;
                //pass value and field
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                //bind data
                ddlStateID.DataBind();
                //Insert in dropdown as pseudo data with -1 value
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));

            }
            else
            {
                //clear items
                ddlStateID.Items.Clear();
                //Insert in dropdown as pseudo data with -1 value
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));

                //clear items
                ddlCityID.Items.Clear();
                //Insert in dropdown as pseudo data with -1 value
                ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));


                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in State Related to Selected Country First.";
            }

            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            #endregion Read the value and set the controls

        }
        catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;
        }
        finally
        {
            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion FillDropDownListStateByCountryID

    #region Fill DropDownCity
    public static void FillDropDownCity(DropDownList ddlStateID,SqlInt32 UserID,Label lblErrMsg,Panel lblMsgDiv)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variables
        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectForDropDownListByUserID";
           
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            
            #endregion Connection & Command Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows == true)
            {
                ddlStateID.DataSource = objSDR;
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                ddlStateID.DataBind();
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add State First.";
            }
            ddlStateID.Items.Insert(0, new ListItem("Select Your State", "-1"));

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Read the value and set the controls

        }
        catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;

        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }

    }
    #endregion Fill DropDownCity

    #region fillDropdDownCityByStateID
    public static void fillDropdDownCityByStateID(DropDownList ddl, SqlInt32 StateID, SqlInt32 UserID,Label lblErrMsg,Panel lblMsgDiv)
    {
        #region Local Variable

        //define connnection string
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        lblErrMsg.Visible = false;
        lblMsgDiv.Visible = false;
        #endregion Local Variable

        try
        {

            #region Set Connection & Command Object
            //open connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            //create command
            SqlCommand objCmd = objConn.CreateCommand();

            //define command type
            objCmd.CommandType = CommandType.StoredProcedure;

            //define command text
            objCmd.CommandText = "PR_City_SelectByStateIDUserID";

            //passing values
            objCmd.Parameters.AddWithValue("@StateID", StateID);           
            objCmd.Parameters.AddWithValue("@UserID", UserID);
            
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            //execute command
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                //define source
                ddl.DataSource = objSDR;
                //pass value and field
                ddl.DataValueField = "CityID";
                ddl.DataTextField = "CityName";
                //bind data
                ddl.DataBind();
                //Insert in dropdown as pseudo data with -1 value
                ddl.Items.Insert(0, new ListItem("Select City", "-1"));
            }
            else
            {
                //clear items
                ddl.Items.Clear();
                //Insert in dropdown as pseudo data with -1 value
                ddl.Items.Insert(0, new ListItem("Select City", "-1"));

                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in City Releated to Selected State First.";
            }

            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Read the value and set the controls

        }
        catch (Exception ex)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = ex.Message;
        }
        finally
        {
            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

    }
    #endregion fillDropdDownCityByStateID

    #region FillCheckBoxListForContactCategoryList
    public static void FillCheckBoxListForContactCategoryList(CheckBoxList cbl, SqlInt32 UserID, Label lblErrMsg, Panel lblMsgDiv)
    {
        #region Local Variable

        //define connnection string
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        
        #endregion Local Variable

        try
        {
            #region Set Connection & Command Object

            //open connection
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            //create command
            SqlCommand objCmd = objConn.CreateCommand();

            //define command type
            objCmd.CommandType = CommandType.StoredProcedure;

            //define command text
            objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";

            //passing values
            objCmd.Parameters.AddWithValue("@UserID", UserID);
            
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            //execute command
            SqlDataReader objSDR = objCmd.ExecuteReader();
     
            if (objSDR.HasRows == true)
            {
                //define source
                cbl.DataSource = objSDR;
                //pass value and field
                cbl.DataValueField = "ContactCategoryID";
                cbl.DataTextField = "ContactCategoryName";
                //bind data
                cbl.DataBind();
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in Contact Category First.";
            }

            //close connection
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            #endregion Read the value and set the controls

        }
        catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;
        }
        finally
        {
            //close connection
            if (objConn.State == ConnectionState.Open)
               objConn.Close();
        }
    }
    #endregion FillCheckBoxListForContactCategoryList

    #region Encryption Decryption

    #region Base64Encode
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    #endregion Base64Encode

    #region Base64Decode
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
         
    }
    #endregion Base64Decode

    #region Encryption Based On Key
    public static string Encrypt(string encryptString)
    {
        string EncryptionKey = "SURAJ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }
    #endregion Encryption Based On Key

    #region Dicryption Based On Key
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "SURAJ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    #endregion Dicryption Based On Key


    #endregion Encryption Decryption
}