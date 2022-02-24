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

public partial class AdminPanel_Default : System.Web.UI.Page
{

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                Response.Redirect("~/AdminPanel/Home", true);
            }
        }
    }
    #endregion Page Load

    #region Button: Login
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region Local Variable
        String strErrorMsg = "";
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Varable

        #region Server Side Validation
        if (txtUserName.Text.Trim() == "")
        {
            strErrorMsg += "- Enter User Name </br>";
        }
        if (txtUserName.Text.Trim() == "")
        {
            strErrorMsg += "- Enter Password <br/>";
        }
        if(strErrorMsg != ""){
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = strErrorMsg;
            return;
        }
        #endregion Server Side Validation

        #region Assign the Value
        if (txtUserName.Text.Trim() != "")
        {
            strUserName = txtUserName.Text.Trim();
        }
        if (txtPassword.Text.Trim() != "")
        {
            strPassword = txtPassword.Text.Trim();
        }
        #endregion Assign the Value

        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectByUserNamePassword";
            objCmd.Parameters.AddWithValue("@UserName", strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);
            #endregion Connection & Command Object

            #region Read the Value & Set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if(objSDR.HasRows)
            {
                
                while (objSDR.Read())
                {
                    if (!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }
                    if (!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                    }
                }
                Response.Redirect("~/AdminPanel/Home", true);
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Wrong Username or Password.";
            }
            if(objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Read the Value & Set the Controls
        }
        catch(Exception ex)
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
    #endregion Button: Login
  
}