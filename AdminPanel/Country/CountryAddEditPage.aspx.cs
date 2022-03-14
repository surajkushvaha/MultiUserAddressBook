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
using MultiUserAddressBook;
using MultiUserAddressBook.BAL;
using MultiUserAddressBook.ENT;

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
                fillControls(Convert.ToInt32(EncryptionDecryption.Base64Decode( Page.RouteData.Values["CountryID"].ToString().Trim())));
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
       
        #endregion Server side validation

        CountryENT entCountry = new CountryENT();
        if (txtCountryName.Text != "")
        {
             entCountry.CountryName = txtCountryName.Text.Trim();

        }

        if (txtCountryCode.Text != "")
        {
            entCountry.CountryCode = txtCountryCode.Text.Trim();

        }
         if (Session["UserID"] != null)
        {
            entCountry.UserID = Convert.ToInt32(Session["UserID"]);
        }

        
        CountryBAL balCountry = new CountryBAL();

        if (Page.RouteData.Values["CountryID"] != null)
        {
            SqlInt32 CountryID = Convert.ToInt32(EncryptionDecryption.Base64Decode(Page.RouteData.Values["CountryID"].ToString().Trim()));
            entCountry.CountryID = CountryID;
            if (balCountry.Update(entCountry))
            {
                ClearField();
                Response.Redirect("~/AdminPanel/Country/CountryList.aspx");
            }
            else
            {
                lblErrMsg.Text = balCountry.Message;
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;

            }
        }
        else
        {

            if (balCountry.Insert(entCountry))
            {
                ClearField();
                lblErrMsg.Text = "Data Inserted Successfully";
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
            }
            else
            {
                lblErrMsg.Text = balCountry.Message;
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
            }
        }
            

    }
    #endregion Button : Save


    #region Clear Fields
    private void ClearField()
    {
        txtCountryCode.Text = "";
        txtCountryName.Text = "";
        txtCountryName.Focus();
    }
    #endregion Clear Fields


    #region Fill Controls
    private void fillControls(SqlInt32 CountryID)
    {
        CountryBAL balCountry = new CountryBAL();
        CountryENT entCountry = new CountryENT();

        entCountry = balCountry.SelectByPKUserID(Convert.ToInt32(Session["UserID"]),CountryID);

        if (!entCountry.CountryName.IsNull)
        {
            txtCountryName.Text = entCountry.CountryName.ToString();
        }
        if (!entCountry.CountryCode.IsNull)
        {
            txtCountryCode.Text = entCountry.CountryCode.ToString();
        }

        if (balCountry.Message != null && balCountry.Message != "")
        {
            lblErrMsg.Text = balCountry.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
        }

    }
    #endregion Fill Controls
}