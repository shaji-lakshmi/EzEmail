using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using System.Data;
using System.Data.SqlClient;

namespace shaji_emailSystem
{
    public partial class Default : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        SqlCommand procedure = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //String getUser = "SELECT * FROM Users WHERE SystemEmail = '" + txtEmail.Text + "' AND Password = '" + txtPassword.Text + "'";
            //DataSet userData = dBConnect.GetDataSet(getUser);

            DataSet userData = verifyUserLogin(txtEmail.Text, txtPassword.Text);

            int responseSize = userData.Tables[0].Rows.Count;
            if (responseSize != 0)
            {
                int userStatus = int.Parse(dBConnect.GetField("Active", 0).ToString());
                if (userStatus == 0)
                {
                    Response.Write("<script>alert('This account is banned. Please contact System Admin at 1-800-xxx-xxxx')</script>");
                }
                else
                {
                    
                        Session["UserId"] = dBConnect.GetField("UserID", 0);
                        Session["UserName"] = dBConnect.GetField("UserName", 0);
                        Session["Address"] = dBConnect.GetField("Address", 0);
                        Session["PhoneNumber"] = dBConnect.GetField("PhoneNumber", 0);
                        Session["SystemEmail"] = dBConnect.GetField("SystemEmail", 0);
                        Session["Avatar"] = dBConnect.GetField("Avatar", 0);
                        Session["UserType"] = dBConnect.GetField("UserType", 0);
                        Response.Redirect("emailClientPage.aspx");
                    

                }
            }
            else
            {
                //The DB didn't find User
                Response.Write("<script>alert('Account not found. Please check credentials and try again.')</script>");
            }
        }

//Let's do some DB stuff using my stored procedures :)

        //This will match the ccredentials entered on login screen with users in the db
        public DataSet verifyUserLogin(String email, String password)
        {
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "verifyUserLogin";

            SqlParameter userEmail = new SqlParameter("@systemEmail", email);
            userEmail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userEmail);

            SqlParameter userPassword = new SqlParameter("@systempass", password);
            userPassword.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userPassword);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }
    }

   
}