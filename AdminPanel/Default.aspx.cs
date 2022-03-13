using MultiUserAddressBook;
using MultiUserAddressBook.ENT;
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

        #region Server Side Validation
        string strErrorMsg = "";
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
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

        #region Login
        UserBAL balUser = new UserBAL();
        UserENT entUser = new UserENT();

        entUser = balUser.SelctByUserNamePassword(strUserName,strPassword);

        if (entUser != null)
        {
            if (!entUser.UserID.IsNull)
                Session["UserID"] = entUser.UserID.ToString().Trim();
            if (!entUser.DisplayName.IsNull)
                Session["DisplayName"] = entUser.DisplayName.ToString().Trim();

            Response.Redirect("~/AdminPanel/Home", true);
        }

        else
        {
            lblErrMsg.Text = balUser.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            return;

        }
        #endregion Login

    }
    #endregion Button: Login
  
}