<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master"
    AutoEventWireup="true" CodeFile="CountryAddEditPage.aspx.cs" Inherits="AdminPanel_Country_CountryAddEditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <section class="mx-3 rounded p-2 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div class="mb-3">

            <h2 class="text-dark ">
                <asp:HyperLink ID="hlBackBtn" runat="server"  NavigateUrl="~/AdminPanel/Country/List"
                    CssClass="px-2 text-decoration-none text-danger">
<span class="material-icons">
arrow_back
</span>    </asp:HyperLink>
                <asp:Label runat="server" ID="lblMode"></asp:Label>

            </h2>
        </div>
        <asp:Panel id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblCountryMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </asp:Panel>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                                <sup class="text-danger font-weight-bold">*</sup>

                <asp:Label ID="lblCountryName" runat="server" Text="Country Name" CssClass="text-daek"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtCountryName" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCountryName" runat="server" 
                    ErrorMessage="Enter Country Name" ControlToValidate="txtCountryName" 
                    Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgCountry"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                                <sup class="text-danger font-weight-bold">*</sup>

                <asp:Label ID="lblCountryCode" runat="server" Text="Country Code" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtCountryCode" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revCountryCode" runat="server" 
                    ErrorMessage="Enter Capital Alphabetic Value only &lt;br/&gt;" 
                    ControlToValidate="txtCountryCode" Display="Dynamic" 
                    ValidationExpression="^[A-Z]*$" Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgCountry"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvCountryCode" runat="server" 
                    ErrorMessage=" Enter Country Code &lt;br/&gt;" Display="Dynamic" 
                    ControlToValidate="txtCountryCode" Font-Italic="True" ForeColor="#FF5E5E" ValidationGroup="vgCountry"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row p-2 my-4 justify-content-center">

            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-success shadowCard"
                OnClick="btnAdd_Click" ValidationGroup="vgCountry" />
        </div>
    </section>
</asp:Content>

