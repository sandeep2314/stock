using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class MasterBLL
    {
        Util_BLL util = new Util_BLL();

        public class Location
        {
            public int LocationId { get; set; }
            public string LocationName { get; set; }
        }

        public class AccountParty
        {
            public int AccountId { get; set; }
            public string AccountName { get; set; }
        }


        public List<AccountParty> GetAccounts()
        {
            List<AccountParty> accounts = new List<AccountParty>();
            AccountParty account = null;

            DBSite site = new DBSite();

            string qry = " SELECT AccountMasterId, AccountName"
                + " FROM  tblAccountMaster "
                + " WHERE AccountName is not NULL AND UserId = " + Util_BLL.User.UserId
                + "  ORDER BY AccountName";


            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                account = new AccountParty();
                account.AccountId = util.CheckNullInt(row["AccountMasterId"]);
                account.AccountName = util.CheckNull(row["AccountName"]);

                accounts.Add(account);
            }

            return accounts;
        }




        public List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();
            Location  location = null;

            DBSite site = new DBSite();

            string qry = " SELECT locationId, LocationName "
                + " FROM  tblLocation "
                + " WHERE locationName is not NULL AND UserId = " + Util_BLL.User.UserId
                +"  ORDER BY locationName";


            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                location = new Location();
                location.LocationId  = util.CheckNullInt(row["LocationId"]);
                location.LocationName = util.CheckNull(row["LocationName"]);

                locations.Add(location);
            }
            
            return locations;
        }

       
        public void SaveMasterData(DBSite dbSite, string selectedAccountType, string masterData, AccountType act)
        {

            string qry = "INSERT INTO " + AccountType.GetTableName(act, selectedAccountType) + "(";
            qry += AccountType.GetColumnName(act, selectedAccountType);
            qry += ", UserID"
                + ",SubuserId";
            qry += ") values('" + masterData + "', "
                +Util_BLL.User.UserId +" ," + Util_BLL.SubUser.SubuserId  + ")";                 
          
            dbSite.Execute(qry);
               // string msg = "alert('Record Deleted');"

        }

        //------------  update method ----------------

        public void UpdateMasterData(DBSite dbSite, string selectedAccountType, string masterData, AccountType act, string Id)
        {
            string qry = " UPDATE " + AccountType.GetTableName(act, selectedAccountType) + " SET ";
            qry += AccountType.GetColumnName(act, selectedAccountType);
            qry += " =' " + masterData + "'"
                + ", UserId=" + Util_BLL.User.UserId
                + ", SubuserId=" + Util_BLL.SubUser.SubuserId ;



            qry += "WHERE " + AccountType.GetTypeIdName(act, selectedAccountType);
            qry += " = " + Id + "";

            dbSite.Execute(qry);           

        }





        public DataTable GetAccountTypes(DBSite dbSite, string selectedAccountType, AccountType act)
        {
            return GetAccountTypes(dbSite, selectedAccountType, act, -1);
        }

        public DataTable GetAccountTypes(DBSite dbSite, string selectedAccountType, AccountType act, int typeID = -1)
        {


            string qry = "SELECT  " + AccountType.GetTypeIdName(act, selectedAccountType) + " ID, " + AccountType.GetColumnName(act, selectedAccountType) + " Name ";
            qry += " FROM " + AccountType.GetTableName(act, selectedAccountType);
            qry += " WHERE UserId = " + Util_BLL.User.UserId;
            if (typeID != -1)
                qry += " AND " + AccountType.GetTypeIdName(act, selectedAccountType) + " = " + AccountType.GetTableName(act, selectedAccountType);

            DataTable dt = dbSite.ExecuteSelect(qry);
                         

            return dt;
        }

        public bool IsMasterBeingUsed(DBSite site, string selectedAccountType, AccountType act, int masterId)
        {
            string tableName = "";
            string colName = AccountType.GetTypeIdName(act, selectedAccountType);

            if(colName.ToUpper() == "GROUPID")
            {
                tableName = "tblAccountMaster";
            }
            else if (colName.ToUpper() == "CATEGORYID")
            {
                tableName = "tblProductMaster";
            }
            else if (colName.ToUpper() == "UOMID")
            {
                tableName = "tblProductMaster";
                colName = "UOM";
            }
            else if (colName.ToUpper() == "LOCATIONID")
            {
                tableName = "tblProductLedger";
                
            }
            string qry = " SELECT "+colName+" FROM " + tableName + " ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND " + colName + " = '" + masterId + "'";
            DataTable dt = null;
            dt = site.ExecuteSelect(qry);

            return dt.Rows.Count > 0;
        }

        public void DeleteRecords(DBSite dbSite, string selectedAccountType, AccountType act, string typeID)
        {

            string qry = "DELETE FROM  " + AccountType.GetTableName(act, selectedAccountType);
            qry += " WHERE " + AccountType.GetTypeIdName(act, selectedAccountType) + " IN ( " + typeID + ")";
           
            dbSite.Execute(qry);

        }


       public bool isDuplicateFieldName(DBSite site, string typeName ,string name, int userId)
        {
            AccountType act=new AccountType();
            string qry = "SELECT " + AccountType.GetTypeIdName(act, typeName) + " FROM " + AccountType.GetTableName(act, typeName);
            qry += " WHERE " + AccountType.GetColumnName(act, typeName) + " = '" + name + "' ";
            qry += " AND userID = " + Util_BLL.User.UserId;

           DataTable dt=site.ExecuteSelect(qry);

           return dt.Rows.Count > 0;
        }





       public DataTable GetMatchedRecords(DBSite site, string selectedAccountType, string value_to_search, AccountType act)
       {

           string qry = "SELECT  " + AccountType.GetTypeIdName(act, selectedAccountType) + " ID, " + AccountType.GetColumnName(act, selectedAccountType) + " Name ";
           qry += " FROM " + AccountType.GetTableName(act, selectedAccountType);

           qry += " WHERE " + AccountType.GetColumnName(act, selectedAccountType) + " LIKE '%" + value_to_search + "%'";

           qry += " AND UserId= " + Util_BLL.User.UserId;

           return site.ExecuteSelect(qry);
       }



    }
}