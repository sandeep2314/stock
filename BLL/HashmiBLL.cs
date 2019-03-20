using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;


namespace AccountingSoftware.BLL
{
    public class HashmiBLL
    {
        Util_BLL util = new Util_BLL();


        ////////////////Hashmi Begin/////////////////////////////


        public class ReportCardCBSEEntity_Hashmi
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

            public string UnitTest1 { get; set; }
            public string UnitTest1MaxMarks { get; set; }

            public string SA1 { get; set; }
            public string SA1MaxMarks { get; set; }


            public string UnitTest2 { get; set; }
            public string UnitTest2MaxMarks { get; set; }

            public string SA2 { get; set; }
            public string SA2MaxMarks { get; set; }


            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }




        public List<ReportCardCBSEEntity_Hashmi> GetReportCardCBSE_Hashmi(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity_Hashmi> ReportCardList = new List<ReportCardCBSEEntity_Hashmi>();

            string qry = GetCBSEQry_Hashmi(studentId
                , "Scholastic"
                , "Unit Test 1"
                , "Unit Test 2"
                , "SA 1"
                , "SA 2"
                , "SEA 1"
                , "SEA 2"
                , "Half Yearly"
                , "Annual"
                , "Term 1"
                , "Term 2"
                );

            DataTable dt = site.ExecuteSelect(qry);
            ReportCardCBSEEntity_Hashmi reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity_Hashmi();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);

                reportCard.ClassName = util.CheckNull(dr["ClassName"]);
                reportCard.SectionName = util.CheckNull(dr["SectionName"]);
                reportCard.MotherName = util.CheckNull(dr["MotherName"]);
                reportCard.FatherName = util.CheckNull(dr["FatherName"]);
                reportCard.DOB = util.CheckNull(dr["dob"]);

                reportCard.UnitTest1 = util.CheckNull(dr["PerTest1"]);
                reportCard.UnitTest1MaxMarks = util.CheckNull(dr["UnitTest1MaxMarks"]);

                reportCard.SA1 = util.CheckNull(dr["NoteBook1"]);
                reportCard.SA1MaxMarks = util.CheckNull(dr["SA1MaxMarks"]);


                reportCard.UnitTest2 = util.CheckNull(dr["PerTest2"]);
                reportCard.UnitTest2MaxMarks = util.CheckNull(dr["UnitTest2MaxMarks"]);

