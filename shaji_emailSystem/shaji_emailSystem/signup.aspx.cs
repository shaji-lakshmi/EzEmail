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

    public partial class signup : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnsignup_Click(object sender, EventArgs e)
        {

            //Data to be inserted in order by column
            String userName = txtfirstName.Text + " " + txtlastName.Text;
            String userAddress = txtaddress.Text;
            String PhoneNumber = txtPhoneNumber.Text;
            String sysEmail = txtEmail.Text;
            String altEmail = txtaltemail.Text;
            String avatarLink = setAvatarlink(ddlavatars.SelectedItem.Text);
            String sysPassword = txtPassword.Text;
            int status = 1;
            String userType = rblUserType.SelectedValue;
            Response.Write("<script>alert('"+userType+"')</script>");

            //ask about confirmPhoneNumber
            //if (formValidationforBlanks() && confirmPhoneNumber(PhoneNumber) && checkPhoneNumberLength() && confirmPassword(sysPassword, txtConfirmPassword.Text))
            if (formValidationforBlanks() && checkPhoneNumberLength() && confirmPassword(sysPassword, txtConfirmPassword.Text))
            {
                // ALL Sql strings that worked
                //String sqlUpdate = "INSERT INTO Users VALUES ('" + userName + "','" + userAddress + "','" + PhoneNumber + "','" + sysEmail + "','" + altEmail + "','" + avatarLink + "','" + sysPassword + "','" + status + "','" + userType + "')";
                //int updateStatus = dBConnect.DoUpdate(sqlUpdate);
                //String getUserid = "SELECT * FROM Users WHERE SystemEmail = '" + sysEmail + "'";
                //String tagInsert = "INSERT INTO Tags(TagName, UserId) VALUES ('Inbox','"+userid+"'),('Sent','"+userid+"'),('Flag','"+userid+"'),('Junk','"+userid+"'),('Trash','"+userid+"')";
                //int initializeFolders = dBConnect.DoUpdate(tagInsert);
                //String getUserid = "SELECT * FROM Users WHERE SystemEmail = '" + sysEmail + "'";
                //String tagInsert = "INSERT INTO Tags(TagName, UserId) VALUES ('Inbox','" + userid + "'),('Sent','" + userid + "'),('Flag','" + userid + "'),('Junk','" + userid + "'),('Trash','" + userid + "')";

                //Implemented using stored procedures :)
                int updateStatus = insertData(userName, userAddress, PhoneNumber, sysEmail, altEmail, avatarLink, sysPassword, status, userType);
                DataSet userData = getUseridFromDb(sysEmail);
                int userid = Convert.ToInt16(dBConnect.GetField("UserID",0));
                int initializeFolders = initializeFoldersinDb(userid);

                Response.Write("<script>alert('Profile Created')</script>");
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Write("<script>alert('Profile Not Created')</script>");

            }
            
        }
        
        //Check if any of the form fields are blank
        public bool formValidationforBlanks()
        {
            if (txtfirstName.Text == "" || txtlastName.Text == "" || txtEmail.Text == "" || txtPassword.Text == "" || txtConfirmPassword.Text == "" || txtaddress.Text == "" || txtPhoneNumber.Text == "" || txtaltemail.Text == "")
            {
                Response.Write("<script>alert('Some fields have been left blank. Please confirm that all fields have been filled in.')</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check if Phone Number is numerical
        public bool confirmPhoneNumber(string phoneNumber)
        {
            int number;
            bool isPhone = Int32.TryParse(phoneNumber, out number);

            if (isPhone)
            {
                return true;
            }
            else
            {
                Response.Write("<script>alert('Phone number must be a numerical value')</script>");
                return false;
            }
        }

        //Check if Phone Number is 10 digits
        public bool checkPhoneNumberLength()
        {
            int length = txtPhoneNumber.Text.Length;

            if (length > 10 || length < 10)
            {
                Response.Write("<script>alert('Phone number must contain 10 digits')</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        //Grab and set link for avatar
        public String setAvatarlink(String selectedAvatar)
        {
            String imgURL = "";

            if (selectedAvatar == "Fox")
            {
                imgURL = "assets/images/fox.png";
            }
            else if (selectedAvatar == "Elephant")
            {
                imgURL = "assets/images/elephant.jpg";
            }
            else if (selectedAvatar == "Giraffe")
            {
                imgURL = "assets/images/giraffe.jpg";
            }
            else if (selectedAvatar == "Goat")
            {
                imgURL = "assets/images/goat.jpeg";
            }
            else if (selectedAvatar == "Hamster")
            {
                imgURL = "assets/images/hamster.jpeg";
            }
            else if (selectedAvatar == "Lion")
            {
                imgURL = "assets/images/lion.png";
            }
            else if (selectedAvatar == "Monkey")
            {
                imgURL = "assets/images/monkey.png";
            }
            else if (selectedAvatar == "Panda")
            {
                imgURL = "assets/images/panda.png";
            }
            else if (selectedAvatar == "Tiger")
            {
                imgURL = "assets/images/tiger.png";
            }
            else if (selectedAvatar == "Zebra")
            {
                imgURL = "assets/images/zebra.jpeg";
            }
            return imgURL;
        }

        //Set User Permissions
        public String setUserType(string selectedType)
        {
            string user = "";
            if (selectedType == "Admin")
            {
                user = "Admin";
            }
            else
            {
                user = "User";
            }
            return user;
        }

        //See if Passwords entered match
        public bool confirmPassword(string sysPass, string confirmPass)
        {
            if (sysPass != confirmPass)
            {
                Response.Write("<script>alert('Passwords do not match. Please check your entries.')</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        //Change image on the form based on selection
        protected void ddlavatars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlavatars.SelectedItem.Text == "Fox")
            {
                avatarDisplay.ImageUrl = "assets/images/fox.png";
            }
            else if (ddlavatars.SelectedItem.Text == "Elephant")
            {
                avatarDisplay.ImageUrl = "assets/images/elephant.jpg";
            }
            else if (ddlavatars.SelectedItem.Text == "Giraffe")
            {
                avatarDisplay.ImageUrl = "assets/images/giraffe.jpg";
            }
            else if (ddlavatars.SelectedItem.Text == "Goat")
            {
                avatarDisplay.ImageUrl = "assets/images/goat.jpeg";
            }
            else if (ddlavatars.SelectedItem.Text == "Hamster")
            {
                avatarDisplay.ImageUrl = "assets/images/hamster.jpeg";
            }
            else if (ddlavatars.SelectedItem.Text == "Lion")
            {
                avatarDisplay.ImageUrl = "assets/images/lion.png";
            }
            else if (ddlavatars.SelectedItem.Text == "Monkey")
            {
                avatarDisplay.ImageUrl = "assets/images/monkey.png";
            }
            else if (ddlavatars.SelectedItem.Text == "Panda")
            {
                avatarDisplay.ImageUrl = "assets/images/panda.png";
            }
            else if (ddlavatars.SelectedItem.Text == "Tiger")
            {
                avatarDisplay.ImageUrl = "assets/images/tiger.png";
            }
            else if (ddlavatars.SelectedItem.Text == "Zebra")
            {
                avatarDisplay.ImageUrl = "assets/images/zebra.jpeg";
            }
        }

//Let's write stuff to the DB using my stored Procedure :)

        //This will initialize a user in the DB under the users table
        public int insertData(String userName, String address, String phoneNumber,
            String sysEmail, String altEmail, String avatar, String password,
            int status, String userType)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "createNewUser";

            SqlParameter insertUserName = new SqlParameter("@UserName", userName);
            insertUserName.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertUserName);

            SqlParameter insertAddress = new SqlParameter("@Address", address);
            insertAddress.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertAddress);

            SqlParameter insertPhone = new SqlParameter("@PhoneNumber", phoneNumber);
            insertPhone.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertPhone);

            SqlParameter insertSysEmail = new SqlParameter("@SysEmail", sysEmail);
            insertSysEmail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertSysEmail);

            SqlParameter insertAltEmail = new SqlParameter("@AltEmail", altEmail);
            insertAltEmail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertAltEmail);

            SqlParameter insertAvatar = new SqlParameter("@Avatar", avatar);
            insertAvatar.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertAvatar);

            SqlParameter insertPassword = new SqlParameter("@Password", password);
            insertPassword.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertPassword);

            SqlParameter insertStatus = new SqlParameter("@Status", status);
            insertStatus.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertStatus);

            SqlParameter insertType = new SqlParameter("@UserType", userType);
            insertType.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertType);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

        //This will get the Userid From DB to be used to insert folders to Tags Table
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

        //This will initialize Inbox, Sent, Junk, Trash and Flag folders for new users
        public int initializeFoldersinDb(int userid)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "initializeTags";

            SqlParameter insertUserid = new SqlParameter("@userid", userid);
            insertUserid.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertUserid);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }
    }
    

}
