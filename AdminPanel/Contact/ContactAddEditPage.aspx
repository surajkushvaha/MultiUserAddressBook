<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEditPage.aspx.cs" Inherits="AdminPanel_Contact_ContactAddEditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
     <section class="mx-3 rounded p-2  card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div class="mb-3">

            <h2 class="text-dark">
                <asp:HyperLink ID="hlBackBtn" runat="server" NavigateUrl="~/AdminPanel/Contact/ContactList.aspx"
                    CssClass="px-2 text-decoration-none text-danger">
<span class="material-icons">
arrow_back
</span> 
                </asp:HyperLink>
                <asp:Label runat="server" ID="lblMode"></asp:Label>

            </h2>
        </div>


        <div id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-info ">
            <asp:Label ID="lblContactMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblCountryName" runat="server" Text="Country Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCountryName" runat="server" 
                    ControlToValidate="ddlCountry" Display="Dynamic" 
                    ErrorMessage="Select a Country" Font-Italic="True" ForeColor="#FF5E5E" 
                    InitialValue="-1" ValidationGroup="vgContact"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblState" runat="server" Text="State Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvStateName" runat="server" 
                    ControlToValidate="ddlState" Display="Dynamic" ErrorMessage="Select a State" 
                    Font-Italic="True" ForeColor="#FF5E5E" InitialValue="-1" 
                    ValidationGroup="vgContact"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblCity" runat="server" Text="City Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvvCityName" runat="server" 
                    ControlToValidate="ddlCity" Display="Dynamic" ErrorMessage="Select a City" 
                    Font-Italic="True" ForeColor="#FF5E5E" InitialValue="-1" 
                    ValidationGroup="vgContact"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblContactCategory" runat="server" Text="Contact Category Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlContactCategory" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvContactCategory" runat="server" 
                    ControlToValidate="ddlContactCategory" Display="Dynamic" 
                    ErrorMessage="Select a Contact Category" Font-Italic="True" ForeColor="#FF5E5E" 
                    InitialValue="-1" ValidationGroup="vgContact"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblContactName" runat="server" Text="Contact Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtContactName" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContactName" runat="server" 
                ControlToValidate="txtContactName" Display="Dynamic" 
                ErrorMessage="Enter Contact Name" Font-Italic="True" ForeColor="#FF5E5E" 
                ValidationGroup="vgContact"></asp:RequiredFieldValidator>
            </div>
            
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblContactNo" runat="server" Text="Contact No" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtContactNo" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" 
                    ControlToValidate="txtContactNo" Display="Dynamic" 
                    ErrorMessage="Enter Mobile No &lt;br/&gt;" Font-Italic="True" 
                    ForeColor="#FF5E5E" ValidationGroup="vgContact"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revContactNo" runat="server" 
                    ControlToValidate="txtContactNo" Display="Dynamic" 
                    ErrorMessage="Enter Valid Number &lt;br/&gt;" Font-Italic="True" 
                    ForeColor="#FF5E5E" 
                    ValidationExpression="^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$" 
                    ValidationGroup="vgContact"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblWhatsappNo" runat="server" Text="Whatsapp No" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtWhatsappNo" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revWhatsappNO" runat="server" 
                    ControlToValidate="txtWhatsappNo" Display="Dynamic" 
                    ErrorMessage="Enter Valid Number" Font-Italic="True" ForeColor="#FF5E5E" 
                    ValidationExpression="^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$" 
                    ValidationGroup="vgContact"></asp:RegularExpressionValidator>
            </div>
        </div>


        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtBirthDate"  CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblEmail" runat="server" Text="Email ID" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEmail" type="email" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                    ControlToValidate="txtEmail" Display="Dynamic" 
                    ErrorMessage="Enter Email Address &lt;br/&gt;" Font-Italic="True" 
                    ForeColor="#FF5E5E" ValidationGroup="vgContact"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" Display="Dynamic" 
                    ErrorMessage="Enter Valid Email" Font-Italic="True" ForeColor="#FF5E5E" 
                    ValidationExpression="[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+" 
                    ValidationGroup="vgContact" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblAge" runat="server" Text="Age" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtAge" type="number" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RangeValidator ID="rvAge" runat="server" ControlToValidate="txtAge" 
                    Display="Dynamic" ErrorMessage="Age cant be exceed 130" Font-Italic="True" 
                    ForeColor="#FF5E5E" MaximumValue="130" MinimumValue="0" 
                    ValidationGroup="vgContact" Type="Integer"></asp:RangeValidator>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" Display="Dynamic" ErrorMessage="Enter Address" 
                    Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgAddress" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblBloodGroup" runat="server" Text="BloodGroup" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtBloodGroup" CssClass="form-control" runat="server"></asp:TextBox>
               
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblFacebookID" runat="server" Text="Facebook ID" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtFacebookID"  CssClass="form-control" runat="server" placeholder="iamfacebookuser"></asp:TextBox>
              <asp:RegularExpressionValidator ID="revFacebookID" runat="server" 
                        ControlToValidate="txtFacebookID" Display="Dynamic" 
                        ErrorMessage="Enter valid Facebook ID &lt;br/&gt;"  
                        ValidationExpression="^[a-z0-9_]*$" Font-Italic="True" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        ForeColor="#FF5E5E" ValidationGroup="vgContact"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblLinkedInID" runat="server" Text="LinkedIn ID" CssClass="text-dark" ></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtLinkedInID"  CssClass="form-control" runat="server" placeholder="iamlinkedinuser"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="revLinkedInID" runat="server" 
                        ControlToValidate="txtLinkedInID" Display="Dynamic" 
                        ErrorMessage="Enter Valid Linkedin ID &lt;br/&gt;"  
                        ValidationExpression="^[a-z0-9_]*$" Font-Italic="True" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        ForeColor="#FF5E5E" ValidationGroup="vgContact"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="row p-2 my-4 justify-content-center">

            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info shadowCard" OnClick="btnAdd_Click" ValidationGroup="vgContact" />
        </div>
    </section>
</asp:Content>

