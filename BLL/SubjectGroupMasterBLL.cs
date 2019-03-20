using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;


namespace AccountingSoftware.BLL
{
    public class SubjectGroupMasterBLL
    {
        
        Util_BLL util = new Util_BLL();

        public class SubjectGroupMasterEntity
        {
            public int SubjectGroupMasterId { get; set; }
            public string SubjectGroupName { get; set; }
            public string SubjectGroupType { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }


        public List<SubjectGroupMasterEntity> GetSubjectGroup(DBSite site, int userId, string id = "")
        {
            List<SubjectGroupMasterEntity> groups =  new List<SubjectGroupMasterEntity>();


            string qry = "";
            qry += "SELECT ";
            qry += " SubjectGroupMasterId, SubjectGroupName, SubjectGroupType, UserID, FYear ";
            qry += " FROM tblSubjectGroupMaster sgm ";

            qry += " WHERE sgm.UserId = " + Util_BLL.User.UserId;

            if (id != string.Empty)
                qry += "AND  SubjectGroupMasterId = " + id;


            qry += " ORDER BY SubjectGroupName ";

            DataTable dt = site.ExecuteSelect(qry);
            SubjectGroupMasterEntity sgrp;

            foreach (DataRow dr in dt.Rows)
            {
                sgrp = new SubjectGroupMasterEntity();

                sgrp.SubjectGroupMasterId = util.CheckNullInt(dr["SubjectGroupMasterId"].ToString());
                sgrp.SubjectGroupName = util.CheckNull(dr["SubjectGroupName"]);
                sgrp.SubjectGroupType = util.CheckNull(dr["SubjectGroupType"]);
                
                sgrp.UserID = util.CheckNullInt(dr["UserID"]);
                sgrp.FYear = util.CheckNullInt(dr["FYear"]);

                groups.Add(sgrp);

            }

            return groups;
        }
        
        

    }





}