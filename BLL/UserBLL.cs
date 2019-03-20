using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web;

namespace AccountingSoftware.BLL
{
    public class UserBLL
    {
        Util_BLL util = new Util_BLL();

       
        public class User
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserPassword { get; set; }
            public string EmailId { get; set; }

            public string SenderId { get; set; }
            public string SMSUser { get; set; }
            public string SMSPassword { get; set; }
            public int SMSDelivery { get; set; }

            public string CreationDate { get; set; }
            public decimal AmountPaid { get; set; }

            public string StartDate { get; set; }
            public string EndDate { get; set; }

            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Mobile { get; set; }

            public string TinNumber { get; set; }
            public string SalesTaxNumber { get; set; }
            public string IsActive { get; set; }
            public int UserType { get; set; }
            public string CSTNumber { get; set; }
            public string Remarks { get; set; }

            public int NumberOFSubusers { get; set; }
            public List<SubuserBLL.SubuserEntity> Subusers { get; set; } 
            public int Current_subuserId { get; set; }
            public List<Permission> Permissions { get; set; } 

            //public int userID = 200;
            
            //public  int fYear = 2014;
            public int fYear { get;set; }

            public bool IsUnicode { get; set; }
        }


        public void SaveUserInfo(DBSite site, User user)
        {

            string qry = " INSERT INTO tblUserInfo("
                       + " UserName "
                       + ", Email"
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
                       + ", UserType"
                       + ", NumberOfSubusers"
                       + ", Remarks"
                       +", FYear"
                       +" )";
            qry += " VALUES( '" + user.UserName + "'"
                + ", '" + user.EmailId + "'"
                + ", '" + user.UserPassword + "'"
                + ", '" + user.CreationDate + "'"
                + ", " + user.AmountPaid
                + ", '" + user.StartDate + "'"
                + ", '" + user.EndDate + "'"
                + ", '" + user.Address + "'"
                + ", '" + user.City + "'"
                + ", '" + user.Country + "'"
                + ", '" + user.Phone + "'"
                + ", '" + user.Mobile + "'"
                + ", '" + user.TinNumber + "'"
                + ", '" + user.SalesTaxNumber + "'"
                + ", '" + user.CSTNumber + "'"
                + ", " + user.UserType
                + ", " + user.NumberOFSubusers
                + ", '" + user.Remarks + "'"
                +", "+user.fYear
                + ")";

            site.Execute(qry);

            
   //------  Get User Id Just Inbserted ------------------------

            qry = " SELECT UserInfoId FROM tblUserInfo WHERE Email='" + user.EmailId + "'";
            user.UserId=util.CheckNullInt(site.ExecuteSelect(qry).Rows[0]["UserInfoId"]); //-----  get user id query ----------

            foreach (Permission permission in user.Permissions)
            {

                qry = " INSERT INTO tblUserPermissions"
                    + "("
                    + "UserId"
                    + ", PermissionId"
                    + ")"
                    + " VALUES("
                    + user.UserId
                    + ", "
                    + permission.PermissionId
                    + ")";

                site.Execute(qry);
            }
        }

        public List<User> GetUsers()
        {
            DBSite site = new DBSite();

            return GetUsers(site, Util_BLL.User.UserId.ToString() );
        }