                reportCard.SA2 = util.CheckNull(dr["NoteBook2"]);
                reportCard.SA2MaxMarks = util.CheckNull(dr["SA2MaxMarks"]);


                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }


        public List<ReportCardCBSEEntity_Hashmi> GetReportCardCBSE_CoScholastic_Hashmi(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity_Hashmi> ReportCardList = new List<ReportCardCBSEEntity_Hashmi>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_Hashmi(studentId
               , "Co-Scholastic"
               , "Unit Test 1"
               , "Unit Test 2"
               , "SA 1"
               , "SA 2"
               , "SEA 1"
               , "SEA 2"
               , "Half Yearly"
               , "Annual"
               , "Term 1"
               , "Term 2"
               ));


            ReportCardCBSEEntity_Hashmi reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity_Hashmi();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                reportCard.SA1 = util.CheckNull(dr["NoteBook1"]);
                reportCard.SA2 = util.CheckNull(dr["NoteBook2"]);

                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }

        public List<ReportCardCBSEEntity_Hashmi> GetReportCardCBSE_Disipline_Hashmi(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity_Hashmi> ReportCardList = new List<ReportCardCBSEEntity_Hashmi>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_Hashmi(studentId
               , "Disipline"
               , "Unit Test 1"
               , "Unit Test 2"
               , "SA 1"
               , "SA 2"
               , "SEA 1"
               , "SEA 2"
               , "Half Yearly"
               , "Annual"
               , "Term 1"
               , "Term 2"
               ));


            ReportCardCBSEEntity_Hashmi reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity_Hashmi();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                reportCard.SA1 = util.CheckNull(dr["NoteBook1"]);
                reportCard.SA2 = util.CheckNull(dr["NoteBook2"]);

                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }

        public List<ReportCardCBSEEntity_Hashmi> GetReportCardCBSE_Physical_Hashmi(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity_Hashmi> ReportCardList = new List<ReportCardCBSEEntity_Hashmi>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_Hashmi(studentId
               , "Physical"
               , "Unit Test 1"
               , "Unit Test 2"
               , "SA 1"
               , "SA 2"
               , "SEA 1"
               , "SEA 2"
               , "Half Yearly"
               , "Annual"
               , "Term 1"
               , "Term 2"
               ));


            ReportCardCBSEEntity_Hashmi reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity_Hashmi();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                reportCard.SA1 = util.CheckNull(dr["NoteBook1"]);
                reportCard.SA2 = util.CheckNull(dr["NoteBook2"]);

                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }


        public List<ReportCardCBSEEntity_Hashmi> GetReportCardCBSE_Remarks_Hashmi(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity_Hashmi> ReportCardList = new List<ReportCardCBSEEntity_Hashmi>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_Hashmi(studentId
               , "Remarks"
               , "Unit Test 1"
               , "Unit Test 2"
               , "SA 1"
               , "SA 2"
               , "SEA 1"
               , "SEA 2"
               , "Half Yearly"
               , "Annual"
               , "Term 1"
               , "Term 2"
               ));


            ReportCardCBSEEntity_Hashmi reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity_Hashmi();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                reportCard.SA1 = util.CheckNull(dr["NoteBook1"]);
                reportCard.SA2 = util.CheckNull(dr["NoteBook2"]);

                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }




        public string GetCBSEQry_Hashmi(int studentId, string subjectGroup
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
                        + ", MAX(UnitTest1MaxMarks) UnitTest1MaxMarks "

                        + " , MAX(NoteBook1)NoteBook1 "
                        + ", MAX(SA1MaxMarks) SA1MaxMarks "

                        + " , MAX(SEA1)SEA1 "
                        + " , MAX(HalfYearly)HalfYearly "

                        + " , MAX(PerTest2)PerTest2 "
                        + ", MAX(UnitTest2MaxMarks) UnitTest2MaxMarks "

                        + " , MAX(NoteBook2)NoteBook2 "
                        + ", MAX(SA2MaxMarks) SA2MaxMarks "

                        + " , MAX(SEA2)SEA2 "
                        + " , MAX(Yearly) Yearly "
                        + " FROM "
                        + " ( "
                        + " SELECT "
                        + " StudentName, FatherName, MotherName, DOB, AdmissionNo , SubjectGroupName"
                        + " , ClassName, SectionName, SubjectName,  SubjectOrder , "
                        + " (CASE WHEN (ExamName = '" + unitTest1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest1 "

                        + ", (CASE WHEN (ExamName = '" + unitTest1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) UnitTest1MaxMarks "

                        + " ,(CASE WHEN (ExamName = '" + noteBook1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook1 "

                         + " ,(CASE WHEN (ExamName = '" + noteBook1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) SA1MaxMarks "


                        + " ,(CASE WHEN (ExamName = '" + sea1 + "' AND TermName ='" + term1 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA1 "
                        + " ,(CASE WHEN (ExamName = '" + halfYearly + "' AND TermName ='" + term1 + "')  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) HalfYearly "

                        + " ,(CASE WHEN (ExamName = '" + unitTest2 + "' AND TermName ='" + term2 + "') "
                              + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest2 "

                        + " ,(CASE WHEN (ExamName = '" + unitTest2 + "' AND TermName ='" + term2 + "') "
                              + " THEN IsNull(MAX(MaxMarks), '0') END) UnitTest2MaxMarks "


                        + " ,(CASE WHEN (ExamName = '" + noteBook2 + "' AND TermName ='" + term2 + "') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook2 "

                        + " ,(CASE WHEN (ExamName = '" + noteBook2 + "' AND TermName ='" + term2 + "') "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) SA2MaxMarks "


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
                        + " ,  IsNull(MAX(MaxMarks), '0') MaxMarks  "
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


        ///Hashmi End//////////////////////////////////////////////

    }
}