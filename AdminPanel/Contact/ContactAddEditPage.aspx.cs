using MultiUserAddressBook;
using MultiUserAddressBook.BAL;
using MultiUserAddressBook.ENT;
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
                //btnAdd.Text = "Edit";

                fillControls(Convert.ToInt32(EncryptionDecryption.Base64Decode(Page.RouteData.Values["ContactID"].ToString().Trim())));

            }
            else
            {
                lblMode.Text = "Add Contact";
                CommonDropDownFillMethods.fillDropDownEmpty(ddlStateID, "State");
                CommonDropDownFillMethods.fillDropDownEmpty(ddlCityID, "City");
                //btnAdd.Text = "Add";

            }

        }     
        
    }

    
    #endregion Load Event

    #region Button : Save
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        String strErrorMsg = "";
       
       
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
            if (fuContactPhoto.PostedFile.ContentLength > 5242880)
            {
                strErrorMsg += "- Upload file less than 5 MB <br/>";
            }
            if (fuContactPhoto.FileName == "" && imgShowImg.ImageUrl == "")
            {
                strErrorMsg += "please upload a file <br/>";
            }
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
                lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                return;
            }
            #endregion Server side validation
            ContactBAL balContact = new ContactBAL();
            ContactENT entContact = new ContactENT();

            #region Gather Information
            if (Session["UserID"] != null)
            {
                entContact.UserID = Convert.ToInt32(Session["UserID"].ToString().Trim());
            }
            if (ddlCountryID.SelectedIndex > 0)
            {
                entContact.CountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
            }
            if (ddlStateID.SelectedIndex > 0)
            {
                entContact.StateID = Convert.ToInt32(ddlStateID.SelectedValue);
            }
            if (ddlCityID.SelectedIndex > 0)
            {
                entContact.CityID = Convert.ToInt32(ddlCityID.SelectedValue);
            }
           
            if (txtContactName.Text.Trim() != "")
            {
                entContact.ContactName = txtContactName.Text.Trim();
            }
            if (txtContactNo.Text.Trim() != "")
            {
                entContact.ContactNo = txtContactNo.Text.Trim();
            }
            if (txtEmail.Text.Trim() != "")
            {
                entContact.Email = txtEmail.Text.Trim();
            }
            if (txtAddress.Text.Trim() != "")
            {
                entContact.Address = txtAddress.Text.Trim();
            }
            if (txtBirthDate.Text.Trim() != "")
            { 
                entContact.BirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim()); 
            }
            if(txtWhatsappNo.Text.Trim() != "")
            {
                entContact.WhatsappNo = txtWhatsappNo.Text.Trim();
            }
            if(txtBloodGroup.Text.Trim() != "")
            {
                entContact.BloodGroup = txtBloodGroup.Text.Trim();
            }
            if(txtFacebookID.Text.Trim() != "")
            {
                entContact.FacebookID = txtFacebookID.Text.Trim();
            }
            if(txtLinkedInID.Text.Trim() !="")
            {
                entContact.LinkedINID = txtLinkedInID.Text.Trim();
            }
            if (txtAge.Text.Trim() != "")
            { 
                entContact.Age = Convert.ToInt32(txtAge.Text.Trim()); 
            }
            #endregion Gather Information

         

            if (Page.RouteData.Values["ContactID"] != null)
            {
                #region Update Record
                //edit mode

                String ContactID = EncryptionDecryption.Base64Decode(Page.RouteData.Values["ContactID"].ToString().Trim());
                entContact.ContactID = Convert.ToInt32(ContactID);
                if (balContact.Update(entContact))
                {
                    SavePhoto(Convert.ToInt32(ContactID));
                    DeleteContactWiseContactCategory(Convert.ToInt32(ContactID));
                    InsertContactWiseContactCategory(Convert.ToInt32(ContactID));

                    Response.Redirect("~/AdminPanel/Contact/List", true);

                }
                else
                {
                    lblErrMsg.Text =  balContact.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                    lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                }
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //insert mode         
                if (balContact.Insert(entContact))
                {
                    entContact.ContactID = Convert.ToInt32(balContact.Message);
                    SavePhoto(entContact.ContactID);
                    InsertContactWiseContactCategory(entContact.ContactID);
                    lblContactMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                    lblMsgDiv.CssClass = "w-100 my-2 alert alert-success";

                    lblContactMsg.Text = "Data Inserted Successfully";
                    ClearField();
                }
                else
                {
                    lblErrMsg.Text = balContact.Message;
                    lblMsgDiv.Visible = true;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                }

                #endregion Insert Record
            }

    }
    #endregion Button : Save
    
    #region PhotoCreation Process
    private void SavePhoto(SqlInt32 ContactID){

            #region Variables for photo
       
            String strFileName = "";
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
            if (strFileName != null && strFileName != "")
            {
                ContactPhotoBAL balContactPhoto = new ContactPhotoBAL();
                ContactPhotoENT entContactPhoto = new ContactPhotoENT();
                entContactPhoto.ContactID = ContactID;
                entContactPhoto.UserID = Convert.ToInt32(Session["UserID"]);
                entContactPhoto.ContactPhotoPath = strFileName;
                entContactPhoto.PhotoFileType = strFileType;
                entContactPhoto.PhotoFileExtension = strExtension;
                entContactPhoto.PhotoFileSize = strFileSize;

                if (Page.RouteData.Values["ContactID"] != null)
                {
                    if (balContactPhoto.Update(entContactPhoto))
                    {

                    }
                    else
                    {

                        lblErrMsg.Text = balContactPhoto.Message;
                        lblErrMsg.Visible = true;
                        lblMsgDiv.Visible = true;
                        lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                        return;
                    }
                }
                else
                {
                    if (balContactPhoto.Insert(entContactPhoto))
                    {

                    }
                    else
                    {

                        lblErrMsg.Text = balContactPhoto.Message;
                        lblErrMsg.Visible = true;
                        lblMsgDiv.Visible = true;
                        lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                        return;
                    }
                }
            }
            else
            {
                lblErrMsg.Text = "Please add a Photo";
                lblErrMsg.Visible = true;
                lblMsgDiv.Visible = true;
                lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                return;
            }

    }
    #endregion PhotoCreation Process

    #region FillDropDown
    private void FillDropDownList()
    {
        CommonDropDownFillMethods.fillDropDownCountry(ddlCountryID,Convert.ToInt32(Session["UserID"]));
        CommonDropDownFillMethods.fillCheckBoxContactCategory(cblContactCategoryID, Convert.ToInt32(Session["UserID"]));
    }
    #endregion FillDropDown

    #region Fill DropDown State By Country
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryID.SelectedIndex > 0)
        {
            CommonDropDownFillMethods.fillDropDownEmpty(ddlStateID, "State");
            CommonDropDownFillMethods.fillDropDownEmpty(ddlCityID, "City");
            CommonDropDownFillMethods.fillDropDownStateByCountryID(ddlStateID, Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(Session["UserID"]));
        }
        else
        {
            CommonDropDownFillMethods.fillDropDownEmpty(ddlStateID, "State");
            CommonDropDownFillMethods.fillDropDownEmpty(ddlCityID, "City");
        }
        

    }
    #endregion Fill DropDown State By Country

    #region Fill DropDown City by State
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStateID.SelectedIndex > 0)
        { 
            CommonDropDownFillMethods.fillDropDownCityByStateID(ddlCityID, Convert.ToInt32(ddlStateID.SelectedValue), Convert.ToInt32(Session["UserID"])); 
        }
        else{
            CommonDropDownFillMethods.fillDropDownEmpty(ddlStateID, "State");
            CommonDropDownFillMethods.fillDropDownEmpty(ddlCityID, "City");
        }
        

    }
    #endregion Fill DropDown City State

    #region Fill Controls

    private void fillControls(SqlInt32 ContactID)
    {
        ContactENT entContact = new ContactENT();
        ContactBAL balContact = new ContactBAL();
        ContactPhotoBAL balContactPhoto = new ContactPhotoBAL();
        ContactPhotoENT entContactPhoto = new ContactPhotoENT();
        entContactPhoto = balContactPhoto.SelctByPKUserID(Convert.ToInt32(Session["UserID"].ToString().Trim()), ContactID);
        entContact = balContact.SelctByPKUserID(Convert.ToInt32(Session["UserID"].ToString().Trim()), ContactID);
        if (!entContact.CountryID.IsNull)
        {
            ddlCountryID.SelectedValue = entContact.CountryID.ToString().Trim();
            CommonDropDownFillMethods.fillDropDownStateByCountryID(ddlStateID, Convert.ToInt32(ddlCountryID.SelectedValue), Convert.ToInt32(Session["UserID"]));

        }
        if (!entContact.StateID.IsNull)
        {
            ddlStateID.SelectedValue = entContact.StateID.ToString().Trim();
            CommonDropDownFillMethods.fillDropDownCityByStateID(ddlCityID, Convert.ToInt32(ddlStateID.SelectedValue), Convert.ToInt32(Session["UserID"]));
        }
        if (!entContact.CityID.IsNull)
        {
            ddlCityID.SelectedValue = entContact.CityID.ToString().Trim();
        }
       
        if (!entContact.ContactName.IsNull)
        {
            txtContactName.Text = entContact.ContactName.ToString().Trim();
        }
        if (!entContactPhoto.ContactPhotoPath.IsNull)
        {
            imgShowImg.ImageUrl = ResolveUrl(entContactPhoto.ContactPhotoPath.ToString().Trim());
            lblDivPhotoContainer.Visible = true;
        }
        if (!entContactPhoto.PhotoFileType.IsNull)
        {
            lblImgFileType.Text = entContactPhoto.PhotoFileType.ToString().Trim();
        }
        if (!entContactPhoto.PhotoFileExtension.IsNull)
        {
            lblImgFileExtension.Text = entContactPhoto.PhotoFileExtension.ToString().Trim();
        }
        if (!entContactPhoto.PhotoFileSize.IsNull)
        {
            lblImgFileSize.Text =entContactPhoto.PhotoFileSize.ToString().Trim();

        }
        if (!entContact.ContactNo.IsNull)
        {
            txtContactNo.Text = entContact.ContactNo.ToString().Trim();
        }
        if (!entContact.WhatsappNo.IsNull)
        {
            txtWhatsappNo.Text = entContact.WhatsappNo.ToString().Trim();
        }
        if (!entContact.BirthDate.IsNull)
        {
            txtBirthDate.Text = (entContact.BirthDate).ToString().Trim();
        }
        if (!entContact.Email.IsNull)
        {
            txtEmail.Text = entContact.Email.ToString().Trim();
        }
        if (!entContact.Age.IsNull)
        {
            txtAge.Text = entContact.Age.ToString().Trim();
        }
        if (!entContact.Address.IsNull)
        {
            txtAddress.Text = entContact.Address.ToString().Trim();
        }
        if (!entContact.BloodGroup.IsNull)
        {
            txtBloodGroup.Text = entContact.BloodGroup.ToString().Trim();
        }
        if (!entContact.FacebookID.IsNull)
        {
            txtFacebookID.Text = entContact.FacebookID.ToString().Trim();
        }
        if (!entContact.LinkedINID.IsNull)
        {
            txtLinkedInID.Text = entContact.LinkedINID.ToString().Trim();
        }
        if (balContact.Message != null)
        {
            lblErrMsg.Text = balContact.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }

        CommonDropDownFillMethods.fillSelectedCheckBoxContactCategory(cblContactCategoryID,Convert.ToInt32(Session["UserID"]),entContact.ContactID);
       

    }
    #endregion Fill Controls

    #region InsertContactWiseContactCategory
    private void InsertContactWiseContactCategory(SqlInt32 ContactID)
    {

        foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
        {
            if (liContactCategoryID.Selected)
            {
                ContactWiseContactCategoryBAL balContactWiseCategory = new ContactWiseContactCategoryBAL();
                ContactWiseContactCategoryENT entContactWiseCategory = new ContactWiseContactCategoryENT();
                entContactWiseCategory.ContactID = ContactID;
                entContactWiseCategory.UserID = Convert.ToInt32(Session["UserID"]);
                entContactWiseCategory.ContactCategoryID = Convert.ToInt32(liContactCategoryID.Value);

                if (balContactWiseCategory.Insert(entContactWiseCategory))
                {
                   
                }
                else
                {
                    lblErrMsg.Text = balContactWiseCategory.Message;
                    lblErrMsg.Visible = true;
                    lblMsgDiv.Visible = true;
                    lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

                }
                foreach (ListItem li in cblContactCategoryID.Items)
                {
                    if (li.Selected)
                    {
                        liContactCategoryID.Selected = false;
                    }
                }
            }
        }
    }
    #endregion InsertContactWiseContactCategory

    #region DeleteContactWiseContactCategory
    private void DeleteContactWiseContactCategory(SqlInt32 ContactID)
    {
        ContactWiseContactCategoryBAL balContactWiseCategory = new ContactWiseContactCategoryBAL();
        if (balContactWiseCategory.Delete(ContactID, Convert.ToInt32(Session["UserID"])))
        {

        }
        else
        {
            lblErrMsg.Text = balContactWiseCategory.Message;
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblMsgDiv.CssClass = "w-100 my-2 alert alert-danger";

        }
    }
    #endregion DeleteContactWiseContactCategory

    #region Clear Form
    private void ClearField()
    {
        ddlCountryID.SelectedIndex = 0;
        ddlStateID.SelectedIndex = 0;
        ddlCityID.SelectedIndex = 0;
        
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

    }
    #endregion Clear Form

}