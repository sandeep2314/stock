using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;




namespace AccountingSoftware.BLL
{
    public class Util_BLL
    {

        // 0 - iCollegeManager
        // 1 --  Stock In Hand
        // 2 -- Sunny

        //public static int IsStock = 0   ;
        
        public static int IsStock = 0 ;

        public static string myCompanyName = "Mahi Retails";
        

        public static string myCompanyContact = "Mobile: +91 921 948 4030, Email: CollegeManager@gmail.com";
        public static string myCompanyWebsite = "www.AuthorisedDealer.com";

         public static string myProductName = "iCollegeManager";
        public static string myPunchLine = "Online School & College Management Software";

        public static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        public  static  string MIN_DATE = "01/01/2001";
        public  static string MAX_DATE = DateTime.Now.ToString("d");

        public static string auth_href = "http://www.AuthorisedDealer.com";
        public static string auth_support = "For Support: Mob. 921 948 4030 Email: AuthDealer@gmail.com";

        public static string NO_PERMISSION = "You do not have permission for this page. Please ask your Admin !";


        public static UserBLL.User User 
        {
            get 
            {
                return ((UserBLL.User) System.Web.HttpContext.Current.Session["User"]);
            }
            set
            {
              System.Web.HttpContext.Current.Session["User"] = value;
            }

        }
        
        
        public static SubuserBLL.SubuserEntity  SubUser 
                {
                    get
                    {
                        return ((SubuserBLL.SubuserEntity)System.Web.HttpContext.Current.Session["SubUser"]);
                    }

                    set
                    {
                        System.Web.HttpContext.Current.Session["SubUser"] = value;
                    }

                }

        //public UserBLL.User User2 { get; set; }
        //public SubuserBLL.SubuserEntity SubUser2 { get; set; }

        




        //---------  Get Permissions ------------------------

        public List<Permission> GetPermissions(DBSite site)
        {
            List<Permission> permission_list = new List<Permission>();
            Permission permission=null;

            string qry = " SELECT "
                      + " Module"
                      + ", PermissionId"                   
                      + " FROM tblPermission";                    

            DataTable dt = site.ExecuteSelect(qry);
            foreach (DataRow row in dt.Rows)
            {
                permission=new Permission();


                 permission.ModuleName= CheckNull(row["Module"]);
                permission.PermissionId = CheckNullInt(row["PermissionId"]);

                permission_list.Add(permission);
            }
            return permission_list;
        }

        public List<Permission> GetSubuserPermissions(DBSite site)
        {
            List<Permission> permission_list = new List<Permission>();
            Permission permission = null;

            string qry = " SELECT DISTINCT"
                      + "  p.Module"
                      + ", p.PermissionId"
                      + " FROM tblUserPermissions AS up"
                      +" LEFT OUTER JOIN tblPermission AS p"
                      +" ON up.PermissionId = p.PermissionId"
                      +" WHERE UserId="+ User.UserId;                     


            DataTable dt = site.ExecuteSelect(qry);
            foreach (DataRow row in dt.Rows)
            {
                permission = new Permission();


                permission.ModuleName = CheckNull(row["Module"]);
                permission.PermissionId = CheckNullInt(row["PermissionId"]);

                permission_list.Add(permission);
            }
            return permission_list;
        }


        //public Dictionary<string, string> GetPermissions(DBSite site)
        //{
        //    Dictionary<string, string> permissions = new Dictionary<string, string>();

        //    string qry = " SELECT "
        //              + " Module"
        //              + ", PermissionId"
        //              + " FROM tblPermission";

        //    DataTable dt = site.ExecuteSelect(qry);

        //    string key;
        //    string value;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        key = CheckNull(row["Module"]);
        //        value = CheckNull(row["PermissionId"]);

        //        permissions.Add(key, value);
        //    }
        //    return permissions;
        //}




        //public Dictionary<string, string> GetPermissions(DBSite site)
        //{
        //    Dictionary<string, string> permissions = new Dictionary<string, string>();

        //    string qry = " SELECT "
        //              + " Module"
        //              + ", Module + ' | ' + CONVERT(VARCHAR,PermissionId) AS PermissionId"
        //              + " FROM tblPermission";

        //    DataTable dt = site.ExecuteSelect(qry);

        //    string key;
        //    string value;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        key = CheckNull(row["Module"]);
        //        value = CheckNull(row["PermissionId"]);

        //        permissions.Add(key, value);
        //    }

        //    return permissions;
        //}


        public string SendMail(string toList, string from, string ccList,  string subject, string body)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(from);
                message.From = fromAddress;
                message.To.Add(toList);
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                // We use gmail as our smtp client
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
               
                smtpClient.EnableSsl = true;
               // smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("StockInHand.Team@gmail.com", "parulmywife");

                smtpClient.Send(message);
                msg = "Successful<BR>";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public string GetHomeSetting(DBSite site, string colName)
        {

            string colValue = "";
            
            string checkQry = " SELECT top 1 ColValue FROM tblHome ";
            checkQry += " WHERE ColName = '" + colName + "'";

            DataTable dt = site.ExecuteSelect(checkQry);

            foreach (DataRow dr in dt.Rows)
            {
                colValue = CheckNull( dr["colValue"]);
            }

            return colValue;
        }



