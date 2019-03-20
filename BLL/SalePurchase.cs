using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AccountingSoftware.BLL;

namespace AccountingSoftware.BLL
{
    public class SalePurchase
    {
        static Util_BLL util = new Util_BLL();
       

        public class SalePurchaseEntity
        {
            public int SalePurchaseId { get; set; }

            public string BillNumber { get; set; }
            public string BillDate { get; set; }
            public string SalePurchaseDate { get; set; }
            public string party { get; set; } // account name
            public string mode { get; set; } // account Name

            public int party_accountAutoId { get; set; }
            public int mode_accountAutoId { get; set; }
            public string  Id { get; set; }
            public double SalePurchaseAmount { get; set; }
            public string discount { get; set; }
            public string particulars { get; set; }

            public double TotalAmount { get; set; }

            public  string CheckNumber { get; set; }

            public int drcr { get; set; }  // for opening balance


            public int SaleType = 1; // purchase or Sale

            public int transactionType { get; set; }

            public List<SalePurchaseDetail> SalePurchaseDetails;
            public List<Tax> SalePurchaseTaxes;

        }



        public class SalePurchaseDetail
        {
            public int SalePurchaseDetailId { get; set; }
            public string productId { get; set; }
            public int productAutoId { get; set; }
            public int locationId = 1;
            //public int locationId { get; set; }
            public double qty { get; set; }
            public int uom { get; set; }
            public string UOM { get; set; }
            public double SalePurchaseDetailAmount { get; set; }
            public string discount { get; set; }
            public double rate { get; set; }
            public string MovementDate { get; set; }

            public List<Tax> SalePurchaseDetailsTaxes;

        }


        public class Tax
        {
            public int TaxId;
            public string TaxName;
            public double TaxPercent;
            public double TaxAmount;

        }



        


        


      




        public class TransactionType
        {
            public const int OpeningBalance = 0;
            public const int Sales = 1;
            public const int Purchase = 2;
            public const int PaymentRcd = 3;
            public const int PaymentMade = 4;
            public const int StockMovement = 5;

        }


       




        public class Modes
        {
            public const int SaleAccount = 9999991;
            public const int PurchaseAccount = 9999992;
            public const int PaymentRcd = 9999993;
            public const int PaymentMade = 9999994;
            public const int Issue = 9999995;

        }

        


        public class DrCr
        {
            public const int Debit = 1;
            public const int Credit = 0;
        }




        
        


