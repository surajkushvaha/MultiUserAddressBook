using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_ContactAddEditPage : System.Web.UI.Page{

    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            FillDropDownList();
            
            if (Page.RouteData.Values["ContactID"] != null)
            {
                lblMode.Text = "Edit Contact";
                btnAdd.Text = "Edit";

                fillControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactID"].ToString().Trim())));

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
        SqlInt32 strContactID = 0;
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        //SqlInt32 strContactCategoryID = SqlInt32.Null;
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
            if (ddlCountryID.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  Country <br/>";
            }
            if (ddlStateID.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  State <br/>";
            }
            if (ddlCityID.SelectedIndex == 0)
            {
                strErrorMsg += "- Select  City <br/>";
            }
            //if (ddlContactCategory.SelectedIndex == 0)
            //{
            //    strErrorMsg += "- Select  Contact Category <br/>";
            //}
            bool flag = true;
            foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
            {
                if (liContactCategoryID.Selected)
                {
                    flag = false;
                }
            }
            if (flag != false)
            {
                strErrorMsg += "Please select atleast on Contact Category";
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
            if (ddlCountryID.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
            }
            if (ddlStateID.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
            }
            if (ddlCityID.SelectedIndex > 0)
            {
                strCityID = Convert.ToInt32(ddlCityID.SelectedValue);
            }
            //if (ddlContactCategory.SelectedIndex > 0)
            //{
            //    strContactCategoryID = Convert.ToInt32(ddlContactCategory.SelectedValue);
            //}
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
            //objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryID);
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



            if (Page.RouteData.Values["ContactID"] != null)
            {
                #region Update Record
                //edit mode

                String ContactID = CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactID"].ToString().Trim());
                objCmd.CommandText = "[dbo].[PR_Contact_UpdateByPKUserID]";
                objCmd.Parameters.AddWithValue("@ContactID", ContactID);
                objCmd.ExecuteNonQuery();
                SavePhoto(Convert.ToInt32(ContactID));
                DeleteContactWiseContactCategory(Convert.ToInt32(ContactID));
                InsertContactWiseContactCategory(Convert.ToInt32(ContactID));

                Response.Redirect("~/AdminPanel/Contact/List", true);
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //insert mode         
                objCmd.CommandText = "[dbo].[PR_Contact_InsertByUserID]";
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction =ParameterDirection.Output;
                
                objCmd.ExecuteNonQuery();
                strContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);
                SavePhoto(strContactID);
                InsertContactWiseContactCategory(strContactID);
                lblContactMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblContactMsg.Text = "Data Inserted Successfully";
                ddlCountryID.SelectedIndex = 0;
                ddlStateID.SelectedIndex = 0;
                ddlCityID.SelectedIndex = 0;
                //cblContactCategoryID.SelectedIndex = 0;
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
                ddlCountryID.Focus();

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
    
    #region PhotoCreation Process
    private void SavePhoto(SqlInt32 ContactID){

            #region Variables for photo
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            SqlString strFileName = SqlString.Null;
            String fileName = "";
            String strExtension = "";
            String strFileType = "";
            String strFileSize = "";
            String userUniqueness = Session["UserID"].ToString().Trim() + "_" + Session["DisplayName"].ToString().Trim();
            String folderPath = "~/userContent/" + userUniqueness + "/";
            String abosoluteFolderPath = Server.MapPath(folderPath);
            #endregion Variables for photo

            if (fuContactPhoto.HasFile)
            {
                #region Create and Add photo
                strExtension = System.IO.Path.GetExtension(fuContactPhoto.FileName);
                fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + strExtension;
                strFileName = folderPath + fileName;
                strFileType = fuContactPhoto.PostedFile.ContentType;
                String MBOrKB = "";
                int fileSize = fuContactPhoto.PostedFile.ContentLength;
                float fileSizeInMb = fileSize / (1024*1024);
                MBOrKB = "MB";
                if (fileSizeInMb <= 0)
                {
                    fileSizeInMb = fileSize / 1024;
                    MBOrKB = "KB";

                }
                strFileSize = fileSizeInMb + MBOrKB;
                if (!Directory.Exists(abosoluteFolderPath))
                {
                    Directory.CreateDirectory(abosoluteFolderPath);

                }

                if (Page.RouteData.Values["ContactID"] != null)
                {
                    //edit mode
                    #region Delete photo if available before
                    String AbsolutePath = ResolveUrl(imgShowImg.ImageUrl);
                    FileInfo file = new FileInfo(Server.MapPath(AbsolutePath));
                    if (file.Exists)
                    {
                        file.Delete();

                    }
                    #endregion Delete photo if available before

                }
                fuContactPhoto.SaveAs(abosoluteFolderPath + fileName.ToString().Trim());
                #endregion Create and Add photo

            }
            else
            {
                #region If File Not selected
                if (Page.RouteData.Values["ContactID"] != null)
                {
                    strFileName = imgShowImg.ImageUrl;
                    strFileType = lblImgFileType.Text;
                    strExtension = lblImgFileExtension.Text;
                    strFileSize = lblImgFileSize.Text;
                    

                }
                #endregion If File Not selected

            }
            try
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.AddWithValue("@ContactID", ContactID);
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }
                objCmd.Parameters.AddWithValue("@ContactPhotoPath", strFileName);
                objCmd.Parameters.AddWithValue("@PhotoFileType", strFileType);
                objCmd.Parameters.AddWithValue("@PhotoFileExtension", strExtension);
                objCmd.Parameters.AddWithValue("@PhotoFileSize", strFileSize);
                if (Page.RouteData.Values["ContactID"] != null)
                {
                    objCmd.CommandText = "PR_ContactPhoto_UpdateByContactIdUserID";
                    objCmd.ExecuteNonQuery();
                }
                else
                {
                    objCmd.CommandText = "PR_ContactPhoto_InsertByContactIdUserID";
                    objCmd.ExecuteNonQuery();
                }
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
    #endregion PhotoCreation Process

    #region FillDropDown
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID,Convert.ToInt32(Session["UserID"]),lblErrMsg,lblMsgDiv);
        CommonDropDownFillMethods.FillCheckBoxListForContactCategoryList(cblContactCategoryID, Convert.ToInt32(Session["UserID"]), lblErrMsg, lblMsgDiv);
    }
    #endregion FillDropDown

    #region Fill DropDown State By Country
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryID.SelectedIndex > 0)
        {
            ddlStateID.Items.Clear();
            ddlCityID.Items.Clear();
            //fillDropDownListStateByCountryID(Convert.ToInt32(ddlCountryID.SelectedValue));

            CommonDropDownFillMethods.FillDropDownListStateByCountryID(ddlStateID, Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(Session["UserID"]), ddlCityID,lblErrMsg,lblMsgDiv);
        }
        else
        {
            ddlStateID.Items.Clear();
            ddlCityID.Items.Clear();
            ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));

        }
        

    }
    #endregion Fill DropDown State By Country

    #region Fill DropDown City by State
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStateID.SelectedIndex > 0)
        { 
            CommonDropDownFillMethods.fillDropdDownCityByStateID(ddlCityID, Convert.ToInt32(ddlStateID.SelectedValue), Convert.ToInt32(Session["UserID"]), lblErrMsg, lblMsgDiv); 
        }else{
            ddlStateID.Items.Clear();
            ddlCityID.Items.Clear();
            ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));

        }
        

    }
    #endregion Fill DropDown City State

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
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        CommonDropDownFillMethods.FillDropDownListStateByCountryID(ddlStateID, Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(Session["UserID"]), ddlCityID,lblErrMsg,lblMsgDiv);

                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                        CommonDropDownFillMethods.fillDropdDownCityByStateID(ddlCityID, Convert.ToInt32(ddlStateID.SelectedValue), Convert.ToInt32(Session["UserID"]),lblErrMsg,lblMsgDiv);


                    }
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                   
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (!objSDR["ContactPhotoPath"].Equals(DBNull.Value))
                    {
                        imgShowImg.ImageUrl = ResolveUrl(objSDR["ContactPhotoPath"].ToString().Trim());
                            lblDivPhotoContainer.Visible =true;
                    }
                    if (!objSDR["PhotoFileType"].Equals(DBNull.Value))
                    {
                        lblImgFileType.Text = objSDR["PhotoFileType"].ToString().Trim();
                    }
                    if (!objSDR["PhotoFileExtension"].Equals(DBNull.Value))
                    {
                        lblImgFileExtension.Text =  objSDR["PhotoFileExtension"].ToString().Trim();
                    }
                    if (!objSDR["PhotoFileSize"].Equals(DBNull.Value))
                    {
                        lblImgFileSize.Text =  objSDR["PhotoFileSize"].ToString().Trim();

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

            FillSelecteCheckBoxList(ContactID);

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

    #region InsertContactWiseContactCategory
    private void InsertContactWiseContactCategory(SqlInt32 ContactID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            
            foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
            {
                if (liContactCategoryID.Selected)
                {

                    SqlCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_ContactWiseContactCategory_InsertByContactIDUserIDContactCategoryID";
                    objCmd.Parameters.AddWithValue("@ContactID", ContactID);
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
                    objCmd.Parameters.AddWithValue("@ContactCategoryID", liContactCategoryID.Value.ToString());
                    objCmd.ExecuteNonQuery();
                }
            }
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Set Connection & Command Object
            foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
            {
                if (liContactCategoryID.Selected)
                {
                    liContactCategoryID.Selected = false;
                }
            }

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
    #endregion InsertContactWiseContactCategory

    #region DeleteContactWiseContactCategory
    private void DeleteContactWiseContactCategory(SqlInt32 ContactID)
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
            objCmd.CommandText = "PR_ContactWiseContactCategory_DeleteByContactIDUserID";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID);
            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            objCmd.ExecuteNonQuery();
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Set Connection & Command Object


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
    #endregion DeleteContactWiseContactCategory

    #region FillSelecteCheckBoxList
    private void FillSelecteCheckBoxList(SqlInt32 ContactID)
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
            objCmd.CommandText = "PR_ContactWiseContactCategory_SelectForFillCheckBoxByContactIDUserID";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID);
            objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                    {
                        foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
                        {
                            if (liContactCategoryID.Value == objSDR["ContactCategoryID"].ToString().Trim())
                            {
                                liContactCategoryID.Selected = true;
                            }
                        }
                    }

                }
            }
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Set Connection & Command Object

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
    #endregion FillSelecteCheckBoxList
}