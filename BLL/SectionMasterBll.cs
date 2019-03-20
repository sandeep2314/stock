using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class SectionMasterBll
    {


        Util_BLL util = new Util_BLL();

        public class SectionMasterEntity
        {
            public int SectionMasterId { get; set; }
            public string SectionName { get; set; }
            public int SectionOrder { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }


        public void SaveClassMaster(DBSite site, SectionMasterEntity sec)
        {


            string qry = "INSERT INTO tblSectionMaster(SectionName, SectionOrder ";
            qry += " , UserID, FYear)  VALUES(";
            qry += "'" + sec.SectionName  + "'";
            qry += ", " + sec.SectionOrder ;
            qry += ", " + util.GetUserInsertQryMaster(Util_BLL.User);
             
            qry += " )";
            site.Execute(qry);

        }



        public void UpdateSectionFrm(DBSite site, SectionMasterEntity sec)
        {

            string qry = "UPDATE tblSectionMaster SET ";
            qry += " SectionName ='" + sec.SectionName + "'";
            qry += ", SectionOrder=" + sec.SectionOrder  + "";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);  
            qry += " AND SectionMasterID=" + sec.SectionMasterId ;

            site.Execute(qry);

        }

        public void DeleteSection(DBSite site, string ids)
        {

            string qry = "DELETE FROM  tblSectionMaster";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); 
            qry += " AND SectionMasterID IN (" + ids + ")";

            site.Execute(qry);
        }


        public List<SectionMasterEntity> GetSectionList(DBSite site, int userId, string id = "")
        {
            List<SectionMasterEntity> sectionList = new List<SectionMasterEntity>();

            string qry = "";
            qry += "SELECT ";
            qry += " SectionMasterID, SectionName, SectionOrder, UserID, FYear ";
            qry += " FROM tblSectionMaster sec ";

            qry += " WHERE sec.UserId = " + Util_BLL.User.UserId;

            if (id != string.Empty)
                qry += "AND  SectionMasterID = " + id;


            qry += " ORDER BY SectionOrder ";

            DataTable dt = site.ExecuteSelect(qry);
            SectionMasterEntity sec;

            foreach (DataRow dr in dt.Rows)
            {
                sec = new SectionMasterEntity();

                sec.SectionMasterId = util.CheckNullInt(dr["SectionMasterID"].ToString());
                sec.SectionName = util.CheckNull(dr["SectionName"]);
                sec.SectionOrder = util.CheckNullInt(dr["SectionOrder"]);
                
                sec.UserID = util.CheckNullInt(dr["UserID"]);
                

                sec.FYear = util.CheckNullInt(dr["FYear"]);

                sectionList.Add(sec);

            }

            return sectionList;
        }



    }
}