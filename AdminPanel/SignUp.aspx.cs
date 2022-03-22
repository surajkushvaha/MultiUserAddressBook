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
           lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

           return;
       }
        #endregion Server Side validation

        #region Assign Value

       UserENT entUser = new UserENT();
        if (txtUserName.Text.Trim() != "")
        {
            strUserName = txtUserName.Text.Trim();
            if (checkUsername(strUserName) == true) {
                entUser.UserName = strUserName;
            }
            else
            {
                return;
            }
        }
        if (txtPassword.Text.Trim() != "")
        {
            entUser.Password = txtPassword.Text.Trim();
        }
        if (txtDisplayName.Text.Trim() != "")
        {
            entUser.DisplayName = txtDisplayName.Text.Trim();
        }
        if (txtMobileNo.Text.Trim() != "")
        {
            entUser.MobileNo = txtMobileNo.Text.Trim();
        }
        if (txtAddress.Text.Trim() != "")
        {
            entUser.Address = txtAddress.Text.Trim();
        }

        #endregion Assign Value

        #region insert
        UserBAL balUser = new UserBAL();

        if (balUser.Insert(entUser))
        {
            Response.Redirect("~/AdminPanel/Login", true);
        }
        else
        {
            lblErrMsg.Text = balUser.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }
        #endregion insert
    }
        #endregion Button : Save

    #region Check User Name
    private bool checkUsername(SqlString strUserName)
    {
        UserBAL balUser = new UserBAL();
        if (balUser.CheckForInsert(strUserName))
        {
            return true;
        }
        else
        {
            lblErrMsg.Text = balUser.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            return false;
        }
        
    }
    #endregion Check User Name 
}