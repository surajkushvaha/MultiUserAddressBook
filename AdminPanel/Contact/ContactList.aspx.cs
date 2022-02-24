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
        #region Local Variable
        //Connection String
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();//Open Connection

            //Command Operation
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_SelectAllByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            #endregion Connection & Command Object

            #region Read the value and set the controls
            //Read and fetch Data 
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
            }
            else
            {
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "You Not Added Any Data. Click on Add New to Add Data in Your Table";
            }
            if (objConn.State == ConnectionState.Open)
                objConn.Close(); //Close Connection
            #endregion Read the value and set the controls

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

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteRecrord(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }

    }
    #endregion gvContact : RowCommand

    #region Delete Record
    protected void DeleteRecrord(SqlInt32 ContactID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString.Trim());
        #endregion Local Variable
        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_DeleteByPKUserID";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            objCmd.ExecuteNonQuery();
            
            #endregion Connection & Command Object

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            fillGridView();

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
    #endregion Delete Record

}