        public void SaveSalePurchase(DBSite site, SalePurchaseEntity salePurchase, bool isOpeningBalance)
        {

            Util_BLL util = new Util_BLL();


            /**
            
                // payment made If GM.isFees = 2  Then
            Dim tmp As String
            tmp = mode
            mode = party
            party = tmp
            End If

                **/

            int party = Int32.Parse(util.GetLOVId(salePurchase.party));
            int mode = Int32.Parse(util.GetLOVId(salePurchase.mode));



            if (salePurchase.transactionType == TransactionType.PaymentMade)
            {
                int tmp;
                tmp = Int32.Parse(util.GetLOVId(salePurchase.mode));
                mode = Int32.Parse(util.GetLOVId(salePurchase.party));
                party = tmp;

            }

            if (salePurchase.transactionType == TransactionType.Sales )
            {
                mode = Modes.SaleAccount; 
            }

            int ledgerNumber = util.GetAutoNumber(site, "LedgerNumber");

            // opening balance

            string opening_qry = "";

            if (isOpeningBalance)
            {
                string credit_debit = "Debit";

                if (salePurchase.drcr == DrCr.Debit)
                {
                    credit_debit = "Debit";
                }
                else if (salePurchase.drcr == DrCr.Credit)
                {
                    credit_debit = "Credit";
                }


                opening_qry = " INSERT INTO tblLedger ("
                    + " LedgerNumber, Ldate, AccountMasterId, modeId, Particulars, " + credit_debit + ", sourceId, UserId, SubUserId, FYear )"
                    + " VALUES ( "
                    + ledgerNumber
                    + ", '" + salePurchase.BillDate + "' "
                    + ", " + party 
                    + ", " + mode 
                    + ", '" + salePurchase.particulars + "' "
                    + ", " + salePurchase.SalePurchaseAmount
                    + ", " + salePurchase.transactionType
                    + ", " + util.GetUserInsertQry(Util_BLL.User)
                    + " ) ";


                site.Execute(opening_qry);
            }






            // sales - party 
            string party_qry = " INSERT INTO tblLedger ("
            + " LedgerNumber, drcr, Ldate, AccountMasterId, modeId, BillNumber, Particulars, credit, Discount, sourceId, UserId, SubuserId, FYear )"
            + " VALUES ( "
            +  ledgerNumber
            + ", 'C' "
            + ", '" + salePurchase.BillDate + "' "
            + ", " + party 
            + ", " + mode 
            + ", '" + util.GetLOVName(salePurchase.BillNumber) + "' "
            + ", '" + salePurchase.particulars + "' "
            + ", " + salePurchase.SalePurchaseAmount
             + ", '" + salePurchase.discount +"'"
            + ", " + salePurchase.transactionType
            + ", " + util.GetUserInsertQry(Util_BLL.User)
            + " ) ";




            // sales - party 
            string mode_qry = " INSERT INTO tblLedger ("
            + " LedgerNumber,  drcr,  Ldate, AccountMasterId, modeId, BillNumber, Particulars, debit, Discount, sourceId, UserId, SubuserId, FYear )"
            + " VALUES ( "
            +  ledgerNumber
            + ", 'D' "
            + ", '" + salePurchase.BillDate + "' "
            + ", " + mode 
            + ", " + party 
            + ", '" + util.GetLOVName(salePurchase.BillNumber) + "' "
            + ", '" + salePurchase.particulars + "' "
            + ", " + salePurchase.SalePurchaseAmount
             + ", '" + salePurchase.discount + "'"
            + ", " + salePurchase.transactionType
            + ", " + util.GetUserInsertQry(Util_BLL.User)
            + " ) ";

            site.Execute(party_qry);
            site.Execute(mode_qry);


            if (salePurchase.transactionType != TransactionType.PaymentMade && salePurchase.transactionType != TransactionType.PaymentRcd)
            {


                string tax_qry = "";

                for (int i = 0; i < salePurchase.SalePurchaseTaxes.Count; i++)
                {
                    tax_qry = " INSERT INTO tblTax(LedgerNumber, TaxMasterId, TaxAmount,  UserID, SubuserId, FYear) "
                           + " VALUES ( "
                           + ledgerNumber
                           + ", " + salePurchase.SalePurchaseTaxes[i].TaxId
                           + ", " + salePurchase.SalePurchaseTaxes[i].TaxAmount
                           + ", " + util.GetUserInsertQry(Util_BLL.User)
                           + " ) ";

                    site.Execute(tax_qry);

                }

                SaveProductLedger(site, salePurchase, ledgerNumber);

            }

        }


