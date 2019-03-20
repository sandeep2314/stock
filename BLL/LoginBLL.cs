using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class LoginBLL
    {
        Util_BLL util = new Util_BLL();


        // ----------------  Authenticate User ----------------------------------------------

        public UserBLL.User AuthenticateUser(DBSite site, string email_id, string password, string user_id="", int subuserId = -1)
        {
            UserBLL.User user = null;
            UserBLL user_bll = new UserBLL();
            string qry = "SELECT "
                       + " UserInfoId"
                       + ", UserName"
                       + ", Email"
                       + ", SenderID"
                       + ", SMSUser"
                       + ", SMSPassword"
                       + ", SMSDelivery"
                       + ", UserPassword"
                       + ", CreationDate"
                       + ", AmountPaid"
                       + ", StartDate"
                       + ", EndDate"
                       + ", Address"
                       + ", City"
                       + ", Country"
                       + ", Phone"
                       + ", Mobile"
                       + ", TinNumber"
                       + ", SalesTaxNumber"
                       + ", CSTNumber"
                       + ", IsActive"
                       + ", UserType"
                       + ", IsUnicode"
                       + ", Remarks"
                       + ", NumberOfSubusers"
                       + "  FROM tblUserInfo";
            if (user_id == "")
            {
                qry += " WHERE Email='" + email_id + "'"
                    + " AND UserPassword='" + password + "'";
            }
            else
            {
                qry += " WHERE UserInfoId=" + user_id;            
            }

            DataTable dt = site.ExecuteSelect(qry);

            if (dt.Rows.Count > 0)
            {
                DataRow row=dt.Rows[0];
                user = new UserBLL.User();

                user.UserId = util.CheckNullInt(row["UserInfoID"]);
                user.UserName = util.CheckNull(row["UserName"]);
                user.EmailId = util.CheckNull(row["Email"]);
                user.SenderId = util.CheckNull(row["SenderID"]);
                user.SMSUser = util.CheckNull(row["SMSUser"]);
                user.SMSPassword = util.CheckNull(row["SMSPassword"]);
                user.SMSDelivery = util.CheckNullInt(row["SMSDelivery"]);
                
                user.UserPassword = util.CheckNull(row["UserPassword"]);
                user.CreationDate = Convert.ToDateTime(row["CreationDate"]).ToShortDateString();
                user.AmountPaid = util.CheckNullDecimal(row["AmountPaid"]);
                user.StartDate = Convert.ToDateTime(row["StartDate"]).ToShortDateString();
                user.EndDate = Convert.ToDateTime(row["EndDate"]).ToShortDateString();
                user.Address = util.CheckNull(row["Address"]);
                user.City = util.CheckNull(row["City"]);
                user.Country = util.CheckNull(row["Country"]);
                user.Phone = util.CheckNull(row["Phone"]);
                user.Mobile = util.CheckNull(row["Mobile"]);
                user.TinNumber = util.CheckNull(row["TinNumber"]);
                user.SalesTaxNumber = util.CheckNull(row["SalesTaxNumber"]);
                user.CSTNumber = util.CheckNull(row["CSTNumber"]);
                user.IsActive = util.CheckNull(row["IsActive"]);
                user.UserType = util.CheckNullInt(row["UserType"]);
                user.IsUnicode = util.CheckNullInt(row["IsUnicode"]) == 1 ? true : false;
                user.Remarks = util.CheckNull(row["Remarks"]);
                user.NumberOFSubusers = util.CheckNullInt(row["NumberOfSubusers"]);
                user.fYear = util.CheckNullInt(util.GetHomeSetting(site, "CurrentFYear"));

                user.Subusers = user_bll.GetSubusers(site, user.UserId+"");   //-------  Subusers --------
                user.Current_subuserId = subuserId;
                user.Permissions = user_bll.GetPermissions(site, user.UserId + "");  //---------  Permissions ------------------

            }           

            return user;
        }


        //-------------  Authenticate  subuser --------------------------------------------------

        public UserBLL.User AuthenticateSubuser(DBSite site, string email_id, string password)
        {           
            string user_id="";
            int  subuser_id = -1;
            string qry = "SELECT "
                        + " SubuserID "
                       + ", UserId "
                       + " FROM tblSubuser ";

            qry += " WHERE EmailId='" + email_id + "'"
                + " AND SubuserPassword='" + password + "'";

            DataTable dt = site.ExecuteSelect(qry);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                user_id=util.CheckNull(row["UserId"]);
                subuser_id = util.CheckNullInt(row["SubuserID"]);
            }

            return AuthenticateUser(site, "", "", user_id, subuser_id);
        }


        public SubuserBLL.SubuserEntity  AuthenticateSubuser2(DBSite site, string email_id, string password)
        {

            SubuserBLL.SubuserEntity sub_user = null;
                        
            string qry = "SELECT "
                        + " SubuserID "
                        + ", SubUserName "
                        + ", EmailId "
                        + ", Active "
                       + ", UserId "
                       + " FROM tblSubuser ";

            qry += " WHERE EmailId='" + email_id + "'"
                + " AND SubuserPassword='" + password + "'";

            DataTable dt = site.ExecuteSelect(qry);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                sub_user = new SubuserBLL.SubuserEntity();

                sub_user.SubuserId = util.CheckNullInt(row["SubuserID"]);
                sub_user.SubuserName = util.CheckNull(row["SubUserName"]);
                sub_user.EmailId = util.CheckNull(row["EmailID"]);
                sub_user.UserState = util.CheckNull(row["Active"]);
                sub_user.UserId = util.CheckNullInt(row["UserId"]);
                
            }

            return sub_user;
        }



    }
}