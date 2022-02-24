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

public partial class AdminPanel_ContactCategory_ContactCategoryAddEditPage : System.Web.UI.Page
{
    #region Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ( Page.RouteData.Values["ContactCategoryID"] != null)
            {
                lblMode.Text = "Edit Contact Category";
                btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactCategoryID"].ToString().Trim())));

            }
            else
            {
                lblMode.Text = "Add Contact Category";
                btnAdd.Text = "Add";

            }
        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        SqlString strContactCategoryName = SqlString.Null;
        String strErrorMsg = "";
        #endregion Local variable

        #region Server Side Validation
        if (txtContactCategoryName.Text.Trim() == "")
        {
            strErrorMsg += "- Enter Contry name.<br/>";

        }


        if (strErrorMsg != "")
        {
            lblContactCategoryMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblContactCategoryMsg.Text = strErrorMsg;
            return;
        }

        #endregion Server Side Validation
        try
        {
            #region Set the Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            strContactCategoryName = txtContactCategoryName.Text.Trim();
            objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Set the Connection & Command Object


            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                #region Update Record
                objCmd.CommandText = "[dbo].[PR_ContactCategory_UpdateByPKUserID]";
                objCmd.Parameters.AddWithValue("@ContactCategoryID",CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactCategoryID"].ToString().Trim()));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "[dbo].[PR_ContactCategory_InsertByUserID]";
                objCmd.ExecuteNonQuery();
                lblContactCategoryMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblContactCategoryMsg.Text = "Data Inserted Successfully";
                txtContactCategoryName.Text = "";

                txtContactCategoryName.Focus();
                #endregion Insert Record

            }
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
    }
    #endregion Button : Save

    #region Fill Controls
    private void fillControls(SqlInt32 ContactCategoryID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        try
        {
            #region Set the connection and Command object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_SelectByPKUserID";
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Set the connection and Command object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {

                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                    {
                        txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Something went wrong or Wrong URL";
            }
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
    #endregion Fll Controls
}