        public List<User> GetUsers(DBSite site, string user_id = "")
        {
            List<User> user_list = new List<User>();
            User user = null;

            string qry = " SELECT "
                       + " UserInfoId"
                       + ", UserName"
                       + ", Email"
                       + ", SenderId"
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
                       + ", UserType"
                       + ", IsUnicode"
                       + ", NumberOfSubusers"
                       + ", SMSUser"
                       + ", SMSPassword"
                       + ", SMSDelivery"
                       + ", Remarks";
            qry += " FROM tblUserInfo";

            if (user_id != "")
                qry += " WHERE UserInfoId=" + user_id;

            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                user = new User();

                user.UserId = util.CheckNullInt(row["UserInfoId"]);
                user.UserName = util.CheckNull(row["UserName"]);
                user.EmailId = util.CheckNull(row["Email"]);
                user.SenderId = util.CheckNull(row["SenderId"]);
                user.SMSUser = util.CheckNull(row["SMSUser"]);
                user.SMSPassword = util.CheckNull(row["SMSPassword"]);
                user.SMSDelivery = util.CheckNullInt(row["SMSDelivery"]);
                user.UserPassword = util.CheckNull(row["UserPassword"]);
                user.AmountPaid = util.CheckNullDecimal(row["AmountPaid"]);
                user.CreationDate = ((DateTime)row["CreationDate"]).ToShortDateString();
                user.StartDate = ((DateTime)row["StartDate"]).ToShortDateString();
                user.EndDate = ((DateTime)row["EndDate"]).ToShortDateString();
                user.Address = util.CheckNull(row["Address"]);
                user.City = util.CheckNull(row["City"]);
                user.Country = util.CheckNull(row["Country"]);
                user.Phone = util.CheckNull(row["Phone"]);
                user.Mobile = util.CheckNull(row["Mobile"]);
                user.TinNumber = util.CheckNull(row["TinNumber"]);
                //user.SalesTaxNumber = "http://www.gsc99.com/images/logo/cbselogo.png";
                user.SalesTaxNumber = util.CheckNull(row["SalesTaxNumber"]); ;
               
                
                user.CSTNumber = util.CheckNull(row["CSTNumber"]);
                user.UserType = util.CheckNullInt(row["UserType"]);
                user.IsUnicode = util.CheckNullInt(row["IsUnicode"]) == 1 ? true : false;
                user.Remarks = util.CheckNull(row["Remarks"]);
                user.NumberOFSubusers = util.CheckNullInt(row["NumberOfSubusers"]);
                user.fYear = util.CheckNullInt( util.GetHomeSetting(site, "CurrentFYear"));

                user.Subusers = GetSubusers(site, user.UserId+""); //------  get subuser list --------
                user.Permissions = GetPermissions(site, user.UserId + ""); //-----  get permission list --------

                user_list.Add(user);
            }
            return user_list;
        }

        //----------  Get subusers of user -------------------

        public List<SubuserBLL.SubuserEntity> GetSubusers(DBSite site, string user_id)
        {
            List<SubuserBLL.SubuserEntity> subuser_list = new List<SubuserBLL.SubuserEntity>();
            SubuserBLL.SubuserEntity subuser = null;

            string qry = " SELECT "
                      + "SubuserId"
                      + ", SubuserName"
                      + ", SubuserCreationDate"
                      + ", SubuserPassword"
                      + ", EmailId"
                      + ", Active"
                      + ", Designation"
                      + ", Address"
                      + ", City"
                      + ", Mobile"
                      + " FROM tblSubuser"
                      + " WHERE UserId=" + user_id;

            DataTable subuser_table = site.ExecuteSelect(qry);

            foreach (DataRow row in subuser_table.Rows)
            {
                subuser = new SubuserBLL.SubuserEntity();

                subuser.SubuserId = util.CheckNullInt(row["SubuserId"]);
                subuser.SubuserName = util.CheckNull(row["SubuserName"]);
                subuser.CreationDate = Convert.ToDateTime(row["SubuserCreationDate"]).ToShortDateString();
                subuser.Password = util.CheckNull(row["SubuserPassword"]);
                subuser.EmailId = util.CheckNull(row["EmailId"]);
                subuser.UserState = util.CheckNull(row["Active"]);
                subuser.Designation = util.CheckNull(row["Designation"]);
                subuser.Address = util.CheckNull(row["Address"]);
                subuser.City = util.CheckNull(row["City"]);
                subuser.Mobile = util.CheckNull(row["Mobile"]);

                subuser_list.Add(subuser);
            }

            return subuser_list;
        }


        //------------   Get User Permissions -------------------------

        public List<Permission> GetPermissions(DBSite site, string user_id)
        {
            List<Permission> permission_list = new List<Permission>();
            Permission permission = null;


            string qry = " SELECT "
                      + " p.PermissionId"
                      + ", p.Module"
                      + ", p.ASPXPageName"
                      + " FROM tblUserPermissions as up"
                      +" LEFT OUTER JOIN tblPermission as p"
                      +" ON up.PermissionId = p.PermissionId"
                      + " WHERE up.UserId=" + user_id;

            DataTable permission_table = site.ExecuteSelect(qry);

            foreach (DataRow row in permission_table.Rows)
            {
                permission = new Permission();

                permission.PermissionId = util.CheckNullInt(row["PermissionId"]);
                permission.ModuleName = util.CheckNull(row["Module"]);
                permission.ASPXPageName = util.CheckNull(row["ASPXPageName"]);

                permission_list.Add(permission);
            }
            return permission_list;
        }


        //------------  Edit User Info ----------------------------------------

        public void EditUserInfo(DBSite site, User user, string user_id)
        {
            string qry = " UPDATE tblUserInfo SET "
                      + " UserName ='" + user.UserName + "'"
                      + ", Email ='" + user.EmailId + "'"
                      + ", UserPassword ='" + user.UserPassword + "'"
                      + ", CreationDate='" + user.CreationDate + "'"
                      + ", AmountPaid = " + user.AmountPaid
                      + ", StartDate ='" + user.StartDate + "'"
                      + ", EndDate ='" + user.EndDate + "'"
                      + ", Address ='" + user.Address + "'"
                      + ", City ='" + user.City + "'"
                      + ", Country ='" + user.Country + "'"
                      + ", Phone ='" + user.Phone + "'"
                      + ", Mobile ='" + user.Mobile + "'"
                      + ", TinNumber ='" + user.TinNumber + "'"
                      + ", SalesTaxNumber ='" + user.SalesTaxNumber + "'"
                      + ", CSTNumber ='" + user.CSTNumber + "'"
                      + ", UserType = 1"
                      + ", Remarks ='" + user.Remarks + "'"
                      + ",  NumberOfSubusers=" + user.NumberOFSubusers 
                      + " WHERE UserInfoId=" + user_id;

            site.Execute(qry); //----------  update user information -----------


            qry = " DELETE FROM tblUserPermissions"
                + " WHERE UserId=" + user_id;

            site.Execute(qry); //-----  delete all permissions with  current user -----

            //----------   then insert all the permissions now granted---------
            foreach (Permission permission in user.Permissions)
            {

                qry = " INSERT INTO tblUserPermissions"
                    + "("
                    + "UserId"
                    + ", PermissionId"
                    + ")"
                    + " VALUES("
                    + user_id
                    + ", "
                    + permission.PermissionId
                    + ")";

                site.Execute(qry);
            }

        }

        public void DeleteUser(DBSite site, string user_id)
        {
            string qry = " DELETE FROM tblUserInfo"
                        + " WHERE UserInfoId IN (" + user_id + ")";

            site.Execute(qry);    //----  delete all user's from user information table ---------


            qry = " DELETE FROM tblSubuser WHERE UserId IN (" + user_id + ")";
            site.Execute(qry);       //--------  delete all subusers of all selected users ------------------


            qry = "  DELETE FROM tblUserPermissions"
                + " WHERE UserId IN (" + user_id + ")";

            site.Execute(qry); //-----------  delete all permissions from User Permission table for all selected user's and their corressponding  subusers--------



        }

        public List<User> GetMatchedUsers(DBSite site, string value_to_search)
        {
            List<User> users = new List<User>();
            User user = null;

            string qry = " SELECT "
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
                       + ", UserType"
                       + ", Remarks";
            qry += " FROM tblUserInfo  ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);

           // qry += " WHERE ";
            //qry += " WHERE UserID = " + User.userID + " AND  FYear= " + User.fYear + " AND ";

            qry += "AND (( UserName LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( Email LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( Address LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( City LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( Country LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( Phone LIKE '%" + value_to_search + "%' ) OR";

            qry += " ( Mobile LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( TinNumber LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( SalesTaxNumber LIKE '%" + value_to_search + "%' ) OR";


            if (util.IsDate(value_to_search))
            {
                qry += " ( CreationDate = '" + value_to_search + "' ) OR";
                qry += " ( EndDate = '" + value_to_search + "' ) OR";
                qry += " ( StartDate = '" + value_to_search + "' ) OR";
            }

            if (util.IsNumber(value_to_search))
            {
                qry += " ( AmountPaid = '" + value_to_search + "' ) OR ";
            }
            qry += " ( CSTNumber LIKE '%" + value_to_search + "%' ) ";
            qry += ")";

            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                user = new User();

                user.UserId = util.CheckNullInt(row["UserInfoId"]);
                user.UserName = util.CheckNull(row["UserName"]);
                user.EmailId = util.CheckNull(row["Email"]);
                user.SenderId  = util.CheckNull(row["SenderID"]);
                user.SMSUser = util.CheckNull(row["SMSUser"]);
                user.SMSPassword = util.CheckNull(row["SMSPassword "]);
                user.SMSDelivery = util.CheckNullInt(row["SMSDelivery"]);
                
                user.UserPassword = util.CheckNull(row["UserPassword"]);
                user.AmountPaid = util.CheckNullDecimal(row["AmountPaid"]);
                user.CreationDate = ((DateTime)row["CreationDate"]).ToShortDateString();
                user.StartDate = ((DateTime)row["StartDate"]).ToShortDateString();
                user.EndDate = ((DateTime)row["EndDate"]).ToShortDateString();
                user.Address = util.CheckNull(row["Address"]);
                user.City = util.CheckNull(row["City"]);
                user.Country = util.CheckNull(row["Country"]);
                user.Phone = util.CheckNull(row["Phone"]);
                user.Mobile = util.CheckNull(row["Mobile"]);
                user.TinNumber = util.CheckNull(row["PinNumber"]);
                user.SalesTaxNumber = util.CheckNull(row["SalesTaxNumber"]);
                user.CSTNumber = util.CheckNull(row["CSTNumber"]);
                user.UserType = util.CheckNullInt(row["UserType"]);
                user.Remarks = util.CheckNull(row["Remarks"]);


                users.Add(user);
            }

            return users;
        }

        public bool IsDuplicate(DBSite site, string value)
        {
            string qry = "  SELECT UserInfoId FROM tblUserInfo"
                       + " WHERE UserName='" + value + "'";

            return site.ExecuteSelect(qry).Rows.Count > 0;
        }

    }

    //------------  Subuser ----------------------------------------------------------------------

    public class SubuserBLL
    {
        Util_BLL util = new Util_BLL();

        public class SubuserEntity
        {
            public int SubuserId { get; set; }
            public string SubuserName { get; set; }
            public string EmailId { get; set; }
            public string CreationDate { get; set; }
            public string Password { get; set; }
            public string UserState { get; set; }
            public string Mobile { get; set; }
            public string Designation { get; set; }
            public string Address { get; set; }
            public string City { get; set; }

            public int UserId { get; set; }
            public string CreatedAt { get; set; }

            public List<Permission> Permissions { get; set; }
            public List<ClassMasterBLL.ClassMasterEntity> TeacherClasses { get; set; }
            public List<SubjectMasterBLL.SubjectMasterEntity> TeacherSubjects { get; set; }
        }


        //--------------  savew subuser information -----------------

        public void SaveSubuserInfo(DBSite site, SubuserEntity subuser)
        {

            int user_state = subuser.UserState == "1" ? 1 : 0;

            string subuser_info_qry = "  INSERT INTO tblSubuser ("
                        + "SubuserName"
                        + ", SubuserCreationDate"
                        + ", SubuserPassword"
                        + ", EmailId"
                        + ", Designation"
                        + ", Address"
                        + ", City"
                        + ", Mobile"
                        + ", Active"
                        + ", UserId"
                        + ")";
            subuser_info_qry += " VALUES ("
                + "'" + subuser.SubuserName + "'"
                + ", '" + subuser.CreationDate + "'"
                + ", '" + subuser.Password + "'"
                + ", '" + subuser.EmailId + "'"
                + ", '" + subuser.Designation + "'"
                + ", '" + subuser.Address + "'"
                + ", '" + subuser.City + "'"
                + ", '" + subuser.Mobile + "'"
                + ", " + user_state
                + ", " + Util_BLL.User.UserId //----  User ID in Currently Logged In ---------------
                + " )";

            site.Execute(subuser_info_qry); //------------  Enter subuser information  ------------------



            subuser_info_qry = " SELECT SubuserId FROM tblSubuser"
                             + " WHERE EmailId='" + subuser.EmailId + "'"
                             + " AND UserId=" + Util_BLL.User.UserId;

            string subuser_id=util.CheckNull(site.ExecuteSelect(subuser_info_qry).Rows[0]["SubuserId"]); //--  subuserId query --------------


            foreach (Permission permission in subuser.Permissions)
            {

                subuser_info_qry = " INSERT INTO tblUserPermissions"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", PermissionId"
                    + ")"
                    + " VALUES(" 
                    + Util_BLL.User.UserId   //------ user id of Current User --------
                    + ", " + subuser_id
                    +", " +permission.PermissionId
                    + ")";

                site.Execute(subuser_info_qry);
            }



            foreach (ClassMasterBLL.ClassMasterEntity  clas in subuser.TeacherClasses )
            {

                subuser_info_qry = " INSERT INTO tblTeacherClasses"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", ClassId"
                    + ")"
                    + " VALUES("
                    + Util_BLL.User.UserId   //------ user id of Current User --------
                    + ", " + subuser_id
                    + ", " + clas.ClassMasterId 
                    + ")";

                site.Execute(subuser_info_qry);
            }

            foreach (SubjectMasterBLL.SubjectMasterEntity sub in subuser.TeacherSubjects )
            {

                subuser_info_qry = " INSERT INTO tblTeacherSubjects"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", SubjectId"
                    + ")"
                    + " VALUES("
                    + Util_BLL.User.UserId   
                    + ", " + subuser_id
                    + ", " + sub.SubjectMasterId 
                    + ")";

                site.Execute(subuser_info_qry);
            }


        }

        

        public List<SubuserEntity> GetSubusers(DBSite site, int userId, string subuser_ids = "")
        {
            List<SubuserEntity> subuser_list = new List<SubuserEntity>();
            SubuserEntity subuser = null;


            string qry = "SELECT "
                        + "SubuserId"
                        + ", SubuserName"
                        + ", SubuserCreationDate"
                        + ", SubuserPassword"
                        + ", EmailId"
                        + ", Designation"
                        + ", Address"
                        + ", City"
                        + ", Mobile"
                        + ", Active"
                        + ", UserId"
                        + " FROM tblSubuser "
                        + " WHERE userId=" + userId;

            if (subuser_ids != "")
                qry += " AND SubuserId IN ( " + subuser_ids + " )";


            DataTable dt = site.ExecuteSelect(qry);        


            foreach (DataRow row in dt.Rows)
            {
                subuser = new SubuserEntity();

                subuser.SubuserId = util.CheckNullInt(row["SubuserId"]);
                subuser.SubuserName = util.CheckNull(row["SubuserName"]);

                DateTime date = Convert.ToDateTime(row["SubuserCreationDate"]);
                subuser.CreationDate = date.ToShortDateString();
                subuser.Password = util.CheckNull(row["SubuserPassword"]);
                subuser.EmailId = util.CheckNull(row["EmailId"]);
                subuser.Designation = util.CheckNull(row["Designation"]);
                subuser.Address = util.CheckNull(row["Address"]);
                subuser.City = util.CheckNull(row["City"]);
                subuser.Mobile = util.CheckNull(row["Mobile"]);
                subuser.UserState = util.CheckNullInt(row["Active"]) == 1 ? "Yes" : "No";

                subuser.Permissions = GetPermissions(site, subuser.SubuserId+"");
                subuser.TeacherClasses = GetTeacherClasses(site, subuser.SubuserId + "");
                subuser.TeacherSubjects = GetTeacherSubjects(site, subuser.SubuserId + ""); 

                subuser_list.Add(subuser);
            }         

            return subuser_list;
        }

        //----- get permissions ----------------------------------------------

        public List<Permission> GetPermissions(DBSite site, string subuser_id)
        {
            List<Permission> permission_list = new List<Permission>();
            Permission permission = null;


            string qry = " SELECT "
                      + " p.PermissionId"
                      + ", p.Module"
                      + ", p.ASPXPageName"
                      + " FROM tblUserPermissions as up"
                      + " LEFT OUTER JOIN tblPermission as p"
                      + " ON up.PermissionId = p.PermissionId"
                      + " WHERE up.UserId=" + Util_BLL.User.UserId
                      + " AND SubuserId=" + subuser_id;

            DataTable permission_table = site.ExecuteSelect(qry);

            foreach (DataRow row in permission_table.Rows)
            {
                permission = new Permission();

                permission.PermissionId = util.CheckNullInt(row["PermissionId"]);
                permission.ModuleName = util.CheckNull(row["Module"]);
                permission.ASPXPageName = util.CheckNull(row["ASPXPageName"]);

                permission_list.Add(permission);
            }
            return permission_list;
        }


        public List<ClassMasterBLL.ClassMasterEntity> GetTeacherClasses(DBSite site, string subuser_id)
        {
            List<ClassMasterBLL.ClassMasterEntity> class_list = new List<ClassMasterBLL.ClassMasterEntity>();
            ClassMasterBLL.ClassMasterEntity clas = null;


            string qry = " SELECT "
                      + " tc.classId"
                      + " FROM tblTeacherClasses tc "
                      + " LEFT OUTER JOIN tblClassMaster c  ON  c.ClassMasterID = tc.ClassID "
                      + " WHERE tc.UserId=" + Util_BLL.User.UserId
                      + " AND tc.SubuserId=" + subuser_id;

            DataTable class_table = site.ExecuteSelect(qry);

            foreach (DataRow row in class_table.Rows)
            {
                clas = new ClassMasterBLL.ClassMasterEntity();

                clas.ClassMasterId = util.CheckNullInt(row["ClassID"]);
                
                class_list.Add(clas);
            }
            return class_list;
        }

        public List<SubjectMasterBLL.SubjectMasterEntity> GetTeacherSubjects(DBSite site, string subuser_id)
        {
            List<SubjectMasterBLL.SubjectMasterEntity> sub_list = new List<SubjectMasterBLL.SubjectMasterEntity>();
            SubjectMasterBLL.SubjectMasterEntity sub = null;


            string qry = " SELECT "
                      + " ts.SubjectID"
                      + " FROM tblTeacherSubjects ts "
                      + " LEFT OUTER JOIN tblSubjectMaster s  ON  s.SubjectMasterID = ts.SubjectID "
                      + " WHERE ts.UserId=" + Util_BLL.User.UserId
                      + " AND ts.SubuserId=" + subuser_id;

            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                 sub = new SubjectMasterBLL.SubjectMasterEntity();

                 sub.SubjectMasterId = util.CheckNullInt(row["subjectId"]);

                sub_list.Add(sub);
            }
            return sub_list;
        }



        public bool IsSubUserHasPermissionForThePage(DBSite site, int subuser_id, string aspxPage)
        {
            bool hasPermission = false;

            List<Permission> persList = this.GetPermissions(site, subuser_id.ToString());
            
            foreach(Permission perm in persList)
            {
                if (perm.ASPXPageName == aspxPage)
                {
                    hasPermission = true;
                    break;
                }
            }


            return hasPermission;

        }


        public void EditSubuserInfo_stock(DBSite site, SubuserEntity subuser, string subuser_id)
        {
            string qry = " UPDATE tblSubuser SET "
                      + "SubuserName = '" + subuser.SubuserName + "'"
                      + ", SubuserCreationDate = '" + subuser.CreationDate + "'"
                      + ", SubuserPassword = '" + subuser.Password + "'"
                      + ", EmailId = '" + subuser.EmailId + "'"
                      + ", Designation = '" + subuser.Designation + "'"
                      + ", Address = '" + subuser.Address + "'"
                      + ", City = '" + subuser.City + "'"
                      + ", Mobile = '" + subuser.Mobile + "'"
                      + ", Active = " + subuser.UserState;
            qry += " WHERE SubuserId=" + subuser_id;

            site.Execute(qry);  //  ----  update subuser information ---------------


            qry = " DELETE FROM tblUserPermissions"
               + " WHERE UserId=" + Util_BLL.User.UserId
               + " AND SubuserId=" + subuser_id;

            site.Execute(qry); //------   Delete all permissions to selected Subuser ------ 


            //-----  update permissions to Subuser --------------------

            foreach (Permission permission in subuser.Permissions)
            {

                qry = " INSERT INTO tblUserPermissions"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", PermissionId"
                    + ")"
                    + " VALUES("
                    + Util_BLL.User.UserId   //------ user id of Current User --------
                    + ", " + subuser_id
                    + ", " + permission.PermissionId
                    + ")";

                site.Execute(qry);
            }





        }


        //---- Edit subuser information -----------------------------

        public void EditSubuserInfo(DBSite site, SubuserEntity subuser, string subuser_id)
        {
            string qry = " UPDATE tblSubuser SET "
                      + "SubuserName = '" + subuser.SubuserName + "'"
                      + ", SubuserCreationDate = '" + subuser.CreationDate + "'"
                      + ", SubuserPassword = '" + subuser.Password + "'"
                      + ", EmailId = '" + subuser.EmailId + "'"
                      + ", Designation = '" + subuser.Designation + "'"
                      + ", Address = '" + subuser.Address + "'"
                      + ", City = '" + subuser.City + "'"
                      + ", Mobile = '" + subuser.Mobile + "'"
                      + ", Active = " + subuser.UserState;
            qry += " WHERE SubuserId=" + subuser_id;

            site.Execute(qry);  //  ----  update subuser information ---------------



            qry = " DELETE FROM tblUserPermissions"
                + " WHERE UserId=" + Util_BLL.User.UserId
                + " AND SubuserId=" + subuser_id;

            site.Execute(qry); //------   Delete all permissions to selected Subuser ------ 
            
            
            //-----  update permissions to Subuser --------------------

            foreach (Permission permission in subuser.Permissions)
            {

                qry = " INSERT INTO tblUserPermissions"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", PermissionId"
                    + ")"
                    + " VALUES("
                    + Util_BLL.User.UserId   //------ user id of Current User --------
                    + ", " + subuser_id
                    + ", " + permission.PermissionId
                    + ")";

                site.Execute(qry);
            }




            qry = " DELETE FROM tblTeacherClasses"
                + " WHERE UserId=" + Util_BLL.User.UserId
                + " AND SubuserId=" + subuser_id;

            site.Execute(qry); 


            foreach (ClassMasterBLL.ClassMasterEntity  clas in subuser.TeacherClasses )
            {

                qry = " INSERT INTO tblTeacherClasses"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", ClassId"
                    + ")"
                    + " VALUES("
                    + Util_BLL.User.UserId   //------ user id of Current User --------
                    + ", " + subuser_id
                    + ", " + clas.ClassMasterId 
                    + ")";

                site.Execute(qry);
            }


            qry = " DELETE FROM tblTeacherSubjects"
                + " WHERE UserId=" + Util_BLL.User.UserId
                + " AND SubuserId=" + subuser_id;

            site.Execute(qry);


            foreach (SubjectMasterBLL.SubjectMasterEntity  sub in subuser.TeacherSubjects )
            {

                qry = " INSERT INTO tblTeacherSubjects"
                    + "("
                    + "UserId"
                    + ", SubuserId"
                    + ", SubjectID"
                    + ")"
                    + " VALUES("
                    + Util_BLL.User.UserId   //------ user id of Current User --------
                    + ", " + subuser_id
                    + ", " + sub.SubjectMasterId 
                    + ")";

                site.Execute(qry);
            }



        }


        public void DeleteSubuserInfo(DBSite site, string subuser_ids)
        {
            
            string delete_subuser_qry = " DELETE FROM tblSubuser "
                  + " WHERE UserId=" + Util_BLL.User.UserId
                  + " AND SubuserId IN (" + subuser_ids + " )";

            site.Execute(delete_subuser_qry);               //-------------   Delete Subuser Information ---------------

            string[] subuserIds = subuser_ids.Split(',');  //---------  Get Array Of SubuserId's -----------
            string delete_associated_permission_qry;

            //-------------   Delete Associated Permissions Of Subuser -----------------------------------

            foreach (string subuser_id in subuserIds)
            {
                delete_associated_permission_qry = " DELETE FROM tblUserPermissions "
                                                         + " WHERE UserId=" +Util_BLL.User.UserId
                                                         + " AND SubuserId=" + subuser_id;
                site.Execute(delete_associated_permission_qry);
            }

            foreach (string subuser_id in subuserIds)
            {
                delete_associated_permission_qry = " DELETE FROM tblTeacherClasses "
                                                         + " WHERE UserId=" + Util_BLL.User.UserId
                                                         + " AND SubuserId=" + subuser_id;
                site.Execute(delete_associated_permission_qry);
            }

            foreach (string subuser_id in subuserIds)
            {
                delete_associated_permission_qry = " DELETE FROM tblTeacherSubjects "
                                                         + " WHERE UserId=" + Util_BLL.User.UserId
                                                         + " AND SubuserId=" + subuser_id;
                site.Execute(delete_associated_permission_qry);
            }


        }

    }

    public class Permission
    {
        public int PermissionId { get; set; }
        public string ModuleName { get; set; }
        public string  ASPXPageName { get; set; }
    }

}