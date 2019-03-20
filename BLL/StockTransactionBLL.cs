using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace AccountingSoftware.BLL
{
    public class StockTransactionBLL
    {

        Util_BLL util = new Util_BLL();


        public class StockTransactionType
        {
            public int StockTransactionId { get; set; }
            public string StockTransactionName { get; set; }
            public int StockTransactionFlow { get; set; }
            public string StockTransactionFlowName { get; set; }
        }



        public void SaveInfo(DBSite site, StockTransactionType trn)
        {

            string qry = " INSERT INTO "
                + " tblStockTransactionMaster"
                + "( "
                + "TransactionName"
                + ", Flow"
                + ", UserId, FYear )"
                + " VALUES( '" + trn.StockTransactionName + "'"
                + ", " + trn.StockTransactionFlow
                + ", " + util.GetUserInsertQryMaster(Util_BLL.User) //------  user insert query ----------
                + ")";

            site.Execute(qry);
        }


        public void EditInfo(DBSite site, StockTransactionType st, string trn_id)
        {

            string qry = " UPDATE tblStockTransactionMaster SET"
                + " TransactionName='" + st.StockTransactionName + "'"
                + ", Flow=" + st.StockTransactionFlow 
                + " WHERE UserId=" + Util_BLL.User.UserId
                + " AND FYear=" + Util_BLL.User.fYear
                + " AND TransactionId=" + trn_id;

            site.Execute(qry);
        }

        public List<StockTransactionType> GetInfo()
        {
            DBSite site =  new DBSite();
            return GetInfo(site);
        }

        public List<StockTransactionType> GetInfo(DBSite site, string trn_id = "")
        {
            List<StockTransactionType> trn_list = new List<StockTransactionType>();
            StockTransactionType trn = null;

            string qry = " SELECT "
                + "TransactionId "
                + ", TransactionName "
                + ", Flow"
                + " FROM tblStockTransactionMaster"
                + " WHERE "
                + " UserId =" + Util_BLL.User.UserId
                + " AND FYear=" + Util_BLL.User.fYear;

            if (trn_id != "")
                qry += " AND TransactionId=" + trn_id;

            DataTable dt = site.ExecuteSelect(qry);


            StoreBll store = new StoreBll();

           

            foreach (DataRow row in dt.Rows)
            {
                trn = new StockTransactionType();

                trn.StockTransactionId = util.CheckNullInt(row["TransactionId"]);
                trn.StockTransactionName   = util.CheckNull(row["TransactionName"]);
                trn.StockTransactionFlow = util.CheckNullInt(row["Flow"]);
                trn.StockTransactionFlowName = store.stock_transaction_type[trn.StockTransactionFlow];
                trn_list.Add(trn);
            }

            return trn_list;
        }

        public bool IsTransactionInProductLedger(DBSite site, int transId)
        {
            string qry = " SELECT SourceId FROM tblProductLedger ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND SourceId = '" + transId + "'";
            DataTable dt = null;
            dt = site.ExecuteSelect(qry);

            return dt.Rows.Count > 0;
        }


        public void DeleteInfo(DBSite site, string trn_ids)
        {
            string qry = " DELETE FROM tblStockTransactionMaster "
                   + " WHERE UserId=" + Util_BLL.User.UserId
                   + " AND FYear=" + Util_BLL.User.fYear
                   + " AND TransactionId IN (" + trn_ids + ")";
            site.Execute(qry);
        }



        public List<StockTransactionType> GetMatches(DBSite site, string value_to_search)
        {
            List<StockTransactionType> trn_list = new List<StockTransactionType>();
            StockTransactionType trn = null;


            string qry = " SELECT "
                + " TransactionId"
                + ", TransactionName"
                + ", Flow"
                + " FROM tblStockTransactionMaster"
                + " WHERE "
                + " UserId =" + Util_BLL.User.UserId
                + " AND FYear=" + Util_BLL.User.fYear + " AND"
                + "(";

            qry += " (TransactionName LIKE '%" + value_to_search + "%') ";

            if (util.isNumeric(value_to_search))
            {
                qry += "OR ( Flow  ='" + value_to_search + "')";
                
            }

            qry += ")";


            DataTable dt = site.ExecuteSelect(qry);

            SalePurchase sp = new SalePurchase();


            foreach (DataRow row in dt.Rows)
            {
                StoreBll store = new StoreBll();

                trn.StockTransactionId = util.CheckNullInt(row["TransactionId"]);
                trn.StockTransactionName = util.CheckNull(row["TransactionName"]);
                trn.StockTransactionFlow = util.CheckNullInt(row["Flow"]);
                trn.StockTransactionFlowName = store.stock_transaction_type[trn.StockTransactionFlow];
                
                trn_list.Add(trn);
            }

            return trn_list;
        }



    }
}