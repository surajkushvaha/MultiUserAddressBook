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

public partial class AdminPanel_Contact_ContactAddEditPage : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            FillDropDownForCountryList();
            
            FillDropDownForContactCategoryList();
            if (Request.QueryString["ContactID"] != null)
            {
                lblMode.Text = "Edit Contact";
                btnAdd.Text = "Edit";
                fillControls(Convert.ToInt32(Request.QueryString["ContactID"]));

            }
            else
            {
                lblMode.Text = "Add Contact";
                btnAdd.Text = "Add";

            }

        }     
        
    }

    
    #endregion Load Event

    #region Button : Save
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Local Variables
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlInt32 strContactCategoryID = SqlInt32.Null;
        SqlString strContactName = SqlString.Null;
        SqlString strContactNo = SqlString.Null;
        SqlString strWhatsappNo = SqlString.Null;
        SqlDateTime strBirthDate = SqlDateTime.Null;
        SqlString strEmail = SqlString.Null;
        SqlInt32 strAge = SqlInt32.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strFacebookID = SqlString.Null;
        SqlString strLinkedInID = SqlString.Null;

        String strErrorMsg = "";

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variables
        try
        {
            #region Server side validation
            if (ddlCountry.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  Country <br/>";
            }
            if (ddlState.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  State <br/>";
            }
            if (ddlCity.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  City <br/>";
            }
            if (ddlContactCategory.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  Contact Category <br/>";
            }
            if (txtContactName.Text.Trim() == "")
            {
                strErrorMsg += "- Enter  Contact Name <br/>";
            }
            if (txtContactNo.Text.Trim() == "")
            {
                strErrorMsg += "- Enter  Contact No <br/>";
            }
            if (txtEmail.Text.Trim() == "")
            {
                strErrorMsg += "- Enter  Email <br/>";
            }
            if (txtAddress.Text.Trim() == "")
            {
                strErrorMsg += "- Enter  Address <br/>";
            }

            if (strErrorMsg != "")
            {
                lblContactMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblContactMsg.Text = strErrorMsg;
                return;
            }
            #endregion Server side validation

            #region Gather Information
            if (ddlCountry.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            if (ddlState.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlState.SelectedValue);
            }
            if (ddlCity.SelectedIndex > 0)
            {
                strCityID = Convert.ToInt32(ddlCity.SelectedValue);
            }
            if (ddlContactCategory.SelectedIndex > 0)
            {
                strContactCategoryID = Convert.ToInt32(ddlContactCategory.SelectedValue);
            }
            if (txtContactName.Text.Trim() != "")
            {
                strContactName = txtContactName.Text.Trim();
            }
            if (txtContactNo.Text.Trim() != "")
            {
                strContactNo = txtContactNo.Text.Trim();
            }
            if (txtEmail.Text.Trim() != "")
            {
                strEmail = txtEmail.Text.Trim();
            }
            if (txtAddress.Text.Trim() != "")
            {
                strAddress = txtAddress.Text.Trim();
            }
            if (txtBirthDate.Text.Trim() != "")
            { 
                strBirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim()); 
            }
            strWhatsappNo = txtWhatsappNo.Text.Trim();
            strBloodGroup = txtBloodGroup.Text.Trim();
            strFacebookID = txtFacebookID.Text.Trim();
            strLinkedInID = txtLinkedInID.Text.Trim();
            if (txtAge.Text.Trim() != "")
            { 
                strAge = Convert.ToInt32(txtAge.Text.Trim()); 
            }
            #endregion Gather Information

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
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityID", strCityID);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryID);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@WhatsappNo", strWhatsappNo);

            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
            objCmd.Parameters.AddWithValue("@FacebookID", strFacebookID);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedInID);
            #endregion Set Connection & Command Object
            if (Request.QueryString["ContactID"] != null)
            {
                #region Update Record
                //edit mode
                objCmd.CommandText = "[dbo].[PR_Contact_UpdateByPKUserID]";
                objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Contact/ContactList.aspx", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //insert mode
                objCmd.CommandText = "[dbo].[PR_Contact_InsertByUserID]";
                objCmd.ExecuteNonQuery();
                lblContactMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblContactMsg.Text = "Data Inserted Successfully";
                ddlCountry.SelectedIndex = 0;
                ddlState.SelectedIndex = 0;
                ddlCity.SelectedIndex = 0;
                ddlContactCategory.SelectedIndex = 0;
                txtContactName.Text = "";
                txtContactNo.Text = "";
                txtWhatsappNo.Text = "";
                txtBirthDate.Text = "";
                txtEmail.Text = "";
                txtAge.Text = "";
                txtAddress.Text = "";
                txtBloodGroup.Text = "";
                txtFacebookID.Text = "";
                txtLinkedInID.Text = "";

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

    #region Fill DropDown Country
    private void FillDropDownForCountryList()
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectForDropDownListbyUserID";
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
                lblErrMsg.Text = "Kindly Add Something in Country First.";
                
            }
            ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
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
    #endregion Fill DropDown Country

    #region Fill DropDown State
    private void fillDropDownListStateByCountryID(SqlInt32 CountryID){
        #region Local Variable
        lblErrMsg.Visible = false;
        lblMsgDiv.Visible = false;
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {

            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectByCountryIDUserID";
           
                objCmd.Parameters.AddWithValue("@CountryID", CountryID);
            
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }

            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows == true)
            {
                ddlState.DataSource = objSDR;
                ddlState.DataValueField = "StateID";
                ddlState.DataTextField = "StateName";
                ddlState.DataBind();

                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

            }
            else
            {
                ddlState.Items.Clear();
                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));

                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in State Related to Selected Country First.";
              
            }
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
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex > 0)
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            fillDropDownListStateByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));
        }
        else
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));

        }
        

    }
    #endregion Fill DropDown State

    #region Fill DropDown City
    private void fillDropdDownCityByStateID(SqlInt32 StateID){
       #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        lblErrMsg.Visible = false;
        lblMsgDiv.Visible = false;
        #endregion Local Variable

        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectByStateIDUserID";
            
                objCmd.Parameters.AddWithValue("@StateID", StateID);
            
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows == true)
            {
                ddlCity.DataSource = objSDR;
                ddlCity.DataValueField = "CityID";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));

            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in City Releated to Selected State First.";
            }
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
        { 
            fillDropdDownCityByStateID(Convert.ToInt32(ddlState.SelectedValue)); 
        }else{
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));

        }
        

    }
    #endregion Fill DropDown City

    #region Fill DropDown Contact Category

    private void FillDropDownForContactCategoryList()
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows == true)
            {
                ddlContactCategory.DataSource = objSDR;
                ddlContactCategory.DataValueField = "ContactCategoryID";
                ddlContactCategory.DataTextField = "ContactCategoryName";
                ddlContactCategory.DataBind();
            }
            else
            {
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblErrMsg.Text = "Kindly Add Something in Contact Category First.";
            }
            ddlContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
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
    #endregion Fill DropDown Contact Category

    #region Fill Controls

    private void fillControls(SqlInt32 ContactID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Contact_SelectByPKUserID";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            #endregion Set Connection & Command Object

            #region Read the value and set the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        fillDropDownListStateByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        //fillDropDownState();
                        ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                        fillDropdDownCityByStateID(Convert.ToInt32(ddlState.SelectedValue));

                    }
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        //fillDropdDownCity();
                        ddlCity.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                    {
                        ddlContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                    }
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["WhatsappNo"].Equals(DBNull.Value))
                    {
                        txtWhatsappNo.Text = objSDR["WhatsappNo"].ToString().Trim();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        txtBirthDate.Text = Convert.ToDateTime(objSDR["BirthDate"]).Date.ToString().Trim();
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFacebookID.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (!objSDR["LinkedInID"].Equals(DBNull.Value))
                    {
                        txtLinkedInID.Text = objSDR["LinkedInID"].ToString().Trim();
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