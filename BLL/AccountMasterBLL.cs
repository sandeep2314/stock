using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class AccountMasterBLL
     {

         //public static object GroupList { get; set; }
         Util_BLL util = new Util_BLL();
        


        public class AccountMasterEntity
        {
          
            public int AccountMasterId { get; set; }
            public string AccountName { get; set; }
            public int DRCR { get; set; }

            public string DRCR_Name { get; set; }

            public decimal OpeningBalance { get; set; }
            public string CreationDate { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Phone { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Remarks { get; set; }

            public string GroupName { get; set; }

            public int GroupId { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }  

            public void SaveAccountMasterData(DBSite site, AccountMasterEntity ame)
            {                
               
                
                string qry = "INSERT INTO tblAccountMaster(AccountName, CreationDate, OpeningBalance, DRCR, ";
                qry += " Address, City, Phone,  Mobile, Email, Remarks, GroupId, UserID, SubuserId ,FYear)  VALUES(";
                qry += "'" + ame.AccountName + "'";
                qry += ", '" + ame.CreationDate + "'";
                qry += ", " + ame.OpeningBalance;
                qry += ", " + ame.DRCR;
                qry += ", '" + ame.Address + "'";
                qry += ", '" + ame.City + "'";
                qry += ", '" + ame.Phone + "'";
                qry += ", '" + ame.Mobile + "'";
                qry += ", '" + ame.Email + "'";
                qry += ", '" + ame.Remarks + "'";
                qry += ", " + ame.GroupId+",";
                qry += util.GetUserInsertQry(Util_BLL.User);

                //qry += ", " + UserBLL.userID;
                //qry += ", " + UserBLL.fYear;


                qry += " )";

               
                    site.Execute(qry);                

            }

            public List<AccountMasterEntity> GetAccountMasterData(DBSite site, int userId, string id = "")
            {
                List<AccountMasterEntity> accountMasterList = new List<AccountMasterEntity>();

                string qry = "";
                qry += "SELECT ";
                qry += "AccountMasterId, AccountName, CreationDate, OpeningBalance,";
                qry += "DRCR,  Address, City, Phone, Mobile, Email,";
                qry += " Remarks, acc.UserID, FYear ";

                qry += ", acc.GroupID  GroupID";   
                qry += ", GroupName ";   


                qry += " FROM tblAccountMaster as acc ";
                qry += " LEFT OUTER JOIN tblGroup as gp ON ";
                qry += " acc.GroupId = gp.GroupId ";


                //qry += Util_BLL.GetUserWhereCondition();
                qry += " Where acc.UserId = " + Util_BLL.User.UserId  ;

                if (id != string.Empty)
                    qry += "AND  AccountMasterId = " + id;
             

                    DataTable dt = site.ExecuteSelect(qry);
                    AccountMasterEntity ac;

                    foreach (DataRow dr in dt.Rows)
                    {
                        ac = new AccountMasterEntity();

                        ac.AccountMasterId = util.CheckNullInt(dr["AccountMasterId"].ToString());
                        ac.AccountName = dr["AccountName"].ToString();


                        DateTime date = Convert.ToDateTime(dr["CreationDate"]);

                        ac.CreationDate = date.ToShortDateString();

                        ac.OpeningBalance=util.ToDecimal(util.CheckNullDecimal(dr["OpeningBalance"]));

                        ac.DRCR=util.CheckNullInt(dr["DRCR"]);
                        ac.DRCR_Name = (util.CheckNullInt(dr["DRCR"]) == 0) ? "Credit" : "Debit";
                        ac.Address = util.CheckNull(dr["Address"].ToString());
                        ac.City = util.CheckNull(dr["City"].ToString());
                        ac.Phone = util.CheckNull(dr["Phone"].ToString());
                        ac.Mobile = util.CheckNull(dr["Mobile"].ToString());
                        ac.Email = util.CheckNull(dr["Email"].ToString());
                        ac.Remarks = util.CheckNull(dr["Remarks"].ToString());
                        ac.GroupId = util.CheckNullInt(dr["GroupId"]);
                        ac.GroupName = util.CheckNull(dr["GroupName"]);
                        ac.UserID = util.CheckNullInt(dr["UserID"]);
                        ac.FYear = util.CheckNullInt(dr["FYear"]);

                        accountMasterList.Add(ac);

                    }

                return accountMasterList;
            }


            public void DeleteRecords(DBSite site, string ids)
            {

                string qry = "DELETE FROM  tblAccountMaster";
                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); // --------get user where condition  -----------------
                qry += " AND AccountMasterId IN (" + ids+ ")";

                    site.Execute(qry);
            }


            public void EditAccountMasterData(DBSite site,AccountMasterEntity ame)
            {
      
                string qry = "UPDATE tblAccountMaster SET ";
                qry += " AccountName='" + ame.AccountName + "'";
                qry += ", CreationDate='" + ame.CreationDate + "'";
                qry += ", OpeningBalance=" + ame.OpeningBalance + "";
                qry += ", DRCR=" + ame.DRCR + "";
                qry += ", Address='" + ame.Address + "'";
                qry += ", City='" + ame.City + "'";
                qry += ", Phone='" + ame.Phone + "'";
                qry += ", Mobile='" + ame.Mobile + "'";
                qry += ", Email='" + ame.Email + "'";
                qry += ", Remarks='" + ame.Remarks + "'";
                qry += ", GroupId=" + ame.GroupId;
                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);  //-------------  gwt user where condition --------------
                qry += " AND AccountMasterId=" + ame.AccountMasterId;
               
                site.Execute(qry);

            }


            public bool IsProductAccountInProductLedger(DBSite site, int accountMasterId)
            {
                string qry = " SELECT AccountID FROM tblProductLedger ";
                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
                qry += " AND AccountID = '" + accountMasterId + "'";
                DataTable dt = null;
                dt = site.ExecuteSelect(qry);

                return dt.Rows.Count > 0;
            }

            public bool  isDuplicateAccountName(DBSite site, string accountName)
            {
                string qry = " SELECT AccountName FROM tblAccountMaster ";
                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);  //  ------- get user where condition ----------
                qry += " AND AccountName = '" + accountName + "'";
                DataTable dt = null;
                dt = site.ExecuteSelect(qry);
                              
                return dt.Rows.Count > 0 ;
            }


            public List<AccountMasterEntity> GetMatchedRecords(DBSite site, string value_to_search)
            {
                              
                List<AccountMasterEntity> selected_account_list = new List<AccountMasterEntity>();

                string qry = "";
                qry += "SELECT ";
                qry += "AccountMasterId, AccountName,  CreationDate, OpeningBalance,";
                qry += "DRCR,  Address, City, Phone, Mobile, Email,";
                qry += " Remarks, acc.UserID, FYear ";
                qry += ", acc.GroupId  ";
                qry += ", GroupName  ";

                qry += " FROM tblAccountMaster as acc ";
                qry += " LEFT OUTER JOIN tblGroup as gp ON ";
                qry += " acc.GroupId = gp.GroupId ";


                //----- selection condition ----------

                qry += " WHERE acc.UserID = " + Util_BLL.User.UserId + " AND  FYear= " + Util_BLL.User.fYear + " AND "; //------  user from session -----
                qry += " (( AccountName LIKE '%" + value_to_search + "%' ) OR";

                if (IsDate(value_to_search))
                {                   
                    qry += " ( CreationDate = '" + value_to_search + "' ) OR";
                }

                qry += " ( Address LIKE '%" + value_to_search + "%' ) OR";
                qry += " ( City LIKE '%" + value_to_search + "%' ) OR";

                if (util.IsNumber(value_to_search))
                {

                    qry += " ( OpeningBalance = '" + value_to_search + "' ) OR";
                    qry += " ( Phone = '" + value_to_search + "' ) OR";
                    qry += " ( Mobile = '" + value_to_search + "' ) OR";

                }

                qry += " ( Email LIKE '%" + value_to_search + "%' ) OR";
                qry += " ( GroupName LIKE '%" + value_to_search + "%') )";
                


                DataTable dt = site.ExecuteSelect(qry);
                AccountMasterEntity ac;

                foreach (DataRow dr in dt.Rows)
                {
                    ac = new AccountMasterEntity();

                    ac.AccountMasterId = util.CheckNullInt(dr["AccountMasterId"].ToString());
                    ac.AccountName = dr["AccountName"].ToString();
                    ac.CreationDate = util.CheckNullDate(dr["CreationDate"]);

                    ac.OpeningBalance = util.ToDecimal(util.CheckNullDecimal(dr["OpeningBalance"]));

                    ac.DRCR = util.CheckNullInt(dr["DRCR"]);
                    ac.DRCR_Name = (util.CheckNullInt(dr["DRCR"]) == 0) ? "Credit" : "Debit";
                    ac.Address = util.CheckNull(dr["Address"].ToString());
                    ac.City = util.CheckNull(dr["City"].ToString());
                    ac.Phone = util.CheckNull(dr["Phone"].ToString());
                    ac.Mobile = util.CheckNull(dr["Mobile"].ToString());
                    ac.Email = util.CheckNull(dr["Email"].ToString());
                    ac.Remarks = util.CheckNull(dr["Remarks"].ToString());
                    ac.GroupId = util.CheckNullInt(dr["GroupId"]);
                    ac.GroupName = util.CheckNull(dr["GroupName"]);
                    ac.UserID = util.CheckNullInt(dr["UserID"]);
                    ac.FYear = util.CheckNullInt(dr["FYear"]);

                    selected_account_list.Add(ac);

                }

                return selected_account_list;

            }


            protected bool IsDate(String date)
            {
                bool valid_date = true;

                try
                {
                    Convert.ToDateTime(date);
                }
                catch (Exception e)
                {
                    valid_date = false;
                }
                return valid_date;
            }


    }
}