<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Crud_Practice1.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <h3 class="text-center">Signup</h3>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                    <div class="form-group">
                        <label for="txtName">Name</label>
                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtPassword">Password</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="ddlRegions">Region</label>
                        <asp:DropDownList ID="ddlRegions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRegions_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="ddlProvinces">Province</label>
                        <asp:DropDownList ID="ddlProvinces" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlProvinces_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="ddlCities">City</label>
                        <asp:DropDownList ID="ddlCities" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCities_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
