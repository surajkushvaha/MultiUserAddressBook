<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master"
    AutoEventWireup="true" CodeFile="UserEditPage.aspx.cs" Inherits="AdminPanel_User_UserEditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">

    <div class="container w-50 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div class="my-3">

            <h2 class="text-dark  text-center">

                <asp:Label runat="server" ID="lblMode" Text="Edit Profile"></asp:Label>

            </h2>
        </div>
        <div id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </div>
        <div class="d-flex flex-column mt-4 mb-2">
            <asp:Label ID="lblUserName" CssClass="font-weight-bold" runat="server" Text="Username"></asp:Label>
            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" pattern="^[a-z0-9_]*$"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revUsername" runat="server"
                ControlToValidate="txtUserName" Display="Dynamic"
                ErrorMessage="Only smallcase and numeric Value is Allowed &lt;br/&gt;"
                ValidationExpression="^[a-z0-9_]*$" Font-Italic="True"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                ErrorMessage="Enter Username" Display="Dynamic"
                ControlToValidate="txtUserName" Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RequiredFieldValidator>
        </div>
       
        <div class="d-flex flex-column  mb-2">
            <asp:Label ID="lblDislayName" CssClass="font-weight-bold" runat="server" Text="Display Name"></asp:Label>
            <asp:TextBox ID="txtDisplayName" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDisplayName" runat="server"
                ControlToValidate="txtDisplayName" Display="Dynamic"
                ErrorMessage="Enter Display Name" Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RequiredFieldValidator>
        </div>
        <div class="d-flex flex-column  mb-2">
            <asp:Label ID="lblMobileNo" CssClass="font-weight-bold" runat="server" Text="Mobile No"></asp:Label>
            <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revMobileNo" runat="server"
                ControlToValidate="txtMobileNo" Display="Dynamic"
                ErrorMessage="Enter Valid Mobile Number &lt;br/&gt; Spcial Character except '+' not alloed &lt;br/&gt;"
                ValidationExpression="^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$"
                Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RegularExpressionValidator>
        </div>
        <div class="d-flex flex-column  mb-2">
            <asp:Label ID="lblAddress" CssClass="font-weight-bold" runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
         <div class="d-flex flex-column  mb-2">
                    <asp:Label ID="lblPassword" CssClass="font-weight-bold" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ErrorMessage="Enter Password to Edit Profile" ControlToValidate="txtPassword"
                        Display="Dynamic" Font-Bold="False" Font-Italic="True" 
                        ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RequiredFieldValidator>
                </div>
        <div class="d-flex flex-column  mb-2">
            <asp:Label ID="lblNewPassword" CssClass="font-weight-bold" runat="server" Text="New Password"></asp:Label>
            <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revPassword" runat="server"
                ControlToValidate="txtNewPassword" CssClass="font-weight-lighter"
                Display="Dynamic"
                ErrorMessage="Minimum eight characters, at least one upper case English letter, one lower case English letter, one number and one special character &lt;br/&gt;"
                ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&amp;*-+]).{8,}$"
                Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RegularExpressionValidator>
            
        </div>
        <div class="d-flex flex-column  mb-2">
            <asp:Label ID="lblConfirmPassword" CssClass="font-weight-bold" runat="server" Text="Confirm Password"></asp:Label>
            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="form-control"
                runat="server"></asp:TextBox>
            <asp:CompareValidator ID="cvConfirmPassword" runat="server"
                ErrorMessage="Password should be matched<br/>" ControlToCompare="txtNewPassword"
                ControlToValidate="txtConfirmPassword" Display="Dynamic"
                Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:CompareValidator>
            
        </div>
        <div class="d-flex flex-row my-3">
            <asp:Button ID="btnSave" runat="server" CssClass="form-control btn btn-success text-light shadowCard mx-1"
                Text="Save"  ValidationGroup="vgRegister" OnClick="btnSave_Click" />
            <asp:HyperLink ID="btnCancel" runat="server" CssClass="form-control btn btn-danger text-light shadowCard mx-1"
                        Text="Cancel" NavigateUrl="~/AdminPanel/Profile" />
        </div>
    </div>
</asp:Content>

