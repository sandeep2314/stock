using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class AccountType
    {
        Util_BLL util = new Util_BLL();        


        //--------------  Account Types -----------------------------

            public string[] accountTypes = { "Group", "Category", "UOM", "Location"};

            public string[] typeTables = { "tblGroup", "tblProductCategory", "tblUOM", "tblLocation"};
            public string[] columnName = { "GroupName", "CategoryName", "UnitName" , "LocationName" };           
            public string[] idName = { "GroupId", "CategoryId", "UOMId" , "LocationId" };


            public static string GetColumnName(AccountType act, string selectedAcct)
            {
                int idx = Array.IndexOf(act.accountTypes, selectedAcct);
                return act.columnName[idx];
            }


            public static string GetTableName(AccountType act, string selectedAcct)
            {
                int idx = Array.IndexOf(act.accountTypes, selectedAcct);
                return act.typeTables[idx];
            }


            public static string GetTypeIdName(AccountType act, string selectedAcct)
            {
                int idx = Array.IndexOf(act.accountTypes, selectedAcct);
                return act.idName[idx];
            }



            public String GetName(DBSite site, string selectedAccountType, string id)
            {
                string name = null;

                string qry = " SELECT " + GetColumnName(this, selectedAccountType);
                qry += " FROM " + GetTableName(this, selectedAccountType);
                qry += " WHERE " + GetTypeIdName(this, selectedAccountType) + " = " + id;

                DataTable dt = null;

                    dt = site.ExecuteSelect(qry);
                    DataRow dr = dt.Rows[0];
                    name = util.CheckNull(dr[GetColumnName(this, selectedAccountType)]);
               
                return name;
            }


    }
}