<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Crud_Practice1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div class="container mt-5">
                <div class="row justify-content-center">
                    <div class="col-md-4">
                        <h3 class="text-center">Login</h3>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                        <div class="form-group">
                            <label for="txtName">Name</label>
                            <asp:TextBox ID="txtName" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
                        </div>
                        <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" runat="server" />
                        <asp:Button ID="btnSignup" CssClass="btn btn-primary btn-block" Text="Signup" OnClick="btnSignup_Click" runat="server" />

                    </div>
                </div>
            </div>
    </form>
</body>
</html>