        public int GetAutoNumber(DBSite site, string colName)
        {

            int autoNumber = 1;

            string insertQry = " ";
            insertQry = " INSERT INTO tblUtil(ColName, ColValue, UserId, Fyear) ";
            insertQry += " VALUES( '" + colName + "', 1000, " + User.UserId + ", " + User.fYear + " )  ";

            string updateQry = " UPDATE tblUtil SET ColValue = colValue +1 " ;  //WHERE ColName = '" + colName + "'";
            updateQry += GetUserWhereCondition(User);
            updateQry +=" AND ColName = '" + colName + "'";



            string checkQry = " SELECT count(*) cnt FROM tblUtil "; //WHERE ColName = '" + colName + "'";
            checkQry += GetUserWhereCondition(User);
            checkQry += " AND ColName = '" + colName + "'";


            DataTable dt = site.ExecuteSelect(checkQry);


            foreach (DataRow dr in dt.Rows)
            {
                if (CheckNullInt(dr["cnt"]) == 1)
                {
                    site.Execute(updateQry);
                    string qry2 = " SELECT colValue FROM tblUtil "
                    + GetUserWhereCondition(User)
                    + " AND ColName = '" + colName + "'";

                    DataTable dt2 = site.ExecuteSelect(qry2);
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        autoNumber = CheckNullInt( dr2["colValue"]);
                    }
            

                }
                else
                {
                    site.Execute(insertQry);
                }
            }

            return autoNumber;
        }




        public string GetLOVName(string lovValue)
        {
            return lovValue.Split('|')[0];
        }

    
        public string GetLOVId(string lovValue)
        {
            return lovValue.Split('|')[1];
        }


        //--------- Where Condition -----------------------

        public static string GetUserWhereCondition(UserBLL.User usr)
        {
            return " WHERE 1=1 AND UserID = " + usr.UserId + " AND FYear =  " + usr.fYear;
        }

        public static string GetUserWhereCondition(UserBLL.User usr, string tbl)
        {

            return " WHERE 1=1 AND " + tbl + ".UserID = " + usr.UserId + " AND " + tbl + ".FYear =  " + usr.fYear;
        }

        public string GetUserInsertQry(UserBLL.User usr)
        {
            string insertValuesQry = "";
            insertValuesQry = Util_BLL.User.UserId  + ", " + Util_BLL.SubUser.SubuserId   + ", " + usr.fYear;
            return insertValuesQry;
        }


        public string GetUserUpdateQry(UserBLL.User usr)
        {
            string udateQry = "";
            udateQry = " UserId = " + Util_BLL.User.UserId + ", SubuserId=" + Util_BLL.SubUser.SubuserId + ", FYear = " + usr.fYear;
            return udateQry;
        }

