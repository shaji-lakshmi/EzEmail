using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using emailLibrary;


namespace shaji_emailSystem
{
    public partial class emailClientPage : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            //All session fields used in the login page
            //Session["UserId"] = dBConnect.GetField("UserID", 0);
            //Session["UserName"] = dBConnect.GetField("UserName", 0);
            //Session["Address"] = dBConnect.GetField("Address", 0);
            //Session["PhoneNumber"] = dBConnect.GetField("PhoneNumber", 0);
            //Session["SystemEmail"] = dBConnect.GetField("SystemEmail", 0);
            //Session["Avatar"] = dBConnect.GetField("Avatar", 0);


            imguserAvatar.ImageUrl = Session["Avatar"].ToString();
            lblUserName.Text = Session["UserName"].ToString();


            if (!IsPostBack)
            {
                //Check if the user is an Admin and then if only they are, display the manage system option in the side menu
                if(Session["UserType"].ToString() == "Admin")
                {
                    btnManageSystem.Visible = true;
                }
                else
                {
                    btnManageSystem.Visible = false;
                }
                Session["Tag"] = "Inbox";
                fillEmailList("Inbox");
                getTagsFordropdowns();

                displayAddNewLabel(false);
                displayEmailDetails(false);
                displayProfile(false);
                displayComposeView(false);
                btnFlagEmail.Visible=false;
            }
        }

        public void fillEmailList(String SelectedTag)
        {
            String tagName = SelectedTag;
            int userid = int.Parse(Session["UserId"].ToString());
            DataSet tagId = getTagidFromDb(userid, tagName);
            int emailTagId = int.Parse(tagId.Tables[0].Rows[0]["TagId"].ToString());
            DataSet emails = getEmailsFromDb(userid, emailTagId);
            int size = emails.Tables[0].Rows.Count;
            lblFolderName.Text = Session["Tag"].ToString();
            if (size > 0)
            {
                ArrayList emailList = new ArrayList();
                for (int i = 0; i < size; i++)
                {
                    int senderId = int.Parse(emails.Tables[0].Rows[i]["SenderId"].ToString());
                    DataSet sender = getsenderInfo(senderId);

                    Email email = new Email();
                    email.SenderName = sender.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = emails.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = emails.Tables[0].Rows[i]["EmailBody"].ToString();
                    email.CreatedTime = emails.Tables[0].Rows[i]["CreatedTime"].ToString();
                    

                    emailList.Add(email);
                }
                gv_email.DataSource = emailList;
                gv_email.DataBind();
                displayEmailGrid(true);
                lblFolderEmpty.Visible = false;
            }
            else
            {
                gv_email.Visible = false;
                btnWriteEmail.Visible = true;
                lblFolderEmpty.Visible = true;
                lblFolderEmpty.Text = "Your " + tagName + " is empty.";
            }

            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(false);
            displayComposeView(false);



        }

        public void sentMail(String SelectedTag)
        {
            String tagName = SelectedTag;
            int userid = int.Parse(Session["UserId"].ToString());
            DataSet tagId = getTagidFromDb(userid, tagName);
            int emailTag = int.Parse(tagId.Tables[0].Rows[0]["TagId"].ToString());
            lblFolderName.Text = "Sent";

            DataSet emails = getSentEmails(userid, emailTag);
            int size = emails.Tables[0].Rows.Count;

            if (size > 0)
            {
                ArrayList emailList = new ArrayList();
                for (int i = 0; i < size; i++)
                {
                    int recieverId = int.Parse(emails.Tables[0].Rows[i]["RecieverId"].ToString());
                    DataSet sender = getsenderInfo(recieverId);

                    Email email = new Email();
                    email.SenderName = sender.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = emails.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = emails.Tables[0].Rows[i]["EmailBody"].ToString();
                    email.CreatedTime = emails.Tables[0].Rows[i]["CreatedTime"].ToString();
                    

                    emailList.Add(email);
                }
                gv_email.DataSource = emailList;
                gv_email.DataBind();
                gv_email.Visible = true;
                lblFolderEmpty.Visible = false;
            }
            else
            {
                gv_email.Visible = false;
                lblFolderEmpty.Visible = true;
                lblFolderEmpty.Text = "Your " + tagName + " is empty.";
            }

            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(false);
            displayComposeView(false);
        }

        protected void lnkbtnSent_Click(object sender, EventArgs e)
        {
            Session["Tag"] = "Sent";
            sentMail("Sent");

            displayAddNewLabel(false);
            displayEmailDetails(false);
        }

        protected void lnkbtnInbox_Click(object sender, EventArgs e)
        {
            Session["Tag"] = "Inbox";
            fillEmailList("Inbox");

            displayAddNewLabel(false);
            displayEmailDetails(false);
        }

        protected void lnkbtnJunk_Click(object sender, EventArgs e)
        {
            Session["Tag"] = "Junk";
            fillEmailList("Junk");

            displayAddNewLabel(false);
            displayEmailDetails(false);
        }

        protected void lnkbtnFlag_Click(object sender, EventArgs e)
        {
            Session["Tag"] = "Flag";
            fillEmailList("Flag");

            displayAddNewLabel(false);
            displayEmailDetails(false);
        }

        protected void lnkbtnTrash_Click(object sender, EventArgs e)
        {
            Session["Tag"] = "Trash";
            fillEmailList("Trash");

        }

        protected void lnkbtnCreateFolder_Click(object sender, EventArgs e)
        {
            displayEmailDetails(false);
            displayAddNewLabel(true);
            displayProfile(false);
            displayEmailGrid(false);
            displayComposeView(false);

        }

        public void getTagsFordropdowns()
        {
            int userid = int.Parse(Session["UserId"].ToString());
            DataSet usertags = getTagName(userid);

            ArrayList sideBarTags = new ArrayList();
            ArrayList moveMenuTags = new ArrayList();

            int size = usertags.Tables[0].Rows.Count;
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    Tag sideMenu = new Tag();
                    Tag moveMenu = new Tag();
                    String tagName = usertags.Tables[0].Rows[i]["TagName"].ToString();
                    //Sent folder should not be moved to or out of
                    //There is a delete button for moving to trash 
                    //There is a flag button for moving to flag
                    if (tagName != "Sent" && tagName != "Trash" && tagName != "Flag") { 
                    moveMenu.TagName = tagName;
                    moveMenuTags.Add(moveMenu);
                    }

                    if (tagName != "Inbox" && tagName != "Sent" && tagName != "Flag" && tagName != "Junk" && tagName != "Trash")
                    {
                        sideMenu.TagName = tagName;
                        sideBarTags.Add(sideMenu);
                    }
                }

                ddlUserFolders.DataSource = sideBarTags;
                ddlUserFolders.DataTextField = "TagName";
                ddlUserFolders.DataValueField = "TagName";
                ddlUserFolders.DataBind();

                ddlMoveEmails.DataSource = moveMenuTags;
                ddlMoveEmails.DataTextField = "TagName";
                ddlMoveEmails.DataValueField = "TagName";
                ddlMoveEmails.DataBind();
            }
        }

        protected void btnFolderUpdate_Click(object sender, EventArgs e)
        {
            String tagName = ddlUserFolders.SelectedValue;
            fillEmailList(tagName);

            Session["Tag"] = tagName;

            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(false);
            displayComposeView(false);
            lblFolderName.Text = tagName;

        }

        protected void btnAddNewLabel_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            String tagName = txtNewLabelName.Text;

            int updateSuccess = addNewTagtoDb(userId, tagName);

            if (updateSuccess != -1)
            {
                Response.Write("<script>alert('Added " + tagName + " to your account')</script>");

            }
            else
            {
                Response.Write("<script>alert('An error occurred. " + tagName + " has not been added to your account')</script>");

            }
            getTagsFordropdowns();
            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(false);
            displayComposeView(false);
            displayEmailGrid(true);

        }

        protected void gv_email_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayEmailDetails(true);
            displayAddNewLabel(false);
            displayProfile(false);
            displayEmailGrid(false);
            displayComposeView(false);
            int row = gv_email.SelectedIndex;
            String subject = gv_email.Rows[row].Cells[3].Text;
            String sname = gv_email.Rows[row].Cells[2].Text;
            String rname = Session["UserName"].ToString();
            String emailBody = gv_email.Rows[row].Cells[4].Text;
            String createdDate = gv_email.Rows[row].Cells[5].Text;
            DataSet senderAvatar = getSenderAvatarFromDb(sname);
            String responseSenderAvatar = senderAvatar.Tables[0].Rows[0]["Avatar"].ToString();

            if (Session["Tag"].ToString() != "Sent")
            {
                btnFlagEmail.Visible = true;
                lblEmailDisplaySubject.Text = subject;
                lblEmailDisplaySenderName.Text = sname;
                lblEmailDisplayReceiverName.Text = rname;
                imgEmailDisplayReceiverAvatar.ImageUrl = Session["Avatar"].ToString();
                imgEmailDisplaySenderAvatar.ImageUrl = responseSenderAvatar;
                lblSentDate.Text = createdDate;
                lblEmailBody.Text = emailBody;
            }
            else
            {
                btnFlagEmail.Visible = false;
                lblEmailDisplaySubject.Text = subject;
                lblEmailDisplaySenderName.Text = rname;
                lblEmailDisplayReceiverName.Text = sname;
                imgEmailDisplayReceiverAvatar.ImageUrl = responseSenderAvatar;
                imgEmailDisplaySenderAvatar.ImageUrl = Session["Avatar"].ToString();
                lblSentDate.Text = createdDate;
                lblEmailBody.Text = emailBody;
            }
            //Response.Write("<script>alert('" + gv_email.SelectedIndex + "')</script>");
            //Response.Write("<script>alert('" + subject + "')</script>");


        }

        protected void btnFlagEmail_Click(object sender, EventArgs e)
        {
            String senderName = lblEmailDisplaySenderName.Text;
            DataSet getsenderid = getSenderidFromDb(senderName);
            int senderid = Convert.ToInt32(dBConnect.GetField("UserID", 0));
            int receiverid = Convert.ToInt32(Session["UserID"]);
            String createdTime = lblSentDate.Text;
            String emailContent = lblEmailBody.Text;
            Response.Write("< script > alert('"+emailContent+"') </ script >");
            DataSet getemailid = getEmailIdFromDb(senderid, receiverid, createdTime);
            int emailid = Convert.ToInt16(dBConnect.GetField("emailid", 0));
            //Response.Write("<script>alert('created email id: " + emailid + "')</script>");

            String getCurrentTag = Session["Tag"].ToString();
            DataSet tagid = getTagidFromDb(receiverid, getCurrentTag);
            int currenttagid = Convert.ToInt16(dBConnect.GetField("TagId", 0));
            //Response.Write("<script>alert('Current tag: " + currenttagid + "')</script>");
            DataSet updatingtagid = getTagidFromDb(receiverid, "Flag");
            int newtagid = Convert.ToInt16(dBConnect.GetField("TagId", 0));
            //Response.Write("<script>alert('new tag: " + newtagid + "')</script>");
            int tagupdate = flagEmail(emailid, newtagid, currenttagid, receiverid);
            if(tagupdate != -1) { 
            Response.Write("<script>alert('Email Flagged')</script>");
            }
            else
            {
                Response.Write("<script>alert('Email was not Flagged. Contact system admin.')</script>");
            }
            
        }

        protected void lnkbtnViewProfile_Click(object sender, EventArgs e)
        {
            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(true);
            displayEmailGrid(false);
            displayComposeView(false);

            //All session fields used in the login page
            //Session["UserId"] = dBConnect.GetField("UserID", 0);
            //Session["UserName"] = dBConnect.GetField("UserName", 0);
            //Session["Address"] = dBConnect.GetField("Address", 0);
            //Session["PhoneNumber"] = dBConnect.GetField("PhoneNumber", 0);
            //Session["SystemEmail"] = dBConnect.GetField("SystemEmail", 0);
            //Session["Avatar"] = dBConnect.GetField("Avatar", 0);


            lblname.Text = Session["UserName"].ToString();
            lbladdress.Text = Session["Address"].ToString();
            lblPhone.Text = Session["PhoneNumber"].ToString();
            lblsysEmail.Text = Session["SystemEmail"].ToString();
            imgprofileavatar.ImageUrl = Session["Avatar"].ToString();
        }

        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        protected void btnWriteEmail_Click(object sender, EventArgs e)
        {

            txtSenderInfo.Text = Session["SystemEmail"].ToString();

            displayComposeView(true);
            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(false);
            displayEmailGrid(false);
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {

            int senderid = int.Parse(Session["UserId"].ToString());
            String recieverEmail = txtReceiverInfo.Text;
            DataSet getreceiverid = getUseridFromDb(recieverEmail);
            int receiverid = Convert.ToInt16(dBConnect.GetField("UserID", 0));
            String subject = txtSubject.Text;
            String messageContent = txtmessagecontent.Text;
            DateTime createdTime = DateTime.Now;
            String time = createdTime.ToString();

            int emailInsert = addEmailtoDb(senderid, receiverid, subject, messageContent, time);
            

            if (emailInsert > 0)
            {
                DataSet getemailid = getEmailIdFromDb(senderid, receiverid, time);
                int emailid = Convert.ToInt16(dBConnect.GetField("emailid", 0));
                
                DataSet sendersentfolder = getTagidFromDb(senderid, "sent");
                int sendertagid = Convert.ToInt16(dBConnect.GetField("tagid", 0));

                DataSet receiverinboxfolder = getTagidFromDb(receiverid, "inbox");
                int receivertagid = Convert.ToInt16(dBConnect.GetField("tagid", 0));

                int senderemailreceipt = createEmailReciept(senderid, emailid, sendertagid);
                int receiveremailreceipt = createEmailReciept(receiverid, emailid, receivertagid);

                if (senderemailreceipt >0 && receiveremailreceipt >0)
                {
                    Response.Write("<script>alert('the email has been sent.')</script>");

                }
                else
                {
                    Response.Write("<script>alert('the email was not sent.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('the email was not recorded.')</script>");

            }
            fillEmailList("Inbox");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            updateTag("Trash");
            fillEmailList(Session["Tag"].ToString());
        }

        protected void btnMoveMail_Click(object sender, EventArgs e)
        {
            updateTag(ddlMoveEmails.SelectedValue);
            fillEmailList(Session["Tag"].ToString());
        }

        protected void btnManageSystem_Click(object sender, EventArgs e)
        {
            Response.Redirect("systemMngmt.aspx");
        }

        public void updateTag(String TagName)
        {
            for (int i = 0; i < gv_email.Rows.Count; i++)
            {
                CheckBox selectedEmails = (CheckBox)gv_email.Rows[i].FindControl("cbSelect");
                if (selectedEmails.Checked)
                {

                    String senderName = gv_email.Rows[i].Cells[2].Text;
                    //Response.Write("<script>alert('" + senderName + "')</script>");
                    DataSet senderinfo = getSenderidFromDb(senderName);
                    int senderid = Convert.ToInt16(dBConnect.GetField("UserID", 0));
                    //Response.Write("<script>alert('" + senderid + "')</script>");
                    int receiverid = int.Parse(Session["UserId"].ToString());
                    String creation = gv_email.Rows[i].Cells[5].Text;
                    //Response.Write("<script>alert('I got this far')</script>");

                    DataSet getemailid = getEmailIdFromDb(senderid, receiverid, creation);
                    int emailid = Convert.ToInt16(dBConnect.GetField("emailid", 0));
                   

                    String getCurrentTag = Session["Tag"].ToString();
                    DataSet tagid = getTagidFromDb(receiverid, getCurrentTag);
                    int currenttagid = Convert.ToInt16(dBConnect.GetField("TagId", 0));
                    //Response.Write("<script>alert('Current tag: " + currenttagid + "')</script>");
                    DataSet updatingtagid = getTagidFromDb(receiverid, TagName);
                    int newtagid = Convert.ToInt16(dBConnect.GetField("TagId", 0));
                    //Response.Write("<script>alert('new tag: " + newtagid + "')</script>");


                    int tagupdate = updateEmailTag(emailid, newtagid, currenttagid, receiverid);
                    //Response.Write("<script>alert('" + tagupdate + "')</script>");
                    if(tagupdate > 0)
                    {
                        Response.Write("<script>alert('Email has been moved.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Unable to move Email at this time.')</script>");
                    }
                }
            }
        }

        //Define visibility of Sections
        public void displayEmailDetails(bool condition)
        {
            lblEmailDisplaySubject.Visible = condition;
            lblEmailDisplaySenderName.Visible = condition;
            lblEmailDisplayReceiverName.Visible = condition;
            imgEmailDisplayReceiverAvatar.Visible = condition;
            imgEmailDisplaySenderAvatar.Visible = condition;
            lblSentDate.Visible = condition;
            lblEmailBody.Visible = condition;
            lblReceiverLabel.Visible = condition;
            lblsenderLabel.Visible = condition;
            lblEmailBodyLabel.Visible = condition;
            lblReceivedDate.Visible = condition;
            btnFlagEmail.Visible = condition;
            

        }

        public void displayAddNewLabel(bool condition)
        {
            txtNewLabelName.Visible = condition;
            btnAddNewLabel.Visible = condition;
            lblAddNewFolder.Visible = condition;

        }

        public void displayEmailGrid(bool condition)
        {
            btnWriteEmail.Visible = condition;
            gv_email.Visible = condition;
            btnDelete.Visible = condition;
            txtSearchFeild.Visible = condition;
            btnSearchEmail.Visible = condition;
            ddlMoveEmails.Visible = condition;
            btnMoveMail.Visible = condition;
            lblFolderName.Visible = condition;
        }

        public void displayProfile(bool condition)
        {
            lblnametag.Visible = condition;
            lblname.Visible = condition;
            lbladdresstag.Visible = condition;
            lbladdress.Visible = condition;
            lblphonetag.Visible = condition;
            lblPhone.Visible = condition;
            lblsysEmailtag.Visible = condition;
            lblsysEmail.Visible = condition;
            imgprofileavatar.Visible = condition;


        }

        public void displayComposeView(bool condition)
        {
            btnSendEmail.Visible = condition;
            lblSender.Visible = condition;
            txtSenderInfo.Visible = condition;
            lblReceiver.Visible = condition;
            txtReceiverInfo.Visible = condition;
            lblSubject.Visible = condition;
            txtSubject.Visible = condition;
            lblmessagecontent.Visible = condition;
            txtmessagecontent.Visible = condition;
            btnCancelEmail.Visible = condition;

        }

        protected void btnSearchEmail_Click(object sender, EventArgs e)
        {
            String userInput = txtSearchFeild.Text;
            dbProcedures procedure = new dbProcedures();
            int userid = int.Parse(Session["UserId"].ToString());
            DataSet emails = procedure.getSearchResult(userInput, userid);
            int size = emails.Tables[0].Rows.Count;
            if (size > 0)
            {
                ArrayList emailList = new ArrayList();
                for (int i = 0; i < size; i++)
                {
                    int senderId = int.Parse(emails.Tables[0].Rows[i]["SenderId"].ToString());
                    DataSet senderName = getsenderInfo(senderId);

                    Email email = new Email();
                    email.SenderName = senderName.Tables[0].Rows[0]["UserName"].ToString();
                    email.Subject = emails.Tables[0].Rows[i]["Subject"].ToString();
                    email.EmailBody = emails.Tables[0].Rows[i]["EmailBody"].ToString();
                    email.CreatedTime = emails.Tables[0].Rows[i]["CreatedTime"].ToString();


                    emailList.Add(email);
                }
                gv_email.DataSource = emailList;
                gv_email.DataBind();
                displayEmailGrid(true);
                lblFolderEmpty.Visible = false;
            }
            else
            {
                gv_email.Visible = false;
                btnWriteEmail.Visible = true;
                lblFolderEmpty.Visible = true;
                lblFolderEmpty.Text = "There are no Emails that match this criteria.";
            }

            displayEmailDetails(false);
            displayAddNewLabel(false);
            displayProfile(false);
            displayComposeView(false);
        }

        protected void btnCancelEmail_Click(object sender, EventArgs e)
        {
            fillEmailList("Inbox");
            getTagsFordropdowns();

            displayAddNewLabel(false);
            displayEmailDetails(false);
            displayProfile(false);
            displayComposeView(false);
            btnFlagEmail.Visible = false;
        }

        //Let's get some stuff from the DB using my stored procedures :) -- Also Available in class to use for mngmnt
        public DataSet getTagidFromDb(int userId, String tagName)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getTagid";

            SqlParameter userid = new SqlParameter("@userid", userId);
            userid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userid);

            SqlParameter usertag = new SqlParameter("@tagName", tagName);
            usertag.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(usertag);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getEmailsFromDb(int userId, int tagid)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getEmails";

            SqlParameter userid = new SqlParameter("@userId", userId);
            userid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userid);

            SqlParameter usertag = new SqlParameter("@emailTag", tagid);
            usertag.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(usertag);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getsenderInfo(int userId)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getSender";

            SqlParameter userid = new SqlParameter("@userId", userId);
            userid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userid);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getSentEmails(int userId, int tagid)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getSentMail";

            SqlParameter userid = new SqlParameter("@userId", userId);
            userid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userid);

            SqlParameter usertag = new SqlParameter("@emailTag", tagid);
            usertag.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(usertag);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getTagName(int userId)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getTagName";

            SqlParameter userid = new SqlParameter("@userId", userId);
            userid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userid);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public int addNewTagtoDb(int userid, String tagname)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "addNewTag";

            SqlParameter insertUserid = new SqlParameter("@userid", userid);
            insertUserid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertUserid);

            SqlParameter insertTagName = new SqlParameter("@tagName", tagname);
            insertTagName.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertTagName);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

        public DataSet getSenderAvatarFromDb(String userName)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getSenderAvatar";

            SqlParameter name = new SqlParameter("@userName", userName);
            name.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(name);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getUseridFromDb(String email)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getUserid";

            SqlParameter userEmail = new SqlParameter("@sysEmail", email);
            userEmail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userEmail);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public int addEmailtoDb(int sender, int receiver, String subject, String content, String creation)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "createEmail";

            SqlParameter insertsender = new SqlParameter("@senderId", sender);
            insertsender.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertsender);

            SqlParameter insertreceiver = new SqlParameter("@receiverId", receiver);
            insertreceiver.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertreceiver);

            SqlParameter insertsubject = new SqlParameter("@subject", subject);
            insertsubject.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertsubject);

            SqlParameter insertcontent = new SqlParameter("@emailBody", content);
            insertcontent.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertcontent);

            SqlParameter insertcreation = new SqlParameter("@createdTime", creation);
            insertcreation.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertcreation);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

        public int createEmailReciept(int user, int email, int tag)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "createEmailReceipt";

            SqlParameter insertuser = new SqlParameter("@userid", user);
            insertuser.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertuser);

            SqlParameter insertemail = new SqlParameter("@emailid", email);
            insertemail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertemail);

            SqlParameter inserttag = new SqlParameter("@tagid", tag);
            inserttag.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(inserttag);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

        public DataSet getSenderidFromDb(String name)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getSenderid";

            SqlParameter creationTime = new SqlParameter("@userName", name);
            creationTime.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(creationTime);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;

        }

        public int updateEmailTag(int emailid, int desiredTag, int currentTag, int userId)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "updateTag";

            SqlParameter emailinfo = new SqlParameter("@emailid", emailid);
            emailinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(emailinfo);

            SqlParameter updateTag = new SqlParameter("@emailtag", desiredTag);
            updateTag.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(updateTag);

            SqlParameter currentTaginfo = new SqlParameter("@currentemailtag", currentTag);
            currentTaginfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(currentTaginfo);

            SqlParameter userinfo = new SqlParameter("@userid", userId);
            userinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userinfo);



            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }
        
        public DataSet getEmailIdFromDb(int sender, int receiver, String createdtime)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getEmailId";

            SqlParameter senderinfo = new SqlParameter("@senderid", sender);
            senderinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(senderinfo);

            SqlParameter receiverinfo = new SqlParameter("@receiverid", receiver);
            receiverinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(receiverinfo);

            SqlParameter timeinfo = new SqlParameter("@createdTime", createdtime);
            timeinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(timeinfo);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;

        }

        public int flagEmail(int emailid, int desiredTag, int currentTag, int userId)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "flagEmail";

            SqlParameter emailinfo = new SqlParameter("@emailid", emailid);
            emailinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(emailinfo);

            SqlParameter updateTag = new SqlParameter("@emailtag", desiredTag);
            updateTag.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(updateTag);

            SqlParameter currentTaginfo = new SqlParameter("@currenttag", currentTag);
            currentTaginfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(currentTaginfo);

            SqlParameter userinfo = new SqlParameter("@userid", userId);
            userinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userinfo);



            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

        
    }
}