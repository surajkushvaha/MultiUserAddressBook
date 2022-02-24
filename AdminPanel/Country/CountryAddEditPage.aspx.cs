using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Configuration;

public partial class AdminPanel_Country_CountryAddEditPage : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Page.RouteData.Values["CountryID"] != null)
            {
                lblMode.Text = "Edit Country";
                btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode( Page.RouteData.Values["CountryID"].ToString().Trim())));
            }
            else
            {
                lblMode.Text = "Add Country";
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
        SqlString strCountryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        String strErrorMsg = "";
        #endregion Local Variable

        #region Server side validation
        if (txtCountryName.Text.Trim() == "")
        {
            strErrorMsg += "- Enter Contry name.<br/>";

        }
        if (txtCountryCode.Text.Trim() == "")
        {
            strErrorMsg += "- Enter Contry code.<br/>";

        }

        if (strErrorMsg != "")
        {
            lblCountryMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblCountryMsg.Text = strErrorMsg;
            return;
        }
        if (Session["UserID"] != null)
        {
            strUserID = Convert.ToInt32(Session["UserID"]);
        }

        #endregion Server side validation

        #region Assign the value
        if (txtCountryName.Text != "")
        {
             strCountryName = txtCountryName.Text.Trim();

        }

        if (txtCountryCode.Text != "")
        {
            strCountryCode = txtCountryCode.Text.Trim();

        }
        #endregion Assign the value
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
           

            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);
            
            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["CountryID"] != null)
            {
                #region Update Record
                objCmd.CommandText = "[dbo].[PR_Country_UpdateByPKUserID]";
                objCmd.Parameters.AddWithValue("@CountryID",CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CountryID"].ToString().Trim()));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "[dbo].[PR_Country_InsertByUserID]";
                objCmd.ExecuteNonQuery();
                lblCountryMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblCountryMsg.Text = "Data Inserted Successfully";
                txtCountryName.Text = "";
                txtCountryCode.Text = "";

                txtCountryName.Focus();
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
    private void fillControls(SqlInt32 CountryID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        
        #endregion Local Variable
        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectByPKUserID";
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            }
            #endregion Connection & Command Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {

                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
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
    #endregion Fill COntrols
}