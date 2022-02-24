<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="AdminPanel_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    
    <section class="mx-3 rounded p-2 card shadowCard" style="background-color: rgba(0, 0, 254, 0.30)">
        <h2 class="mx-2">Hi, <asp:Label runat="server" ID="lblWelcomeUser" CssClass="text-danger"></asp:Label> Welcome<br /></h2>
        <div class="d-flex justify-content-between mb-3">
            <h3 class="m-2">All Tables View Pages</h3>
        </div>
       <div class="d-flex flex-row ">

           <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/Profile" class="card alert-danger  p-5 m-2 text-danger shadowCard text-decoration-none">
               <strong> Profile</strong>
           </asp:HyperLink>
           <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/Contact/List" class="card alert-danger p-5 m-2 text-danger shadowCard text-decoration-none">
               <strong> Contact</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/ContactCategory/List" class="card alert-danger p-5 m-2 text-danger shadowCard text-decoration-none">
               <strong> Contact Category</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/City/List" class="card alert-danger p-5 m-2  text-danger shadowCard text-decoration-none">
               <strong> City</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/State/List" class="card alert-danger p-5 m-2 text-danger shadowCard text-decoration-none">
               <strong> State</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/Country/List" class="card alert-danger p-5 m-2 text-danger shadowCard text-decoration-none">
               <strong> Country</strong>
           </asp:HyperLink>
            
       </div>

         <div class="d-flex justify-content-between my-3">
            <h3 class="mx-2">All Add Edit Pages</h3>
        </div>
        
        <div class="d-flex flex-row mb-3">

           <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/Profile/Edit" class="card alert-success  p-5 m-2 text-success shadowCard text-decoration-none">
               <strong> Profile</strong>
           </asp:HyperLink>
           <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/Contact/Add" class="card alert-success  p-5 m-2 text-success shadowCard text-decoration-none">
               <strong> Contact</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/ContactCategory/Add" class="card alert-success  p-5 m-2 text-success shadowCard text-decoration-none">
               <strong> Contact Category</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/City/Add" class="card p-5 m-2 alert-success   text-success shadowCard text-decoration-none">
               <strong> City</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/State/Add" class="card alert-success  p-5 m-2 text-success shadowCard text-decoration-none">
               <strong> State</strong>
           </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="~/AdminPanel/Country/Add" class="card alert-success  p-5 m-2 text-success shadowCard text-decoration-none">
               <strong> Country</strong>
           </asp:HyperLink>
            
       </div>
    </section>
</asp:Content>

