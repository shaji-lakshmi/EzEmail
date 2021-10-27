using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utilities;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace emailLibrary
{
    public class dbProcedures
    {
        DBConnect dBConnect = new DBConnect();

        
        public DataSet getUsers()
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getAllUsers";

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getuserifo(String systemEmail)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getuserInfo";

            SqlParameter userEmail = new SqlParameter("@userEmail", systemEmail);
            userEmail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userEmail);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getflaggedemailid()
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getFlaggedEmails";

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public DataSet getflagdetails(int id)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "getFlaggedEmailDetails";

            SqlParameter userEmail = new SqlParameter("@emailid", id);
            userEmail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(userEmail);

            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }

        public int updateuserstatus(String email, int status)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "updateUserStatus";

            SqlParameter insertuser = new SqlParameter("@email", email);
            insertuser.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertuser);

            SqlParameter insertemail = new SqlParameter("@status", status);
            insertemail.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(insertemail);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

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

        public int createFlagDetails(int email, String content, int sender, int flagger)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "recordFlag";

            SqlParameter emailinfo = new SqlParameter("@emailId", email);
            emailinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(emailinfo);

            SqlParameter emailcontent = new SqlParameter("@emailBody", content);
            emailcontent.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(emailcontent);

            SqlParameter senderinfo = new SqlParameter("@sender", sender);
            senderinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(senderinfo);

            SqlParameter flaggerinfo = new SqlParameter("@flagger", flagger);
            flaggerinfo.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(flaggerinfo);

            int response = dBConnect.DoUpdateUsingCmdObj(procedure);
            return response;
        }

        public DataSet getSearchResult(String insert, int userid)
        {
            SqlCommand procedure = new SqlCommand();
            procedure.CommandType = CommandType.StoredProcedure;
            procedure.CommandText = "searchEmails";

            SqlParameter criteria = new SqlParameter("@insertString", insert);
            criteria.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(criteria);

            SqlParameter user = new SqlParameter("@userid", userid);
            user.Direction = ParameterDirection.Input;
            procedure.Parameters.Add(user);


            DataSet responseData = dBConnect.GetDataSetUsingCmdObj(procedure);
            return responseData;
        }
    }
}
