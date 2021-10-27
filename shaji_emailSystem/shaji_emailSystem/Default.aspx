<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="shaji_emailSystem.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous" />
    <link href="assets/css/loginpage.css" rel="stylesheet" />
    <title>Login Page</title>
</head>
<body>
    <div class="container">
        <form id="frmLogin" runat="server">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="card">
                        <div class="card-header" style="text-align: center;">
                            EZ Email System
                        </div>
                        <div class="card-body" style="text-align: center;">
                            <img id="logo" src="assets/images/email.png" />
                            <h5 class="card-title">Enter your Credentials</h5>
                            <br />
                            <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblPassword" runat="server" Text="Password:  "></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="btnLogin" class="btn btn-primary" runat="server" Text="Log In" OnClick="btnLogin_Click" />
                            <br />
                            <br />
                            <a href="signup.aspx">Create a new account</a>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
