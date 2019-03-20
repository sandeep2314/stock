using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class OakWoodBLL
    {
        Util_BLL util = new Util_BLL();
        public class ReportCardCBSEEntity
        {
            public int StudentMasterId { get; set; }
            public string StudentName { get; set; }
            public string FatherName { get; set; }
            public string MotherName { get; set; }
            public string DOB { get; set; }
            public string AdmNo { get; set; }
            public string MobileNo { get; set; }
            public string ClassName { get; set; }
            public string SectionName { get; set; }

            public string Subject { get; set; }

            public string PerTest1 { get; set; }
            public string NoteBook1 { get; set; }
            public string SEA1 { get; set; }
            public string HalfYearly { get; set; }


            public string PerTest2 { get; set; }
            public string NoteBook2 { get; set; }
            public string SEA2 { get; set; }
            public string Yearly { get; set; }

            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

          public string GetCBSEQry_OakWood(int studentId, string subjectGroup
                                                    , string unitTest1, string unitTest2
                                                    , string noteBook1, string noteBook2
                                                    , string sea1, string sea2
                                                    , string halfYearly, string yearly
                                                    , string term1, string term2
                                                                                        )
        {


            string qry = "";

            qry = "SELECT "
                        + " MAX(StudentName) StudentName, SubjectName "
                        + ", Max(FatherName)FatherName, MAX(MotherName)MotherName, MAX(DOB)DOB, MAX(AdmissionNo)AdmNo"
                        + " , MAX(ClassName) className, MAX(SectionName) SectionName"
                        + " , MAX(SubjectGroupName)SubjectGroupName "

                        + " , MAX(PerTest1)PerTest1 "
                        + " , MAX(NoteBook1)NoteBook1 "
                        + " , MAX(SEA1)SEA1 "
                        + " , MAX(HalfYearly)HalfYearly "

                        + " , MAX(PerTest2)PerTest2 "
                        + " , MAX(NoteBook2)NoteBook2 "
                        + " , MAX(SEA2)SEA2 "
                        + " , MAX(Yearly) Yearly "
                        + " FROM "
                        + " ( "
                        + " SELECT "
                        + " StudentName, FatherName, MotherName, DOB, AdmissionNo , SubjectGroupName"
                        + " , ClassName, SectionName, SubjectName,  SubjectOrder , "
                        + " (CASE WHEN (ExamName = '" + unitTest1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest1 "
                        + " ,(CASE WHEN (ExamName = '" + noteBook1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook1 "
                        + " ,(CASE WHEN (ExamName = '" + sea1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA1 "
                        + " ,(CASE WHEN (ExamName = '" + halfYearly + "' AND TermName ='" + term1 + "')  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) HalfYearly "
                        + " ,(CASE WHEN (ExamName = '" + unitTest2 + "' AND TermName ='" + term2 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest2 "
                        + " ,(CASE WHEN (ExamName = '" + noteBook2 + "' AND TermName ='" + term2 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook2 "
                        + " ,(CASE WHEN (ExamName = '" + sea2 + "' AND TermName ='" + term2 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA2 "
                        + " ,(CASE WHEN (ExamName = '" + yearly + "' AND TermName='" + term2 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) Yearly "
                        + " FROM(  "
                        + " SELECT  "
                        + " StudentMasterId  "
                        + " , StudentName , FatherName, MotherName, DOB, AdmissionNo, SubjectGroupName"

                        + " , MobileF "
                        + " , ClassName  "
                        + " , SectionName "
                        + " , TermName "
                        + " , SubjectName "
                        + " , SubjectOrder "
                        + " , ExamName "
                        + " ,  IsNull(MAX(MarksObtained), '0') marksObtained  "
                //+ " --, m.SubUserId, m.UserId, m.FYear   "
                        + " FROM tblExamMarks m  "
                        + " LEFT OUTER JOIN tblStudentMaster st "
                        + " ON m.studentID = st.StudentMasterID   "
                        + " LEFT OUTER JOIN tblClassMaster c "
                        + " ON c.ClassMasterId = m.ClassID  "
                        + " LEFT OUTER JOIN tblSectionMaster sm "
                        + " ON sm.sectionMasterID = m.SectionID  "
                        + " LEFT OUTER JOIN tblExamMaster em "
                        + " ON em.ExamMasterID = m.ExamID  "
                        + " LEFT OUTER JOIN tblSubjectMaster sub "
                        + " ON sub.SubjectMasterID = m.SubjectId  "
                        + " LEFT OUTER JOIN tblSubjectGroupMaster sgm "
                        + " ON sgm.SubjectGroupMasterID = sub.SubjectGroupMasterID  "
                        + " LEFT OUTER JOIN tblTerm tm ON tm.TermId = em.termID ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User, "st");

            qry += " AND st.studentMasterId = " + studentId
                + " AND SubjectGroupName='" + subjectGroup + "'"
                + " GROUP By "
                  + " StudentMasterId  "
                  + " , StudentName  "
                  + " , FatherName, MotherName, DOB, AdmissionNo, SubjectGroupName"

                  + " , MobileF  "
                  + " , ClassName  "
                  + " , SectionName  "
                  + " , SubjectName"
                  + " , SubjectOrder "
                  + " , ExamName "
                  + " , TermName "

                  + " ) as A "
                  + " GROUP BY StudentName, ExamName "
                  + " , FatherName, MotherName, DOB, AdmissionNo, SubjectGroupName"
                  + " , ClassName, SectionName "
                  + " , TermName "
                  + " , SubjectName "
                  + " , subjectOrder  "
                  + " ) as b "
                  + " GROUP BY StudentName, SubjectName "
                  + " , SubjectOrder "
                  + " ORDER BY SubjectOrder ";


            return qry;
        }



          public List<ReportCardCBSEEntity> GetReportCardCBSE(int studentId)
          {
              DBSite site = new DBSite();
              List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

              DataTable dt = site.ExecuteSelect(GetCBSEQry_OakWood(studentId
                  , "Scholastic"
                  , "Unit Test 1"
                  , "Unit Test 2"
                  , "Unit Test 1"
                  , "Unit Test 2"
                  , "SEA 1"
                  , "SEA 2"
                  , "Half Yearly"
                  , "Annual"
                  , "Term I"
                  , "Term II"
                  ));
              ReportCardCBSEEntity reportCard;

              foreach (DataRow dr in dt.Rows)
              {
                  reportCard = new ReportCardCBSEEntity();

                  reportCard.StudentMasterId = studentId;
                  reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                  reportCard.DOB = util.CheckNullDate(dr["dob"]);
                  reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                  reportCard.PerTest1 = util.CheckNull(dr["PerTest1"]);
                  reportCard.NoteBook1 = util.CheckNull(dr["NoteBook1"]);
                  reportCard.SEA1 = util.CheckNull(dr["SEA1"]);
                  reportCard.HalfYearly = util.CheckNull(dr["HalfYearly"]);
                  reportCard.ClassName = util.CheckNull(dr["ClassName"]);
                  reportCard.SectionName = util.CheckNull(dr["SectionName"]);
                  reportCard.MotherName = util.CheckNull(dr["MotherName"]);
                  reportCard.FatherName = util.CheckNull(dr["FatherName"]);
                  reportCard.PerTest2 = util.CheckNull(dr["PerTest2"]);
                  reportCard.NoteBook2 = util.CheckNull(dr["NoteBook2"]);
                  reportCard.SEA2 = util.CheckNull(dr["SEA2"]);
                  reportCard.Yearly = util.CheckNull(dr["Yearly"]);



                  ReportCardList.Add(reportCard);

              }
              return ReportCardList;
          }

          public List<ReportCardCBSEEntity> GetReportCardCBSEOakWod_Remarks(int studentId)
          {
              DBSite site = new DBSite();
              List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

              //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "Discipline"));
              DataTable dt = site.ExecuteSelect(GetCBSEQry_OakWood(studentId
                  , "Remarks"
                  , "Unit Test 1"
                  , "Unit Test 2"
                  , "Note Book 1"
                  , "Note Book 2"
                  , "SEA 1"
                  , "SEA 2"
                  , "Half Yearly"
                  , "Annual"
                  , "Term I"
                  , "Term II"
                  ));
              ReportCardCBSEEntity reportCard;

              foreach (DataRow dr in dt.Rows)
              {
                  reportCard = new ReportCardCBSEEntity();

                  reportCard.StudentMasterId = studentId;
                  reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                  reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                  reportCard.HalfYearly = util.CheckNull(dr["HalfYearly"]);
                  reportCard.Yearly = util.CheckNull(dr["Yearly"]);

                  ReportCardList.Add(reportCard);

              }
              return ReportCardList;
          }


          public List<ReportCardCBSEEntity> GetReportCardCBSE_Junior(int studentId)
          {
              DBSite site = new DBSite();
              List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

              string qry = GetCBSEQry_OakWood(studentId
                  , "Scholastic"
                  , "UNIT TEST 1"
                  , "UNIT TEST 1"
                  , "UNIT TEST 2"
                  , "UNIT TEST 2"
                  , "SEA 1"
                  , "SEA 2"
                  , "Half Yearly"
                  , "Annual"
                  , "Term I"
                  , "Term II"
                  );


              DataTable dt = site.ExecuteSelect(qry);
              ReportCardCBSEEntity reportCard;

              foreach (DataRow dr in dt.Rows)
              {
                  reportCard = new ReportCardCBSEEntity();
                  //777
                  reportCard.StudentMasterId = studentId;
                  reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                  reportCard.DOB = util.CheckNull(dr["dob"]);
                  reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                  reportCard.PerTest1 = util.CheckNull(dr["PerTest1"]);
                  reportCard.NoteBook1 = util.CheckNull(dr["NoteBook1"]);
                  reportCard.SEA1 = util.CheckNull(dr["SEA1"]);
                  reportCard.HalfYearly = util.CheckNull(dr["HalfYearly"]);
                  reportCard.ClassName = util.CheckNull(dr["ClassName"]);
                  reportCard.SectionName = util.CheckNull(dr["SectionName"]);
                  reportCard.MotherName = util.CheckNull(dr["MotherName"]);
                  reportCard.FatherName = util.CheckNull(dr["FatherName"]);
                  reportCard.PerTest2 = util.CheckNull(dr["PerTest2"]);
                  reportCard.NoteBook2 = util.CheckNull(dr["NoteBook2"]);
                  reportCard.SEA2 = util.CheckNull(dr["SEA2"]);
                  reportCard.Yearly = util.CheckNull(dr["Yearly"]);



                  ReportCardList.Add(reportCard);

              }
              return ReportCardList;
          }



    }
}