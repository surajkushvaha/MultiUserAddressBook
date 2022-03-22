<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="StateAddEditPage.aspx.cs" Inherits="AdminPanel_State_StateAddEditPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <section class="mx-3 rounded p-2 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <div class="mb-3">

            <h2 class="text-dark">
                <asp:HyperLink ID="hlBackBtn" runat="server" NavigateUrl="~/AdminPanel/State/List" CssClass="px-2 text-decoration-none text-danger">
<span class="material-icons">
arrow_back
</span> 
                </asp:HyperLink>
                <asp:Label runat="server" ID="lblMode"></asp:Label>
            </h2>
        </div>


        <asp:Panel id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblStateMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </asp:Panel>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                                <sup class="text-danger font-weight-bold">*</sup>

                <asp:Label ID="lblCountryName" runat="server" Text="Country Name" CssClass="text-dark"></asp:Label>
                
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlCountryID" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDdlCountryID" runat="server" 
                    ErrorMessage="Kindly Select a Country&lt;br/&gt;" 
                ControlToValidate="ddlCountryID" Display="Dynamic" Font-Italic="True" 
                ForeColor="#FF5E5E" InitialValue="-1" ValidationGroup="vgState"></asp:RequiredFieldValidator>
            </div>
            
        </div>

        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                                <sup class="text-danger font-weight-bold">*</sup>

                <asp:Label ID="lblStateName" runat="server" Text="State Name" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtStateName" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvStateName" runat="server" 
                    ErrorMessage="Enter State Name" ControlToValidate="txtStateName" 
                    Display="Dynamic" Font-Italic="True" ForeColor="#FF5E5E" 
                    ValidationGroup="vgState"></asp:RequiredFieldValidator>
            </div>
        </div>


        <div class="row p-2 justify-content-center">
            <div class="col-md-4 align-self-center">
                                <sup class="text-danger font-weight-bold">*</sup>

                <asp:Label ID="lblStateCode" runat="server" Text="State Code" CssClass="text-dark"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtStateCode" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revStateCode" runat="server" 
                    ErrorMessage="Enter Capital Alphabetic Value Only <br/>" 
                    ControlToValidate="txtStateCode" Display="Dynamic" 
                    ValidationExpression="^[A-Z]*$" Font-Italic="True" ForeColor="#FF5E5E" 
                    ValidationGroup="vgState"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvStateCode" runat="server" 
                    ErrorMessage="Enter State Code" ControlToValidate="txtStateCode" 
                    Display="Dynamic" Font-Italic="True" ForeColor="#FF5E5E" 
                    ValidationGroup="vgState"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row p-2 my-4 justify-content-center">

            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-info shadowCard" OnClick="btnAdd_Click" ValidationGroup="vgState" />
        </div>
    </section>
</asp:Content>

