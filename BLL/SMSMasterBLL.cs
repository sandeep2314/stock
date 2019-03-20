using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;


namespace AccountingSoftware.BLL
{
    public class SMSMasterBLL
    {
        Util_BLL util = new Util_BLL();

         public class SMSMasterEntity
         {
             public string SMSText { get; set; }
             public string MobileNo { get; set; }
             public string SentDate { get; set; }
             public int SMSCount { get; set; }
             public int UserId { get; set; }
         }


         public List<SMSMasterEntity> GetSMSsentList(string fromDt, string toDt)
         {
             List<SMSMasterEntity> smsList = new List<SMSMasterEntity>();


             if (fromDt==null) 
                 fromDt = "1-Jan-17";
             if (toDt == null)
                 toDt = DateTime.Today.ToString();

             string qry = "";
             qry += "SELECT "
              + " smsText, MobileNo, CreatedAt, MsgCount, UserID "
              + " FROM tblSMSsent ss "
              + " WHERE ss.UserId = " + Util_BLL.User.UserId
              + " AND createdAt BETWEEN '" + fromDt + " ' AND '" + toDt + "'";
            qry += " ORDER BY createdAt ";

             DBSite site = new DBSite();

             DataTable dt = site.ExecuteSelect(qry);
             SMSMasterEntity sms;

             foreach (DataRow dr in dt.Rows)
             {
                 sms = new SMSMasterEntity();

                 sms.SMSText = util.CheckNull(dr["smsText"]);
                 sms.MobileNo = util.CheckNull(dr["MobileNo"]);
                 sms.SentDate = util.CheckNull(dr["CreatedAt"]);
                 sms.SMSCount = util.CheckNullInt(dr["MsgCount"]);
                 sms.UserId= util.CheckNullInt(dr["UserID"]);
                 
                 smsList.Add(sms);
             }

             return smsList;
         }







    }

    

    


}