        public void SaveProductLedger(DBSite site, SalePurchaseEntity salePurchase, int ledgerNumber)
        {
            
            

            string qry = "";

            for(int i = 0; i < salePurchase.SalePurchaseDetails.Count; i++)
            {
                int productLedgerNumber = util.GetAutoNumber(site, "ProductLedgerNumber");
                int theLocation = -1;
                if (salePurchase.transactionType == TransactionType.Sales)
                    theLocation = GetLocationId(site, "OUT");
                else
                    theLocation = GetLocationId(site, "IN");


                qry = " INSERT INTO tblProductLedger(LedgerNumber, ProductLedgerNumber, drcr, BillDate, BillNumber, AccountId, ProductID, BoughtQty, Discount, rate, SourceId, LocationID, UserID, subuserId, FYear) "
                + " VALUES ( "
                + ledgerNumber 
                + ", " + productLedgerNumber
                + ", 'C'"
                + ", '" + salePurchase.BillDate + "'"
                + ", '" + salePurchase.BillNumber + "'"
                + ", '" + util.GetLOVId(salePurchase.party) + "'"
                + ", " +  salePurchase.SalePurchaseDetails[i].productAutoId
                + ", " + salePurchase.SalePurchaseDetails[i].qty

             
                + ", '" + salePurchase.SalePurchaseDetails[i].discount +" '"
                + ", " + salePurchase.SalePurchaseDetails[i].rate 
                + ", " + salePurchase.SaleType
                + ", " + theLocation
                //+ ", " + salePurchase.SalePurchaseDetails[i].locationId
                +", " + util.GetUserInsertQry(Util_BLL.User)
                + " ) ";

                site.Execute(qry);

                qry = " INSERT INTO tblProductLedger(LedgerNumber, ProductLedgerNumber, drcr, BillDate, BillNumber, AccountId, ProductID, SoldQty, Discount, rate, SourceId, LocationID, UserID, subuserId, FYear) "
               + " VALUES ( "
               + ledgerNumber
               + ", " + productLedgerNumber
               + ", 'D'"
               + ", '" + salePurchase.BillDate + "'"
               + ", '" + salePurchase.BillNumber + "'"
               + ", '" + Modes.SaleAccount + "'"
               + ", " + salePurchase.SalePurchaseDetails[i].productAutoId
               + ", " + salePurchase.SalePurchaseDetails[i].qty


               + ", '" + salePurchase.SalePurchaseDetails[i].discount + " '"
               + ", " + salePurchase.SalePurchaseDetails[i].rate 
               + ", " + salePurchase.SaleType
               + ", " + theLocation 
               //+ ", " + salePurchase.SalePurchaseDetails[i].locationId
               + ", " + util.GetUserInsertQry(Util_BLL.User)
               + " ) ";

                site.Execute(qry);




                string tax_qry = "";

                for (int j = 0; j < salePurchase.SalePurchaseDetails[i].SalePurchaseDetailsTaxes.Count; j++)
                {

                    tax_qry = " INSERT INTO tblTax(LedgerNumber, ProductLedgerNumber, TaxMasterId, TaxAmount, UserID, subUserId, FYear) "
                    + " VALUES ( "
                    + ledgerNumber
                    + ", " + productLedgerNumber
                    + ", " + salePurchase.SalePurchaseDetails[i].SalePurchaseDetailsTaxes[j].TaxId
                    + ", " + salePurchase.SalePurchaseDetails[i].SalePurchaseDetailsTaxes[j].TaxAmount
                    + ", " + util.GetUserInsertQry(Util_BLL.User)
                    + " ) ";

                    site.Execute(tax_qry);
                }

                

            
            }

        
        }


        public int GetLocationId(DBSite site, string locationName)
        {
            int locationId = -1;
            
            string qry = "SELECT LocationId FROM tblLocation "
            + " WHERE locationName = '" + locationName + "'"
            + " AND userId=  " + Util_BLL.User.UserId;

            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow r in dt.Rows)
            {
                locationId = util.CheckNullInt(r["LocationId"]);
            }

            return locationId;


        }


        public void UpdateSalePurchase(DBSite site, SalePurchaseEntity salePurchase)
        {

        }

        public static string[] GetProductIdAndUOM(DBSite site, string productId)
        {
            string qry = "SELECT SellingPrice , UnitName + ' | ' +CONVERT( VARCHAR, UOMId ) as UnitName"
             + " FROM tblProductMaster as productTable " 
             + " LEFT OUTER  JOIN tblUOM  as unitTable ON productTable.UOM=unitTable.UOMId "
              + /*util.GetUserWhereCondition()*/ " WHERE 1=1 AND productTable.UserID = " + Util_BLL.User.UserId + " AND productTable.FYear =  " + Util_BLL.User.fYear

             + " AND ProductID='" + productId + "' ";

            DataTable dt = site.ExecuteSelect(qry);
            DataRow dr = dt.Rows[0];

            return new string[] { util.CheckNull(dr["UnitName"]), util.CheckNull(dr["SellingPrice"]) };
        }


        public DataTable GetTaxInformation(DBSite site)
        {

            string qry = " SELECT  TaxId as Id , "
            + " TaxName + ' | ' + CONVERT( VARCHAR , TaxAmount ) as TaxName FROM  tblTaxMaster "
            + Util_BLL.GetUserWhereCondition(Util_BLL.User);

            return site.ExecuteSelect(qry);

        }



        //-------------------  Get Payment Information --------------------------------------------------

