<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="shaji_emailSystem.signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous" />
    <link href="assets/css/signuppagecss.css" rel="stylesheet" />
    <title>Create Account</title>
</head>
<body>
    <div class="container">
        <form id="frmSignUp" runat="server">
            <div class="card text-center">
                <div class="card-header">
                    EZ Email System
                </div>
                <div class="card-body">
                    <h5 class="card-title">Create your EZ Email</h5>
                    <p class="card-text">Please fill in the following fields</p>
                    
                    <div class="row" id="signupform">
                        <div class="col-md-6" id="intialsysteminfo">
                            <asp:Label ID="lbluserfName" runat="server" Text="First Name:"></asp:Label>
                            <asp:TextBox ID="txtfirstName" runat="server" Width="250px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lbluserlName" runat="server" Text="Last Name:"></asp:Label>
                            <asp:TextBox ID="txtlastName" runat="server" Width="250px"></asp:TextBox>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblEmail" runat="server" Text="Email:  "></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
                            <br />
                            <br />
                            &nbsp;<asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" Width="250px"></asp:TextBox>
                             <br />
                            <br />
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" Width="188px"></asp:TextBox>
                            <br />
                            <br />
                            <div style="height: 226px; width: 519px">
                                <asp:Label ID="lblusertype" runat="server" Text="User Type"></asp:Label>
                                <br />
                                <asp:RadioButtonList ID="rblUserType" runat="server">
                                    <asp:ListItem Value="User"> I am User</asp:ListItem>
                                    <asp:ListItem Value="Admin">  I am Admin</asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <br />
                        </div>

                        <div class="col-md-6" id="personInformation">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbladdress" runat="server" Text="Address: "></asp:Label>
                            <asp:TextBox ID="txtaddress" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblcontactphone" runat="server" Text="Phone Number: "></asp:Label>
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblaltemail" runat="server" Text="Alternate Email: "></asp:Label>
                            <asp:TextBox ID="txtaltemail" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="lblavatar" runat="server" Text="Choose your Avatar: "></asp:Label>
                            <asp:Image ID="avatarDisplay" runat="server" ImageUrl="assets/images/fox.png" Width="48" Height="48"></asp:Image>
                            <asp:DropDownList ID="ddlavatars" runat="server" AutoPostBack="true" Width="210px" OnSelectedIndexChanged="ddlavatars_SelectedIndexChanged">
                                <asp:ListItem Selected="True">Fox</asp:ListItem>
                                <asp:ListItem>Elephant</asp:ListItem>
                                <asp:ListItem>Giraffe</asp:ListItem>
                                <asp:ListItem>Goat</asp:ListItem>
                                <asp:ListItem>Hamster</asp:ListItem>
                                <asp:ListItem>Lion</asp:ListItem>
                                <asp:ListItem>Monkey</asp:ListItem>
                                <asp:ListItem>Panda</asp:ListItem>
                                <asp:ListItem>Tiger</asp:ListItem>
                                <asp:ListItem>Zebra</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <br />
                            <asp:Button class="btn btn-primary" ID="btnsignup" OnClick="btnsignup_Click" runat="server" Text="Create Account" />
                        </div>
                       
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.6.0/dist/umd/popper.min.js" integrity="sha384-KsvD1yqQ1/1+IA7gi3P0tyJcT3vR+NdBTt13hSJ2lnve8agRGXTTyNaBYmCR/Nwi" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.min.js" integrity="sha384-nsg8ua9HAw1y0W1btsyWgBklPnCUAFLuTMS2G72MMONqmOymq585AcH49TLBQObG" crossorigin="anonymous"></script>
</body>
</html>
