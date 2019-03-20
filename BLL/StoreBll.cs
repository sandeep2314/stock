using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class StoreBll
    {
        Util_BLL util = new Util_BLL();

        public string[] stock_transaction_type = { "IN", "OUT", "WITH_IN" };


        public class StockMovement
        {
            public int ProductLedgerNumber { get; set; }
            public int TransactionTypeId { get; set; }
            public string  TransactionName { get; set; }
            public string MovementDate { get; set; }
            public string Narration { get; set; }
            
            public List<ProductDetail> ProductDetails;

            public string SubUserName { get; set; }
            public DateTime CreatedDate { get; set; }
           

        }

        public class ProductDetail
        {

            public int ProductLedgerId { get; set; }
            public int ProductId { get; set; }
            public double sold_qty { get; set; }
            public double bought_qty { get; set; }
            public int LocationId { get; set; }
            public int AccountId { get; set; }
            public string ToNarration { get; set; }

            public string ProductName { get; set; }
            public string LocationName { get; set; }
            public string AccountName { get; set; }

            public int ProductId2 { get; set; }
            public double sold_qty2 { get; set; }
            public double bought_qty2 { get; set; }

            public double openingBalance { get; set; }
            public double closingBalance { get; set; }

            public int LocationId2 { get; set; }

            public int TransactionTypeId { get; set; }
            public string TransactionName { get; set; }
            public string MovementDate { get; set; }
            public string Narration { get; set; }
            
            
        }

        //select p1.BillDate, p1.productLedgerNumber, pm1.ProductName, p1.soldQty, p1.BoughtQty, p2.productID ProductToId , pm2.ProductName
        // , p2.BoughtQty, p2.SoldQty, p2.LocationId, p2.AccountID, AccountName
        // , LocationName
        // from tblProductLedger p1
        // INNER JOIN tblProductLedger p2 ON p1.productLedgerNumber = p2.productLedgerNumber
        // LEFT OUTER JOIN tblProductMaster PM1 ON pm1.productMasterId = p1.productID
        // LEFT OUTER JOIN tblProductMaster PM2 ON pm2.productMasterId = p2.productID
        // LEFT OUTER JOIN tblAccountMaster AM ON am.accountMasterID = p2.accountID
        // LEFT OUTER JOIN tblLocation L on l.locationId = p2.LocationID
        // where 
        // --p1.productLedgerNumber = 1057
        // --and 
        // p1.productId = 3
        // --and p2.productID <> 3

        // ORDER BY BillDate, p1.productLedgerNumber



        public int GetFlow(DBSite site, int trnTypeId)
        {
            int flow = 0;
            string qry = " SELECT Flow FROM tblStockTransactionMaster WHERE TransactionId = " + trnTypeId;
            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows )
            {
                flow  = util.CheckNullInt(row["Flow"]);
             
            }

            return flow;
        }


        public bool IsInFlow(DBSite site, int trnTypeId, ProductDetail pd )
        {
            bool IsInFlow = true;

            ////get Flow from transaction type
            //// if Flow is with_in the check From/To
            //int the_Flow = -1;

            //the_Flow = GetFlow(site, trnTypeId);



            //// if Flow is with_in the check From/To
            //if (the_Flow == 2)
            //{
            //    IsInFlow = (pd.Flow == 0);
            //}
            //else if(the_Flow == 0)
            //{
            //    IsInFlow = true;
            //}
            //else if (the_Flow == 1)
            //{
            //    IsInFlow = false ;
            //}

        


            return IsInFlow;

        }


        public void SaveStore(DBSite site,  StockMovement store, int update=0)
        {
            
            string qry = "";
            int productLedgerNumber = 0;

            if (update == 1)
                productLedgerNumber = store.ProductLedgerNumber;
            else
                productLedgerNumber = util.GetAutoNumber(site, "ProductLedgerNumber");

            

            foreach (ProductDetail pd in store.ProductDetails)
            {
                qry = " INSERT INTO tblProductLedger(ProductLedgerNumber, drcr";
                    
                if (pd.bought_qty > 0)
                    qry = qry + " , BoughtQty ";
                else
                    qry = qry + " , SoldQty ";

                qry = qry + ",  AccountId, BillDate, ProductID, SourceId, LocationID, Particulars, ToNarration, ProductId2, BoughtQty2, SoldQty2, locationId2, UserID, subuserId, FYear) "
                + " VALUES ( "
                + productLedgerNumber;

                if (pd.bought_qty > 0)
                {
                    qry = qry + ", 'C'"
                     + ", " + pd.bought_qty;
                }
                else
                {
                    qry = qry + ", 'D'"
                        + ", " + pd.sold_qty ;
                }
                   qry = qry + ", '" + pd.AccountId + "'"
                    + ", '" + store.MovementDate + "'"
                       + ", " + pd.ProductId 
                   

                    + ", " + store.TransactionTypeId 
                    + ", " + pd.LocationId
                    + ", '" + store.Narration + "' "
                    + ", '" + pd.ToNarration + "' "

                    + ", " + pd.ProductId2
                    + ", " + pd.bought_qty2
                    + ", " + pd.sold_qty2
                    + ", " + pd.LocationId2  

                    + ", " + util.GetUserInsertQry(Util_BLL.User)
                    + " ) ";

               
                
                site.Execute(qry);

                

            }

        }


        public void UpdateStore(DBSite site, StockMovement store)
            {
            string qry = "DELETE FROM tblProductLedger WHERE productLedgerNumber = " + store.ProductLedgerNumber ;
            site.Execute(qry);

            // update
            SaveStore(site, store, 1);

            }
        




        public void UpdateStoreOld(DBSite site, StockMovement store)
        {
            

            string qry = "";
            int productLedgerNumber = util.GetAutoNumber(site, "ProductLedgerNumber");



            foreach (ProductDetail pd in store.ProductDetails)
            {
                qry = " UPDATE tblProductLedger SET ";

                if (pd.bought_qty > 0)
                {
                    qry = qry + "  BoughtQty = " + pd.bought_qty
                    + "DrCr = 'C'";
                }
                else
                {
                    qry = qry + " , SoldQty =" + pd.sold_qty 
                    + "DrCr = 'D'";
                }
                qry = qry + ", productId = " + pd.ProductId
                + ", accountId = " + pd.AccountId
                + ", locationId " + pd.LocationId
                + ", SourceId = " + store.TransactionTypeId
                + ", BillDate =  " + store.MovementDate
                + ", particulars = " + store.Narration
                + ",  " + util.GetUserUpdateQry(Util_BLL.User);

                qry = qry + " WHERE productLedgerId =  " + pd.ProductLedgerId ;

                
 
                site.Execute(qry);

            }
        }


        public List<ProductDetail>  GetProductDetailList()
        {
            DBSite site = new DBSite();

            return GetProductDetailList(site, -1);

        }

        public List<ProductDetail> GetProductByDate(string fromDate, string toDate, int ProductId, int StockTransactionId)
        {
            DBSite site = new DBSite();
            List<ProductDetail> product_detail_list = new List<ProductDetail>();

            if (fromDate == null)
                fromDate = "2013/09/01";
            if (toDate == null)
                toDate = "2014/09/01";

            
            DateTime fDt = DateTime.Parse(fromDate);
            DateTime tDt = DateTime.Parse(toDate);

            DataTable dt = site.Execute_sp_productLedger(ProductId, StockTransactionId, -1, fDt, tDt, Util_BLL.User.UserId, 2012);

            DataRow row = null;

            ProductDetail product;

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                product = new ProductDetail();

                row = dt.Rows[i];

                product.ProductLedgerId = util.CheckNullInt(row["ProductLedgerNumber"]);
                product.ProductId = util.CheckNullInt(row["ProductId"]);
                product.ProductName = row["ProductName"].ToString();

                
                product.LocationName = util.CheckNull(row["Location"]);

                
                product.AccountName = util.CheckNull(row["AccountName"]);

                product.sold_qty = util.CheckNullDouble(row["credit"]);
                product.bought_qty = util.CheckNullDouble(row["debit"]);

                product.openingBalance  = util.CheckNullDouble(row["openingBalance"]);
                product.closingBalance = util.CheckNullDouble(row["runningBalance"]);


                //666

                DateTime date = Convert.ToDateTime(row["BillDate"]);
                product.MovementDate = date.ToShortDateString();
                product.TransactionTypeId = util.CheckNullInt(row["SourceId"]);
                product.TransactionName = util.CheckNull(row["TransactionName"]);
                product.Narration = util.CheckNull(row["Particulars"]);


                product_detail_list.Add(product);
            }


            return product_detail_list;

        }


        public List<ProductDetail> GetProductDetailList(DBSite site, int ProductLedgerNumber)
        {

            List<ProductDetail> product_detail_list = new List<ProductDetail>(); ;


            string qry = " SELECT "
                + " ProductLedgerId "
                + ", ProductLedgerNumber "
                + ", BillDate"
                + ", SourceId, TransactionName "
                + ", pldg.ProductId, ProductName "
                + ", BoughtQty"
                + ", SoldQty"
                + ", LocationName, pldg.LocationId "
                 + ", AccountName, pldg.AccountId "
                + ", UnitName ";

            qry += " FROM "
                + " tblProductLedger as pldg"
                + " LEFT OUTER JOIN tblProductMaster as pm  ON pldg.ProductId = pm.ProductMasterId "
                + " LEFT OUTER JOIN tblAccountMaster as am  ON pldg.AccountId = am.AccountMasterId "
                + " LEFT OUTER JOIN tblLocation as loc ON pldg.LocationId = loc.LocationId  "
                + " LEFT OUTER JOIN tblStockTransactionMaster as tm ON tm.TransactionID = pldg.sourceId "
                + " LEFT OUTER JOIN tblUOM as uom ON pm.UOM = uom.UOMId";

            qry += " WHERE "
                + " pldg.UserId = " + Util_BLL.User.UserId + " AND "
                + " pldg.FYear = " + Util_BLL.User.fYear;
               // + " AND pldg.SourceId = " + TransactionType.StockMovement;

            
            if(ProductLedgerNumber != -1)
                qry += " AND pldg.ProductLedgerNumber = " + ProductLedgerNumber;


            qry += " ORDER BY BillDate, pldg.ProductLedgerNumber, pldg.ProductLedgerId";

            ProductDetail product = null;


            DataTable dt = site.ExecuteSelect(qry);
            DataRow row = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                product = new ProductDetail();

                row = dt.Rows[i];

                product.ProductLedgerId = util.CheckNullInt(row["ProductLedgerId"]);
                product.ProductId = util.CheckNullInt(row["ProductId"]);
                product.ProductName = row["ProductName"].ToString() + "(In " + util.CheckNull(row["UnitName"]) + ")";

                product.LocationId = util.CheckNullInt(row["LocationId"]);
                product.LocationName = row["LocationName"].ToString();

                product.AccountId = util.CheckNullInt(row["AccountId"]);
                product.AccountName = row["AccountName"].ToString();

                product.sold_qty = util.CheckNullDouble(row["SoldQty"]);
                product.bought_qty = util.CheckNullDouble(row["BoughtQty"]);

                DateTime date = Convert.ToDateTime(row["BillDate"]);
                product.MovementDate = date.ToShortDateString();
                product.TransactionTypeId = util.CheckNullInt(row["SourceId"]);
                product.TransactionName = util.CheckNull(row["TransactionName"]);


                product_detail_list.Add(product);
            }


            return product_detail_list;

        }

        
        public List<StockMovement> GetStore(DBSite site, string search_term, int product_ledger_number = 0)
        {
            List<StockMovement> stock_list = new List<StockMovement>();
            //444
            string qry = " SELECT TOP 20 "

                + " ProductLedgerNumber "
                + ", TransactionId"
                + ",  TransactionName "
                + ",  Particulars "
                + ", BillDate "
               + ", SubUserID "
                + ", SubUserName "
                + ", CreatedAt "

                + " FROM ( "


                + " SELECT  "
                + " Distinct ProductLedgerNumber "
                + ", SourceId TransactionId"
                + ",  TransactionName "
                + ",  Particulars "
                + ", BillDate "
                + ", pldg.SubUserID "
                + ", SubUserName "
                + ", DATEADD(dd, 0, DATEDIFF(dd, 0, pldg.CreatedAt)) as createdAt ";
            qry += " FROM "
                + " tblProductLedger as pldg "
                + " LEFT OUTER JOIN tblStockTransactionMaster stm ON stm.TransactionId = pldg.sourceID "
                + " LEFT OUTER JOIN tblSubUser sub ON sub.SubUserID = pldg.SubUserID ";
                
            qry += " WHERE "
                + " pldg.UserId = " + Util_BLL.User.UserId + " AND "
                + " pldg.FYear = " + Util_BLL.User.fYear;
               // + " AND pldg.SourceId = " + TransactionType.StockMovement;

            if (product_ledger_number != 0)
            {
                qry += " AND pldg.ProductLedgerNumber = " + product_ledger_number;
            }

            if (search_term.Length > 0)
            {
                qry += " AND (TransactionName like '%" + search_term + "%'";
                qry += " OR Particulars like '%" + search_term + "%'";
                qry += " OR pldg.ProductLedgerNumber  like '%" + search_term + "%'";
              
                if (util.IsDate(search_term))
               {
                qry += " OR BillDate = '" + search_term + "'";
               }
                qry += " OR SubUserName like '%" + search_term + "%')";
            }

            qry += " ) a1  ";
            qry += " ORDER BY ProductLedgerNumber DESC,  BillDate DESC";

            
            StockMovement stock_movement_entity = null;
            DataTable dt = site.ExecuteSelect(qry);
            DataRow row = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stock_movement_entity = new StockMovement();
                List<ProductDetail> product_details = new List<ProductDetail>();
                

                row = dt.Rows[i];

                stock_movement_entity.ProductLedgerNumber = util.CheckNullInt(row["ProductLedgerNumber"]);
                DateTime date = Convert.ToDateTime(row["BillDate"]);
                stock_movement_entity.MovementDate = date.ToShortDateString();
                stock_movement_entity.TransactionTypeId = util.CheckNullInt(row["TransactionId"]);
                stock_movement_entity.TransactionName = util.CheckNull(row["TransactionName"]);
                stock_movement_entity.Narration = util.CheckNull(row["Particulars"]);
                stock_movement_entity.SubUserName = util.CheckNull(row["SubUserName"]);
                stock_movement_entity.CreatedDate = Convert.ToDateTime(row["CreatedAt"]);

                product_details = this.GetProductDetailList(site, stock_movement_entity.ProductLedgerNumber );

                stock_movement_entity.ProductDetails = product_details;
                stock_list.Add(stock_movement_entity);
                
            }

            return stock_list;
        }


        public bool ValidMovementDateEntry(DBSite site, string product_ids, string issueDate)
        {
            string qry = " SELECT CreatedAt FROM tblProductMaster "
            + " WHERE ProductMasterId IN (" + product_ids + ")";

            bool validIssueDate = true;

            DataTable dt = site.ExecuteSelect(qry);
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["CreatedAt"]);
                string product_creation_date = date.ToShortDateString();

                validIssueDate = util.isDateRangeValid(product_creation_date, issueDate);
            }

            return validIssueDate;
        }


        public void DeleteStore(DBSite site, string product_ledger_numbers)
        {
            string qry = " DELETE FROM ";
            qry += " tblProductLedger ";
            qry += " WHERE  ProductLedgerNumber IN (" + product_ledger_numbers + ")";
            site.Execute(qry);
        }


        public List<SalePurchase.SalePurchaseEntity> GetMatchedIssueInformation(DBSite site, string value_to_search)
        {
            List<SalePurchase.SalePurchaseEntity> issue_info_list = new List<SalePurchase.SalePurchaseEntity>();
            //    StockMovement issue_info_entity = null;

            //    string qry = " SELECT DISTINCT ProductLedgerNumber "
            //               + " FROM ( tblProductLedger as pldg "
            //               + " LEFT OUTER JOIN tblProductMaster as pm"

            //               + " ON pldg.ProductId = pm.ProductMasterId )"

            //                + " LEFT OUTER JOIN tblLocation as loc"

            //               + " ON pldg.LocationId = loc.LocationId "

            //               + " WHERE ( "
            //               + " ( pm.ProductId LIKE '%" + value_to_search + "%')"
            //               + " OR ( LocationName LIKE '%" + value_to_search + "%')";


            //    if (IsDate(value_to_search))
            //    {
            //        qry += " OR ( pldg.BillDate = '" + value_to_search + "' ) ";
            //    }


            //    if (util.IsNumber(value_to_search))
            //    {
            //        qry += " OR ( BoughtQty = '" + value_to_search + "' ) ";
            //        qry += " OR( SoldQty = '" + value_to_search + "' ) ";
            //    }

            //    qry += ") AND pldg.UserId = " + Util_BLL.User.UserId + " AND "
            //    + " pldg.FYear = " + Util_BLL.User.fYear
            //    + " AND pldg.SourceId = " + TransactionType.StockMovement;


            //    DataTable dt = site.ExecuteSelect(qry);

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        issue_info_entity = new StockMovement();
            //        string product_ledger_number = util.CheckNull(dr["ProductLedgerNumber"]);
            //        issue_info_entity = GetMovementInformation(site, product_ledger_number)[0];

            //        issue_info_list.Add(issue_info_entity);
            //    }

            return issue_info_list;
        }


    }
}