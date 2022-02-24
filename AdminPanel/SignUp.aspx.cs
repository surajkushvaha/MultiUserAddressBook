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

public partial class SignUp : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        String strErrorMsg = "";
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strDisplayName = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        #region Server Side Validation
        if (txtUserName.Text.Trim() == "")
        {
            strErrorMsg += "Enter Username <br/>";
        }
        if (txtPassword.Text.Trim() == "")
        {
            strErrorMsg += "Enter Password </br>";
        }
        if(txtDisplayName.Text.Trim() == "")
        {
            strErrorMsg += "Enter Display Name </br>";
        }
       if(strErrorMsg != ""){
           lblErrMsg.Text =  strErrorMsg;
           lblErrMsg.Visible =true;
           lblMsgDiv.Visible=true;
           return;
       }
        #endregion Server Side validation

        #region Assign Value
        if (txtUserName.Text.Trim() != "")
        {
            strUserName = txtUserName.Text.Trim();
            if (checkUsername(strUserName) == false) {
                return;
            }
        }
        if (txtPassword.Text.Trim() != "")
        {
            strPassword = txtPassword.Text.Trim();
        }
        if (txtDisplayName.Text.Trim() != "")
        {
            strDisplayName = txtDisplayName.Text.Trim();
        }
        strMobileNo = txtMobileNo.Text.Trim();
        strAddress = txtAddress.Text.Trim();

        #endregion Assign Value
        try
        {
            #region Read the Value & Set the Controls


            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_Insert";
            objCmd.Parameters.AddWithValue("@UserName",strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);
            objCmd.Parameters.AddWithValue("@DisplayName", strDisplayName);
            objCmd.Parameters.AddWithValue("@MobileNo", strMobileNo);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Read the Value & Set the Controls

            Response.Redirect("~/AdminPanel/Login", true);
        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
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

    #region Check User Name
    private bool checkUsername(SqlString strUserName)
    {
        bool flag = true;
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        try
        {

            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_CheckForInsert";
            objCmd.Parameters.AddWithValue("@UserName",strUserName);          

            SqlDataReader objSDR = objCmd.ExecuteReader();
            if(objSDR.HasRows){
                lblErrMsg.Text ="This Username is alredy exist try another username;";
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                flag = false;
            }
            else
            {
                flag = true;
            }
           
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
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
    #endregion Check User Name 
}