using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class ExamMarksBll
    {
        Util_BLL util = new Util_BLL();
        
        public class ExamMarksEntity
        {
            public int SlNo { get; set; }

            public int ExamMarksId { get; set; }
            public int StudentMasterId { get; set; }
            public string StudentName { get; set; }
            public string FatherName { get; set; }
            public string MobileNo { get; set; }

            public int ExamId { get; set; }
            public string ExamName { get; set; }
            public string ExamCode { get; set; }
            public string  MaxMarks { get; set; }
            
            public int SubjectId { get; set; }
            public string SubjectName { get; set; }
            public string SubjectGroupType { get; set; }

            public string MarksObtained { get; set; }

            public int IsPresent { get; set; }

            public int classId { get; set; }
            public string ClassName { get; set; }

            public int SectionId { get; set; }
            public string SectionName { get; set; }

            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }



        public class SMSExamMarksEntity
        {
            public int SlNo { get; set; }

            public int ExamMarksId { get; set; }
            public int StudentMasterId { get; set; }
            public string StudentName { get; set; }
            public string FatherName { get; set; }
            public string MobileNo{ get; set; }
            public int ExamId { get; set; }
            public string ExamName { get; set; }

            public string SMS { get; set; }
                        
            
            public int classId { get; set; }
            public string ClassName { get; set; }

            public int SectionId { get; set; }
            public string SectionName { get; set; }

            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

        // Unit Test 1, Unit Test 2, Final Exam
        public class ReportCardEntity
        {
            public int StudentMasterId { get; set; }
            public string StudentName { get; set; }
            public string FatherName { get; set; }
            public string MobileNo { get; set; }
            public string ClassName { get; set; }
            public string SectionName { get; set; }

            public string UnitTest1Subject { get; set; }
            public string UnitTest1MarksObtained { get; set; }
            public string UnitTest1MaxMarks { get; set; }

            public string UnitTest2Subject { get; set; }
            public string UnitTest2MarksObtained { get; set; }
            public string UnitTest2MaxMarks { get; set; }

            public string FinalExamSubject { get; set; }
            public string FinalExamMarksObtained { get; set; }
            public string FinalExamMaxMarks { get; set; }

            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

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
            public string SEA1  { get; set; }
            public string HalfYearly { get; set; }

            
            public string PerTest2 { get; set; }
            public string NoteBook2 { get; set; }
            public string SEA2 { get; set; }
            public string Yearly { get; set; }

            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

        public class ReportCardCBSEEntity2
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

            public string SubjectGroupName { get; set; }

            public List<ScholasticEntity> ScholasticSubjectList { get; set; }
            public List<CoScholasticEntity> CoScholasticSubjectList { get; set; }
            public List<DisciplineEntity> DisciplineSubjectList { get; set; }
                        
            public int SubUserID { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }

        public class ScholasticEntity
        {

            public string Subject { get; set; }

            public string PerTest1 { get; set; }
            public string NoteBook1 { get; set; }
            public string SEA1 { get; set; }
            public string HalfYearly { get; set; }


            public string PerTest2 { get; set; }
            public string NoteBook2 { get; set; }
            public string SEA2 { get; set; }
            public string Yearly { get; set; }

        }
        
        public class CoScholasticEntity
        {

            public string Subject { get; set; }
            public string HalfYearly { get; set; }
            public string Yearly { get; set; }

        }

        public class DisciplineEntity
        {

            public string Subject { get; set; }
            public string HalfYearly { get; set; }
            public string Yearly { get; set; }

        }


        public void DeleteExam(DBSite site, string examMarksIds)
        {
          
            string qry = "DELETE FROM  tblExamMarks ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); 
            qry += " AND ExamMarksId IN (" + examMarksIds + ")";

            site.Execute(qry);
        }


      



        protected bool isDuplicate(DBSite site, ExamMarksEntity exam)
        {
            bool isDuplicateRecord = true ;

            string qry = "SELECT COUNT(*) records FROM tblExamMarks"
                + " WHERE examID = " + exam.ExamId
                + " AND SubjectID = " + exam.SubjectId
                + " AND studentID = " + exam.StudentMasterId
                + " AND ClassID = " + exam.classId 
                + " AND SectionId = " + exam.SectionId 
                + " AND UserID = " + exam.UserID
                + " AND FYear = " + exam.FYear;

              DataTable dt = site.ExecuteSelect(qry);
              foreach (DataRow dr in dt.Rows)
              {
                  isDuplicateRecord = (util.CheckNullInt(dr["records"])) > 0;
              }
              return isDuplicateRecord;
        }


        

        public void SaveMarks(DBSite site, List<ExamMarksEntity> exams, bool isUpdate)
        {

            string qry = "";
            foreach (ExamMarksEntity exam in exams)
            {
                if (!isUpdate && !isDuplicate(site, exam))
                {
                    qry = " INSERT INTO tblExamMarks ("
                          + " StudentID, ExamID, SubjectID, MarksObtained, IsPresent "
                          + ", ClassID, SectionID "
                          + " ,  UserId, SubUserId, FYear )"
                          + " VALUES ( "

                          + " " + exam.StudentMasterId
                          + ", " + exam.ExamId
                          + ", " + exam.SubjectId
                          + ", '" + exam.MarksObtained + "'"
                          + ", " + exam.IsPresent
                          + ", " + exam.classId
                          + ", " + exam.SectionId;


                    qry += ", " + util.GetUserInsertQry(Util_BLL.User);
                    
                    qry += " ) ";

                    
                }
                else
                {
                    qry = " UPDATE tblExamMarks "
                            + " SET "
                            + " MarksObtained =  '" + exam.MarksObtained + "'"
                            + " , IsPresent = " + exam.IsPresent
                            + " WHERE ExamMarksID = " + exam.ExamMarksId;
                }
                
                site.Execute(qry);
            }
        }

       


        public string CalculatePercentMarks(DataTable dtResult, string subject, decimal MaxMarks, decimal  percentValue)
        {
            // 10 %, 40% , 50 %

            string result = "0.00";

            // to remove divide by zero error
            if (MaxMarks == 0)
                MaxMarks = 1;
         
                decimal totalMarks = 0;
                
                string exm;
                
                 foreach (DataRow drResult2 in dtResult.Rows)
                    {

                        exm = util.CheckNull(drResult2["ExamName"]).ToUpper();
                        
                        if((percentValue == 10 && (exm == "Unit Test I".ToUpper()
                                || exm == "Unit Test II".ToUpper()
                                || exm == "Unit Test III".ToUpper()))
                            || (percentValue == 40 && exm == "Half Yearly".ToUpper())
                            || (percentValue == 50 && exm == "Final Exam".ToUpper())
                            
                            )
                        {
                            if (util.CheckNull(drResult2[subject]) != "-")
                            {
                                totalMarks += util.CheckNullDecimal(drResult2[subject]);

                            }
                            else
                            {
                                totalMarks += 0;
                            }
                        }

                   }
                    result = util.CheckNullDecimal((((totalMarks /MaxMarks ) * percentValue))).ToString();

            return result;

        }

    public string GetColTotal(DataTable dtResult, string subject)
        {
                        
            decimal colTotal = 0;

            foreach (DataRow drResult in dtResult.Rows)
            {
                string exmName = util.CheckNull(drResult["ExamName"]);
                if (exmName.ToUpper() == "10 PERCENT" || exmName.ToUpper() == "40 PERCENT" || exmName.ToUpper() == "50 PERCENT")
                {
                    if (util.CheckNull(drResult[subject]) != "-")
                    {
                        colTotal += util.CheckNullDecimal(drResult[subject]);
                    }
                }

            }
            return colTotal.ToString();
                            
        }
              public DataTable GetResultDT(DBSite site, int studentID, int clasId, int sectionId)
        {
            DataTable dtResult = new DataTable();

            string qry = " SELECT "
                      + " SubjectMasterID "
                      + " , SubjectName "
                      + " FROM tblSubjectMaster "
                      + " WHERE subjectClassID in (0, " + clasId + ") "
                      + " ORDER BY SubjectOrder ";

            DataTable dt_subjects = site.ExecuteSelect(qry);

            dtResult.Columns.Add("ExamName");
            foreach (DataRow dr_subject in dt_subjects.Rows)
            {
                dtResult.Columns.Add(util.CheckNull(dr_subject["SubJectName"]));

            }

            qry = "SELECT ExamMasterID, ExamName, MaxMarks, IsFormula, Formula "
                + " FROM tblExamMaster "
                + " ORDER BY ExamOrder";

            DataTable dt_exams = site.ExecuteSelect(qry);

            DataRow drResult;

            bool isFrmula = false;
            decimal formulaValue = 1;

            foreach (DataRow drExam in dt_exams.Rows)
            {
                drResult = dtResult.NewRow();
                
                string exmName = util.CheckNull(drExam["ExamName"]);
                drResult["ExamName"] = exmName;

                int examId = util.CheckNullInt(drExam["ExamMasterID"]);

                decimal MaxMarks = util.CheckNullDecimal(drExam["MaxMArks"]);

                isFrmula = util.CheckNullInt(drExam["IsFormula"])==1;

                    foreach (DataRow dr_subject in dt_subjects.Rows)
                    {
                        string subject = util.CheckNull(dr_subject["SubJectName"]);
                        int subjectId = util.CheckNullInt(dr_subject["SubjectMasterID"]);


                        qry = " SELECT SubjectID "
                            + ", ExamID "
                            + ", MarksObtained "
                            + " FROM tblExamMarks "
                            + " WHERE studentID = " + studentID
                            + " AND SubjectID =   " + subjectId
                            + " AND ExamID =  " + examId
                            + " AND classId= " + clasId 
                            + " AND SectionID= " + sectionId ;

                        string cellValue = "-";
                        if (!isFrmula)
                        {
                            DataTable dtMarks = site.ExecuteSelect(qry);

                            
                            foreach (DataRow drMark in dtMarks.Rows)
                            {
                                cellValue = util.CheckNull(drMark["MarksObtained"]);
                            }

                            
                        }
                        else
                        {

                            formulaValue = util.CheckNullDecimal(drExam["Formula"]);
                            


                            if (exmName.ToUpper() == "TOTAL")
                            {
                                cellValue = GetColTotal(dtResult, subject);
                            }
                            else if (exmName.ToUpper() == "MAX MARKS/TERM")
                            {
                                cellValue = "25/50";
                            }
                            else
                            {
                                cellValue = CalculatePercentMarks(dtResult, subject, MaxMarks, formulaValue);
                            }
                            
                        }


                        drResult[subject] = cellValue;
                    }
                
                dtResult.Rows.Add(drResult);
            }
            return dtResult;
                        
        }
                public DataTable GetExamMarksDT(DBSite site, int classId, int sectionId, int examId, int subjectId, int IsNew)
        {
            
            string qry;
                       
            qry = "  SELECT    "
                  + " StudentMasterId "
                  + " , StudentName "
                  //+ " , ExamMarksId "
                  //+ " , ExamID "
                  + " , ExamName "
                  //+ " , m.ClassID "
                  + " , ClassName "
                  //+ " , m.SectionID "
                  + " , SectionName "
                  //+ " , SubjectID  "
                  + " , SubjectName  "
                  //+ " , IsNull(IsPresent, 1) IsPresent  "
                  + ", IsNull(MarksObtained, '0') MarksObtained "
                  //+ " , m.SubUserId, m.UserId, m.FYear  "
                  + " FROM tblExamMarks m "
                  + " LEFT OUTER JOIN tblStudentMaster st ON m.studentID = st.StudentMasterID  "
                  + " LEFT OUTER JOIN tblClassMaster c ON c.ClassMasterId = m.ClassID "
                  + " LEFT OUTER JOIN tblSectionMaster sm ON sm.sectionMasterID = m.SectionID "
                  + " LEFT OUTER JOIN tblExamMaster em ON em.ExamMasterID = m.ExamID "
                  + " LEFT OUTER JOIN tblSubjectMaster sub ON sub.SubjectMasterID = m.SubjectId ";

               qry += Util_BLL.GetUserWhereCondition(Util_BLL.User, "m");
            

            if (IsNew == 0 && subjectId > 0) // update
                qry +=   " AND m.subjectID = " + subjectId ;
            if (IsNew == 0 && examId > 0) //update
                   qry +=   " AND m.ExamID = " + examId ;

            
            if(classId > 0)
                qry +=   " AND st.ClassID = " + classId ;
            if(sectionId > 0)
                   qry +=   " AND st.sectionID = " + sectionId ;

            
            qry += " ORDER BY StudentName, ClassOrder, SectionOrder ";


            return site.ExecuteSelect(qry);
        }
        
       
        public List<SMSExamMarksEntity> GetSMSExamMarks(DBSite site, int classId, int sectionId, int examId, string subjectIds, int IsNew)
        {

            List<SMSExamMarksEntity> SMSMarksList = new List<SMSExamMarksEntity>();
            SMSExamMarksEntity smsMarks = null;

            List<ExamMarksEntity> marks_list = GetExamMarks(site, classId, sectionId, examId, subjectIds,-1, IsNew);

              


            List<ExamMarksEntity> distinct_studentList = new List<ExamMarksEntity>();

            int studentId = -1;
            ExamMarksEntity marks2 = null;
            foreach (ExamMarksEntity marks in marks_list)
            {
                marks2 = new ExamMarksEntity();
                if (marks.StudentMasterId != studentId)
                {
                    marks2.StudentMasterId = marks.StudentMasterId;
                    marks2.StudentName = marks.StudentName;
                    marks2.FatherName = marks.FatherName;
                    marks2.MobileNo = marks.MobileNo;
                    marks2.ClassName = marks.ClassName;
                    marks2.SectionName = marks.SectionName;
                    marks2.ExamName = marks.ExamName;
                    distinct_studentList.Add(marks2);
                }
                studentId = marks.StudentMasterId;
                
            }

            

            string smsStr = "";
            foreach (ExamMarksEntity marks in distinct_studentList)
            {
                smsMarks = new SMSExamMarksEntity();
                
                smsMarks.StudentMasterId = marks.StudentMasterId;
                smsMarks.StudentName = marks.StudentName;
                smsMarks.FatherName = marks.FatherName;
                smsMarks.MobileNo = marks.MobileNo;
                smsMarks.ClassName = marks.ClassName;
                smsMarks.SectionName = marks.SectionName;
                smsMarks.ExamName = marks.ExamName;

                smsStr = "";
                foreach(ExamMarksEntity marks3 in marks_list)
                {
                    if(marks3.StudentMasterId==marks.StudentMasterId)
                        smsStr += ", " + marks3.SubjectName + " " + marks3.MarksObtained + "/" + marks3.MaxMarks;
                }

                if (smsStr.Length > 2)
                        smsStr = smsStr.Substring(2);
                    smsMarks.SMS = smsStr;
                   
                             
                SMSMarksList.Add(smsMarks);

            }

            return SMSMarksList;
        }
        //999
        


        public List<ReportCardEntity> GetReportCard(int studentId)
        {
            DBSite site = new DBSite();
            
            int classId = -1;
            int sectionId=-1;


            List<ReportCardEntity> reportCardList = new List<ReportCardEntity>();
            ReportCardEntity reportCard =  new ReportCardEntity();

            int examId = -1;

            // Unit Test 1
            examId = 2;
            List<ExamMarksEntity> marks_list = GetExamMarks(site, classId, sectionId, examId, "", studentId, 0);
            foreach (ExamMarksEntity marks in marks_list)
            {
                reportCard = new ReportCardEntity();
                reportCard.StudentMasterId = marks.StudentMasterId;
                reportCard.StudentName = marks.StudentName;
                reportCard.ClassName = marks.ClassName;
                reportCard.FatherName = marks.FatherName;
                reportCard.UnitTest1Subject = marks.SubjectName;
                reportCard.UnitTest1MarksObtained = marks.MarksObtained;
                reportCard.UnitTest1MaxMarks = marks.MaxMarks;

                reportCardList.Add(reportCard);

            }


            return reportCardList;
        }

        




        //777

        public string GetCBSEQry(int studentId, string subjectGroup)
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
                        + " (CASE WHEN (ExamName = 'Per Test' AND TermName ='First Term') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest1 "
                        + " ,(CASE WHEN (ExamName = 'Note Book' AND TermName ='First Term') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook1 "
                        + " ,(CASE WHEN (ExamName = 'SEA' AND TermName ='First Term') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA1 "
                        + " ,(CASE WHEN (ExamName = 'Half Yearly' AND TermName ='First Term')  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) HalfYearly "
                        + " ,(CASE WHEN (ExamName = 'Per Test' AND TermName ='Second') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest2 "
                        + " ,(CASE WHEN (ExamName = 'Note Book' AND TermName ='Second') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook2 "
                        + " ,(CASE WHEN (ExamName = 'SEA' AND TermName ='Second') "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA2 "
                        + " ,(CASE WHEN (ExamName = 'Yearly' AND TermName='Second') "
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
                  + " GROUP BY SubjectName "
                  + " , SubjectOrder "
                  + " ORDER BY SubjectOrder ";

            
            return qry;
        }

             



       
        public List<ExamMarksEntity> GetExamMarksByClass(int classId, int examId, string subjectIds)
        {
            DBSite site = new DBSite();
            
            if (subjectIds == null)
                subjectIds="";    
            return  GetExamMarks(site, classId, 0, examId, subjectIds, -1, 0);
            
        }


        public List<ExamMarksEntity> GetExamMarks(DBSite site, int classId, int sectionId, int examId, string subjectIds, int studentId, int IsNew)
        {
            // if isNew=0 then get reocrds from tblStudentMaster 
            // else get the reocrds from  tblExamMarks

            List<ExamMarksEntity> marks = new List<ExamMarksEntity>();
            ExamMarksEntity mark = null;

            string qry = "";

            ExamMarksEntity exam = new ExamMarksEntity();
            //exam.StudentMasterId = stu

            if (IsNew == 1)
            {
                qry = " SELECT    "
                    + " StudentMasterId "
                    + " , StudentName "
                    + " , MobileF "
                    + " , FatherName "
                    + " , 0 ExamMarksId "
                    + " , 0 ExamId "
                    + " , '' ExamName "
                    + " , '' ExamCode "
                    + ", '' MaxMarks "
                    + " , 0 SubjectID"
                    + " , '' SubjectName" 
                    + ", '' SubjectGroupType "
                    + " , ClassID "
                    + " , ClassName "
                    + " , SectionID "
                    + " , SectionName "
                    + " , 1 IsPresent "
                    + " , '0' MarksObtained "
                    + " , st.SubUserId, st.UserId, st.FYear  "
                    + " FROM tblStudentMaster st    "
                    + " LEFT OUTER JOIN tblClassMaster c ON c.ClassMasterId = st.ClassID "
                    + " LEFT OUTER JOIN tblSectionMaster s On s.SectionMasterID = st.SectionId ";
                    

                qry += Util_BLL.GetUserWhereCondition(Util_BLL.User, "st");
            }
            else
            {
               // update


                qry = "  SELECT    "
                  + " StudentMasterId "
                  + " , StudentName "
                  + " , FatherName "
                  + " , MobileF "
                  + " , ExamMarksId "
                  + " , ExamID "
                  + " , ExamName "
                  + " , ExamCode "
                  + ",  MaxMarks "
                  + " , m.ClassID "
                  + " , ClassName "
                  + " , m.SectionID "
                  + " , SectionName "
                  + " , SubjectID  "
                  + " , SubjectName  "
                  + " , SubjectGroupType "
                  + " , IsNull(IsPresent, 1) IsPresent  "
                  + ", IsNull(MarksObtained, '0') MarksObtained "
                  + " , m.SubUserId, m.UserId, m.FYear  "
                  + " FROM tblExamMarks m "
                  + " LEFT OUTER JOIN tblStudentMaster st ON m.studentID = st.StudentMasterID  "
                  + " LEFT OUTER JOIN tblClassMaster c ON c.ClassMasterId = m.ClassID "
                  + " LEFT OUTER JOIN tblSectionMaster sm ON sm.sectionMasterID = m.SectionID "
                  + " LEFT OUTER JOIN tblExamMaster em ON em.ExamMasterID = m.ExamID "
                  + " LEFT OUTER JOIN tblSubjectMaster sub ON sub.SubjectMasterID = m.SubjectId "
                  + " LEFT OUTER JOIN tblSubjectGroupMaster sgm ON sgm.SubjectGroupMasterID = sub.SubjectGroupMasterID ";

               qry += Util_BLL.GetUserWhereCondition(Util_BLL.User, "m");
            }

            if (IsNew == 0 && subjectIds.Length  > 1) // update
                qry += " AND m.subjectID IN (" + subjectIds +")";
            if (IsNew == 0 && examId > 0) //update
                   qry +=   " AND m.ExamID = " + examId ;

            
            if(classId > 0)
                qry +=   " AND st.ClassID = " + classId ;
            if(sectionId > 0)
                   qry +=   " AND st.sectionID = " + sectionId ;

            if (studentId > 0)
                qry += " AND st.studentMasterId = " + studentId;

            if (IsNew == 1)
                qry += " ORDER BY StudentName, ClassOrder, SectionOrder ";
            else
            {
                if (studentId > 0)
                    qry += " ORDER BY ExamOrder, subjectOrder ";
                else
                    qry += " ORDER BY StudentName, ClassOrder, SectionOrder, subjectOrder ";
            }

            int SerialCount = 0;

            DataTable dt = site.ExecuteSelect(qry);
            foreach (DataRow dr in dt.Rows)
            {
                mark = new ExamMarksEntity();

                SerialCount += 1;

                mark.SlNo = SerialCount;
                mark.ExamMarksId = util.CheckNullInt(dr["ExamMarksId"]);
                
                mark.StudentMasterId = util.CheckNullInt(dr["StudentMasterId"]);
                mark.StudentName = util.CheckNull(dr["StudentName"]);
                mark.FatherName = util.CheckNull(dr["FatherName"]);
                mark.MobileNo = util.CheckNull(dr["MobileF"]);


                mark.ExamId = util.CheckNullInt(dr["ExamId"]);
                mark.ExamName = util.CheckNull(dr["ExamName"]);
                mark.ExamCode = util.CheckNull(dr["ExamCode"]);
                mark.MaxMarks = util.CheckNull(dr["MaxMarks"]);
                

                mark.SubjectId = util.CheckNullInt(dr["SubjectID"]);
                mark.SubjectName = util.CheckNull(dr["SubjectName"]);
                mark.SubjectGroupType = util.CheckNull(dr["SubjectGroupType"]);
                

                mark.classId = util.CheckNullInt(dr["ClassID"]);
                mark.ClassName = util.CheckNull(dr["ClassName"]);

                mark.SectionId = util.CheckNullInt(dr["SectionID"]);
                mark.SectionName = util.CheckNull(dr["SectionName"]);

                mark.MarksObtained = util.CheckNull(dr["MarksObtained"]);
                mark.IsPresent = util.CheckNullInt(dr["IsPresent"]);

                mark.SubUserID = util.CheckNullInt(dr["SubUserId"]);
                mark.UserID = util.CheckNullInt(dr["userID"]);
                mark.FYear = util.CheckNullInt(dr["FYear"]);

                marks.Add(mark);
            }

            return marks;
        }


       



             
    }
}