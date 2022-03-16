using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using MultiUserAddressBook.BAL;
using MultiUserAddressBook;
using MultiUserAddressBook.ENT;
public partial class AdminPanel_Contact_ContactList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fillGridView();
        }
    }
    #endregion Load Event

    #region Fill GridView
    private void fillGridView()
    {
        ContactBAL balContact = new ContactBAL();
        DataTable dtContact = new DataTable();

        dtContact = balContact.SelectAllByUserID(Convert.ToInt32(Session["UserID"]));

        if (dtContact != null && dtContact.Rows.Count > 0)
        {
            gvContact.DataSource = dtContact;
            gvContact.DataBind();
        }
        else
        {
            gvContact.DataSource = dtContact;
            gvContact.DataBind();

            lblErrMsg.Text = "No data";
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
        }

    }
    #endregion Fill GridView

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                SqlInt32 ContactID =Convert.ToInt32(e.CommandArgument.ToString().Trim());
                ContactBAL balContact = new ContactBAL();
                ContactPhotoBAL balContactPhoto = new ContactPhotoBAL();
                ContactPhotoENT entContactPhoto = new ContactPhotoENT();
                entContactPhoto = balContactPhoto.SelctByPKUserID(Convert.ToInt32(Session["UserID"].ToString().Trim()),ContactID);
                if (balContactPhoto.Delete(ContactID, Convert.ToInt32(Session["UserID"].ToString().Trim())))
                {
                    DeletePhotoRecord(ContactID, entContactPhoto.ContactPhotoPath);
                    ContactWiseContactCategoryBAL balContactWiseContactCategory = new ContactWiseContactCategoryBAL();
                    if (balContactWiseContactCategory.Delete(ContactID, Convert.ToInt32(Session["UserID"].ToString().Trim())))
                    {
                        if (balContact.Delete(ContactID, Convert.ToInt32(Session["UserID"].ToString().Trim())))
                        {
                            fillGridView();
                        }
                        else
                        {
                            lblErrMsg.Text = balContact.Message;
                            lblErrMsg.Visible = true;
                            lblMsgDiv.Visible = true;
                        }
                    }
                    else
                    {
                        lblErrMsg.Text = balContactWiseContactCategory.Message;
                        lblErrMsg.Visible = true;
                        lblMsgDiv.Visible = true;

                    }
                }
                else
                {
                    lblErrMsg.Text = balContactPhoto.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                }
            }
        }

    }
    #endregion gvContact : RowCommand

    #region Delete File Record
    private void DeletePhotoRecord(SqlInt32 ContactID,SqlString PhotoPath)
    {
            if (!PhotoPath.IsNull)
            {

                String AbsolutePath = ResolveUrl(PhotoPath.ToString().Trim()); ;
                FileInfo file = new FileInfo(Server.MapPath(AbsolutePath));
                if (file.Exists)
                {
                    file.Delete();
                }

            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Something went wrong or Wrong URL";
            }
        

    }
    #endregion Delete File Record
 
}