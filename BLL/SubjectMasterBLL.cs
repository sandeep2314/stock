using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;



namespace AccountingSoftware.BLL
{
    public class SubjectMasterBLL
    {
          Util_BLL util = new Util_BLL();
        
        public class SubjectMasterEntity
        {
          
            public int SubjectMasterId { get; set; }
            public string SubjectName { get; set; }
            public string SubjectCode { get; set; }

            public int SubjectClassID { get; set; }
            public string SubjectClassName { get; set; }

            public int SubjectSectionID { get; set; }
            public string SubjectSectionName { get; set; }
            
            public int SubjectGroupID { get; set; }
            public string SubjectGroupName { get; set; }
            public string SubjectGroupType { get; set; }

            public int SubjectOrder { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

        public void SaveSubject(DBSite site, SubjectMasterEntity subject)
        {
            string qry = "INSERT INTO tblSubjectMaster(SubjectName, SubjectCode, SubjectGroupMasterId, SubjectClassID, ";
            qry += " SubjectSectionID, SubjectOrder, UserID, FYear)  VALUES(";
            qry += "'" + subject.SubjectName + "'";
            qry += ", '" + subject.SubjectCode + "'";
            qry += ", " + subject.SubjectGroupID ;
            qry += ", " + subject.SubjectClassID ;
            qry += ", " + subject.SubjectSectionID ;
            qry += ", " + subject.SubjectOrder ;

            qry += ", " + util.GetUserInsertQryMaster(Util_BLL.User);

            qry += " )";

            site.Execute(qry);                

        }


        public void UpdateSubject(DBSite site, SubjectMasterEntity subject)
        {
            string qry = "UPDATE tblSubjectMaster SET ";
            qry += " SubjectName ='" + subject.SubjectName  + "'";
            qry += ", SubjectCode ='" + subject.SubjectCode + "'";
            qry += ", SubjectGroupMasterId=" + subject.SubjectGroupID;
            qry += ", SubjectClassID =" + subject.SubjectClassID ;
            qry += ", SubjectSectionID =" + subject.SubjectSectionID ;

            qry += ", SubjectOrder=" + subject.SubjectOrder ;
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);  
            qry += " AND SubjectMasterID=" + subject.SubjectMasterId ;

            site.Execute(qry);
        }


        public void DeleteSubject(DBSite site, string ids)
        {

            string qry = "DELETE FROM  tblSubjectMaster";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); 
            qry += " AND SubjectMasterID IN (" + ids + ")";

            site.Execute(qry);
        }

        public List<SubjectMasterEntity> GetSubjects(DBSite site, int userId, string id = "")
        {
            return GetSubjectsBySubUser(site, userId, id, false);
        }

        public List<SubjectMasterEntity> GetSubjectsBySubUser(DBSite site, int userId, string id, bool BySubUser)
        {
            List<SubjectMasterEntity> subjects = new List<SubjectMasterEntity>();

            string qry = "";
            qry += "SELECT ";
            qry += " SubjectMasterId, SubjectName ";
            qry += " , SubjectOrder, SubjectCode";
            
            qry += ", sub.SubjectGroupMasterId ";
            qry += ", SubjectGroupName ";
            qry += ", SubjectGroupType ";

            qry += ", sub.SubjectClassId ";
            qry += ", ISNull(ClassName, 'All') ClassName ";

            qry += ", sub.SubjectSectionId ";
            qry += ", IsNull(SectionName, 'All') SectionName ";
            qry += " , sub.UserID, sub.FYear ";


            qry += " FROM tblSubjectMaster sub ";
            qry += " LEFT OUTER JOIN tblSubjectGroupMaster sgp ON ";
            qry += " sub.SubjectGroupMasterId = sgp.SubjectGroupMasterId ";
            qry += " LEFT OUTER JOIN tblClassMaster cm ON ";
            qry += " cm.ClassMasterID = sub.SubjectClassId ";
            qry += " LEFT OUTER JOIN tblSectionMaster sm ON ";
            qry += " sm.SectionMasterId = sub.SubjectSectionID ";

            if (BySubUser)
            {
                qry += " INNER JOIN tblTeacherSubjects ts ON ts.SubjectId = sub.SubjectMasterID "
                + "AND ts.UserId=sub.userID AND ts.SubUserId=" + Util_BLL.SubUser.SubuserId;
            }

            
            qry += " Where sub.UserId = " + Util_BLL.User.UserId;



            if (id != string.Empty)
                qry += "AND  SubjectMasterId = " + id;


            DataTable dt = site.ExecuteSelect(qry);
            SubjectMasterEntity sub;

            foreach (DataRow dr in dt.Rows)
            {
                sub = new SubjectMasterEntity();

                sub.SubjectMasterId = util.CheckNullInt(dr["SubjectMasterId"]);
                sub.SubjectName = util.CheckNull( dr["SubjectName"]);

                sub.SubjectGroupID = util.CheckNullInt(dr["SubjectGroupMasterId"]);
                sub.SubjectGroupName = util.CheckNull(dr["SubjectGroupName"]);
                sub.SubjectGroupType = util.CheckNull(dr["SubjectGroupType"]);

                sub.SubjectClassID = util.CheckNullInt(dr["SubjectClassId"]);
                sub.SubjectClassName = util.CheckNull(dr["ClassName"]);

                sub.SubjectSectionID = util.CheckNullInt(dr["SubjectSectionID"]);
                sub.SubjectSectionName = util.CheckNull(dr["SectionName"]);

                sub.SubjectCode = util.CheckNull(dr["SubjectCode"]);

                sub.SubjectOrder = util.CheckNullInt(dr["SubjectOrder"]);

                sub.UserID = util.CheckNullInt(dr["UserID"]);
                sub.FYear = util.CheckNullInt(dr["FYear"]);

                

                subjects.Add(sub);
            }


            return subjects;

        }


        public List<SubjectMasterEntity> GetMatchedRecords(DBSite site, string value_to_search)
        {

            List<SubjectMasterEntity> subjects = new List<SubjectMasterEntity>();


            return subjects;

        }

        public string GetSubjectGroupType(DBSite site, string subjectId)
        {
            return GetSubjects(site, Util_BLL.User.UserId, subjectId)[0].SubjectGroupType;
        }


        public bool IsDulicateSubject(DBSite site, SubjectMasterEntity subject)
        {
            bool isDuplicate = true;

            string qry = "";
            qry += "SELECT subject FROM tblExamMaster ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND ExamName ='" + subject.SubjectName+ "'";
            //qry += " AND TermId =" + exam.TermId;
            //qry += " AND MaxMarks =" + exam.MaxMarks;

            DataTable dt = site.ExecuteSelect(qry);

            isDuplicate = dt.Rows.Count > 0;

            return isDuplicate;
        }


        public string IsSubjectPresentInExamMarksTable(DBSite site, string subjectMasterIds)
        {
            string qry = "";
            qry += "SELECT "
             + " SubjectId, SubjectName "
             + " FROM tblExamMarks em "
             + " LEFT JOIN tblSubjectMaster m ON m.SubjectMasterId=em.SubjectId "

             + " WHERE em.UserId = " + Util_BLL.User.UserId
             + " AND SubjectId IN (" + subjectMasterIds + ")";


            DataTable dt = site.ExecuteSelect(qry);

            string subjectName = "";

            foreach (DataRow dr in dt.Rows)
            {

                subjectName = util.CheckNull(dr["SubjectName"]);
            }

            return subjectName;

        }



    }
}