        public List<SalePurchaseEntity> GetPaymentInformation(DBSite site, string payment_made_or_received,string ledger_number="")
        {
            List<SalePurchaseEntity> sale_purchase_entity_list = new List<SalePurchaseEntity>();
            SalePurchaseEntity sale_purchase_entity = null;

            string qry = " SELECT ";
            qry += "  CONVERT( VARCHAR, LedgerId ) + '-' +  CONVERT( VARCHAR, LedgerNumber ) as LedgerNumber ";
            qry += ", LDate ";
            qry += ", AccountName + ' | ' +CONVERT( VARCHAR, AccountMasterId ) as AccountName ";
            qry += ", ModeName + ' | ' +CONVERT( VARCHAR, ModeId ) as ModeName ";
            if (Int32.Parse(payment_made_or_received) == TransactionType.PaymentMade)
                qry += ", debit  amount" ;
            else
                qry += ", credit amount ";

            qry += ", Particulars ";
            qry += ", BillNumber ";
            qry += ", CheckNumber ";


            qry += " FROM vwLedger as ldg ";
                        
            qry += " WHERE SourceId= " + payment_made_or_received ;
            if (Int32.Parse(payment_made_or_received) == TransactionType.PaymentMade)
                qry += " AND drcr ='D'";
            else
                qry += " AND drcr ='C'";
            qry += " AND ldg.UserId=" + Util_BLL.User.UserId;
            qry += " AND ldg.FYear=" + Util_BLL.User.fYear;
            

            if (ledger_number != "")
            {
                qry += " AND LedgerNumber = " + ledger_number;
            }


            DataTable dt = site.ExecuteSelect(qry);
            DataRow row = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sale_purchase_entity = new SalePurchaseEntity();
                row = dt.Rows[i];

                sale_purchase_entity.Id = util.CheckNull(row["LedgerNumber"]);

                DateTime date = Convert.ToDateTime(row["LDate"]);
                sale_purchase_entity.BillDate = date.ToShortDateString();               
                sale_purchase_entity.CheckNumber = util.CheckNull(row["CheckNumber"]);
                sale_purchase_entity.particulars = util.CheckNull(row["Particulars"]);
                sale_purchase_entity.BillNumber = util.CheckNull(row["BillNumber"]);
                sale_purchase_entity.SalePurchaseAmount = util.CheckNullDouble(row["amount"]);
                sale_purchase_entity.party = util.CheckNull(row["AccountName"]);
                sale_purchase_entity.mode = util.CheckNull(row["modeName"]);


                sale_purchase_entity_list.Add(sale_purchase_entity);            
            }