        public bool isSubUserAdmin(DBSite site)
        {
            bool isAdmin = false;

            string qry = "SELECT IsAdmin FROM tblSubUser WHERE subUserID=" + Util_BLL.SubUser.SubuserId ;

            DataTable dt = site.ExecuteSelect(qry);
            DataRow row = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];
                isAdmin = this.CheckNullInt(row["IsAdmin"])==1;
            }

            return isAdmin;
        }

        public string GetCurrentPageName(HttpRequest  req)
        {
            string sPath = req.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }


        public string GetUserInsertQryMaster(UserBLL.User usr)
        {
            string insertValuesQry = "";
            insertValuesQry = usr.UserId + ", "  + usr.fYear;
            return insertValuesQry;
        }

        //public static string GetUserWhereCondition(Util_BLL.User usr)
        //{
        //    return " WHERE 1=1 AND UserID = " + UserBLL.User.userID + " AND FYear =  " + UserBLL.User.fYear;
        //}


        //public string GetUserInsertQry()
        //{
        //    return UserBLL.User.userID + ", " + UserBLL.User.fYear;
        //}


        public static string GetJSONString(DataTable Dt) 
        { 
            string[] StrDc = new string[Dt.Columns.Count]; 
            string HeadStr = string.Empty; 
            for (int i = 0; i < Dt.Columns.Count; i++) 
            { 
                StrDc[i] = Dt.Columns[i].Caption; 
                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\","; 
            } 
            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1); 
            StringBuilder Sb = new StringBuilder(); 
            Sb.Append( Dt.TableName + "["); 
            for (int i = 0; i < Dt.Rows.Count; i++) 
            { 
                string TempStr = HeadStr; 
                Sb.Append("{"); 
                for (int j = 0; j < Dt.Columns.Count; j++) 
                { 
                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString()); 
                 } 
                Sb.Append(TempStr + "},"); 
            } Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1)); 
            Sb.Append("]"); 
            return Sb.ToString(); 
        } 


        
        public void HandleExpception(string msg, HttpResponse res)
        {
            res.Redirect("ErrorPage.aspx?msg="+msg);
        }

        public void HandleExpception(Exception ex, HttpResponse res)
        {
            //throw ex;
            res.Redirect("ErrorPage.aspx?msg=" + ex.Message );
        }
       

        public string CheckNull(object obj)
        {   
            if (obj == DBNull.Value)
                return "";
            return Convert.ToString(obj);
        }


        public string IfDateThenFormat(string dtStr)
        {
            string strDt = "";
            strDt = CheckNullDate(dtStr);
            if (strDt == "")
                strDt = dtStr;
            return strDt;
        }

        
        public string CheckNullDate(object obj)
        {
            string dateStr = "";
            DateTime dt;
            if (obj != DBNull.Value)
            {
                dateStr = Convert.ToString(obj);
                if (DateTime.TryParse(dateStr, out dt))
                    dateStr = string.Format("{0:d/MMM/yyyy}", dt);
            }
            return dateStr;       
        }


        public string CheckNullDate(object obj, string theFormat)
        {
            string dateStr = "";
            DateTime dt;
            if (obj != DBNull.Value)
            {
                dateStr = Convert.ToString(obj);
                if (DateTime.TryParse(dateStr, out dt))
                    dateStr = string.Format("{0:" + theFormat + "}", dt);
            }
            return dateStr;
        }

        public int CheckNullInt(object obj)
        {
            int theNum = 0;
            
            if (obj != DBNull.Value &&  !object.ReferenceEquals(obj, "") && (isNumeric(obj.ToString()) || (bool)obj == true ) )
            {
                theNum = Convert.ToInt32(obj);
            }
            return theNum;
        }

        public Double CheckNullDouble(object obj)
        {
            Double  theNum = 0;
            if (obj != DBNull.Value && !object.ReferenceEquals(obj, "") && obj.ToString() != "&nbsp;" && (isDouble(obj.ToString()) || (bool)obj == true))
            {
                theNum = Convert.ToDouble(obj);
            }
            return theNum;
        }


        // ---  Decimal handler -----

        public Decimal CheckNullDecimal(object obj)
        {
            Decimal theNum = 0;

            if (obj != DBNull.Value && obj.ToString().Trim() != "")
            {
                //if((isDouble(obj.ToString()) || (bool)obj == true))
                if ((isDecimal(obj.ToString())))
                    theNum = ToDecimal( Convert.ToDecimal(obj));
                
            }
            return theNum;
        }



        public string CheckSpace(string str)
        {
            return str.Replace("&nbsp;", "");
        }


        public string GetMonthName(int mnth)
        {
            string strMonth = "";
            DateTime theDate = new DateTime(1900, mnth, 1);  
            strMonth  =  theDate.ToString("MMM");  
            return strMonth; 
        }
               

        public string GetYear(DateTime theDate)
        {
            string theYear = "1900";
            theYear = theDate.ToString("YYYY");
            return theYear;
        }

        public bool isDate(int dd, int mnth, int year)
        {
            bool isValid = true;
            string dt = "";
            try
            {
                dt = GetDateForDB(dd, mnth, year);
            }
            catch(Exception)
            {
                isValid = false;
            }
            if (dt == "")
                isValid = false;
            return isValid;
        }


        public bool isDateRangeValid(string fromDate, string toDate)
        {
            if (fromDate.Length >0 && toDate.Length > 0)
            {
            DateTime fDate = DateTime.Parse(fromDate);
            DateTime tDate = DateTime.Parse(toDate);
            return fDate < tDate;
            }
            else
                return true;
            
        }


        public string GetDateForDB(int dd, int mnth, int year)
        {
          string theDate = "";
            
           if(year >= 1900)
               if(mnth > 0)
                   if(dd > 0)
                       {
                        DateTime theDte = new DateTime(year, mnth, dd);
                        theDate = CheckNullDate(theDte, "yyyy/M/d");  //short date time
                       }
            
            return theDate;
        }

        public string GetDateForDisplay(string dt, string theFormat)
        {
            string theDate = CheckNullDate(dt, theFormat);
            if (theDate.Length > 0)
                theDate = Convert.ToInt32(theDate).ToString();
            else
                theDate = "0";
            return theDate;
        }


        public bool isNumeric(string str)
        {
            bool isNumber = false;
            int  num = 0;
            isNumber = int.TryParse(str, out num);
            return isNumber;
        }


        public bool isDouble(string str)
        {
            bool isNumber = false;
            double num = 0;
            isNumber = System.Double.TryParse(str, out num);
            return isNumber;
        }


        public bool isDecimal(string str)
        {
            bool isNumber = false;
            decimal  num = 0;
            isNumber = System.Decimal.TryParse(str, out num);
            return isNumber;
        }


        public int GetIntForDB(string str)
        {
            int num = 0;
            if (str.Length > 0 && isNumeric(str))
                num = Convert.ToInt32(str);
            return num;
        }

        public string ConvertListToCommaSeparatedString(List<string> theStrList)
        {
            string theStrs = "";
            theStrs = String.Join(",", theStrList.ToArray());
            return theStrs;
        }


        public string ConvertListToCommaSeparatedString(List<int> theIntList)
        {
            string theStrs = "";
            StringBuilder builder = new StringBuilder();
            foreach(int i in theIntList)
            {
                builder.Append(",").Append(i); 
            }
            theStrs = builder.ToString();
            if (theStrs.Length > 1)
                theStrs = theStrs.Substring(1);
            return theStrs;
        }


        

        public string AddSingeQuotes(string str)
        {
            string quotedStr = "";
            quotedStr = str.Replace(",", "','");
            quotedStr = "'" + quotedStr  + "'";
            return quotedStr;
        }

        public string GetDobFromAge(string theAge)
        {
            string theDob = "";
            if (isNumeric(theAge))
            {
                int age = Convert.ToInt32(theAge);
                int ageDays = age * 365;

                int leapDays = 0;
                leapDays =  age / 4;
                ageDays = ageDays + leapDays;

                DateTime today = DateTime.Now;
                TimeSpan ts = new TimeSpan(ageDays, 0, 0, 0, 0);
                theDob = CheckNullDate(today.Subtract(ts), "yyyy/M/d");
            }
            return theDob;

        }



        public bool isApprovePermission(int userID)
        {
             
            return false ;
        }




        public static class Modules
        {
            public static string[] modules = { "Assesment", "Training" };
            public static int[] moduleValues = {1, 2};

            public static int ASSESMENT = 1;
            public static int TRAINING = 2;
        }

        public static class UserPermissions
        {
            public static string[] permissions = {"Insert", "Update", "Delete", "View", "Approve"};
            public static int[] permissionValues = {0, 1, 2, 3, 4};

            public static int INSERT = 0;
            public static int UPDATE = 1;
            public static int DELETE = 2;
            public static int VIEW = 3;
            public static int APPROVE = 4;
        }

        public static class UserRole
        {
            public static string[] roles = {"Admin", "Data Entry Operator", "Management User", "Director" };
            public static int[] roleValues = {1, 2, 3, 4};

            public static int ADMIN = 1;
            public static int DATA_ENTRY_OPERATOR = 2;
            public static int MANAGEMENT_USER = 3;
            public static int DIRECTOR = 4;

        }

        public static class UserState
        {
            public static string[] states = { "Active", "InActive" };
            public static int[] stateValues = {0, 1};

            public static int ACTIVE = 0;
            public static int INACTIVE = 1;

        }


        public static class TrainingStatus
        {
            public static string[] statuses = {"To Be Conducted", "Conducted"};
            public static int[] statusValues = {0, 1};

            public static int TOBECONDUCTED = 0;
            public static int CONDUCTED = 1;

        }


        public static class ActionType
        {
            public static string[] actionTypes = {"", "INSERT", "UPDATE", "DELETE", "APPROVE", "REJECT" };
            public static int[] actionValues = {0, 1, 2, 3, 4, 5};

            public static int INSERT = 1;
            public static int UPDATE = 2;
            public static int DELETE = 3;
            public static int APPROVE = 4;
            public static int REJECT = 5;

        }


        public static class Category
        {
            public static string[] categories = { "Select Category", "General", "SC", "ST", "OB"};
            public static int[] categoryValues = { 0, 1, 2, 3, 4};

            public static int NONE = 0;
            public static int GENERAL = 1;
            public static int SC = 2;
            public static int ST = 3;
            public static int OB = 4;

        }



        public decimal ToDecimal(decimal value)
        {
            //return decimal.Round(value, 2, MidpointRounding.AwayFromZero); 
            return decimal.Round(value, 2); 
        }

        public bool IsInvalidAmount(string amount)
        {
            
            if (amount == "")
                return true;
            else
            {
                decimal result = 0;
                return !Decimal.TryParse(amount, out result);

            }
        }



        public bool IsValidEmail(string email)
        {

            string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

              if (email != null) 
                return Regex.IsMatch(email, MatchEmailPattern);
              else 
                 return false;
        
        }



        public string ValidateField(DBSite site,string account_type,string value)
        {
            AccountType ac=new AccountType();

            string qry = "SELECT " + AccountType.GetColumnName(ac, account_type) ;
            qry += " + '|'+ CONVERT( VARCHAR, " + AccountType.GetTypeIdName(ac, account_type);
            qry += " ) Field_Name FROM " + AccountType.GetTableName(ac,account_type) + " WHERE " + AccountType.GetColumnName(ac, account_type) + "='" + value + "'";

            DataTable dt = site.ExecuteSelect(qry);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                return CheckNull(dr["Field_Name"]);
            }
            return "";
        }



        //public string ValidateGroup(DBSite site, string name)
        //{
        //    string qry = "SELECT GroupName+'|'+ CONVERT( VARCHAR, GroupId ) GroupName FROM tblGroup WHERE GroupName='" + name + "'";
        //    DataTable dt = site.ExecuteSelect(qry);

        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];

        //        return CheckNull(dr["GroupName"]);
        //    }
        //    return "";
        //}



        public bool IsValidMobileNumber(string Mobile_number)
        {
            return true ;
        }


        public bool IsValidPhoneNumber(string phone_number)
        {

             return true ;
        }


        public bool IsValidName(string name)
        {

            if (name == "")
                return false;
            else
            {
                string MatchAccountPattern = @"^([^(^)^'^,^%^$^.^|^*^&^\]^\[]*)$";
                if (name != null)
                    return Regex.IsMatch(name, MatchAccountPattern);
                else
                    return false;
            }
        }


        public bool IsNumber(string value)
        {
            decimal result = 0;
            return Decimal.TryParse(value, out result);
        }



        public double GetDiscountAmount(string discount, double total_amount)
        {
            double discount_amount = 0;
            double discount_percent = 0;

            if (DiscountInPercent(discount))
            {

                discount_percent = CheckNullDouble(discount.Split('%')[0]);
                discount_amount = total_amount * discount_percent / 100;
            }
            else
            {
                discount_amount = CheckNullDouble(discount);
            }

            return discount_amount;
        }


        protected bool DiscountInPercent(string discount)
        {
            bool amount_in_percent = false;

            if (discount.EndsWith("%"))
            {
                amount_in_percent = true;
            }

            return amount_in_percent;
        }

        public string GetLedgerId(string value)
        {
             
            return value.Split('-')[0];  
        }

        public string GetLedgerNumber(string value)
        {
            return value.Split('-')[1];
        }


        public bool ValidField(DBSite site, string tableName_columnName, string field_value)
        {
            string table_name = GetLOVName(tableName_columnName);
            string column_name = GetLOVId(tableName_columnName);

            field_value = GetLOVName(field_value);

            string qry = " SELECT "
                      + column_name
                      + " FROM " + table_name
                      + " WHERE UserId=" + User.UserId                    
                      + " AND " + column_name + "='" + field_value+"'";

            return site.ExecuteSelect(qry).Rows.Count > 0;
        }


        public bool IsDate(String date)
        {

            bool valid_date = true;
            try
            {
                Convert.ToDateTime(date);
            }
            catch (Exception ex)
            {
                valid_date = false;
            }
            return valid_date;
        }

        public DataRow GridViewRowToDataRow(GridViewRow gvr)
        {
            object di = null;
            DataRowView drv = null;
            DataRow dr = null;
            if (gvr != null)
            {
                di = gvr.DataItem as System.Object;
                if (di != null)
                {
                    drv = di as System.Data.DataRowView;
                    if (drv != null)
                    {
                        dr = drv.Row as System.Data.DataRow;
                    }
                }
            }
            return dr;
        }


    public List<string> GetMsgInParts(string str, bool isUnicode)
      {
        List<string> str_parts = new List<string>();

        int maxLength = 155;
        if (isUnicode)
            maxLength = 60;

        for (int index = 0; index < str.Length; index += maxLength)
        {
            str_parts.Add(str.Substring(index, Math.Min(maxLength, str.Length - index)));
        }
        return str_parts;
     }

        
   public int SaveSentSMSToDB(DBSite site, string mobileNos, string msg, bool isUnicode, UserBLL.User usr)
    {
       
       /// 777 
       
       string mobileNoStr = "0";
       string qry = "";

       int count = 0;

       string[] arr =  mobileNos.Split(',');

       int msg_count = 0;

       for (int i = 0; i < arr.Length; i++)
       {

           mobileNoStr = arr[i].Trim();

           if (mobileNoStr.Length > 9)
           {
               /// break sms message in into tokens of 157 words

                List<string> strparts = GetMsgInParts(msg, isUnicode);
                msg_count = strparts.Count;

                qry = "INSERT INTO tblSMSSent(SMSText, MsgCount, mobileNo, UserID)"
                   + " VALUES ("
                   + "N'" + msg + "'"
                   + ", " + msg_count + ""
                  + ", '" + mobileNoStr + "'"
                   + ", " + usr.UserId
                   + ")";

                   site.Execute(qry);
                   count += 1;
             
               
                // if message is long then send in parts
                //foreach (string msg_str in strparts)
                //{

                //    qry = "INSERT INTO tblSMSSent(SMSText, mobileNo, UserID)"
                //    + " VALUES ("
                //    + "N'" + msg_str + "'"
                //    + ", '" + mobileNoStr + "'"
                //    + ", " + usr.UserId
                //    + ")";

                //    site.Execute(qry);
                //    count += 1;
                //}
           }
      
       }

       return count;
      
    }



   public List<int> GetMobileListForDuplicateSMS(DBSite site, string mobileNos, string msg, UserBLL.User usr)
   {
             
       // if msg is same in last 8 hours then stop the sms of same mobileNos

       List<int> duplicate_sms_mob_list = new List<int>();


       List<int> mob_List = new List<int>();
       mob_List = mobileNos.Split(',').Select(int.Parse).ToList();


       string qry = "SELECT MobileNo, SMSText FROM tblSMSSent  "
                    + " WHERE userId=" + usr.UserId
                    + " AND CreatedAT >  dateadd(hh,-8,getdate()) ";
        
       DataTable dt = site.ExecuteSelect(qry);
       DataRow row = null;

       foreach (int mob in mob_List)
       {

           for (int j = 0; j < dt.Rows.Count; j++)
           {
               row = dt.Rows[j];

                if(mob == CheckNullInt(row["MobileNo"]))
                {
                   
                    // sms is same
                    if (msg == CheckNull(row["SMSText"]))
                    {
                        duplicate_sms_mob_list.Add(mob);  
                    }
                }
           }

       }
       //9999

       return duplicate_sms_mob_list;
            
   }

   public bool ContainsUnicodeCharacter(string input)
   {
       const int MaxAnsiCode = 255;

       return input.Any(c => c > MaxAnsiCode);
   }


    public string SendSms222(DBSite site, string mobileNos, string msg, UserBLL.User usr, HttpResponse res, bool isUnicode)
    
    {
        //smsIndiaHub
        string sUser = "sandeep99";
        string spwd = "pantnagar";
               
        string sNumber = mobileNos;
        
        string sMessage = msg;
        string sSenderID = usr.SenderId ;
        string MessageData = msg;

        //string sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=" + sUser + "&password=" + spwd + "&msisdn=" + sNumber + "&sid=" + sSenderID + "&msg=" + msg + "&fl=0&gwid=2";
        // unicode
        // http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=yourUserID&password=yourPassword&msisdn=919898xxxxxx&sid=SenderId&msg=परीक्षण संदेश &fl=0&dc=8&gwid=2
        
        string sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=" + sUser + "&password=" + spwd + "&msisdn=" + sNumber + "&sid=" + sSenderID + "&msg=" + msg + "&fl=0&dc=8&gwid=2";


        
        string sResponse = GetResponse(sURL);
        //res.Write(sResponse);
        
        return sResponse;

    }


    public string SMSDelivery(string mobileNos, UserBLL.User usr)
    {
        // SMS delivery percent as per DB

        string toDeliverMobileNos = "";

        Random r = new Random();

        List<int> mob_List = new List<int>();
        mob_List = mobileNos.Split(',').Select(int.Parse).ToList();

        List<int> mob_List2 = new List<int>();

       // mob_List2 = r.Next(mob_List2, );


        return toDeliverMobileNos;


    }


  

    public string SendSms(DBSite site, string mobileNos, string msg, UserBLL.User usr, HttpResponse res, bool isUnicode)
    {
        //smsIndiaHub
        //string sUser = "sandeep99";
        //string spwd = "pantnagar";


        // get user and password from db

        string sUser = usr.SMSUser ;
        string spwd = User.SMSPassword ;


        //string spwd = "Pantnagar@123";
        string sNumber = mobileNos;
        string sMessage = msg;
        string sSenderID = usr.SenderId;

        string MessageData = msg;

        string sURL = "";

        isUnicode=ContainsUnicodeCharacter(msg);
        

        if (isUnicode)
        {
            //sURL = "http://trans.smsleads.in/sendsms.jsp?user=" + sUser + "&password=" + spwd + "&mobiles=" + sNumber + "&sms=" + msg + "&senderid=" + sSenderID + "&verion=3&unicode=1";
            
            //sURL = "http://websms.itfisms.com/vendorsms/pushsms.aspx?user=" + sUser + "&password=" + spwd + "&msisdn=" + sNumber + "&sid=" + sSenderID + "&msg=" + msg + "&fl=0&dc=8&gwid=2";

            
                sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=" + sUser + "&password=" + spwd + "&msisdn=" + sNumber + "&sid=" + sSenderID + "&msg=" + msg + "&fl=0&dc=8&gwid=2";
                //Gujrat
            //     sURL = "http://sms.hspsms.com/sendSMS?username=" + sUser + "&password=2314&message=" + sMessage + "&sendername=" + sSenderID + "&smstype=TRANS&numbers=" + sNumber + "&apikey=d6a74a66-e6b8-430a-9121-52aa758c34fa";

            

             //sURL = "http://login.blesssms.com/api/mt/SendSMS?"


            //sURL = "http://184.171.171.74/api/mt/SendSMS?"
            //+ "user=Sandeep99"
            //+ "&password=pantnagar"
            // + "&senderid=" + sSenderID
            // + "&channel=Trans"
            // + "&DCS=8"
            // + "&flashsms=0"

            //+ "&text=" + msg
            // + "&route=4"
            //+ "&number=" + mobileNos;

            

           
        }
        else
        {
            //sURL = "http://trans.smsleads.in/sendsms.jsp?user=" + sUser + "&password=" + spwd + "&mobiles=" + sNumber + "&sms=" + msg + "&senderid=" + sSenderID + "&verion=3&unicode=0";
            //sURL = "http://websms.itfisms.com/vendorsms/pushsms.aspx?user=" + sUser + "&password=" + spwd + "&msisdn=" + sNumber + "&sid=" + sSenderID + "&msg=" + msg + "&fl=0&gwid=2";

            //if (!(sSenderID == "FifthC" || sSenderID == "FSSPSC"))
            //{

                sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=" + sUser + "&password=" + spwd + "&msisdn=" + sNumber + "&sid=" + sSenderID + "&msg=" + msg + "&fl=0&gwid=2";
            //}
            //else
            //{
                //Gujrat
                 //sURL = "http://sms.hspsms.com/sendSMS?username="+sUser+"&password=2314&message="+sMessage+"&sendername="+sSenderID+"&smstype=TRANS&numbers="+sNumber+"&apikey=d6a74a66-e6b8-430a-9121-52aa758c34fa";

            //}


            //sURL = "http://184.171.171.74/api/mt/SendSMS?"
            //+ "user=Sandeep99"
            //+ "&password=pantnagar"
            // + "&senderid=" + sSenderID
            // + "&channel=Trans"
            // + "&DCS=0"
            // + "&flashsms=0"

            //+ "&text=" + msg

            //+ "&route=4"
            //+ "&number=" + mobileNos;


          //  sURL = "http://sms.hspsms.com/sendSMS?username=admin&message=" + sMessage + "&sendername=INFORM&smstype=TRANS&numbers=9219484030&apikey=d0002689-c347-4b82-af90-9d3188ad6c6e";
            
         

        }
        



        string sResponse = GetResponse(sURL);
        //res.Write(sResponse);

        return sResponse;

    }




    public string SMSResult(string smsResponse)
    {
        string smsResult = "";
        
        
        JObject parseTree = JObject.Parse(smsResponse);

        foreach (var prop in parseTree.Properties())
        {
            //Console.WriteLine(prop.Name + ": " + prop.Value.ToObject<object>());
            smsResult = smsResult + " \n  " + prop.Name + ": " + prop.Value.ToObject<object>();
            
        }

        return smsResult;

    }

    public int GetSMSBalance2(DBSite site, int userId)
    {
        return -99;
    }


    public int GetSMSBalance(DBSite site, int userId)
    {
        // 7777

        int smsBalance = 0;

        string qry = "";

        qry = "SELECT SMSBalance From tblUserInfo "
            + " WHERE USerInfoID = " + userId;

        DataTable dt = site.ExecuteSelect(qry);
        int smsUserBalance = 0;

        foreach (DataRow row in dt.Rows)
        {

            smsUserBalance = CheckNullInt(row["SMSBalance"]);
        }
        if (smsUserBalance == 0)
        {
            smsBalance = -99;
            return smsBalance;
        }


        qry = "SELECT t1.userID, purchased, IsNull(smsSent, 0) SMSSent "
            + " , T1.Purchased - IsNull(smsSent, 0) as BalanceOnHand "
            + " FROM "
            + "( "
            + " select userID, SUM(SmsPurchasedCount) Purchased "
            + " FROM tblSMSPurchased "
            + " GROUP BY UserID "
            + " ) T1  "
            + " LEFT OUTER JOIN  "
            + " ( "
            + " select userID, SUM(msgCount) SMSSent "
            + " FROM tblSMSSent "
            + " GROUP BY UserID "
            + " ) T2 ON t2.userId = t1.UserID "
            + " WHERE t1.USerID = " + userId;


           dt = site.ExecuteSelect(qry);
           foreach (DataRow row in dt.Rows)
            {

             smsBalance= CheckNullInt(row["BalanceOnHand"]);
            }


        return smsBalance;

    }

    public static string GetResponse(string sURL)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
        request.MaximumAutomaticRedirections = 4;
        request.Credentials = CredentialCache.DefaultCredentials;
        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream( );
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string sResponse = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            return sResponse;
        }
        catch
        {
            return "";
        }
    }


      
        public string SendSms2(string mobileNos, string msg, UserBLL.User usr, HttpResponse res)
        {

            WebClient client = new WebClient();
            // Add a user agent header in case the requested URI contains a  query.

            client.Headers.Add("user-agent", "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
           

            client.QueryString.Add("username", "sandeep99");
            client.QueryString.Add("password", "pantnagar");
            client.QueryString.Add("sender", usr.SenderId  );
            


            //client.QueryString.Add("user", "sandeep99");
            //client.QueryString.Add("pwd", "pantnagar");
            //client.QueryString.Add("sid", usr.SenderId);
            
            client.QueryString.Add("to", mobileNos);
            client.QueryString.Add("message", msg);
            client.QueryString.Add("format", "text");

            //client.QueryString.Add("to", mobileNos);
            //client.QueryString.Add("msg", msg);
            //client.QueryString.Add("fl", "0");

            
            
           string baseurl = "http://login.smsindiahub.in/API/WebSMS/Http/v1.0a/index.php";
           // string baseurl = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx";

            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            return (s);
        }


        public void FillClass(DBSite site, DropDownList ddl)
        {


            string qry = " SELECT "
                      + " ClassMasterID"
                      + ", ClassName"
                      + " FROM tblClassMaster "

                    + " WHERE UserId=" + User.UserId   
                    + " ORDER By ClassOrder ";

            DataTable dt = site.ExecuteSelect(qry);
            foreach (DataRow row in dt.Rows)
            {

                ddl.Items.Add(new ListItem(CheckNull(row["ClassName"]), CheckNull(row["ClassMasterId"])));


            }

        }

        public void FillYear(DropDownList ddl)
        {
            
            for (int i = int.Parse(indianTime.Year.ToString()); i < int.Parse(DateTime.Now.Year.ToString()) + 20; i++)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }


        public string[] MonthFullNameArr = {"January"
                                         , "Feburary"
                                         , "March"
                                         , "April"
                                         , "May"
                                         , "June"
                                         , "July"
                                         , "August"
                                         , "September"
                                         , "October"
                                         , "November"
                                         , "December"
                                        
                                        };
        public string[] MonthShortNameArr = {"Jan"
                                         , "Feb"
                                         , "Mar"
                                         , "Apr"
                                         , "May"
                                         , "Jun"
                                         , "Jul"
                                         , "Aug"
                                         , "Sep"
                                         , "Oct"
                                         , "Nov"
                                         , "Dec"
                                        
                                        };

        public int[] MonthNumberArr = {1,2,3,4,5,6,7,8,9,10,11,12};
                                         


        public class MonthEntity
        {
            public int MonthNumber { get; set; }
            public string MonthShortName { get; set; }
            public string MonthFullName { get; set; }
        }

        public List<MonthEntity>  GetMonths()
        {
            List<MonthEntity> mnth_list = new List<MonthEntity>();

            MonthEntity mnth;

            for (int i = 0; i < MonthNumberArr.Length; i++)
            {
                mnth = new MonthEntity();
                mnth.MonthFullName = MonthFullNameArr[i];
                mnth.MonthShortName = MonthShortNameArr[i];
                mnth.MonthNumber = MonthNumberArr[i];
                mnth_list.Add(mnth);
            }
            return mnth_list;
         }


        public void FillMonth(DropDownList ddl)
        {
            ddl.Items.Add(new ListItem("Select Month", "0"));

            for (int i = 0; i < MonthNumberArr.Length; i++)
            {
                ddl.Items.Add(new ListItem(MonthShortNameArr[i], MonthNumberArr[i].ToString()));
            }

            //ddl.Items.Add(new ListItem("Jan", "1"));
            //ddl.Items.Add(new ListItem("Feb", "2"));
            //ddl.Items.Add(new ListItem("Mar", "3"));

            //ddl.Items.Add(new ListItem("Apr", "4"));
            //ddl.Items.Add(new ListItem("May", "5"));
            //ddl.Items.Add(new ListItem("Jun", "6"));

            //ddl.Items.Add(new ListItem("Jul", "7"));
            //ddl.Items.Add(new ListItem("Aug", "8"));
            //ddl.Items.Add(new ListItem("Sep", "9"));

            //ddl.Items.Add(new ListItem("Oct", "10"));
            //ddl.Items.Add(new ListItem("Nov", "11"));
            //ddl.Items.Add(new ListItem("Dec", "12"));

        }


        public void FillDay(DropDownList ddl)
        {

            for (int i = 1; i < int.Parse(indianTime.Day.ToString()) + 1; i++)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }



        public void FillStaff(DBSite site, DropDownList ddl)
        {
            ddl.Items.Add(new ListItem("Select Staff", "-1"));

            string qry = "  SELECT studentMasterID, StudentName  "
                          + " FROM tblStudentMaster s "
                      + " INNER JOIN tblClassMaster c ON c.classMasterID = s.classID ANd c.className = 'Staff' "
                      + " WHERE s.userId= " + Util_BLL.User.UserId
                      + " ORDER BY StudentName ";

            DataTable dt = site.ExecuteSelect(qry);
            DataRow row = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];
                ddl.Items.Add(new ListItem(row["StudentName"].ToString(), row["StudentMasterID"].ToString()));
            }
        }






        public void FillDesignation(DBSite site, DropDownList ddl)
        {
            ddl.Items.Add(new ListItem("Select Designation", "-1"));

            string qry = "  SELECT DesignationId, Designation  "
                          + " FROM tblDesignation d "
                     
                      + " WHERE userId= " + Util_BLL.User.UserId
                      + " ORDER BY Designation ";

            DataTable dt = site.ExecuteSelect(qry);
            DataRow row = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];
                ddl.Items.Add(new ListItem(row["Designation"].ToString(), row["DesignationId"].ToString()));
            }
        }



        public  void ClearMessages(Label lblError, Label lblMessage)
        {
            
            lblError.Visible = false;
            lblError.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblMessage.Visible = false;
        }

        public void ClearForm(System.Web.UI.Page frm)
        {
            foreach (System.Web.UI.Control  ctl in frm.Form.Controls )
            {
                
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = String.Empty;
                }
            }
        }


        public void ClearSuccessMessage(Label lblMessage)
        {
            lblMessage.Text = string.Empty;
            lblMessage.Visible = false;
        }


        public void CreatePDFNoTemplate(string pdfPath)
        {
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, new FileStream(pdfPath, FileMode.OpenOrCreate));

            pdfDoc.Open();
            pdfDoc.Add(new iTextSharp.text.Paragraph("Some data"));
            iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
            cb.MoveTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height / 2);
            cb.LineTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height);
            cb.Stroke();

            pdfDoc.Close();
        }


        public string  TranslateSanUtil(string input, string languagePair)
        {

            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string result = webClient.DownloadString(url);
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf(">") + 1);
            result = result.Substring(0, result.IndexOf("</span>"));
            return result.Trim();
        }


        ///rdlc to pdf /////
        ///
        
        ///



        public void ExportToPdf(DataTable dt, string pdfPath, string studentName, string className, string sectionName, string yr)
        {
                        
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                //iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream("d://sample.pdf", FileMode.Create));
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                iTextSharp.text.Rectangle rec3 = new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.A4.Rotate());

                document.Add(new iTextSharp.text.Paragraph("ST. MARY'S SCHOOL, BINOR" ));
                document.Add(new iTextSharp.text.Paragraph("PROGRESS REPORT (Class VI-VIII)"));
                document.Add(new iTextSharp.text.Paragraph("  " ));
                document.Add(new iTextSharp.text.Paragraph("NAME: " + studentName + "  CLASS: " + className + "  SEC: "+ sectionName+"  YEAR: "+ yr ));
                document.Add(new iTextSharp.text.Paragraph("  " ));
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 5);
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count);

                iTextSharp.text.pdf.PdfPRow row = null;
                
            
                iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Products"));
                cell.Colspan = dt.Columns.Count;

                List<float> fList = new List<float>();
                float[] widths = new float[] { };

                foreach (DataColumn c in dt.Columns)
                {
                    fList.Add(1);

                }

                widths = fList.ToArray();
                table.SetWidths(widths);
                table.WidthPercentage = 100;
            
                foreach (DataColumn c in dt.Columns)
                {

                    table.AddCell(new iTextSharp.text.Phrase(c.ColumnName, font5));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        int colCount = 0;
                        foreach (DataColumn c in dt.Columns)
                        {
                            table.AddCell(new iTextSharp.text.Phrase(r[colCount].ToString(), font5));
                            colCount += 1;
                        }
                    
                    }
                }
                document.Add(table);
                document.Add(new iTextSharp.text.Paragraph("  "));
                document.Add(new iTextSharp.text.Paragraph("                                                                             PRINCIPAL"));
                
                
                document.Close();

            
         }

        


    }

}

