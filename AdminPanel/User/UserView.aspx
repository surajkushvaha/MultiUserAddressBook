<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master"
    AutoEventWireup="true" CodeFile="UserView.aspx.cs" Inherits="AdminPanel_User_UserView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="container w-50 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>

        </div>

        <h2 class="text-center mt-2">Your Profile</h2>
        <div class="row my-4">
            <asp:Label ID="lblUserName" CssClass="font-weight-bold col-md-6" runat="server" Text="Username"></asp:Label>
            <asp:Label ID="lblShowUserName" CssClass="font-italic col-md-6" runat="server" Text="Username"></asp:Label>
        </div>
        <div class="row my-4">
            <asp:Label ID="lblDisplayName" CssClass="font-weight-bold col-md-6" runat="server"
                Text="Display Name"></asp:Label>
            <asp:Label ID="lblShowDisplayName" CssClass="font-italic col-md-6" runat="server"
                Text="Display Name"></asp:Label>
        </div>
        <div class="row my-4">
            <asp:Label ID="lblMobileNo" CssClass="font-weight-bold col-md-6" runat="server" Text="Mobile No"></asp:Label>
            <asp:Label ID="lblShowMobileNo" CssClass="font-italic col-md-6" runat="server" Text="Mobile No"></asp:Label>
        </div>
        <div class="row my-4">
            <asp:Label ID="lblAddress" CssClass="font-weight-bold col-md-6" runat="server" Text="Address"></asp:Label>
            <asp:Label ID="lblShowAddress" CssClass="font-italic col-md-6" runat="server" Text="Address"></asp:Label>
        </div>
        <div class="row my-4">
            <asp:Label ID="lblCreation" CssClass="font-weight-bold col-md-6" runat="server" Text="Created On"></asp:Label>
            <asp:Label ID="lblShowCreation" CssClass="font-italic col-md-6" runat="server" Text="Created On"></asp:Label>
        </div>
        <div class="row my-4">
            <asp:Label ID="lblModification" CssClass="font-weight-bold col-md-6" runat="server"
                Text="Modified On"></asp:Label>
            <asp:Label ID="lblShowModification" CssClass="font-italic col-md-6" runat="server"
                Text="Modified On"></asp:Label>
        </div>
        <div class="w-100 my-2 alert alert-danger ">
            
            Alert: After Clicking on Delete Account You lost all of your data.
        </div>
        <div class="d-flex flex-row my-3">
            <asp:HyperLink ID="hlEdit" runat="server" CssClass="form-control btn btn-success text-light shadowCard mx-1"
                Text="Edit" NavigateUrl="~/AdminPanel/Profile/Edit" />
            <asp:Button ID="btnDelete" runat="server" CssClass="form-control btn btn-danger text-light shadowCard mx-1"
                Text="Delete Account" OnClick="btnDelete_Click" />
        </div>
        
    </div>


</asp:Content>

