<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="CityList.aspx.cs" Inherits="AdminPanel_City_CityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <section class="mx-3 rounded p-2 card shadowCard" style="background-color:rgba(0, 0, 254, 0.30)">

        <div class="d-flex justify-content-between mb-3">
            <h3 class="">City List</h3>
            <asp:HyperLink runat="server" ID="add" NavigateUrl="~/AdminPanel/City/Add"><span class="btn btn-success shadowCard">Add New</span></asp:HyperLink>

        </div>
         <div id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </div>
        <div class="text-left table-responsive">
            <asp:GridView ID="gvCity" runat="server" OnRowCommand="gvCity_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm shadowCard"
                                CommandName="deleteRecord" CommandArgument='<%#Eval("CityID").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="btnEdit" NavigateUrl='<%# "~/AdminPanel/City/Edit/"+ MultiUserAddressBook.EncryptionDecryption.Base64Encode(Eval("CityID").ToString().Trim()) %>'
                                Text="Edit" CssClass="btn btn-info btn-sm shadowCard"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StateName" HeaderText="City Name" />
                    <asp:BoundField DataField="CityName" HeaderText="City Name" />
                    <asp:BoundField DataField="PinCode" HeaderText="Pin Code" />
                    <asp:BoundField DataField="STDCode" HeaderText="STD Code" />
                     <asp:BoundField DataField="CreationDate" HeaderText="Created on" />
                     <asp:BoundField DataField="ModificationDate" HeaderText="Last  Modified on" />
                </Columns>
            </asp:GridView>
        </div>

    </section>
</asp:Content>