                return sale_purchase_entity_list;
        }


        //--------------  Delete Payment information -----------------------------------------------------

        public void DeletePaymentEntry(DBSite site, string ledger_number)
        {
            string qry = " DELETE FROM ";
            qry += " tblLedger ";
            qry += " WHERE  LedgerNumber IN (" + ledger_number+ ")";
            qry += "AND UserId = " + Util_BLL.User.UserId;
            qry += "AND FYear = " + Util_BLL.User.fYear;

            site.Execute(qry);
        
        }



        //--------------------  Update Payment Information ----------------------------------------------


        public void UpdatePaymentInformation(DBSite site, SalePurchaseEntity salePurchase, string ledgerIdNumber )
        {


            string ledger_id = util.GetLedgerId(ledgerIdNumber);
            string ledger_number = util.GetLedgerNumber(ledgerIdNumber);

            // 444
            int party = Int32.Parse(util.GetLOVId(salePurchase.party));
            int mode = Int32.Parse(util.GetLOVId(salePurchase.mode));


            if (salePurchase.transactionType == TransactionType.PaymentMade)
            {
                int tmp;
                tmp = Int32.Parse(util.GetLOVId(salePurchase.mode));
                mode = Int32.Parse(util.GetLOVId(salePurchase.party));
                party = tmp;

            }



            // sales - party 
            string party_qry = " UPDATE tblLedger SET "
            + " LDate = '" + salePurchase.BillDate + "' "
            + ", AccountMasterId= " + mode 
            + ", modeid= " + party  
            + ", BillNumber= '" + util.GetLOVName(salePurchase.BillNumber)+"' "
            + ", Particulars= '" + salePurchase.particulars + "' "
            + ", debit = " + salePurchase.SalePurchaseAmount
            + ", CheckNumber= '" + salePurchase.CheckNumber + "'  "
            + ", subUserId = " + Util_BLL.User.Subusers[0].SubuserId 
            + Util_BLL.GetUserWhereCondition(Util_BLL.User)
           // + " AND LedgerId=" + ledger_id
            + " AND LedgerNumber=" + ledger_number
            + " AND drcr = 'D'" ;




            // sales - mode 
            string mode_qry = " UPDATE tblLedger SET "
            + " LDate = '" + salePurchase.BillDate + "' "
            + ", AccountMasterId= " + party 
            + ", modeId = " + mode 
            + ", BillNumber= '" + util.GetLOVName(salePurchase.BillNumber) + "' "
            + ", Particulars= '" + salePurchase.particulars + "' "
            + ", credit = " + salePurchase.SalePurchaseAmount
            + ", CheckNumber= '" + salePurchase.CheckNumber + "' "
            + ", subUserId = " + Util_BLL.User.Subusers[0].SubuserId 
            + Util_BLL.GetUserWhereCondition(Util_BLL.User)
            + " AND LedgerNumber=" + ledger_number
            +" AND drcr = 'C'";



            site.Execute(party_qry);
            site.Execute(mode_qry);           

        }




        //------------------  Get Sale-Purchage Information -----------------------------------------------


        public List<SalePurchaseEntity> GetSalePurchase(DBSite site, string sp_ids="")
        {

            List<SalePurchaseEntity> sale_purchase_entity_list = new List<SalePurchaseEntity>();
            SalePurchaseEntity sale_purchase_entity = null;


            //444
            string qry = " SELECT ledgerId "
                + ", Billdate "
                + ", AccountName "
                + ",  credit "  // if sales else debit
                + ", particulars "
                + ", discount "
                + " FROM tblLedger l"
                + Util_BLL.GetUserWhereCondition(Util_BLL.User);
                

            //string qry = GetQueryForSimpleFields();


            DataTable dt = dt = site.ExecuteSelect(qry);
                
               

            foreach(DataRow row in dt.Rows)
            {
                sale_purchase_entity = new SalePurchaseEntity();      //  -------- sale purchase entity  ----------
            
                //---------------  Normal Fields  ----------------------------

                sale_purchase_entity.SalePurchaseId = util.CheckNullInt(row["LedgerId"]);
                sale_purchase_entity.BillDate = util.CheckNull(row["LDate"]);
                sale_purchase_entity.party = util.CheckNull(row["AccountName"]);
                sale_purchase_entity.BillNumber = util.CheckNull(row["BillNumber"]);
                sale_purchase_entity.SalePurchaseAmount = util.CheckNullDouble(row["Debit"]);
                sale_purchase_entity.discount = util.CheckNull(row["Particulars"]);
                sale_purchase_entity.discount = util.CheckNull(row["Discount"]);
                double discount = util.GetDiscountAmount(sale_purchase_entity.discount,  sale_purchase_entity.SalePurchaseAmount);
                sale_purchase_entity.TotalAmount =  sale_purchase_entity.SalePurchaseAmount - discount ; 
                           

                //----------------  sale detail entity [ product information ]------------------




                // --------------  tax information [ taxes on the whole ] ------------------------





               sale_purchase_entity_list.Add(sale_purchase_entity);
            }

            return sale_purchase_entity_list;
        }


