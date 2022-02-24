<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi-User Addressbook | Create New Account</title>
    <link rel="stylesheet" href="~/Content/CSS/bootstrap.min.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <link rel="stylesheet" href="~/Content/CSS/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">

        <section id="loginForm" class="mt-3">
            <div class="container text-center w-25">
                <asp:Image ID="imgDUlogo" runat="server" ImageUrl="~/Content/Images/DU-logo.svg" />

            </div>

            <div class="container text-center w-25 pt-2">
                <h2 class="font-weight-bold text-danger">AddressBook</h2>
                <h3>Create New Account</h3>
            </div>
            <div class="container w-25 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
                <div id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </div>
                <div class="d-flex flex-column mt-4 mb-2">
                    <asp:Label ID="lblUserName" CssClass="font-weight-bold" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"  pattern="^[a-z0-9_]*$"
                      ></asp:TextBox>
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
                    <asp:Label ID="lblPassword" CssClass="font-weight-bold" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revPassword" runat="server" 
                        ControlToValidate="txtPassword" CssClass="font-weight-lighter" 
                        Display="Dynamic" 
                        ErrorMessage="Minimum eight characters, at least one upper case English letter, one lower case English letter, one number and one special character &lt;br/&gt;" 
                         
                        
                        ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&amp;*-+]).{8,}$" 
                        Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ErrorMessage="Enter Password" ControlToValidate="txtPassword"
                        Display="Dynamic" Font-Bold="False" Font-Italic="True" 
                        ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RequiredFieldValidator>
                </div>
                <div class="d-flex flex-column  mb-2">
                    <asp:Label ID="lblConfirmPassword" CssClass="font-weight-bold" runat="server" Text="Confirm Password"></asp:Label>
                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="form-control"
                        runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cvConfirmPassword" runat="server"
                        ErrorMessage="Password should be matched<br/>" ControlToCompare="txtPassword"
                        ControlToValidate="txtConfirmPassword" Display="Dynamic" 
                        Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server"
                        ErrorMessage="Enter Confirm Password"
                        ControlToValidate="txtConfirmPassword" Display="Dynamic" 
                        Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgRegister"></asp:RequiredFieldValidator>
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
                <div class="d-flex flex-column my-3">
                    <asp:Button ID="btnSave" runat="server" CssClass="form-control btn btn-success text-light shadowCard"
                        Text="Sign Up" OnClick="btnSave_Click" ValidationGroup="vgRegister" />
                </div>
            </div>
            <p class="container text-center w-25 mt-3 shadowCard" style="background-color: rgba(0, 0, 0, 0.30);
                border-radius: 5px; padding: 10px; font-size: small; font-weight: 500">
                <span>Already an User ?</span>
                <asp:HyperLink ID="hlNewUser" CssClass="text-decoration-none text-danger font-weight-bold"
                    runat="server" NavigateUrl="~/AdminPanel/Login">Login to AddressBook</asp:HyperLink>
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
