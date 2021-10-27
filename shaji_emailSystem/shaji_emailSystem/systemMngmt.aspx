<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="systemMngmt.aspx.cs" Inherits="shaji_emailSystem.systemMngmt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous" />
    <link href="assets/css/emailClient.css" rel="stylesheet" />
    <title>Admin Panel</title>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
        <div>
                    <nav class="navbar navbar navbar-expand-lg" id="navbar0">
                        <a class="navbar-brand">Ezemail</a>
                        <asp:LinkButton class="nav-link" ID="lnkbtnBan" runat="server" OnClick="lnkbtnBan_Click">Ban Users</asp:LinkButton>
                         <asp:LinkButton class="nav-link" ID="lnkbtnunban" runat="server" OnClick="lnkbtnunban_Click" >Unban Users</asp:LinkButton>

                        <asp:LinkButton class="nav-link" ID="lnkbtnViewFlaggedEmails" runat="server" OnClick="lnkbtnViewFlaggedEmails_Click">Flagged Emails</asp:LinkButton>
                        <asp:LinkButton class="nav-link" ID="lnkbtnLogout" runat="server" OnClick="lnkbtnLogout_Click">Logout</asp:LinkButton>

                    </nav>
                </div>
        <br />
        <br />
        <div class="row">
            <h2>
                System Users
            </h2>
            <asp:GridView ID="gv_users" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" CssClass="table table-hover">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                <asp:BoundField DataField="UserName" HeaderText="Name" />
                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" />
                                <asp:BoundField DataField="SystemEmail" HeaderText="System Email" />
                                <asp:BoundField DataField="AlternateEmail" HeaderText="Alt. Email" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="UserType" HeaderText="User Type" />
                            </Columns>
            </asp:GridView>
            </div>
        <div class="row">
            <h2>
                 <asp:Label ID="lblflagHeading" runat="server" Text="Flagged Emails"></asp:Label>
                
            </h2>
            <asp:GridView ID="gv_flaggedEmails" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" CssClass="table table-hover" AutoGenerateSelectButton="true" ShowHeader="false" OnSelectedIndexChanged="gv_flaggedEmails_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="SenderName" HeaderText="Sender" />
                                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                <asp:BoundField DataField="EmailBody" HeaderText="Content" />
                                <asp:BoundField DataField="CreatedTime" HeaderText="Time Stamp" />
                </Columns>
            </asp:GridView>
            <br />
            </div>
            <div class="row">
            <div class="col-md-6">
                <h2>
                <asp:Label ID="lblSubject" runat="server" ></asp:Label>
            </h2>
            </div>
                <div class="col-md-3"></div>
                <div class="col-md-3">
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
            <p>
            <strong><asp:Label ID="lbltime" runat="server" Text="Sent at: "></asp:Label></strong>
            <asp:Label ID="lblcreatedTime" runat="server" ></asp:Label>
            </p>
            <p>
            <asp:Image ID="imgSenderAvatar" runat="server" width="45px" Height="45px" />
            
            <strong><asp:Label ID="lblSender" runat="server" Text="Sender: "></asp:Label></strong>
            &nbsp;
            <asp:Label ID="lblSenderName" runat="server" ></asp:Label>
            </p>
                    </div>
                <div class="col-md-8">
            <p>
            <strong><asp:Label ID="lblContent" runat="server" Text="Content: "></asp:Label></strong>
           &nbsp;
                <asp:Label ID="lblEmailBody" runat="server" ></asp:Label>
                </p>
                    </div>
           
                </div>
          </form>
        </div>
  
</body>
</html>
