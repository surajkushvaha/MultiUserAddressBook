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
       
        String strErrMsg = "";

        #endregion Local Variables

        #region Server Side Validation and Assgining values
        UserENT entUser = new UserENT();
        if (Session["UserID"] != null)
        {
            entUser.UserID = Convert.ToInt32(Session["UserID"]);
        }
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
            if (chkPassword(Convert.ToInt32(Session["UserID"]),txtPassword.Text.Trim()) == true)
            {
                entUser.Password = txtPassword.Text.Trim();

            }
            else
            {
                return;
            }

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
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            return;
        }
       
        if (txtNewPassword.Text.Trim() != "")
        {
            entUser.Password = txtNewPassword.Text.Trim();
        }
        if (txtUserName.Text.Trim() != "")
        {
            entUser.UserName = txtUserName.Text.Trim();
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
        #endregion Server Side Validation and Assgining values
      
        #region Update
        UserBAL balUser = new UserBAL();

        if (balUser.Update(entUser))
        {
            Session["DisplayName"] = entUser.DisplayName.ToString().Trim();
            Response.Redirect("~/AdminPanel/Profile", true);
        }
        else
        {
            lblErrMsg.Text = balUser.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }
        #endregion Update   
    }
    #endregion Button : Save

    #region fill controls
    private void fillControls()
    {
        UserBAL balUser = new UserBAL();
        UserENT entUser = new UserENT();

        entUser = balUser.SelectByUserID(Convert.ToInt32(Session["UserID"]));

        if (entUser != null)
        {

            if (!entUser.UserName.IsNull)
            {
                txtUserName.Text = entUser.UserName.ToString().Trim();
            }
            if (!entUser.DisplayName.IsNull)
            {
                txtDisplayName.Text = entUser.DisplayName.ToString().Trim();
            }
            if (!entUser.MobileNo.IsNull)
            {
                txtMobileNo.Text = entUser.MobileNo.ToString().Trim();          
            }
            if (!entUser.Address.IsNull)
            {
                txtAddress.Text = entUser.Address.ToString().Trim();              
            }         
        }
        else
        {
            lblErrMsg.Text = balUser.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            return;
        }          

    }
    #endregion fill controls

    #region check password
    private bool chkPassword(SqlInt32 UserID,SqlString Password)
    {
        UserBAL balUser = new UserBAL();
        if (balUser.CheckPassword(UserID,Password))
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
    #endregion check password
}