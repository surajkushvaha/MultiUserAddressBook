<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEditPage.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryAddEditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
     <section class="mx-3 rounded p-2 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div class="mb-3">

            <h2 class="text-dark">
                <asp:HyperLink ID="hlBackBtn" runat="server" NavigateUrl="~/AdminPanel/ContactCategory/List"
                    CssClass="px-2 text-decoration-none text-danger">
<span class="material-icons">
arrow_back
</span>
                </asp:HyperLink>
                <asp:Label runat="server" ID="lblMode"></asp:Label>

            </h2>
        </div>
        <asp:Panel id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblContactCategoryMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </asp:Panel>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                                <sup class="text-danger font-weight-bold">*</sup>

                <asp:Label ID="lblContactCategoryName" runat="server" Text="Contact Category Name"
                    CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtContactCategoryName" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContactCategory" runat="server" 
                    ErrorMessage="Enter Contact Category" Display="Dynamic" Font-Italic="True" 
                    ControlToValidate="txtContactCategoryName" ForeColor="#FF5E5E" ValidationGroup="vgContactCategory"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row p-2 my-4 justify-content-center">

            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-info  shadowCard"
                OnClick="btnAdd_Click" ValidationGroup="vgContactCategory" />
        </div>
    </section>
</asp:Content>

