using MultiUserAddressBook.BAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonDropDownFillMethods
/// </summary>
namespace MultiUserAddressBook
{
    public static class CommonDropDownFillMethods
    {
        #region FillDropDownListCountry
        public static void fillDropDownCountry(DropDownList ddl, SqlInt32 UserID)
        {
            CountryBAL balCountry = new CountryBAL();
            ddl.DataSource = balCountry.SelectForDropDownList(UserID);
            ddl.DataValueField = "CountryID";
            ddl.DataTextField = "CountryName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Country"));

        }

        #endregion FillDropDownListCountry

        #region FillDropDownState
        public static void fillDropDownState(DropDownList ddl, SqlInt32 UserID)
        {
            StateBAL balState = new StateBAL();
            ddl.DataSource = balState.SelectForDropDownList(UserID);
            ddl.DataValueField = "StateID";
            ddl.DataTextField = "StateName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select State"));

        }
        #endregion FillDropDownState

        #region FillDropDownListStateByCountryID
        public static void fillDropDownStateByCountryID(DropDownList ddl, SqlInt32 CountryID, SqlInt32 UserID)
        {
            StateBAL balState = new StateBAL();
            ddl.DataSource = balState.SelectByCountryIDUserID(CountryID, UserID);
            ddl.DataValueField = "StateID";
            ddl.DataTextField = "StateName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select State"));

        }
        #endregion FillDropDownListStateByCountryID

        #region Fill DropDownCity
        public static void fillDropDownCity(DropDownList ddl, SqlInt32 UserID)
        {
            CityBAL balCity = new CityBAL();
            ddl.DataSource = balCity.SelectForDropDownList(UserID);
            ddl.DataValueField = "CityID";
            ddl.DataTextField = "CityName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select City"));
        }
        #endregion Fill DropDownCity

        #region fillDropdDownCityByStateID
        public static void fillDropDownCityByStateID(DropDownList ddl, SqlInt32 StateID, SqlInt32 UserID)
        {
            CityBAL balCity = new CityBAL();
            ddl.DataSource = balCity.SelectByStateIDUserID(StateID,UserID);
            ddl.DataValueField = "CityID";
            ddl.DataTextField = "CityName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select City"));
        }        
        #endregion fillDropdDownCityByStateID

        #region FillCheckBoxListForContactCategoryList
        //public static void fillSelectedCheckBoxContactCategory(CheckBoxList cbl, SqlInt32 UserID)
        //{
        //    ContactCategoryBAL balContactCategory = new ContactCategoryBAL();
        //    cbl.DataSource = balContactCategory.SelectAllByUserID(UserID);
        //    cbl.DataValueField = "ContactCategoryID"; 
        //    cbl.DataTextField = "ContactCategoryName";
        //    cbl.DataBind();

        //}
        public static void fillSelectedCheckBoxContactCategory(CheckBoxList cbl, SqlInt32 UserID, SqlInt32 ContactID)
        {
            ContactWiseContactCategoryBAL balContactWiseContactCategory = new ContactWiseContactCategoryBAL();
            DataTable dt = new DataTable();
            dt =  balContactWiseContactCategory.SelectForCheckBoxList(UserID, ContactID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr[1].Equals(DBNull.Value))
                        cbl.Items.FindByValue(dr[1].ToString().Trim()).Selected = true;
                }
            }
        }
        public static void fillCheckBoxContactCategory(CheckBoxList cbl, SqlInt32 UserID)
        {
            ContactCategoryBAL balContactCategory = new ContactCategoryBAL();
            cbl.DataSource = balContactCategory.SelectAllByUserID(UserID);
            cbl.DataValueField = "ContactCategoryID";
            cbl.DataTextField = "ContactCategoryName";
            cbl.DataBind();

        }
        #endregion FillCheckBoxListForContactCategoryList 



        #region fillDropDownEmpty
        public static void fillDropDownEmpty(DropDownList ddl , String DropDownListName)
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0,new ListItem("Select" + DropDownListName, "-1"));

        }
        #endregion fillDropDownEmpty
    }
}