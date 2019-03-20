using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class TaxMasterBLL
    {
       
        Util_BLL util = new Util_BLL();
        

        public class TaxEntity
        {
            public int TaxId { get; set; }
            public string  TaxName { get; set; }
            public decimal TaxPercent { get; set; }
            public decimal TaxAmount { get; set; }
        }


//----------------- Save Tax --------------------------------

        public void SaveInfo(DBSite site, TaxEntity tax)
        {

            string qry = " INSERT INTO "
                + " tblTaxMaster"
                + "( "
                + "TaxName"
                + ", TaxPercent"
                + ", TaxAmount"
                + ", UserId, subuserId, FYear )"
                + " VALUES( '" + tax.TaxName + "'"
                + ", " + tax.TaxPercent
                + ", " + tax.TaxAmount
                + ", " + util.GetUserInsertQry(Util_BLL.User) //------  user insert query ----------
                + ")";

            site.Execute(qry);      
        }


  // ------------  Get Tax Info -------------------------------------

        public List<TaxEntity> GetInfo(DBSite site,string tax_id="")
        {
            List<TaxEntity> tax_list = new List<TaxEntity>();
            TaxEntity tax = null;

            string qry=" SELECT "
                +"TaxId"
                +", TaxName"
                +", TaxPercent"
                +", TaxAmount"
                +" FROM tblTaxMaster"
                +" WHERE "
                + " UserId =" + Util_BLL.User.UserId
                + " AND FYear=" + Util_BLL.User.fYear;

            if (tax_id != "")
                 qry += " AND TaxId=" + tax_id;

            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow row in dt.Rows)
            {
                tax = new TaxEntity();

                tax.TaxId = util.CheckNullInt(row["TaxId"]);
                tax.TaxName = util.CheckNull(row["TaxName"]);
                tax.TaxPercent = util.CheckNullDecimal(row["TaxPercent"]);
                tax.TaxAmount = util.CheckNullDecimal(row["TaxAmount"]);

                tax_list.Add(tax);
            }
               
            return tax_list;
        }
 
// ------------------ Edit Tax Info ---------------------------------------

        public void EditInfo(DBSite site, TaxEntity tax, string tax_id)
        {

            string qry = " UPDATE tblTaxMaster SET"
                + " TaxName='" + tax.TaxName + "'"
                + ", TaxAmount=" + tax.TaxAmount
                + ", TaxPercent=" + tax.TaxPercent
                + ", subUserId =" + Util_BLL.User.Subusers[0].SubuserId 
                + " WHERE UserId=" + Util_BLL.User.UserId
                + " AND FYear=" + Util_BLL.User.fYear
                +" AND TaxId=" + tax_id;

            site.Execute(qry);
        }

//----------- Delete Tax Info -------------------------------------------

        public void DeleteInfo(DBSite site, string tax_ids)
        {
                    string qry=" DELETE FROM tblTaxMaster "
                           + " WHERE UserId=" + Util_BLL.User.UserId
                           + " AND FYear=" + Util_BLL.User.fYear
                           + " AND TaxId IN (" + tax_ids+ ")";
                    site.Execute(qry);
        }

        public List<TaxEntity> GetMatches(DBSite site, string value_to_search)
        {
            List<TaxEntity> tax_list = new List<TaxEntity>();
            TaxEntity tax = null;
                 

            string qry = " SELECT "
                + " TaxId"
                + ", TaxName"
                + ", TaxAmount"
                + ", TaxPercent"
                + " FROM tblTaxMaster"
                + " WHERE "
                + " UserId =" + Util_BLL.User.UserId
                + " AND FYear=" + Util_BLL.User.fYear + " AND"
                + "(";

            qry += " (TaxName LIKE '%" + value_to_search + "%') ";

            if (util.isNumeric(value_to_search))
            {
                qry += "OR ( TaxAmount ='" + value_to_search + "')";
                qry += " OR ( TaxPercent ='" + value_to_search + "')";
            }

            qry += ")";


             DataTable dt = site.ExecuteSelect(qry);

             foreach (DataRow row in dt.Rows)
             {
                 tax = new TaxEntity();

                 tax.TaxId = util.CheckNullInt(row["TaxId"]);
                 tax.TaxName = util.CheckNull(row["TaxName"]);
                 tax.TaxPercent = util.CheckNullDecimal(row["TaxPercent"]);
                 tax.TaxAmount = util.CheckNullDecimal(row["TaxAmount"]);

                 tax_list.Add(tax);
             }

            return tax_list;
        }

    }
}