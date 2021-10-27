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
    public partial class systemMngmt : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        dbProcedures procedure = new dbProcedures();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillUserGrid();
                displayEmailContent(false);
                lblflagHeading.Visible = false;
            }
        }

        public void fillUserGrid()
        {

            ArrayList userList = new ArrayList();
            DataSet users = procedure.getUsers();
            int size = users.Tables[0].Rows.Count;

            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    Users addusers = new Users();
                    addusers.UserName = users.Tables[0].Rows[i]["UserName"].ToString();
                    addusers.Address = users.Tables[0].Rows[i]["Address"].ToString();
                    addusers.PhoneNumber = users.Tables[0].Rows[i]["PhoneNumber"].ToString();
                    addusers.SystemEmail = users.Tables[0].Rows[i]["SystemEmail"].ToString();
                    addusers.AlternateEmail = users.Tables[0].Rows[i]["AlternateEmail"].ToString();
                    addusers.Avatar = users.Tables[0].Rows[i]["Avatar"].ToString();
                    addusers.Status = int.Parse(users.Tables[0].Rows[i]["Active"].ToString());
                    addusers.UserType = users.Tables[0].Rows[i]["UserType"].ToString();

                    userList.Add(addusers);
                }
            }
            gv_users.DataSource = userList;
            gv_users.DataBind();

        }

        protected void lnkbtnViewFlaggedEmails_Click(object sender, EventArgs e)
        {
            lblflagHeading.Visible = true;
            ArrayList flaggedemailids = new ArrayList();
            DataSet emails = procedure.getflaggedemailid();
            
            int size = emails.Tables[0].Rows.Count; 
            if(size > 0)
            {
                for(int i=0; i<size; i++)
                {
                    flaggedemailids.Add(Convert.ToInt32(emails.Tables[0].Rows[i]["EmailId"]));
                    //Response.Write("<script>alert('" + emails.Tables[0].Rows[i]["EmailId"].ToString() + "')</script>");
                }
            }

            ArrayList flaggedEmailDeets = new ArrayList(); 
            for(int i= 0; i<flaggedemailids.Count; i++)
            {
                int id = int.Parse(flaggedemailids[i].ToString());
               

                DataSet emaildetails = procedure.getflagdetails(id);
                int size2 = emaildetails.Tables[0].Rows.Count;
                
                if (size2 > 0)
                {
                    for(int j=0; j< size2; j++) {

                        int senderId = int.Parse(emaildetails.Tables[0].Rows[j]["SenderId"].ToString());
                        DataSet senderinfo = procedure.getsenderInfo(senderId);
                        Email email = new Email();
                        email.SenderName = senderinfo.Tables[0].Rows[0]["UserName"].ToString();
                        email.Subject = emaildetails.Tables[0].Rows[j]["Subject"].ToString();
                        email.EmailBody = emaildetails.Tables[0].Rows[j]["EmailBody"].ToString();
                        email.CreatedTime = emaildetails.Tables[0].Rows[j]["CreatedTime"].ToString();

                        flaggedEmailDeets.Add(email);

                    }
                    gv_flaggedEmails.DataSource = flaggedEmailDeets;
                    gv_flaggedEmails.DataBind();
                }
            }

        }

        protected void lnkbtnBan_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_users.Rows.Count; i++){
                CheckBox selectedUsers = (CheckBox)gv_users.Rows[i].FindControl("cbSelect");
                if (selectedUsers.Checked)
                {
                    String chosenEmail = gv_users.Rows[i].Cells[4].Text;
                    int updateStatus = procedure.updateuserstatus(chosenEmail, 0);
                   
                }
            }
            fillUserGrid();
        }

        protected void gv_flaggedEmails_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = gv_flaggedEmails.SelectedIndex;
            lblSubject.Text = gv_flaggedEmails.Rows[row].Cells[2].Text;
            lblSenderName.Text = gv_flaggedEmails.Rows[row].Cells[1].Text;
            lblEmailBody.Text = gv_flaggedEmails.Rows[row].Cells[3].Text;
            lblcreatedTime.Text = gv_flaggedEmails.Rows[row].Cells[4].Text;
            DataSet senderAvatar = procedure.getSenderAvatarFromDb(gv_flaggedEmails.Rows[row].Cells[1].Text);
            imgSenderAvatar.ImageUrl = senderAvatar.Tables[0].Rows[0]["Avatar"].ToString();

            gv_flaggedEmails.Visible = false;
            displayEmailContent(true);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            gv_flaggedEmails.Visible = true;
            displayEmailContent(false);
        }

        public void displayEmailContent(bool condition)
        {
            lblSubject.Visible = condition;
            lblContent.Visible = condition;
            lblcreatedTime.Visible = condition;
            lblEmailBody.Visible = condition;
            lblSender.Visible = condition;
            lblSenderName.Visible = condition;
            lbltime.Visible = condition;
            btnBack.Visible = condition;
            imgSenderAvatar.Visible = condition;
            lblflagHeading.Visible = false;
        }

        protected void lnkbtnunban_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < gv_users.Rows.Count; i++){
                CheckBox selectedUsers = (CheckBox)gv_users.Rows[i].FindControl("cbSelect");
                if (selectedUsers.Checked)
                {
                    String chosenEmail = gv_users.Rows[i].Cells[4].Text;
                    int updateStatus = procedure.updateuserstatus(chosenEmail, 1);
                   if(updateStatus > 0)
                    {
                        Response.Write("<script>alert('User has been banned.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Unable to ban user at this time.')</script>");
                    }
                }
            }
            fillUserGrid();
        }

        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}


