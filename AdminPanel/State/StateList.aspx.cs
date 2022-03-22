using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlTypes;
using MultiUserAddressBook.BAL;

public partial class AdminPanel_State_StateList : System.Web.UI.Page
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

    #region FillGridView
    private void fillGridView()
    {
        StateBAL balState = new StateBAL();
        DataTable dtState = new DataTable();

        dtState = balState.SelectAllByUserID(Convert.ToInt32(Session["UserID"]));

        if (dtState != null && dtState.Rows.Count > 0)
        {

            gvState.DataSource = dtState;
            gvState.DataBind();
        }
        else
        {
            gvState.DataSource = dtState;
            gvState.DataBind();

            lblErrMsg.Text = "You Not Added Any Data. Click on Add New to Add Data in Your Table";
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }

        if (balState.Message != null && balState.Message != "")
        {
            lblErrMsg.Text = balState.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            return;
        }
    }
    #endregion FillGridView

    #region gvState : RowCommand
    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                StateBAL balState = new StateBAL();
                if (balState.Delete(Convert.ToInt32(e.CommandArgument.ToString().Trim()), Convert.ToInt32(Session["UserID"])))
                {
                    fillGridView();
                }
                else
                {
                    lblErrMsg.Text = balState.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                    lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                }
            }
        }

    }
    #endregion gvState : RowCommand

}