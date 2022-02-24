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

public partial class AdminPanel_City_CityAddEditPage : System.Web.UI.Page
{
    #region Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropDownList();

            if (Page.RouteData.Values["CityID"] != null)
            {
                lblMode.Text = "Edit City";
                btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CityID"].ToString().Trim())));

            }
            else
            {
                lblMode.Text = "Add City";
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
        SqlInt32 strStateID = SqlInt32.Null;
        SqlString strCityName = SqlString.Null;
        SqlString strPinCode = SqlString.Null;
        SqlString strSTDCode = SqlString.Null;
        #endregion Local Variable

        #region Server Side Validation
        //server side validation
        String strErrorMsg = "";
        if (ddlStateID.SelectedIndex == 0)
        {
            strErrorMsg += "- Select  State <br/>";
        }
        if (txtCityName.Text.Trim() == "")
        {
            strErrorMsg += "- Enter  City <br/>";
        }
       
        
        
        if (strErrorMsg != "")
        {
            lblCityMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblCityMsg.Text = strErrorMsg;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        //Gather the information
        if (ddlStateID.SelectedIndex > 0)
        {
            strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
        }
        if (txtCityName.Text.Trim() != "")
        {
            strCityName = txtCityName.Text.Trim();
        }    
            strSTDCode = txtSTDCode.Text.Trim();  
            strPinCode = txtPinCode.Text.Trim();
        
        #endregion Gather Information

        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityName", strCityName);
            objCmd.Parameters.AddWithValue("@PinCode", strPinCode);
            objCmd.Parameters.AddWithValue("@STDCode", strSTDCode);
            
            #endregion Connection & Command Object


            if (Page.RouteData.Values["CityID"] != null)
            {
                #region Update Record
                objCmd.CommandText = "[dbo].[PR_City_UpdateByPKUserID]";
                objCmd.Parameters.AddWithValue("@CityID",CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CityID"].ToString().Trim()));
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "[dbo].[PR_City_InsertByUserID]";
                objCmd.ExecuteNonQuery();
                lblCityMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblCityMsg.Text = "Data Inserted Successfully";
                ddlStateID.SelectedIndex = 0;
                txtCityName.Text = "";
                txtPinCode.Text = "";
                txtSTDCode.Text = "";

                ddlStateID.Focus();
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

    #region Fill DropDown
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownCity(ddlStateID,Convert.ToInt32( Session["UserID"]), lblErrMsg, lblMsgDiv);
    }
    #endregion Fill DropDown

    #region Fill Controls

    private void fillControls(SqlInt32 CityID)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variables

        try
        {
            #region Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectByPKUserID";
            objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Connection & Command Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
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
    #endregion Fill Controls

}