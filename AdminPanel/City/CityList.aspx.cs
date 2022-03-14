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
public partial class AdminPanel_City_CityList : System.Web.UI.Page
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

    #region FillGriedView
    private void fillGridView()
    {

        CityBAL balCity = new CityBAL();
        DataTable dtCity = new DataTable();

        dtCity = balCity.SelectAllByUserID(Convert.ToInt32(Session["UserID"]));

        if (dtCity != null && dtCity.Rows.Count > 0)
        {

            gvCity.DataSource = dtCity;
            gvCity.DataBind();
        }
        else
        {
            gvCity.DataSource = dtCity;
            gvCity.DataBind();

            lblErrMsg.Text = "You Not Added Any Data. Click on Add New to Add Data in Your Table";
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
        }

        if (balCity.Message != null && balCity.Message != "")
        {
            lblErrMsg.Text = balCity.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            return;
        }
    }
    #endregion FillGriedView

    #region gvCity : RowCommand
    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                CityBAL balCity = new CityBAL();
                if (balCity.Delete(Convert.ToInt32(e.CommandArgument.ToString().Trim()), Convert.ToInt32(Session["UserID"])))
                {
                    fillGridView();
                }
                else
                {
                    lblErrMsg.Text = balCity.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                }
            }
        }

    }
    #endregion gvCity : RowCommand

}