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
        #region Local Variable
        //Connection String
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();//open connection

            //Command Operation
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            #endregion Connection & Command Object

            #region Read the Value and set the controls
            //sqlDataa reading and fetching
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                gvContactCategory.DataSource = objSDR;
                gvContactCategory.DataBind();
            }
            else
            {
                gvContactCategory.DataSource = objSDR;
                gvContactCategory.DataBind();
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "You Not Added Any Data. Click on Add New to Add Data in Your Table";
            }
            if (objConn.State == ConnectionState.Open)
                objConn.Close(); //close connection
            #endregion Read the Value and set the controls
        }
        catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;

        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

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
                DeleteRecrord(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }

    }
    #endregion  gvContactCategory : RowCommand

    #region Delete Record
    protected void DeleteRecrord(SqlInt32 ContactCategoryID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable
        try
        {
            #region  Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_DeleteByPKUserID";
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            objCmd.ExecuteNonQuery();
            #endregion  Connection & Command Object

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }
        catch (Exception exc)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = exc.Message;

        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();

        }

        fillGridView();
    }
    #endregion Delete Record

}