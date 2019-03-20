using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class ProductMasterBLL
    {
        Util_BLL util = new Util_BLL();
        

        public class ProductMasterEntity
        {

            public int ProductMasterId { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal OpeningBalance { get; set; }


            public DateTime  productDate { get; set; }

            public decimal CostPrice { get; set; }
            public decimal SellingPrice { get; set; }

            public int UOM { get; set; }

            public int AccountId { get; set; }
            public int LocationId { get; set; }

            public string UOMName { get; set; }
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }

            public decimal ReOrderQty { get; set; }
            public string ProductDescription { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }
            public void SaveProductMasterData(DBSite site,ProductMasterEntity pme)
            {

                string qry = "INSERT INTO tblProductMaster( ProductID, ProductName, OpeningBalance,  ProductDate, CostPrice, SellingPrice, UOM, CategoryID";
                qry += ", AccountId, LocationId, ReOrderQty ,ProductDescription, UserID, SubuserId , FYear)  VALUES(";

                qry += "'" + pme.ProductId + "'";
                qry += ",'" + pme.ProductName + "'";
                qry += ", " + pme.OpeningBalance;
                qry += ", '" + pme.productDate + "'";
                qry += ", " + pme.CostPrice;
                
                qry += ", " + pme.SellingPrice;
                qry += ", " + pme.UOM;
                qry += ", " + pme.CategoryID;
                qry += ", " + pme.AccountId;
                qry += ", " + pme.LocationId;
                qry += ", " + pme.ReOrderQty;
                qry += ", '" + pme.ProductDescription + "'";
                qry +=", "+ util.GetUserInsertQry(Util_BLL.User);

                //qry += ", " + Util_BLL.User.UserId;
                //qry += ", " + Util_BLL.User.fYear;

                qry += ")";

               
                    site.Execute(qry);             

            }


            public void EditProductMasterData(DBSite site, ProductMasterEntity pme)
            {
                string qry = " UPDATE tblProductMaster SET ";
                qry += " ProductID='" + pme.ProductId + "'";
                qry += ", ProductName='" + pme.ProductName + "'";
                qry += ", OpeningBalance=" + pme.OpeningBalance;
                qry += ", ProductDate='" + pme.productDate +"'";
                qry += ", CostPrice=" + pme.CostPrice;
                qry += ", SellingPrice=" + pme.SellingPrice;
                qry += ", UOM=" + pme.UOM + "";
                qry += ", CategoryID=" + pme.CategoryID;
                qry += ", AccountId=" + pme.AccountId ;
                qry += ", LocationId=" + pme.LocationId ;
                qry += ", SubuserId=" + Util_BLL.SubUser.SubuserId ;
                qry += ", ReOrderQty=" + pme.ReOrderQty ;
                qry += ", ProductDescription='" + pme.ProductDescription + "'";

                qry += " WHERE ProductMasterId=" + pme.ProductMasterId;


               
                site.Execute(qry);
                
            }

            

          

            public List<ProductMasterEntity> GetProductMasterData(DBSite site)
            {
                return GetProductMasterData(site, "");
            }



            public List<ProductMasterEntity> GetProductMasterData(DBSite site, string id = "")
            {
                List<ProductMasterEntity> productMasterList = new List<ProductMasterEntity>();

                string qry = "";
                qry += " SELECT ";
                qry += " ProductMasterId, ProductID,  ProductName, OpeningBalance, ProductDate, CostPrice, SellingPrice ";
                qry += " , ReOrderQty , ProductDescription, product.UserID, FYear, product.CreatedAt ";

                qry += ", category.CategoryId CategoryId ";
                qry += ", CategoryName ";
                
                qry += ", uom.UOMId UOMId ";
                qry += ", product.LocationId LocationId ";
                qry += ", product.AccountId AccountId ";
                qry += ", UnitName  ";
                qry += " FROM ( tblProductMaster as product ";
                qry += " LEFT OUTER JOIN tblProductCategory as category ON ";
                qry += " product.CategoryId = category.CategoryId )";
                qry += " LEFT OUTER JOIN tblUOM as uom ON ";
                qry += " product.UOM= uom.UOMId ";


                qry += " WHERE product.UserID =  " + Util_BLL.User.UserId;
                if (id != string.Empty)
                    qry += " AND  ProductMasterId = " + id;

                    DataTable dt = site.ExecuteSelect(qry);
                    ProductMasterEntity pme;

                    foreach (DataRow dr in dt.Rows)
                    {
                        pme = new ProductMasterEntity();


                        pme.ProductMasterId = util.CheckNullInt(dr["ProductMasterId"]);
                        pme.ProductId = util.CheckNull(dr["ProductID"]);
                        pme.ProductName = util.CheckNull(dr["ProductName"]);
                        pme.OpeningBalance = util.ToDecimal(util.CheckNullDecimal(dr["OpeningBalance"]));

                        DateTime date = Convert.ToDateTime(dr["ProductDate"]);

                        pme.productDate = date;


                        pme.CostPrice = util.ToDecimal(util.CheckNullDecimal(dr["CostPrice"]));
                        pme.SellingPrice = util.ToDecimal(util.CheckNullDecimal(dr["SellingPrice"]));

                        pme.UOM = util.CheckNullInt(dr["UOMId"]);
                        pme.UOMName = util.CheckNull(dr["UnitName"]);
                        pme.CategoryID = util.CheckNullInt(dr["CategoryID"]);
                        pme.CategoryName = util.CheckNull(dr["CategoryName"]);

                        pme.AccountId = util.CheckNullInt(dr["AccountId"]);
                        pme.LocationId  = util.CheckNullInt(dr["LocationId"]);

                        pme.ReOrderQty = util.CheckNullDecimal(dr["ReOrderQty"]);

                        pme.ProductDescription = util.CheckNull(dr["ProductDescription"]);
                        pme.UserID = util.CheckNullInt(dr["UserID"]);
                        pme.FYear = util.CheckNullInt(dr["FYear"]);

                        productMasterList.Add(pme);
                    }
               

                return productMasterList;
            }



            public void DeleteProduct(DBSite site, string ids)
            {

                string qry = "DELETE FROM  tblProductMaster";
                qry += " WHERE ProductMasterId IN (" + ids+ ")";               

                site.Execute(qry);
            }



            public bool IsDuplicate(DBSite site, string productId)
            {
                string qry = " SELECT ProductId FROM tblProductMaster ";
                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
                qry += " AND ProductId = '" + productId+"'";
                DataTable dt = null;
                dt = site.ExecuteSelect(qry);

                return dt.Rows.Count > 0;
            }

            public bool IsProductPresentInProductLedger(DBSite site,  int productMasterId)
            {
                string qry = " SELECT ProductId FROM tblProductLedger ";
                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
                qry += " AND ProductId = '" + productMasterId + "'";
                DataTable dt = null;
                dt = site.ExecuteSelect(qry);

                return dt.Rows.Count > 0;
            }


        



        public List<ProductMasterBLL.ProductMasterEntity> GetMatchedRecords(DBSite site, string value_to_search)
        {

            List<ProductMasterBLL.ProductMasterEntity> selected_product_list = new List<ProductMasterBLL.ProductMasterEntity>();

            string qry = "";
            qry += " SELECT ";
            qry += " ProductMasterId, ProductID,  ProductName, OpeningBalance, CostPrice, SellingPrice ";
            qry += " , ReOrderQty, ProductDescription, product.UserID, FYear, product.CreatedAt ";
            qry += ", category.CategoryId CategoryId ";
            qry += ", CategoryName ";
            qry += ", uom.UOMId UOMId ";
            qry += ", product.LocationId LocationId ";
            qry += ", product.AccountId AccountId ";
            qry += ", UnitName  ";
            qry += " FROM ( tblProductMaster as product ";
            qry += " LEFT OUTER JOIN tblProductCategory as category ON ";
            qry += " product.CategoryId = category.CategoryId )";
            qry += " LEFT OUTER JOIN tblUOM as uom ON ";
            qry += " product.UOM= uom.UOMId ";
            //------------ selection condition ----------------------
            qry += " WHERE product.UserID =  " + Util_BLL.User.UserId + " AND ";
            qry += " (( ProductID LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( ProductName LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( UnitName LIKE '%" + value_to_search + "%' ) OR";
            qry += " ( CategoryName LIKE '%" + value_to_search + "%' ) ";

            if (util.IsNumber(value_to_search))
            {

                qry += "OR ( OpeningBalance = '" + value_to_search + "' ) OR";
                qry += " ( CostPrice = '" + value_to_search + "' ) OR";
                qry += " ( SellingPrice = '" + value_to_search + "' ) ";

            }
            qry += " ) ";
 

            DataTable dt = site.ExecuteSelect(qry);
            ProductMasterEntity pme;

            foreach (DataRow dr in dt.Rows)
            {
                pme = new ProductMasterEntity();


                pme.ProductMasterId = util.CheckNullInt(dr["ProductMasterId"]);
                pme.ProductId = util.CheckNull(dr["ProductID"]);
                pme.ProductName = util.CheckNull(dr["ProductName"]);
                pme.OpeningBalance = util.ToDecimal(util.CheckNullDecimal(dr["OpeningBalance"]));
                pme.CostPrice = util.ToDecimal(util.CheckNullDecimal(dr["CostPrice"]));
                pme.SellingPrice = util.ToDecimal(util.CheckNullDecimal(dr["SellingPrice"]));
                pme.UOM = util.CheckNullInt(dr["UOMId"]);
                pme.UOMName = util.CheckNull(dr["UnitName"]);
                pme.CategoryID = util.CheckNullInt(dr["CategoryID"]);
                pme.CategoryName = util.CheckNull(dr["CategoryName"]);
                pme.AccountId = util.CheckNullInt(dr["AccountId"]);
                pme.LocationId = util.CheckNullInt(dr["LocationId"]);
                pme.ReOrderQty = util.CheckNullDecimal(dr["ReOrderQty"]);
                pme.ProductDescription = util.CheckNull(dr["ProductDescription"]);
                pme.UserID = util.CheckNullInt(dr["UserID"]);
                pme.FYear = util.CheckNullInt(dr["FYear"]);

                selected_product_list.Add(pme);
            }


            return selected_product_list;
        }


        }

    }