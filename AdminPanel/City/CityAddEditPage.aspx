<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="CityAddEditPage.aspx.cs" Inherits="AdminPanel_City_CityAddEditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <section class="mx-3 rounded p-2  card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div class="mb-3">

            <h2 class="text-info">
                <asp:HyperLink ID="hlBackBtn" runat="server" NavigateUrl="~/AdminPanel/City/List"
                    CssClass="px-2  text-decoration-none text-danger">
<span class="material-icons">
arrow_back
</span>
                </asp:HyperLink>
                <asp:Label runat="server" ID="lblMode"></asp:Label>

            </h2>
        </div>
        <asp:Panel id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-info ">
            <asp:Label ID="lblCityMsg" runat="server" 
             EnableViewState="False" Visible="False"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </asp:Panel>
        
         <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblStateName" runat="server" Text="State Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlStateID" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDdlStateID" runat="server" 
                    ControlToValidate="ddlStateID" ErrorMessage="Please Select a State" 
                    Font-Italic="True" ForeColor="#FF5E5E" InitialValue="-1" 
                    ValidationGroup="vgCity"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblCityName" runat="server" Text="City Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtCityName" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCityName" runat="server" 
                    ControlToValidate="txtCityName" Display="Dynamic" 
                    ErrorMessage="Enter City Name" Font-Italic="True" ForeColor="#FF5E5E" 
                    ValidationGroup="vgCity"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblSTDCode" runat="server" Text="STD Code" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtSTDCode" CssClass="form-control" runat="server"></asp:TextBox>
                
            </div>
        </div>
         <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                <asp:Label ID="lblPinCode" runat="server" Text="PIN Code" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtPinCode" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="revPINCode" runat="server" 
                 ErrorMessage="Enter Valid Pincode" Display="Dynamic" Font-Italic="True" 
                    ForeColor="#FF5E5E" ControlToValidate="txtPinCode" 
                    ValidationExpression="^(\d{4}|\d{6})$" ValidationGroup="vgCity"></asp:RegularExpressionValidator>
            </div>
           
        </div>
        <div class="row p-2 my-4 justify-content-center">

            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info shadowCard"
                OnClick="btnAdd_Click" ValidationGroup="vgCity" />
        </div>
    </section>
</asp:Content>

