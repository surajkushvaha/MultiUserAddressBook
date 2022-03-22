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
using MultiUserAddressBook.BAL;

public partial class AdminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
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
        ContactCategoryBAL balContactCategory = new ContactCategoryBAL();
        DataTable dtContactCategory = new DataTable();

        dtContactCategory = balContactCategory.SelectAllByUserID(Convert.ToInt32(Session["UserID"]));

        if (dtContactCategory != null && dtContactCategory.Rows.Count > 0)
        {
            gvContactCategory.DataSource = dtContactCategory;
            gvContactCategory.DataBind();
        }
        else
        {
            gvContactCategory.DataSource = dtContactCategory;
            gvContactCategory.DataBind();

            lblErrMsg.Text = "No data";
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }

    }
    #endregion Fill GridView

    #region  gvContactCategory : RowCommand
    protected void gvContactCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {

                ContactCategoryBAL balContactCategory = new ContactCategoryBAL();
                if (balContactCategory.Delete(Convert.ToInt32(e.CommandArgument.ToString().Trim()), Convert.ToInt32(Session["UserID"])))
                {
                    fillGridView();
                }
                else
                {
                    lblErrMsg.Text = balContactCategory.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                    lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                }
            }
        }

    }
    #endregion  gvContactCategory : RowCommand


}