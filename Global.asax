<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        RegisterRoutes(System.Web.Routing.RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
    {
        routes.Ignore("{resource}.axd/{*pathinfo}");

        #region navigation Login & Register
        //login
        routes.MapPageRoute("AdminPanelLogin",
                            "AdminPanel/Login",
                            "~/AdminPanel/Default.aspx");
        //register
        routes.MapPageRoute("AdminPanelRegister",
                            "AdminPanel/Register",
                            "~/AdminPanel/SignUp.aspx");
        #endregion navigation Login & Register

        #region navigation User Profile & Home
        //home
        routes.MapPageRoute("AdminPanelHome", 
                            "AdminPanel/Home", 
                            "~/AdminPanel/Home.aspx");     
        //user profile
        routes.MapPageRoute("AdminPanelUser", 
                            "AdminPanel/Profile",
                            "~/AdminPanel/User/UserView.aspx");      
        //user profile edit
        routes.MapPageRoute("AdminPanelUserEdit",
                            "AdminPanel/Profile/Edit", 
                            "~/AdminPanel/User/UserEditPage.aspx");
        #endregion navigation User Profile & Home
        
        #region navigation Country
        //country list
        routes.MapPageRoute("AdminPanelCountryList", 
                            "AdminPanel/Country/List",
                            "~/AdminPanel/Country/CountryList.aspx");
        //country add
        routes.MapPageRoute("AdminPanelCountryAdd",
                            "AdminPanel/Country/Add",
                            "~/AdminPanel/Country/CountryAddEditPage.aspx");
        //country edit
        routes.MapPageRoute("AdminPanelCountryEdit",
                            "AdminPanel/Country/Edit/{CountryID}",
                            "~/AdminPanel/Country/CountryAddEditPage.aspx");
        #endregion navigation Country

        #region navigation State
        //state list
        routes.MapPageRoute("AdminPanelStateList",
                            "AdminPanel/State/List",    
                            "~/AdminPanel/State/StateList.aspx");
        //state add
        routes.MapPageRoute("AdminPanelStateAdd",
                            "AdminPanel/State/Add",
                            "~/AdminPanel/State/StateAddEditPage.aspx");
        //state edit
        routes.MapPageRoute("AdminPanelStateEdit",
                            "AdminPanel/State/Edit/{StateID}",
                            "~/AdminPanel/State/StateAddEditPage.aspx");
        #endregion navigation State

        #region navigation City
        //city list
        routes.MapPageRoute("AdminPanelCityList",
                            "AdminPanel/City/List",
                            "~/AdminPanel/City/CityList.aspx");
        //city add
        routes.MapPageRoute("AdminPanelCityAdd",
                            "AdminPanel/City/Add",
                            "~/AdminPanel/City/CityAddEditPage.aspx");
        //city edit
        routes.MapPageRoute("AdminPanelCityEdit",
                            "AdminPanel/City/Edit/{CityID}",
                            "~/AdminPanel/City/CityAddEditPage.aspx");
        #endregion navigation City

        #region navigation ContactCategory
        //contact category list
        routes.MapPageRoute("AdminPanelContactCategoryList",
                            "AdminPanel/ContactCategory/List",
                            "~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
        //contact category add
        routes.MapPageRoute("AdminPanelContactCategoryAdd", 
                            "AdminPanel/ContactCategory/Add",
                            "~/AdminPanel/ContactCategory/ContactCategoryAddEditPage.aspx");
        //contact category edit
        routes.MapPageRoute("AdminPanelContactCategoryEdit",
                            "AdminPanel/ContactCategory/Edit/{ContactCategoryID}",
                            "~/AdminPanel/ContactCategory/ContactCategoryAddEditPage.aspx");
        #endregion navigation ContactCategory

        #region navigation Contact
        //contact category list
        routes.MapPageRoute("AdminPanelContactList",
                            "AdminPanel/Contact/List",
                            "~/AdminPanel/Contact/ContactList.aspx");
        //contact category add
        routes.MapPageRoute("AdminPanelContactAdd",
                            "AdminPanel/Contact/Add",
                            "~/AdminPanel/Contact/ContactAddEditPage.aspx");
        //contact category edit
        routes.MapPageRoute("AdminPanelContactEdit",
                            "AdminPanel/Contact/Edit/{ContactID}",
                            "~/AdminPanel/Contact/ContactAddEditPage.aspx");
        #endregion navigation Contact

    }
</script>
