<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emailClientPage.aspx.cs" Inherits="shaji_emailSystem.emailClientPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous" />
    <link href="assets/css/emailClient.css" rel="stylesheet" />
    <title>Email Client</title>
</head>
<body>
    <div class="container-fluid">
        <form id="frmEmailClient" runat="server">
            <!--START main email controls-->
            <div class="sidenav">
                <asp:Image ID="imguserAvatar" runat="server" />
                <br />
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                <asp:LinkButton ID="lnkbtnInbox" runat="server" OnClick="lnkbtnInbox_Click">Inbox</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnSent" runat="server" OnClick="lnkbtnSent_Click">Sent</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnFlag" runat="server" OnClick="lnkbtnFlag_Click">Flag</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnJunk" runat="server" OnClick="lnkbtnJunk_Click">Junk</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnTrash" runat="server" OnClick="lnkbtnTrash_Click">Trash</asp:LinkButton>

                <asp:DropDownList ID="ddlUserFolders" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnFolderUpdate" runat="server" Text="Filter" OnClick="btnFolderUpdate_Click" />
                <asp:LinkButton ID="btnManageSystem" runat="server" OnClick="btnManageSystem_Click">Manage System</asp:LinkButton>
            </div>
            <!--END main email controls-->

            <!--START system content-->
            <div class="main">
                <!--START main navbar-->
                <div>
                    <nav class="navbar navbar navbar-expand-lg" id="navbar0">
                        <a class="navbar-brand" href="emailClientPage.aspx">Ezemail</a>
                        <asp:LinkButton class="nav-link" ID="lnkbtnCreateFolder" runat="server" OnClick="lnkbtnCreateFolder_Click">Create New Folder</asp:LinkButton>
                        <asp:LinkButton class="nav-link" ID="lnkbtnViewProfile" runat="server" OnClick="lnkbtnViewProfile_Click">Profile</asp:LinkButton>
                        <asp:LinkButton class="nav-link" ID="lnkbtnLogout" runat="server" OnClick="lnkbtnLogout_Click">Logout</asp:LinkButton>

                    </nav>
                </div>
                <!--END main navbar-->
                <br />
                <br />

                <!--START email display grid-->
                <h2>
                    <asp:Label ID="lblFolderName" runat="server"></asp:Label>
                </h2>
                <div class="row" id="emailControls">
                    <div class="col-md-1">
                        <asp:Button ID="btnWriteEmail" class="btn btn-info" runat="server" Text="Compose" OnClick="btnWriteEmail_Click" />
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnDelete" class="btn btn-info" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearchFeild" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnSearchEmail" class="btn-info" runat="server" Text="Search" OnClick="btnSearchEmail_Click" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlMoveEmails" runat="server" Width="150px"></asp:DropDownList>
                        <asp:Button ID="btnMoveMail" class="btn-info" runat="server" Text="Move" OnClick="btnMoveMail_Click" />
                    </div>
                    <div class="col-md-1">
                    </div>
                </div>
               
                <div class="row" id="emailgrid">
                    
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                    <p>
                        &nbsp;</p>
                     <p>
                    <asp:Label ID="lblFolderEmpty" runat="server"></asp:Label>
                </p>
                    <div class="col-md-12">
                        <asp:GridView ID="gv_email" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" CssClass="table table-hover" AutoGenerateSelectButton="true" ShowHeader="false" OnSelectedIndexChanged="gv_email_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="SenderName" HeaderText="Sender" />
                                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                <asp:BoundField DataField="EmailBody" HeaderText="Content" />
                                <asp:BoundField DataField="CreatedTime" HeaderText="Time Stamp" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!--END email display grid-->

                <!--START email display section-->

                <div class="row" id="emailDisplayHeading">
                    <div class="col-md-4">
                        <h2>
                            <asp:Label ID="lblEmailDisplaySubject" runat="server" Text="Label"></asp:Label></h2>
                        <br />
                    </div>
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnFlagEmail" class="btn" runat="server" Text="Flag" OnClick="btnFlagEmail_Click" Width="146px" />
                    </div>
                     <p>
                    <strong>
                        <asp:Label ID="lblReceivedDate" runat="server" Text="Received on:  "></asp:Label></strong>
                    <asp:Label ID="lblSentDate" runat="server" Text="Label"></asp:Label>
                </p>

                </div>
                <div class="row" id="emailDisplayContent">
                    <div class="col-md-4">
                        <p>
                            <strong>
                                <asp:Label ID="lblsenderLabel" runat="server" Text="Sender:"></asp:Label></strong>
                        </p>

                        <asp:Image ID="imgEmailDisplaySenderAvatar" runat="server" Width="45px" Height="45px" />
                        <asp:Label ID="lblEmailDisplaySenderName" runat="server" Text="SenderName"></asp:Label>
                        <br />
                        <br />
                        <p>
                            <strong>
                                <asp:Label ID="lblReceiverLabel" runat="server" Text="Receiver:"></asp:Label></strong>
                        </p>

                        <asp:Image ID="imgEmailDisplayReceiverAvatar" runat="server" Width="45px" Height="45px" />
                        <asp:Label ID="lblEmailDisplayReceiverName" runat="server" Text="ReceiverName"></asp:Label>
                    </div>

                    <div class="col-md-8">
                        <p id="bodylabelptag">
                            <strong>
                                <asp:Label ID="lblEmailBodyLabel" runat="server" Text="Message: "></asp:Label></strong>
                        </p>

                        <asp:Label ID="lblEmailBody" runat="server" Text="Label"></asp:Label>
                    </div>

                </div>

                <!--END email display section-->
                <!--Start email composing section-->
                <div class="row" id="composeEmail">
                    <div class="col-md-2">
                        <asp:Button ID="btnSendEmail" class="btn btn-info" runat="server" Text="Send Email" OnClick="btnSendEmail_Click" />
                        <br />
                        <br />
                        <asp:Button ID="btnCancelEmail" class="btn btn-info" runat="server" Text="Cancel Email" OnClick="btnCancelEmail_Click" />
                    </div>
                    <div class="col-md-8">
                        <p>
                            <strong>
                                <asp:Label ID="lblSender" runat="server" Text="Sender:  "></asp:Label>&nbsp;&nbsp; </strong>
                            <asp:TextBox ID="txtSenderInfo" runat="server"></asp:TextBox>
                        </p>

                        <p>
                            <strong>
                                <asp:Label ID="lblReceiver" runat="server" Text="Receiver:  "></asp:Label>&nbsp;</strong>
                            <asp:TextBox ID="txtReceiverInfo" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <strong>
                                <asp:Label ID="lblSubject" runat="server" Text="Subject:  "></asp:Label>&nbsp;&nbsp; </strong>
                            <asp:TextBox ID="txtSubject" runat="server" Width="460px"></asp:TextBox>
                        </p>
                        <br />
                        <div class="form-group">
                            <p>
                                <strong>
                                    <asp:Label ID="lblmessagecontent" runat="server" Text="Message Content:  "></asp:Label></strong>
                                <asp:TextBox class="form-control" ID="txtmessagecontent" runat="server" Height="150px" TextMode="MultiLine"></asp:TextBox>
                            </p>
                        </div>
                    </div>
                </div>

                <!--END email composing view-->
                <!--START profile display-->



                <div class="row" id="profileDisplay">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:Image class="img-fluid" ID="imgprofileavatar" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <p>
                            <strong>
                                <asp:Label ID="lblnametag" runat="server" Text="Name On File: "></asp:Label></strong>

                            <asp:Label ID="lblname" runat="server"></asp:Label>
                        </p>
                        <p>
                            <strong>
                                <asp:Label ID="lbladdresstag" runat="server" Text="Address On File: "></asp:Label></strong>

                            <asp:Label ID="lbladdress" runat="server"></asp:Label>
                        </p>
                        <p>
                            <strong>
                                <asp:Label ID="lblphonetag" runat="server" Text="Phone Number On File: "></asp:Label></strong>

                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </p>
                        <p>
                            <strong>
                                <asp:Label ID="lblsysEmailtag" runat="server" Text="System Email: "></asp:Label></strong>

                            <asp:Label ID="lblsysEmail" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
                <!--END profile display-->

                <!--START add new tag section-->
                <div class="row" id="addNewFolder">
                    <div class="col-md-3"></div>
                    <div class="col-md-8">
                        <p>
                            <strong>
                                <asp:Label ID="lblAddNewFolder" runat="server" Text="Folder Name: "></asp:Label>
                            </strong>
                        </p>
                        <asp:TextBox ID="txtNewLabelName" runat="server"></asp:TextBox>

                        <asp:Button ID="btnAddNewLabel" class="btn-info" runat="server" Text="Add New Label" OnClick="btnAddNewLabel_Click" />
                    </div>
                </div>
                <!--END add new tag section-->


            </div>
            <!--END system content-->
        </form>
    </div>
</body>
</html>
