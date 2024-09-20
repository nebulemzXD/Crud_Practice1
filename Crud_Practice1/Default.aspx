<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Crud_Practice1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">CRUD</h1>
      
             <div class="card-body">
              <div class="row">
                <div class="col-lg-12">
                  <div class ="form-group">
                      <p class="lead">INSERT DATA HERE</p>
                      <label>Name</label>
                      <asp:Textbox ID ="txtName1" CssClass="form-control" runat="server"></asp:Textbox><br />
                      <label>Email</label>
                      <asp:Textbox ID ="txtEmail" CssClass="form-control" runat="server"></asp:Textbox><br />
                      <label>Gender</label>
                      <asp:Textbox ID ="txtGender" CssClass="form-control" runat="server"></asp:Textbox><br />
                      <label>Birthday</label>
                      <asp:Textbox ID ="txtBirthdate" CssClass="form-control" runat="server"></asp:Textbox><br />

<%--                       <asp:Button ID="btnSubmit" runat="server" text="Submit" Css-class="btn-btn-success" Font-Bold="true" OnClick="btnSubmit_Click" />--%>
<%--                       <asp:Button ID="btnReset" runat="server" text="Reset" Css-class="btn-btn-danger" Font-Bold="true" OnClick="btnReset_Click" />--%>
                      <!--try push -->
                      <!--try push -->
                      <!--try push -->
                      <!--try push -->
                      
                  </div>
                 </div>
                </div>
                </div>
            <p><a href="http://www.asp.net" class="btn btn-primary btn-md">Learn more &raquo;</a></p>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Getting started</h2>
                <p>
                    ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
                A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="librariesTitle">
                <h2 id="librariesTitle">Get more libraries</h2>
                <p>
                    NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="hostingTitle">
                <h2 id="hostingTitle">Web Hosting</h2>
                <p>
                    You can easily find a web hosting company that offers the right mix of features and price for your applications.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
