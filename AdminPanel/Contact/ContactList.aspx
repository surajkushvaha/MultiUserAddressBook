<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master"
    AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="AdminPanel_Contact_ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">

    <section class="mx-3 rounded p-2 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">

        <div class="d-flex justify-content-between mb-3">
            <h3 class="">Contact List</h3>
            <asp:HyperLink runat="server" ID="add" NavigateUrl="~/AdminPanel/Contact/Add"><span class="btn btn-success shadowCard">Add New</span></asp:HyperLink>

        </div>
        <asp:Panel id="lblMsgDiv" runat="server" visible="false" class="w-100 my-2 alert alert-danger ">
            <asp:Label ID="lblErrMsg" runat="server"
                EnableViewState="False" Visible="False"></asp:Label>
        </asp:Panel>
        <div class="text-left table-responsive" >

            <asp:GridView ID="gvContact" runat="server" OnRowCommand="gvContact_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm shadowCard"
                                CommandName="deleteRecord" OnClientClick="javascript : return confirm('Are you sure you want to delete?')" CommandArgument='<%# Eval("ContactID").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="btnEdit" NavigateUrl='<%# "~/AdminPanel/Contact/Edit/"+ MultiUserAddressBook.EncryptionDecryption.Base64Encode( Eval("ContactID").ToString().Trim()) %>'
                                Text="Edit" CssClass="btn btn-info btn-sm shadowCard"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContactName" HeaderText="Contact Name"  />
                    <asp:BoundField DataField="ContactCategoryID" HeaderText="Contact Category Name" />
                    <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                    <asp:TemplateField HeaderText="Contact Photo">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%#  Eval("ContactPhotoPath") %>' Height="75px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Image Attributes" ControlStyle-Width="200px">
                        <ItemTemplate >
                            <asp:Label runat="server" Text='<%# "Image Type :" +  Eval("PhotoFileType").ToString().Trim() %>' /><br />
                            <asp:Label runat="server" Text='<%# "Image Extension :" + Eval("PhotoFileExtension") %>' /><br />
                            <asp:Label runat="server" Text='<%# "Image Size :" + Eval("PhotoFileSize") %>' />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CityName" HeaderText="City Name" />
                    <asp:BoundField DataField="StateName" HeaderText="State Name" />
                    <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                    <asp:BoundField DataField="WhatsappNo" HeaderText="Whatsapp No" />
                    <asp:BoundField DataField="BirthDate" HeaderText="Birthdate" />
                    <asp:BoundField DataField="Email" HeaderText="Email ID" />
                    <asp:BoundField DataField="Age" HeaderText="Age" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" />
                    <asp:BoundField DataField="FacebookID" HeaderText="Facebook ID" />
                    <asp:BoundField DataField="LinkedINID" HeaderText="Linkedin ID" />
                    <asp:BoundField DataField="CreationDate" HeaderText="Created on" />
                    <asp:BoundField DataField="ModificationDate" HeaderText="Last  Modified on" />
                </Columns>
            </asp:GridView>
        </div>

    </section>
</asp:Content>

