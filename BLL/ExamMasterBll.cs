using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;


namespace AccountingSoftware.BLL
{
    public class ExamMasterBll
    {
        Util_BLL util = new Util_BLL();

        

        public class ExamMasterEntity
        {
            public int ExamMasterId { get; set; }
            public string ExamDate { get; set; }

            public string  ExamName { get; set; }
            public string ExamCode { get; set; }
            public string TermName { get; set; }
            public int TermId { get; set;}
            public string ClassName { get; set; }
            public int ClassMasterID { get; set; }
            public int SectionMasterID { get; set; }
            public string SectionName { get; set; }
            public int MaxMarks { get; set; }
            public int PassMarks { get; set; }
            public int IsFormula { get; set; }
            public int ExamOrder { get; set; }
            
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

        public void SaveExam(DBSite site, ExamMasterEntity exam)
        {
            string qry = "INSERT INTO tblExamMaster( ";
            qry += "   ExamName, ExamCode, ExamDate, MaxMarks, PassMarks ";
            qry += "  , TermId, ClassMasterID, SectionMasterID, ExamOrder, IsFormula,";
            qry += " UserID, FYear)  VALUES(";
            qry += "'" + exam.ExamName + "'";
            qry += ", '" + exam.ExamCode+ "'";
            qry += ", '" + exam.ExamDate  + "'";
            qry += ", " + exam.MaxMarks  ;
            qry += ", " + exam.PassMarks ;
            qry += ", " + exam.TermId ;
            qry += ", " + exam.ClassMasterID ;
            qry += ", " + exam.SectionMasterID  ;
            qry += ", " + exam.ExamOrder ;
            qry += ", " + exam.IsFormula ;
            
            qry += ", " + util.GetUserInsertQryMaster(Util_BLL.User);

            qry += " )";

            site.Execute(qry);
        }

        public void UpdateExam(DBSite site, ExamMasterEntity exam)
        {

            string qry = "UPDATE tblExamMaster SET ";
            qry += " ExamName ='" + exam.ExamName  + "'";
            qry += " , ExamCode ='" + exam.ExamCode + "'";
            qry += ", ExamDate='" + exam.ExamDate   + "'";
            qry += ", ClassMasterID=" + exam.ClassMasterID;
            qry += ", SectionMasterID =" + exam.SectionMasterID;
            qry += ", MaxMarks =" + exam.MaxMarks;
            qry += ", PassMarks =" + exam.PassMarks;
            qry += ", ISFormula =" + exam.IsFormula ;
            qry += ", ExamOrder =" + exam.ExamOrder ;
            qry += ", TermId =" + exam.TermId ;
            

            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); 
            qry += " AND ExamMasterId = " + exam.ExamMasterId;

            site.Execute(qry);

        }

        public bool IsDulicateExam(DBSite site, ExamMasterEntity exam, bool isUpdate)
        {
            bool isDuplicate = true;

            string qry = "";
            qry += "SELECT ExamName FROM tblExamMaster ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND ExamName ='" + exam.ExamName + "'";
            //qry += " AND TermId =" + exam.TermId;
            //qry += " AND MaxMarks =" + exam.MaxMarks;

            DataTable dt = site.ExecuteSelect(qry);

            if (isUpdate)
            {
                isDuplicate = dt.Rows.Count > 1;
            }
            else
            {
                isDuplicate = dt.Rows.Count > 0;
            }

            return false; 
            //return isDuplicate; 
        }


        public List<ExamMasterEntity> GetExamList(DBSite site, int userId, string id = "")
        {
            List<ExamMasterEntity> examList = new List<ExamMasterEntity>();

            string qry = "";
            qry += "SELECT "
             + " ExamMasterId, ExamDate, ExamName, ExamCode, MaxMarks, PassMarks "
                + ", TermId, ClassMasterID, SectionMasterID, ExamOrder, IsFormula, UserID, FYear "
             + " FROM tblExamMaster em "

             + " WHERE em.UserId = " + Util_BLL.User.UserId
             + " AND isFormula = 0 ";

            if (id != string.Empty)
                qry += "AND  ExamMasterId = " + id;


            qry += " ORDER BY ExamOrder ";

            DataTable dt = site.ExecuteSelect(qry);
            ExamMasterEntity exam;

            foreach (DataRow dr in dt.Rows)
            {
                exam = new ExamMasterEntity();
                              

                exam.ExamMasterId = util.CheckNullInt(dr["ExamMasterId"]);
                exam.ExamDate = util.CheckNullDate(dr["ExamDate"]);
                exam.ExamName = util.CheckNull(dr["ExamName"]);
                exam.ExamCode = util.CheckNull(dr["ExamCode"]);
                exam.TermId = util.CheckNullInt(dr["TermId"]);

                exam.ClassMasterID = util.CheckNullInt(dr["ClassMasterID"]);
                
                exam.SectionMasterID = util.CheckNullInt(dr["SectionMasterID"]);
                exam.MaxMarks = util.CheckNullInt(dr["MaxMarks"]);
                exam.PassMarks = util.CheckNullInt(dr["PassMarks"]);
                exam.ExamOrder = util.CheckNullInt(dr["ExamOrder"]);
                exam.IsFormula = util.CheckNullInt(dr["IsFormula"]);

                exam.UserID = util.CheckNullInt(dr["UserID"]);
                exam.FYear = util.CheckNullInt(dr["FYear"]);

                examList.Add(exam);

            }

            return examList;
        }

        
        public decimal GetExamMaxMarks(DBSite site, string  examId)
        {
            string mmStr = util.CheckNull(GetExamList(site, Util_BLL.User.UserId, examId)[0].MaxMarks);

            if (util.isDecimal(mmStr))
                return util.CheckNullDecimal(mmStr);
            else
                return -1;

        }

        public void DeleteExam(DBSite site, string ids)
        {

            string qry = "DELETE FROM  tblExamMaster";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND ExamMasterID IN (" + ids + ")";

            site.Execute(qry);

            qry = "DELETE FROM  tblExamMarks";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND ExamId IN (" + ids + ")";
            site.Execute(qry);
        }

        public string IsExamPresentInExamMarksTable(DBSite site, string examMasterIds)
        {
            string qry = "";
            qry += "SELECT "
             + " ExamId, ExamName "
             + " FROM tblExamMarks em "
             + " LEFT JOIN tblExamMaster m ON m.ExamMasterId=em.ExamId "

             + " WHERE em.UserId = " + Util_BLL.User.UserId
             + " AND ExamId IN (" + examMasterIds + ")";

            
            DataTable dt = site.ExecuteSelect(qry);

            string examName = "";

            foreach (DataRow dr in dt.Rows)
            {
                
                examName = util.CheckNull(dr["ExamName"]);
            }

            return examName;

        }



    }
}