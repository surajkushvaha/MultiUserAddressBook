<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AdminPanel_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi-User Addressbook | Sign in</title>
    <link rel="stylesheet" href="~/Content/CSS/bootstrap.min.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <link rel="stylesheet" href="~/Content/CSS/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">

        <section id="loginForm" class="my-5">
            <div class="container text-center w-25">

                <asp:Image ID="imgDUlogo" runat="server" ImageUrl="~/Content/Images/DU-logo.svg" />
            </div>
            <div class="container text-center w-25 pt-2">
                <h2 class="font-weight-bold text-danger">AddressBook</h2>
                <h3>Sign in to AddressBook</h3>
            </div>
            <div class="container w-25 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
                <div id="lblMsgDiv" runat="server" visible="false" class="w-100 mt-4 mb-0 alert alert-danger ">
                    <asp:Label ID="lblErrMsg" runat="server"
                        EnableViewState="False" Visible="False"></asp:Label>
                </div>
                <div class="d-flex flex-column my-4">
                    <asp:Label ID="lblUserName" CssClass="font-weight-bold" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                        ControlToValidate="txtUserName" Display="Dynamic"
                        ErrorMessage="Enter User Name" ForeColor="#FF5E5E" Font-Italic="True" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                </div>
                <div class="d-flex flex-column  mb-2">
                    <asp:Label ID="lblPassword" CssClass="font-weight-bold" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Enter Password"
                        ForeColor="#FF5E5E" Font-Italic="True" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                </div>
                <div class="d-flex flex-column my-3">
                    <asp:Button ID="btnLogin" runat="server" CssClass="form-control btn btn-success text-light shadowCard"
                        Text="Login" onclick="btnLogin_Click" ValidationGroup="vgLogin" />
                </div>
            </div>
            <p class="container text-center w-25 mt-3 shadowCard" style="background-color: rgba(0, 0, 0, 0.30);
                border-radius: 5px; padding: 10px; font-size: small; font-weight: 500">
                <span>New to AddressBook ?</span>
                <asp:HyperLink ID="hlNewUser" CssClass="text-decoration-none text-danger font-weight-bold"
                    runat="server" NavigateUrl="~/AdminPanel/Register">Create an Account</asp:HyperLink>
            </p>
        </section>
        <section id="footer" class="container text-center my-3">
            <div>
                <small>Created by </small><strong>190540107116 | Kushvaha Suraj</strong>
                <a href="mailto:190540107116@darshan.ac.in" class="text-decoration-none text-danger font-weight-bold">
                    <div class="d-flex flex-row  justify-content-center">
                        <span class="material-icons">email
                        </span>190540107116@darshan.ac.in
                    </div>
                </a>
            </div>
            <div>&copy; 2022 Darshan University All right reserved</div>

        </section>
    </form>
</body>
</html>
