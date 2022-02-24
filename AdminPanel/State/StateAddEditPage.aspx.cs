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

public partial class AdminPanel_State_StateAddEditPage : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();

            if (Request.QueryString["StateID"] != null)
            {
                lblMode.Text = "Edit State";
                btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(Request.QueryString["StateID"]));
            }
            else
            {
                lblMode.Text = "Add State";
                btnAdd.Text = "Add";

            }
        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        SqlInt32 strCountryID = SqlInt32.Null;
        SqlString strStateName = SqlString.Null;
        SqlString strStateCode = SqlString.Null;
        #endregion Local Variables

        #region Server Side Validation
        //server side validation
        String strErrorMsg = "";
        if (ddlCountry.SelectedIndex == 0)
        {
            strErrorMsg += "- Select  Country <br/>";
        }
        if (txtStateName.Text.Trim() == "")
        {
            strErrorMsg += "- Enter  State <br/>";
        }
        if (txtStateCode.Text.Trim() == "")
        {
            strErrorMsg += "- Enter  State Code <br/>";
        }
        if (strErrorMsg != "")
        {
            lblStateMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblStateMsg.Text = strErrorMsg;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        //Gather the information
        if (ddlCountry.SelectedIndex > 0)
        {
            strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }
        if (txtStateName.Text.Trim() != "")
        {
            strStateName = txtStateName.Text.Trim();
        }
        if (txtStateCode.Text.Trim() != "")
        {
            strStateCode = txtStateCode.Text.Trim();
        }
        #endregion Gather Information

        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateName", strStateName);
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);
            
            #endregion Set Connection & Command Object


            if (Request.QueryString["StateID"] != null)
            {
                #region Update Record
                objCmd.CommandText = "[dbo].[PR_State_UpdateByPKUserID]";
                objCmd.Parameters.AddWithValue("@StateID", Request.QueryString["StateID"].ToString().Trim());
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/State/StateList.aspx", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "[dbo].[PR_State_InsertByUserID]";
                objCmd.ExecuteNonQuery();
                lblStateMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblStateMsg.Text = "Data Inserted Successfully";
                ddlCountry.SelectedIndex = 0;
                txtStateName.Text = "";
                txtStateCode.Text = "";

                ddlCountry.Focus();
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

    #region FillDropDown
    private void FillDropDownList()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variabes
        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }

            #endregion Set Connection & Command Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlCountry.DataSource = objSDR;
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataBind();
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Country First.";
            }

            ddlCountry.Items.Insert(0, new ListItem("Select Your Country", "-1"));

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
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
    #endregion FillDropDown

    #region FillControl
    private void fillControls(SqlInt32 StateID)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variables
        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectByPKUserID";
            objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Set Connection & Command Object

            #region Read the Value and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    if (!objSDR["StateName"].Equals(DBNull.Value))
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();
                    }
                    if (!objSDR["StateCode"].Equals(DBNull.Value))
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
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
            #endregion Read the Value and set the Controls

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
    #endregion FillControl
}