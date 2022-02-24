using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_User_UserEditPage : System.Web.UI.Page
{
    #region On Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                fillControls();
            }
            else
            {
                Response.Redirect("~/AdminPanel/Login", true);
            }
        }
    }
    #endregion On Load

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variables
        SqlString strUsername = SqlString.Null;
        SqlString strNewPassword = SqlString.Null;
        SqlString strDisplayName = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        String strErrMsg = "";

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variables

        #region Server Side Validation and Assgining values
        if (txtUserName.Text.Trim() == "")
        {
            strErrMsg += "Enter Username </br>";
        }
        if (txtDisplayName.Text.Trim() == "")
        {
            strErrMsg += "Enter Display Name </br>";
        }
        if (txtPassword.Text.Trim() != "")
        {
            if (chkPassword(txtPassword.Text.Trim()) != true)
            {
                lblMsgDiv.Visible = true;
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Please Enter Correct Password";
                return;
            }
            strNewPassword = txtPassword.Text.Trim();

        }
        else
        {
            strErrMsg += "Enter Password  to Edit Profile</br>";

        }
        if (strErrMsg != "")
        {
            lblMsgDiv.Visible = true;
            lblErrMsg.Visible = true;
            lblErrMsg.Text = strErrMsg;
            return;
        }
        if (txtNewPassword.Text.Trim() != "")
        {
            strNewPassword = txtNewPassword.Text.Trim();
        }
        if (txtUserName.Text.Trim() != "")
        {
            strUsername = txtUserName.Text.Trim();
        }
        if (txtDisplayName.Text.Trim() != "")
        {
            strDisplayName = txtDisplayName.Text.Trim();
        }
        strMobileNo = txtMobileNo.Text.Trim();
        strAddress = txtAddress.Text.Trim();
        #endregion Server Side Validation and Assgining values

        try
        {
            #region Connection & Command object
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            
            
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_UpdateByPK";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            objCmd.Parameters.AddWithValue("@UserName", strUsername);
            objCmd.Parameters.AddWithValue("@Password", strNewPassword);
            objCmd.Parameters.AddWithValue("@DisplayName", strDisplayName);
            objCmd.Parameters.AddWithValue("@MobileNo", strMobileNo);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.ExecuteNonQuery();
            Response.Redirect("~/AdminPanel/Profile",true);

            #endregion Connection & Command Object

            
            
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        catch (Exception ex)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }

    }
    #endregion Button : Save

    #region fill controls
    private void fillControls()
    {

        #region local variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion local variable

        try
        {
            #region Connection & Command object
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Connection & Command Object

            #region read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["Username"].Equals(DBNull.Value))
                    {
                        txtUserName.Text = objSDR["Username"].ToString().Trim();
                    }
                    if (!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        txtDisplayName.Text = objSDR["DisplayName"].ToString().Trim();
                    }
                    if (!objSDR["MobileNo"].Equals(DBNull.Value))
                    {
                        txtMobileNo.Text = objSDR["MobileNo"].ToString().Trim();
                    }

                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }


                }
            }
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion  read the value and set the controls
        }
        catch (Exception ex)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
    }
    #endregion fill controls

    #region check password
    private bool chkPassword(SqlString password)
    {
        bool flag = false;
        #region local variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion local variable

        try
        {
            #region Connection & Command object
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_CheckPassword";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Connection & Command Object

            #region read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["Password"].Equals(DBNull.Value))
                    {
                        if(objSDR["Password"].ToString().Trim() == password)
                            flag = true;
                    }
                }

            }
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion  read the value and set the controls
        }
        catch (Exception ex)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        return flag;
    }
    #endregion check password
}