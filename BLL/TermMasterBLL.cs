using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class TermMasterBLL
    {

        Util_BLL util = new Util_BLL();

        public class TermMasterEntity
        {
            public int TermMasterId { get; set; }
            public string TermName { get; set; }
            public string TermStartDate { get; set; }
            public string TermEndDate { get; set; }

            public int UserID { get; set; }
            public int FYear { get; set; }

        }



        public List<TermMasterEntity> GetTermList(DBSite site, int userId, string id = "")
        {
            List<TermMasterEntity> termList = new List<TermMasterEntity>();

            string qry = "";
            qry += "SELECT "
             + " TermId, TermName, TermStartDate, TermEndDate, UserID, FYear "
             + " FROM tblTerm tm "

             + " WHERE tm.UserId = " + Util_BLL.User.UserId
             + " ";

            if (id != string.Empty)
                qry += "AND  TermId = " + id;


            //qry += " ORDER BY TermStartDate ";

            DataTable dt = site.ExecuteSelect(qry);
            TermMasterEntity term;

            foreach (DataRow dr in dt.Rows)
            {
                term = new TermMasterEntity();


                term.TermMasterId = util.CheckNullInt(dr["TermId"]);
                term.TermName = util.CheckNull(dr["TermName"]);
                term.TermStartDate = util.CheckNullDate(dr["TermStartDate"]);
                term.TermEndDate = util.CheckNullDate(dr["TermEndDate"]);
                term.UserID = util.CheckNullInt(dr["UserID"]);
                term.FYear = util.CheckNullInt(dr["FYear"]);

                termList.Add(term);

            }

            return termList;
        }



        public void SaveTerm(DBSite site, TermMasterEntity trm)
        {


            string qry = "INSERT INTO tblTerm(TermName, TermStartDate,  TermEndDate,";
            qry += " UserID, FYear)  VALUES(";
            qry += "'" + trm.TermName + "'";
            qry += ", '" + trm.TermStartDate + "'";
            qry += ", '" + trm.TermEndDate + "'";
            qry += ", " + util.GetUserInsertQryMaster(Util_BLL.User);

            qry += " )";

            site.Execute(qry);
        }



        public void UpdateTerm(DBSite site, TermMasterEntity trm)
        {

            string qry = "UPDATE tblTerm SET ";
            qry += " TermName ='" + trm.TermName + "'";
            qry += ", TermStartDate=" + trm.TermStartDate + "";
            qry += ", TermEndDate=" + trm.TermEndDate + "";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);  //-------------  gwt user where condition --------------
            qry += " AND TermID=" + trm.TermMasterId;

            site.Execute(qry);

        }

        public void DeleteTerm(DBSite site, string ids)
        {
            String qry = "DELETE FROM  tblTerm";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); // --------get user where condition  -----------------
            qry += " AND TermID IN (" + ids + ")";

            site.Execute(qry);
        }


    }

}