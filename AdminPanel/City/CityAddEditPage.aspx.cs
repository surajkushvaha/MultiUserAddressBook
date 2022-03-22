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
                //btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(EncryptionDecryption.Base64Decode(Page.RouteData.Values["CityID"].ToString().Trim())));

            }
            else
            {
                lblMode.Text = "Add City";
                //btnAdd.Text = "Add";

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
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        CityENT entCity = new CityENT();

        //Gather the information
        if (ddlStateID.SelectedIndex > 0)
        {
            entCity.StateID = Convert.ToInt32(ddlStateID.SelectedValue);
        }
        if (txtCityName.Text.Trim() != "")
        {
            entCity.CityName = txtCityName.Text.Trim();
        }
        if (txtSTDCode.Text.Trim() != "")
        {
            entCity.STDCode = txtSTDCode.Text.Trim();  
        }
        if (txtPinCode.Text.Trim()  != "")
        {
            entCity.PinCode = txtPinCode.Text.Trim();
        }
        if (Session["UserID"] != null)
        {
            entCity.UserID = Convert.ToInt32(Session["UserID"].ToString());
        }
        #endregion Gather Information

        CityBAL balCity = new CityBAL();

        if (Page.RouteData.Values["CityID"] != null)
        {
            SqlInt32 CityID = Convert.ToInt32(EncryptionDecryption.Base64Decode(Page.RouteData.Values["CityID"].ToString().Trim()));
            entCity.CityID = CityID;
            if (balCity.Update(entCity))
            {
                ClearField();
                Response.Redirect("~/AdminPanel/City/CityList.aspx");
            }
            else
            {
                lblErrMsg.Text = balCity.Message;
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            }
        }
        else
        {

            if (balCity.Insert(entCity))
            {
                ClearField();
                lblErrMsg.Text = "Data Inserted Successfully";
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblMsgDiv.CssClass = "w-100 my-2 alert alert-success";

            }
            else
            {
                lblErrMsg.Text = balCity.Message;
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

            }
        }
            



    }
    #endregion Button : Save

    #region Fill DropDown
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.fillDropDownState(ddlStateID,Convert.ToInt32( Session["UserID"]));
    }
    #endregion Fill DropDown

    #region Clear Form
    private void ClearField()
    {
        ddlStateID.SelectedIndex = 0;
        txtCityName.Text = "";
        txtPinCode.Text = "";
        txtSTDCode.Text = "";

        ddlStateID.Focus();
    }
    #endregion Clear Form

    #region Fill Controls

    private void fillControls(SqlInt32 CityID)
    {
        CityENT entCity = new CityENT();
        CityBAL balCity = new CityBAL();
        entCity = balCity.SelectByPKUserID(Convert.ToInt32(Session["UserID"]),CityID);
        if (!entCity.StateID.IsNull)
            ddlStateID.SelectedValue = entCity.StateID.Value.ToString().Trim();
        if (!entCity.CityName.IsNull)
            txtCityName.Text = entCity.CityName.Value.ToString().Trim();
        if (!entCity.STDCode.IsNull)
            txtSTDCode.Text = entCity.STDCode.Value.ToString().Trim();
        if (!entCity.PinCode.IsNull)
            txtPinCode.Text = entCity.PinCode.Value.ToString().Trim();

        if (balCity.Message != null && balCity.Message != "")
        {
            lblErrMsg.Text = balCity.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }

    }
    #endregion Fill Controls

}