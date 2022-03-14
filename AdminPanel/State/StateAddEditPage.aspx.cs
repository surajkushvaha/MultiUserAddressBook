using MultiUserAddressBook;
using MultiUserAddressBook.BAL;
using MultiUserAddressBook.ENT;
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

            if (Page.RouteData.Values["StateID"] != null)
            {
                lblMode.Text = "Edit State";
                btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(MultiUserAddressBook.EncryptionDecryption.Base64Decode(Page.RouteData.Values["StateID"].ToString().Trim())));
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
       

        #region Server Side Validation
        //server side validation
        String strErrorMsg = "";
        if (ddlCountryID.SelectedIndex == 0)
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
        StateENT entState = new StateENT();
        if (ddlCountryID.SelectedIndex > 0)
        {
            entState.CountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
        }
        if (txtStateName.Text.Trim() != "")
        {
            entState.StateName = txtStateName.Text.Trim();
        }
        if (txtStateCode.Text.Trim() != "")
        {
            entState.StateCode = txtStateCode.Text.Trim();
        }
        if (Session["UserID"] != null)
        {
            entState.UserID = Convert.ToInt32(Session["UserID"]);
        }
        #endregion Gather Information
        StateBAL balState = new StateBAL();

        if (Page.RouteData.Values["StateID"] != null)
        {
            SqlInt32 StateID = Convert.ToInt32(EncryptionDecryption.Base64Decode(Page.RouteData.Values["StateID"].ToString().Trim()));
            entState.StateID = StateID;
            if (balState.Update(entState))
            {
                ClearField();
                Response.Redirect("~/AdminPanel/State/StateList.aspx");
            }
            else
            {
                lblErrMsg.Text = balState.Message;
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;

            }
        }
        else
        {

            if (balState.Insert(entState))
            {
                ClearField();
                lblErrMsg.Text = "Data Inserted Successfully";
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
            }
            else
            {
                lblErrMsg.Text = balState.Message;
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
            }
        }
   
       

    }
    #endregion Button : Save

    #region Clear Fields
    private void ClearField()
    {
        ddlCountryID.SelectedIndex = 0;
        txtStateCode.Text = "";
        txtStateName.Text = "";
        ddlCountryID.Focus();
    }
    #endregion Clear Fields

    #region FillDropDown
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.fillDropDownCountry(ddlCountryID,Convert.ToInt32(Session["UserID"]));
    }
    #endregion FillDropDown

    #region FillControl
    private void fillControls(SqlInt32 StateID)
    {
        StateBAL balState = new StateBAL();
        StateENT entState = new StateENT();

        entState = balState.SelctByPKUserID(Convert.ToInt32(Session["UserID"]),StateID);

        if (!entState.CountryID.IsNull)
        {
            ddlCountryID.SelectedValue = entState.CountryID.ToString().Trim();

        }
        if (!entState.StateName.IsNull)
        {
            txtStateName.Text = entState.StateName.ToString();

        }
        if (!entState.StateCode.IsNull)
        {
            txtStateCode.Text = entState.StateCode.ToString();
        }

        if (balState.Message != null)
        {
            lblErrMsg.Text = balState.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
        }
    }
    #endregion FillControl
}