//-------------   Sale purchse Entity  Objects  ---------------------------------


        public List<SalePurchaseEntity> Get(DBSite site, string sp_ids = "")
        {
            List<SalePurchaseEntity> sale_purchase_entity_list = new List<SalePurchaseEntity>();

            SalePurchaseEntity sale_purchase_entity_1 = new SalePurchaseEntity
            {

                SalePurchaseId = 1001,
                party = "gurgaon branch1|4",
                BillNumber = "BN108",
                BillDate = "02/02/2012",
                SalePurchaseAmount = 3000.00,
                discount = "50%",
                TotalAmount = 1500.00,
                particulars = "Sale 1",
                SalePurchaseTaxes = new List<Tax>(

                                       new Tax[]{
                                           new Tax{
                                                     TaxId=101,
                                                     TaxName="Cst|3",
                                                     TaxPercent=0,
                                                     TaxAmount=40
                                           }
                                       }

                ),

                SalePurchaseDetails = new List<SalePurchaseDetail>(

                                        new SalePurchaseDetail[]{
                                        
                                            new SalePurchaseDetail{
                                               SalePurchaseDetailId=100,
                                               productId = "new Product 10|4",
                                               qty = 20,
                                               UOM = "Litre|6",
                                               SalePurchaseDetailAmount = 2000,
                                               discount = "30",

                                               SalePurchaseDetailsTaxes = new List<Tax>(
                                                                               new Tax[]{
                                                                               new Tax{
                                                                                         TaxId = 301,
                                                                                         TaxName="Vat|2",
                                                                                         TaxPercent = 10,
                                                                                         TaxAmount = 100   }
                                                                               }
                                               )


                                        }
                                        }
                )


            };

            SalePurchaseEntity sale_purchase_entity_2 = new SalePurchaseEntity
            {

                SalePurchaseId = 1002,
                party = "newaccount 1|21",
                BillNumber = "BN109",
                BillDate = "02/05/2012",
                SalePurchaseAmount = 2000.00,
                discount = "50%",
                TotalAmount = 1000.00,
                particulars = "Sale 2",


                SalePurchaseTaxes = new List<Tax>(

                                         new Tax[]{
                                           new Tax{
                                                     TaxId=102,
                                                     TaxName="Cst|3",
                                                     TaxPercent=0,
                                                     TaxAmount=50
                                           }
                                       }

                  ),

                SalePurchaseDetails = new List<SalePurchaseDetail>(

                                        new SalePurchaseDetail[]{
                                        
                                            new SalePurchaseDetail{
                                               SalePurchaseDetailId=101,
                                               productId = "new Product 10|4",
                                               qty = 30,
                                               UOM = "Litre|6",
                                               SalePurchaseDetailAmount = 2000,
                                               discount = "20",

                                               SalePurchaseDetailsTaxes = new List<Tax>(
                                                                               new Tax[]{
                                                                               new Tax{
                                                                                         TaxId = 302,
                                                                                         TaxName="Vat|2",
                                                                                         TaxPercent = 0,
                                                                                         TaxAmount = 50   }
                                                                               }
                                               )


                                        }
                                        }
                )

            };

            sale_purchase_entity_list.Add(sale_purchase_entity_1);
            sale_purchase_entity_list.Add(sale_purchase_entity_2);

            return sale_purchase_entity_list;
        }




        //----------------  Get Query For Sale Detail   -------------------------------

        //protected string GetQueryForSimpleFields()
        //{
        //    string qry = "SELECT DISTINCT";

        //    qry += " LedgerId, LDate, AccountName + '|' + CONVERT( VARCHAR, acc.AccountMasterId) as AccountName , ";

        //    qry+=" Particulars, Debit, BillNumber, ldg.SourceId, ldg.Discount ";

    



        //    qry += " FROM ( tblLedger as ldg ";

        //    qry+="LEFT OUTER JOIN   tblAccountMaster as acc ON ldg.AccountMasterId = acc.AccountMasterId )";

        //    qry += " LEFT OUTER JOIN tblProductLedger as prd ON ldg.LedgerNumber = prd.LedgerNumber ";

        //    qry += " WHERE ldg.UserId = " + User.userID;
        //    qry += " AND Credit IS NOT NULL";                     


        //    return qry;
        //}

        //----------------  Get Query  For Tax Information [ Whole Taxes ]  -------------------------------

        //protected string GetQueryForTaxesOnTheWhole()
        //{

        //    string qry = " SELECT ";

        //    qry += " tm.TaxName + '|' + CONVERT( VARCHAR , tm.TaxId ) as TaxName , tax.TaxAmount ";

        //    qry += " FROM (  tblLedger as ldg ";

        //    qry += " LEFT OUTER JOIN  tblTax as tax ON ldg.LedgerNumber = tax.LedgerNumber ) ";

        //    qry += " LEFT OUTER JOIN tblTaxMaster as tm ON tax.TaxMasterId = tm.TaxId ";

        //    qry+="WHERE tax.ProductLedgerNumber = NULL AND ldg.UserId = " +User.userID;


        //    return qry;
        //}




        //----------------  Get Query For [ Associated Product ] -----------------------------------


        //protected string GetQueryForAssociatedProducts()
        //{
        //    string qry = " SELECT ";

        //    qry += " ";


        //    return qry;
        //}

        
        
        public List<string> GetAccountListWithCashOrBankAsGroup(DBSite site, bool accounts_with_group_bank_only )
        {
            List<string> account_list = new List<string>();

            string qry = " SELECT ";
            qry += " AccountName " + " + ' | ' + CONVERT( VARCHAR, AccountMasterId ) as  AccountName ";
            qry += " FROM tblAccountMaster as acc ";
            qry += " LEFT OUTER JOIN ";
            qry += " tblGroup as grp ";
            qry += " ON acc.GroupId = grp.GroupId ";
            qry += " WHERE grp.GroupName = 'bank' ";
            if (!accounts_with_group_bank_only)
            {
                qry += " OR grp.GroupName = 'cash'  ";
            }
            qry += " AND acc.UserId = " + Util_BLL.User.UserId;


            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                account_list.Add(util.CheckNull(row["AccountName"]));
            }

            return account_list;
        }


      //-----------------  Search Method [ For Payment ] ----------------------------

        public List<SalePurchaseEntity> GetMatchedRecords(DBSite site ,  string payment_made_or_received, string value_to_search )
        {
             List<SalePurchaseEntity> sale_purchase_entity_list = new List<SalePurchaseEntity>();
             SalePurchaseEntity sale_purchase_entity = null;

             string qry = " SELECT DISTINCT LedgerNumber FROM tblLedger as ldg "
                 + " LEFT OUTER JOIN tblAccountMaster as acc"
                 + " ON ldg.AccountMasterId = acc.AccountMasterId " 
             + " WHERE ( "
             + " ( BillNumber LIKE '%" + value_to_search + "%')"
             + " OR ( CheckNumber LIKE '%" + value_to_search + "%')"
             +" OR ( AccountName LIKE '%" + value_to_search + "%')";

             if (IsDate(value_to_search))
             {
                 qry += " OR ( LDate = '" + value_to_search + "' ) ";
             }

             
            if (util.IsNumber(value_to_search))
                {
                    qry += " OR ( Credit = '" + value_to_search + "' ) ";
                    qry += " OR( Debit = '" + value_to_search + "' ) ";                   
                }

            qry += " )";
            qry += " AND ldg.UserId = " + Util_BLL.User.UserId;
            qry += " AND ldg.FYear = " + Util_BLL.User.fYear;
            qry += " AND ldg.SourceId = " + payment_made_or_received;

            string ledger_number=null;
            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                sale_purchase_entity = new SalePurchaseEntity();
                ledger_number = util.CheckNull(row["LedgerNumber"]);
                sale_purchase_entity = GetPaymentInformation(site, payment_made_or_received, ledger_number)[0];

                sale_purchase_entity_list.Add(sale_purchase_entity);
            }

            return sale_purchase_entity_list;
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


        public List<string> GetUomlistOfProducts(DBSite site)
        {
            List<string> uom_list = new List<string>();

            string qry = "SELECT "
                + " CONVERT( VARCHAR, ProductMasterId) +'-'+ uom.UnitName as UnitName"
                + " FROM tblProductMaster as prdt "
                + " LEFT OUTER JOIN tblUOM as uom "
                + " ON prdt.UOM = uom.UOMId "
                + " WHERE prdt.UserId = " + Util_BLL.User.UserId;


            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow dr in dt.Rows)
            {
                uom_list.Add(util.CheckNull(dr["UnitName"]));
            }

            return uom_list;
        }



        //- -----------------------  Save Issue Information -------------------------------------------------------------------

        public void SaveIssueInformation(DBSite site, SalePurchaseDetail from_product, SalePurchaseDetail to_product)
        {

            int productLedgerNumber = util.GetAutoNumber(site, "ProductLedgerNumber");

            string qry = "";

            qry = " INSERT INTO tblProductLedger( ProductLedgerNumber, drcr , BillDate, ProductID, Soldqty, LocationID, ProductID2, BoughtQty2, LocationID2, SourceId,UserID,subuserId, FYear) "
                + " VALUES ( "             
                + productLedgerNumber +" "
                +", 'C' "
                + ", '" + from_product.MovementDate + "'"              
              
                + ", " + from_product.productAutoId
                + ", " + from_product.qty
                + ", " + from_product.locationId

                + ", " + to_product.productAutoId
                + ", " + to_product.qty
                + ", " + to_product.locationId
                
                
                + ", " + TransactionType.StockMovement
                + ", " + util.GetUserInsertQry(Util_BLL.User) //--- current user
                + " ) ";

            site.Execute(qry);



            qry = " INSERT INTO tblProductLedger( ProductLedgerNumber, drcr , BillDate , ProductID, BoughtQty, LocationID, ProductID2, SoldQty2, LocationID2, SourceId, UserID, subuserId, FYear) "
                + " VALUES ( "
                + productLedgerNumber 
                 + ", 'D' "
                + ", '" + to_product.MovementDate + "'"

                + ", " + to_product.productAutoId
                + ", " + to_product.qty
                + ", " + to_product.locationId

                + ", " + from_product.productAutoId
                + ", " + from_product.qty
                + ", " + from_product.locationId
               
                
                
                + ", " + TransactionType.StockMovement
                + ", " + util.GetUserInsertQry(Util_BLL.User) //-------  surrent user --------
                + " ) ";

            site.Execute(qry);

        }

        //----------------  Get Issue Information -------------------------------------------------------------


        


        //------------  Delete Issue Info --------------------------------

        public void DeleteIssueInfo(DBSite site, string product_ledger_number)
        {
            string qry = " DELETE FROM ";
            qry += " tblProductLedger ";
            qry += " WHERE  ProductLedgerNumber IN (" + product_ledger_number + ")";
            qry += "AND UserId = " + Util_BLL.User.UserId;
            qry += "AND FYear = " + Util_BLL.User.fYear;
            qry += "AND SourceId = " + TransactionType.StockMovement;

            site.Execute(qry);
        }


        //--------------  Edit Issuse Information --------------------------------


        public void EditIssueInformation(DBSite site, SalePurchaseDetail from_product, SalePurchaseDetail to_product, string product_ledger_number)
        {
            string qry = " ";

            qry = " UPDATE tblProductLedger SET "
                + " BillDate = '" + from_product.MovementDate + " '"
                + ", ProductId = " + from_product.productAutoId
                + ", SoldQty = " + from_product.qty
                + ", LocationId = " + from_product.locationId
                + Util_BLL.GetUserWhereCondition(Util_BLL.User);

            qry += " AND ProductLedgerNumber = " + product_ledger_number
                + " AND SourceId = " + TransactionType.StockMovement
                + " AND drcr = 'C'";


            site.Execute(qry);


            qry = " UPDATE tblProductLedger SET "
              + " BillDate = '" + to_product.MovementDate + " '"
              + ", ProductId = " + to_product.productAutoId
              + ", BoughtQty = " + to_product.qty
              + ", LocationId = " + to_product.locationId
              + Util_BLL.GetUserWhereCondition(Util_BLL.User);

            qry += " AND ProductLedgerNumber = " + product_ledger_number
                + " AND SourceId = " + TransactionType.StockMovement
                + " AND drcr = 'D'";
            

            site.Execute(qry);
                
        }

//-------------- Matched Issue Information -----------------------------------------------------------

        public List<SalePurchase.SalePurchaseEntity > GetMatchedIssueInformation(DBSite site, string value_to_search)
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




        public bool ValidDateEntry(DBSite site, string acc_id, string payment_date)
        {
            string qry = " SELECT CreationDate FROM tblAccountMaster "
                       + " WHERE AccountMasterId=" + acc_id;                    

           DataTable dt=site.ExecuteSelect(qry);
           DataRow row=dt.Rows[0];

           DateTime date = Convert.ToDateTime(row["CreationDate"]);
           string acc_creation_date = date.ToShortDateString();

           return util.isDateRangeValid(acc_creation_date, payment_date);
        }


        
    }
}