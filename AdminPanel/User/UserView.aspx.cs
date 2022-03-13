using MultiUserAddressBook;
using MultiUserAddressBook.ENT;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_User_UserView : System.Web.UI.Page
{
    #region On Load Page
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Session["UserID"]!= null)
            {
                fillData();
            }
            else
            {
                Response.Redirect("~/AdminPanel/Login", true);
            }
        }
    }
    #endregion On Load Page

    #region fill data
    private void fillData()
    {
        UserBAL balUser = new UserBAL();
        UserENT entUser = new UserENT();

        entUser = balUser.SelectByUserID(Convert.ToInt32(Session["UserID"]));

        if (entUser != null)
        {

                if (!entUser.UserName.IsNull)
                {
                    lblShowUserName.Text = entUser.UserName.ToString().Trim();
                }
                if (!entUser.DisplayName.IsNull)
                {
                    lblShowDisplayName.Text = entUser.DisplayName.ToString().Trim();
                }
                if (!entUser.MobileNo.IsNull)
                {
                    lblShowMobileNo.Text = entUser.MobileNo.ToString().Trim();
                    if (lblShowMobileNo.Text == "")
                    {
                        lblShowMobileNo.Text = "Mobile No Not Provided By You";
                    }
                }
                else
                {
                    lblShowMobileNo.Text = "Mobile No Not Provided By You";
                }

                if (!entUser.Address.IsNull)
                {
                    lblShowAddress.Text = entUser.Address.ToString().Trim();
                    if (lblShowAddress.Text == "")
                    {
                        lblShowAddress.Text = "Address Not Provided By You";
                    }
                }
                else
                {
                    lblShowAddress.Text = "Address Not Provided By You";
                }

                if (!entUser.CreationDate.IsNull)
                {
                    lblShowCreation.Text = entUser.CreationDate.ToString().Trim();
                }
                if (!entUser.ModificationDate.IsNull)
                {
                    lblShowModification.Text = entUser.ModificationDate.ToString().Trim();
                }          
        }
        else
        {
            lblErrMsg.Text = balUser.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            return;
        }           
    }
    #endregion fill data

    #region Delete User
    protected void btnDelete_Click(object sender, EventArgs e)
    {          
            if (Session["UserID"] != null)
            {
                UserBAL balUser= new UserBAL();
                if(balUser.Delete(Convert.ToInt32(Session["UserID"]))){
                    Session.Clear();
                    Response.Redirect("~/AdminPanel/Login", true);
                }
                else
                {
                    lblErrMsg.Text = balUser.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                }
            }
    }
    #endregion Delete User
  
}