using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_User_UserView : System.Web.UI.Page
{
    #region On Load Page
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Session["UserID"]!= null)
            {
                fillData();
            }
            else
            {
                Response.Redirect("~/AdminPanel/Login", true);
            }
        }
    }
    #endregion On Load Page

    #region fill data
    private void fillData()
    {

        #region local variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion local variable

        try
        {
             #region Connection & Command object
            if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();
                }

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_User_SelectByUserID";
                if (Session["UserID"] != null)
                {
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                }
            #endregion Connection & Command Object

             #region read the value and set the controls
                SqlDataReader objSDR = objCmd.ExecuteReader();
                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        if (!objSDR["Username"].Equals(DBNull.Value))
                        {
                            lblShowUserName.Text = objSDR["Username"].ToString().Trim();
                        }
                        if (!objSDR["DisplayName"].Equals(DBNull.Value))
                        {
                            lblShowDisplayName.Text = objSDR["DisplayName"].ToString().Trim();
                        }
                        if (!objSDR["MobileNo"].Equals(DBNull.Value))
                        {
                            lblShowMobileNo.Text = objSDR["MobileNo"].ToString().Trim();
                             if(lblShowMobileNo.Text == "" )
                            {
                                lblShowMobileNo.Text = "Mobile No Not Provided By You";
                            }
                        }
                        else
                        {
                            lblShowMobileNo.Text = "Mobile No Not Provided By You";
                        }
                        if (!objSDR["Address"].Equals(DBNull.Value))
                        {
                            lblShowAddress.Text = objSDR["Address"].ToString().Trim();
                            if(lblShowAddress.Text == "")
                            {
                                lblShowAddress.Text = "Address Not Provided By You";
                            }
                        }
                        else
                        {
                            lblShowAddress.Text = "Address Not Provided By You";
                        }
                        if (!objSDR["CreationDate"].Equals(DBNull.Value))
                        {
                            lblShowCreation.Text = objSDR["CreationDate"].ToString().Trim();
                        }
                        if (!objSDR["ModificationDate"].Equals(DBNull.Value))
                        {
                            lblShowModification.Text = objSDR["ModificationDate"].ToString().Trim();
                        }
                    }
                }
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
                #endregion  read the value and set the controls
        }
        catch(Exception ex){
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = ex.Message;
        }
        finally{
            if (objConn.State == ConnectionState.Open)
            {
               objConn.Close();
            }
        }
            
    }
    #endregion fill data

    #region Delete User
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        #region local variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion local variable

        try
        {
            #region Connection & Command object
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_DeleteByUserID";
            if (Session["UserID"] != null)
            {
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            }
            objCmd.ExecuteNonQuery();
            
            Session.Clear();

            Response.Redirect("~/AdminPanel/Login", true);
            #endregion Connection & Command Object

           
            #region read the value and set the controls
           
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion  read the value and set the controls
        }
        catch (Exception ex)
        {
            lblErrMsg.Visible = true;
            lblMsgDiv.Visible = true;
            lblErrMsg.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }

    }
    #endregion Delete User
  
}