using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class BPSBll
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
            public string ClassName { get; set; }
            public string SectionName { get; set; }
            public string SectionCode { get; set; }

            public string Subject { get; set; }

            public string PT1 { get; set; }
            public string NoteBook1 { get; set; }
            public string SEA1 { get; set; }
            public string HalfYearly { get; set; }


            public string PT2 { get; set; }
            public string NoteBook2 { get; set; }
            public string SEA2 { get; set; }
            public string Yearly { get; set; }

            
            public string PT3 { get; set; }
            public string PT4 { get; set; }

            public string PT11 { get; set; }
            public string PT12 { get; set; }
            public string PT13 { get; set; }
            public string PT14 { get; set; }

            public string PT11_MM { get; set; }
            public string PT12_MM { get; set; }
            public string PT13_MM { get; set; }
            public string PT14_MM { get; set; }


            public int PerTest1_MM { get; set; }
            public int NoteBook1_MM { get; set; }
            public int SEA1_MM { get; set; }
            public int HalfYearly_MM { get; set; }

            public int PerTest2_MM { get; set; }
            public int NoteBook2_MM { get; set; }
            public int SEA2_MM { get; set; }
            public int Yearly_MM { get; set; }


            public string PerTest1_Code { get; set; }
            public string NoteBook1_Code { get; set; }
            public string SEA1_Code { get; set; }
            public string HalfYearly_Code { get; set; }

            public string PerTest2_Code { get; set; }
            public string NoteBook2_Code { get; set; }
            public string SEA2_Code { get; set; }
            public string Yearly_Code { get; set; }


            public decimal SubjectTotal_HalfYearly { get; set;}
            public decimal SubjectTotal_MM_HalfYearly { get; set; }
            public decimal SubjectTotal_Percent_HalfYearly { get; set; }
            public string SubjectTotal_Grade_HalfYearly { get; set; }
            

            public decimal Total_HalfYearly { get; set; }
            public decimal Total_MM_HalfYearly { get; set; }
            public decimal Total_Percent_HalfYearly { get; set; }
            public string Total_Grade_HalfYearly { get; set; }
            public string Total_Remarks_HalfYearly { get; set; }
            public int PositionInClass_HalfYearly { get; set; }


            public decimal SubjectTotal_Yearly { get; set; }
            public decimal SubjectTotal_MM_Yearly { get; set; }
            public decimal SubjectTotal_Percent_Yearly { get; set; }
            public string SubjectTotal_Grade_Yearly { get; set; }
            
            public decimal Total_Yearly { get; set; }
            public decimal Total_MM_Yearly { get; set; }
            public decimal Total_Percent_Yearly { get; set; }
            public string Total_Grade_Yearly { get; set; }
            public string Total_Remarks_Yearly { get; set; }
            public int PositionInClass_Yearly { get; set; }

            public decimal OverAll_SumTotal { get; set; }
            public decimal OverAll_SumTotal_MM { get; set; }
            public decimal OverAll_Percent { get; set; }
            public string OverAll_Grade { get; set; }
            public string OverAll_Remarks{ get; set; }
            



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


        

        public string GetGradeRemarks(decimal total_marks, bool isGrade)
        {
            string grade = "E";
            string remarks = "Good";

            if (total_marks > 90 && total_marks <= 100)
            {
                grade = "A1";
                remarks = "Excellent ! Keep It Up";
            }
            else if (total_marks > 80 && total_marks <= 90)
            {
                grade = "A2";
                remarks = "Very Very Good!";
            }
            else if (total_marks > 70 && total_marks <= 80)
            {
                grade = "B1";
                remarks = "Very Good!";
            }
            else if (total_marks > 60 && total_marks <= 70)
            {
                grade = "B2";
                remarks = "Good!";
            }
           
            else if (total_marks > 50 && total_marks <= 60)
            {
                grade = "C1";
                remarks = "Good! can do better";
            }

            else if (total_marks > 40 && total_marks <= 50)
            {
                grade = "C2";
                remarks = "Fine";
            }
            else if (total_marks > 32 && total_marks <= 40)
            {
                grade = "D";
                remarks = "Fair!";
            }
            else if (total_marks < 33 )
            {
                grade = "E";
                remarks = "Needs Improvement!";
            }
           

            if(isGrade)
                return grade;
            else
                return remarks;
        }

        public string  BestOfTwo(string  pt1, string pt2)
        {
            string  pt = "0";
            decimal pt1Dec = util.CheckNullDecimal(pt1);
            decimal pt2Dec = util.CheckNullDecimal(pt2);



            if (pt2Dec > pt1Dec)
            {
                pt = pt2Dec.ToString();
            }
            else
            {
                pt = pt1Dec.ToString();
            }

            return pt;

        }


        public decimal CheckMedicalOrAbsent(decimal mrks, bool isMM)
        {
            // if -1 then Medical then Omit MM
            // if -2 then Absent do not omit MM

           // Medical Leave
            if (mrks == -1)
                return 0;
            // Absent    
            else if (mrks == -2)
            {
                
                if (!isMM)
                    return 0;
                else
                    return mrks;
            }
            else
                return mrks;
        }


        public decimal GetTotal(ReportCardCBSEEntity rc, bool isHalfYearly, bool isMM)
        {
            decimal first_Term_total = 0;
            decimal second_Term_total = 0;
            decimal total = 0;

            // 444

           

            // PT13
            Decimal practical1 = 0; //PT11
            Decimal halfYearly1 = 0; //PT13

            Decimal practical2 = 0; //PT12
            Decimal Yearly2 = 0; // PT14

            Decimal practical1_MM = 0;// PT11_MM
            Decimal halfYearly1_MM = 0; // PT13_MM


            Decimal practical2_MM = 0; // PT12_MM
            Decimal Yearly2_MM = 0; // PT14_MM


            string theClass = util.CheckNull(rc.ClassName).Trim();
            

            if (theClass.StartsWith("XI"))
            {
                practical1 = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT11), isMM);

                halfYearly1 = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT13), isMM);

                practical2 = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT12), isMM);
                Yearly2 = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT14), isMM);

                ////MM
                practical1_MM = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT11_MM), isMM);

                halfYearly1_MM = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT13_MM), isMM);

                practical2_MM = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT12_MM), isMM);
                Yearly2_MM = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT14_MM), isMM);
                
            }


            if (isHalfYearly)
            {
                if (!isMM)
                {
                    first_Term_total = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT1), isMM)
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.NoteBook1), isMM)
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.SEA1), isMM)
                                    //Gurur Nakak Practical PT13 PT14
                                    + practical1 + halfYearly1 
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.HalfYearly), isMM);
                }
                else
                {
                    first_Term_total = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PerTest1_MM), isMM)
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.NoteBook1_MM), isMM)
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.SEA1_MM), isMM)
                                    //Gurur Nakak Practical PT13 PT14

                                    + practical1_MM + halfYearly1_MM 
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.HalfYearly_MM), isMM);
                }
            }
            else
            {

                if (!isMM)
                {
                    second_Term_total = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PT3), isMM)
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.NoteBook2), isMM)
                                    
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.SEA2), isMM)
                        //Gurur NakakPractical PT13 PT14
                                    + practical2 + Yearly2
                                    
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.Yearly), isMM);
                }
                else
                {
                    second_Term_total = CheckMedicalOrAbsent(util.CheckNullDecimal(rc.PerTest2_MM), isMM)
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.NoteBook2_MM), isMM)
                                    
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.SEA2_MM), isMM)
                        //Gurur NakakPractical PT13 PT14
                                    + practical2_MM + Yearly2_MM
                                    
                                    + CheckMedicalOrAbsent(util.CheckNullDecimal(rc.Yearly_MM), isMM);
                }
            }

            if (isHalfYearly)
                total = first_Term_total;
            else
                total = second_Term_total;

            return total;
        }


        public decimal GetOverAllMarks(DataTable dt, bool isMM, bool isHalfYearly)
        {
            decimal overallMarks = 0;

            // add all subject total marks obtained
            // add all subject total max marks 
            // overAllMarks = (total marks obtained/total max marks)*100

           // add practical marks for class XI

            

            foreach (DataRow dr in dt.Rows)
            {

                // 333
                string theClass = util.CheckNull(dr["ClassName"]).Trim();


                               
                if (!isMM)
                {


                    Decimal practical1 = 0;
                    Decimal practical2 = 0;
                    Decimal HalfYearly2 = 0;
                    Decimal Yearly2 = 0;
                    
                    // 444

                    
                    if(theClass.StartsWith("XI"))
                    {
                        practical1 = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT11"]), false));
                        practical2 = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT12"]), false));
                        HalfYearly2 = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT13"]), false));
                        Yearly2 = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT14"]), false));
                    }

                    
                    // Best OF Two

                    Decimal best = 0;


                    best = util.CheckNullDecimal(BestOfTwo(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PerTest1"]), false).ToString(), CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PerTest2"]), false).ToString()));


                    overallMarks += best + practical1 + HalfYearly2 
                                                  + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["NoteBook1"]), false)
                                                  + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["SEA1"]), false)
                                                  + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["HalfYearly"]), false);
                    


                        if(!isHalfYearly)
                        {
                            best = util.CheckNullDecimal(BestOfTwo(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT3"]), false).ToString(), CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT4"]), false).ToString()));
                            overallMarks += best + practical2 + Yearly2
                                        + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["NoteBook2"]), false)
                                        + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["SEA2"]), false)
                                        + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["Yearly"]), false);
                        }
                }
                else
                {
                    // is MM

                    Decimal practical1_mm = 0;
                    Decimal practical2_mm = 0;
                    Decimal HalfYearly2_mm = 0;
                    Decimal Yearly2_mm = 0;

                    if (theClass.StartsWith("XI"))
                    {
                        practical1_mm = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT11_MM"]), false));
                        practical2_mm = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT12_MM"]), false));
                        HalfYearly2_mm = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT13_MM"]), false));
                        Yearly2_mm = util.CheckNullDecimal(CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PT14_MM"]), false));
                    }


                    overallMarks += CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PerTest1_MM"]), true)
                                                    + practical1_mm +  HalfYearly2_mm 
                                                  + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["NoteBook1_MM"]), true)
                                                  + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["SEA1_MM"]), true)
                                                  + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["HalfYearly_MM"]), true);

                    if (!isHalfYearly)
                    {

                        overallMarks += CheckMedicalOrAbsent(util.CheckNullDecimal(dr["PerTest2_MM"]), true)
                            +practical2_mm + Yearly2_mm 
                        + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["NoteBook2_MM"]), true)
                        + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["SEA2_MM"]), true)
                        + CheckMedicalOrAbsent(util.CheckNullDecimal(dr["Yearly_MM"]), true);
                    }
                }
            }

            
            return overallMarks;

        }

        
        


        public string GetCBSEQry_BPS(int studentId, string subjectGroup
                                                   , string unitTest1, string unitTest2
                                                   , string noteBook1, string noteBook2
                                                   , string sea1, string sea2
                                                   , string halfYearly, string yearly
                                                   , string term1, string term2
                                                                                       )
        {


            string qry = "";

            qry = "SELECT "
                        + " MAX(StudentName) StudentName, SubjectName, SubjectCode "
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

                        + " , MAX(PT3)PT3 "
                        + " , MAX(PT4)PT4 "

                        + " , MAX(PT11)PT11 "
                        + " , MAX(PT12)PT12 "
                        + " , MAX(PT13)PT13 "
                        + " , MAX(PT14)PT14 "

                        + " , MAX(PT11_MM)PT11_MM "
                        + " , MAX(PT12_MM)PT12_MM "
                        + " , MAX(PT13_MM)PT13_MM "
                        + " , MAX(PT14_MM)PT14_MM "
                        
                        + "  , MAX(PerTest1_Code) PerTest1_Code "
                        + " , MAX(NoteBook1_Code)NoteBook1_Code  "
                        + " , MAX(SEA1_Code)SEA1_Code  "
                        + " , MAX(HalfYearly_Code)HalfYearly_Code  "
                        + " , MAX(PerTest2_Code)PerTest2_Code  "
                        + " , MAX(NoteBook2_Code)NoteBook2_Code  "
                        + " , MAX(SEA2_Code)SEA2_Code  "
                        + " , MAX(Yearly_Code) Yearly_Code  "

                        + "  , MAX(PerTest1_MM) PerTest1_MM "
                        + " , MAX(NoteBook1_MM)NoteBook1_MM  "
                        + " , MAX(SEA1_MM)SEA1_MM  "
                        + " , MAX(HalfYearly_MM)HalfYearly_MM  "
                        + " , MAX(PerTest2_MM)PerTest2_MM  "
                        + " , MAX(NoteBook2_MM)NoteBook2_MM  "
                        + " , MAX(SEA2_MM)SEA2_MM  "
                        + " , MAX(Yearly_MM) Yearly_MM  "



                        + " FROM "
                        + " ( "
                        + " SELECT "
                        + " StudentName, FatherName, MotherName, DOB, AdmissionNo , SubjectGroupName"
                        + " , ClassName, SectionName, SubjectName,  SubjectCode, SubjectOrder , "
                  
                        + " (CASE WHEN (ExamName = '" + unitTest1 + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest1 "
                        + " ,(CASE WHEN (ExamName = '" + noteBook1 + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook1 "
                        + " ,(CASE WHEN (ExamName = '" + sea1 + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA1 "
                        + " ,(CASE WHEN (ExamName = '" + halfYearly + "' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) HalfYearly "
                        + " ,(CASE WHEN (ExamName = '" + unitTest2 + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest2 "
                        + " ,(CASE WHEN (ExamName = '" + noteBook2 + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook2 "
                        + " ,(CASE WHEN (ExamName = '" + sea2 + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA2 "
                        + " ,(CASE WHEN (ExamName = '" + yearly + "' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) Yearly "


                        + " ,(CASE WHEN (ExamName = 'PT3' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PT3 "

                        + " ,(CASE WHEN (ExamName = 'PT4' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PT4 "


                        + " ,(CASE WHEN (ExamName = 'PT11' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PT11 "

                        + " ,(CASE WHEN (ExamName = 'PT12' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PT12 "

                        + " ,(CASE WHEN (ExamName = 'PT13' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PT13 "

                        + " ,(CASE WHEN (ExamName = 'PT14' ) "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PT14 "

                        + "  , (CASE WHEN (ExamName = 'PT11' )  "
                        + "       THEN IsNull(MAX(MaxMarks), '0') END) PT11_MM  "

                        + "  , (CASE WHEN (ExamName = 'PT12' )  "
                        + "       THEN IsNull(MAX(MaxMarks), '0') END) PT12_MM  "

                        + "  , (CASE WHEN (ExamName = 'PT13' )  "
                        + "       THEN IsNull(MAX(MaxMarks), '0') END) PT13_MM  "

                        + "  , (CASE WHEN (ExamName = 'PT14' )  "
                        + "       THEN IsNull(MAX(MaxMarks), '0') END) PT14_MM  "


                        + "  , (CASE WHEN (ExamName = 'PT1' )  "
                        + "       THEN IsNull(MAX(MaxMarks), '0') END) PerTest1_MM  "

                        + " ,(CASE WHEN (ExamName = 'NB1' )   "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) NoteBook1_MM  "
                        + " ,(CASE WHEN (ExamName = 'SEA1' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) SEA1_MM  "
                        + " ,(CASE WHEN (ExamName = 'Half Yearly' )   "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) HalfYearly_MM  "
                      
                        + " ,(CASE WHEN (ExamName = 'PT2' )  "
                        + "     THEN IsNull(MAX(MaxMarks), '0') END) PerTest2_MM  "
                        
                        + " ,(CASE WHEN (ExamName = 'NB2' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) NoteBook2_MM  "
                        + " ,(CASE WHEN (ExamName = 'SEA2' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) SEA2_MM  "
                        + " ,(CASE WHEN (ExamName = 'Yearly' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) Yearly_MM  "
                        + ", (CASE WHEN (ExamName = 'PT1' )  "
                        + "      THEN IsNull(MAX(ExamCode), '0') END) PerTest1_Code  "
                        + ", (CASE WHEN (ExamName = 'NB1' )  "
                            + "  THEN IsNull(MAX(ExamCode), '0') END) NoteBook1_Code  "
                        + " ,(CASE WHEN (ExamName = 'SEA1' )  "
                        + "   THEN IsNull(MAX(ExamCode), '0') END) SEA1_Code "
                        + "   ,(CASE WHEN (ExamName = 'Half Yearly' )   "
                        + "   THEN IsNull(MAX(ExamCode), '0') END) HalfYearly_Code "
                        + "   ,(CASE WHEN (ExamName = 'PT2' )  "
                        + "   THEN IsNull(MAX(ExamCode), '0') END) PerTest2_Code  "
                        + "   ,(CASE WHEN (ExamName = 'NB2' )  "
                        + "   THEN IsNull(MAX(ExamCode), '0') END) NoteBook2_Code  "
                        + "   ,(CASE WHEN (ExamName = 'SEA2' )  "
                        + "   THEN IsNull(MAX(ExamCode), '0') END) SEA2_Code  "
                        + "   ,(CASE WHEN (ExamName = 'Yearly' )  "
                        + "   THEN IsNull(MAX(ExamCode), '0') END) Yearly_Code  "


                        + " FROM(  "
                        + " SELECT  "
                        + " StudentMasterId  "
                        + " , StudentName , FatherName, MotherName, DOB, AdmissionNo, SubjectGroupName"
                  
                        + " , MobileF "
                        + " , ClassName  "
                        + " , SectionName "
                        + " , TermName "
                        + " , SubjectName, SubjectCode "
                        + " , SubjectOrder "
                        + " , MaxMarks "
                        + " , ExamName, ExamCode "
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
                  + " , SubjectName, SubjectCode"
                  + " , SubjectOrder "
                  + " , ExamName , ExamCode, MaxMarks"
                  + " , TermName "

                  + " ) as A "
                  + " GROUP BY StudentName, ExamName"
                  + " , FatherName, MotherName, DOB, AdmissionNo, SubjectGroupName"
                  
                  + " , ClassName, SectionName "
                  + " , TermName "
                  + " , SubjectName , SubjectCode "
                  + " , subjectOrder  "
                  + " ) as b "
                  + " GROUP BY StudentName, SubjectName, SubjectCode "
                  + " , SubjectOrder "
                  + " ORDER BY SubjectOrder ";


            return qry;
        }






        public List<ReportCardCBSEEntity> GetReportCardCBSE_BPS(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();
            string qry = GetCBSEQry_BPS(studentId
                , "Scholastic"
                , "PT1"
                , "PT2"
                , "NB1"
                , "NB2"
                , "SEA1"
                , "SEA2"
                , "Half Yearly"
                , "Yearly"
                , "First Term"
                , "Second Term"
                );



            DataTable dt = site.ExecuteSelect(qry);
            ReportCardCBSEEntity reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.DOB = util.CheckNullDate(dr["dob"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                reportCard.ClassName = util.CheckNull(dr["ClassName"]);
                reportCard.SectionName = util.CheckNull(dr["SectionName"]);
                reportCard.MotherName = util.CheckNull(dr["MotherName"]);
                reportCard.FatherName = util.CheckNull(dr["FatherName"]);
                reportCard.AdmNo = util.CheckNull(dr["AdmNo"]);
                
                reportCard.PT1 = util.CheckNull(dr["PerTest1"]);
                reportCard.NoteBook1 = util.CheckNull(dr["NoteBook1"]);
                reportCard.SEA1 = util.CheckNull(dr["SEA1"]);
                reportCard.HalfYearly = util.CheckNull(dr["HalfYearly"]);
                
                reportCard.PT2 = util.CheckNull(dr["PerTest2"]);
                reportCard.NoteBook2 = util.CheckNull(dr["NoteBook2"]);
                reportCard.SEA2 = util.CheckNull(dr["SEA2"]);
                reportCard.Yearly = util.CheckNull(dr["Yearly"]);


                

                reportCard.PT3 = util.CheckNull(dr["PT3"]);
                reportCard.PT4 = util.CheckNull(dr["PT4"]);

                

                reportCard.PT11 = util.CheckNull(dr["PT11"]);
                reportCard.PT12 = util.CheckNull(dr["PT12"]);
                reportCard.PT13 = util.CheckNull(dr["PT13"]);
                reportCard.PT14 = util.CheckNull(dr["PT14"]);

                reportCard.PT11_MM = util.CheckNull(dr["PT11_MM"]);
                reportCard.PT12_MM = util.CheckNull(dr["PT12_MM"]);
                reportCard.PT13_MM = util.CheckNull(dr["PT13_MM"]);
                reportCard.PT14_MM = util.CheckNull(dr["PT14_MM"]);

                reportCard.PT1 = BestOfTwo(reportCard.PT1, reportCard.PT2);
                reportCard.PT3 = BestOfTwo(reportCard.PT3, reportCard.PT4);

                reportCard.PerTest1_Code = util.CheckNull(dr["PerTest1_Code"]);
                reportCard.NoteBook1_Code = util.CheckNull(dr["NoteBook1_Code"]);
                reportCard.SEA1_Code = util.CheckNull(dr["SEA1_Code"]);
                reportCard.HalfYearly_Code = util.CheckNull(dr["HalfYearly_Code"]);

                reportCard.PerTest2_Code = util.CheckNull(dr["PerTest2_Code"]);
                reportCard.NoteBook2_Code = util.CheckNull(dr["NoteBook2_Code"]);
                reportCard.SEA2_Code = util.CheckNull(dr["SEA2_Code"]);
                reportCard.Yearly_Code = util.CheckNull(dr["Yearly_Code"]);



                reportCard.PerTest1_MM = util.CheckNullInt(dr["PerTest1_MM"]);
                reportCard.NoteBook1_MM = util.CheckNullInt(dr["NoteBook1_MM"]);
                reportCard.SEA1_MM = util.CheckNullInt(dr["SEA1_MM"]);
                reportCard.HalfYearly_MM = util.CheckNullInt(dr["HalfYearly_MM"]);

                reportCard.PerTest2_MM = util.CheckNullInt(dr["PerTest2_MM"]);
                reportCard.NoteBook2_MM = util.CheckNullInt(dr["NoteBook2_MM"]);
                reportCard.SEA2_MM = util.CheckNullInt(dr["SEA2_MM"]);
                reportCard.Yearly_MM = util.CheckNullInt(dr["Yearly_MM"]);




                reportCard.SubjectTotal_HalfYearly = GetTotal(reportCard, true, false);
                reportCard.SubjectTotal_MM_HalfYearly = GetTotal(reportCard, true, true);

                decimal  percent_subjectwise=0;
                if(reportCard.SubjectTotal_MM_HalfYearly!=0)
                    percent_subjectwise=(reportCard.SubjectTotal_HalfYearly/reportCard.SubjectTotal_MM_HalfYearly) * 100;

                reportCard.SubjectTotal_Percent_HalfYearly = percent_subjectwise;
                reportCard.SubjectTotal_Grade_HalfYearly = GetGradeRemarks(percent_subjectwise, true);



                reportCard.Total_HalfYearly =  GetOverAllMarks(dt, false, true);
                reportCard.Total_MM_HalfYearly =  GetOverAllMarks(dt, true, true);

                decimal percentage_mo = 0;
                if (reportCard.Total_MM_HalfYearly!=0)
                    percentage_mo = (reportCard.Total_HalfYearly / reportCard.Total_MM_HalfYearly) * 100;

                reportCard.Total_Percent_HalfYearly = util.ToDecimal(percentage_mo);


                reportCard.Total_Grade_HalfYearly = GetGradeRemarks(percentage_mo, true);
                reportCard.Total_Remarks_HalfYearly = GetGradeRemarks(percentage_mo, false);


                //888 yearly total

                reportCard.SubjectTotal_Yearly = GetTotal(reportCard, false, false);
                reportCard.SubjectTotal_MM_Yearly = GetTotal(reportCard, false, true);

                decimal percent_subjectwise_yearly = 0;
                if (reportCard.SubjectTotal_MM_Yearly!=0)
                    percent_subjectwise_yearly = (reportCard.SubjectTotal_Yearly / reportCard.SubjectTotal_MM_Yearly) * 100;


                //////////////////

                
                reportCard.SubjectTotal_Percent_Yearly = percent_subjectwise_yearly;
                reportCard.SubjectTotal_Grade_Yearly = GetGradeRemarks(percent_subjectwise_yearly, true);

                reportCard.Total_Yearly = reportCard.SubjectTotal_HalfYearly + reportCard.SubjectTotal_Yearly;
                reportCard.Total_MM_Yearly = reportCard.SubjectTotal_MM_Yearly + reportCard.SubjectTotal_MM_HalfYearly;

                decimal percentage_mo_yearly = 0;

                if (reportCard.Total_MM_Yearly != 0)
                {
                    percentage_mo_yearly = (reportCard.Total_Yearly / reportCard.Total_MM_Yearly) * 100;
                   // percentage_mo_yearly = (reportCard.Total_Yearly / 200) * 100;
                }

                

                reportCard.Total_Percent_Yearly = util.ToDecimal(percentage_mo_yearly);


                reportCard.Total_Grade_Yearly = GetGradeRemarks(percentage_mo_yearly, true);
                reportCard.Total_Remarks_Yearly =  GetGradeRemarks(percentage_mo_yearly, false);

                // OverAll

                reportCard.OverAll_SumTotal =  GetOverAllMarks(dt, false, false);
                reportCard.OverAll_SumTotal_MM = GetOverAllMarks(dt, true, false);
                if (reportCard.OverAll_SumTotal_MM != 0)
                    reportCard.OverAll_Percent = util.ToDecimal((reportCard.OverAll_SumTotal * 100 / reportCard.OverAll_SumTotal_MM));

                reportCard.OverAll_Grade = GetGradeRemarks(reportCard.OverAll_Percent, true);
                reportCard.OverAll_Remarks = GetGradeRemarks(reportCard.OverAll_Percent, false);

            //public decimal OverAll_Percent { get; set; }
            //public string OverAll_Grade { get; set; }
            //public string OverAll_Remarks{ get; set; }
            




                ///////////////////
                
                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }


        public List<ReportCardCBSEEntity> GetReportCardCBSE_CoScholastic_BPS(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            string qry = GetCBSEQry_BPS(studentId
                 , "CoScholastic"
                 , "PT1"
                 , "PT2"
                 , "NB1"
                 , "NB2"
                 , "SEA1"
                 , "SEA2"
                 , "Half Yearly"
                 , "Yearly"
                 , "First Term"
                 , "Second Term"
                 );


            DataTable dt = site.ExecuteSelect(qry);
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

        public List<ReportCardCBSEEntity> GetReportCardCBSE_Discipline_BPS(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_BPS(studentId
               , "Discipline"
               , "PT1"
                , "PT2"
                , "NB1"
                , "NB2"
                , "SEA1"
                , "SEA2"
                , "HalfYearly"
                , "Yearly"
                , "First Term"
                , "Second Term"
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
        public List<ReportCardCBSEEntity> GetReportCardCBSE_Physical_BPS(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_BPS(studentId
               , "Physical"
             , "PT1"
                , "PT2"
                , "NB1"
                , "NB2"
                , "SEA1"
                , "SEA2"
                , "HalfYearly"
                , "Yearly"
                , "First Term"
                , "Second Term"
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



        public List<ReportCardCBSEEntity> GetReportCardCBSE_Remarks_BPS(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));
            DataTable dt = site.ExecuteSelect(GetCBSEQry_BPS(studentId
               , "Remarks"
               , "PT1"
                , "PT2"
                , "NB1"
                , "NB2"
                , "SEA1"
                , "SEA2"
                , "HalfYearly"
                , "Yearly"
                , "First Term"
                , "Second Term"
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


        public List<ReportCardCBSEEntity> GetReportCardCBSE_BPS_Junior(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();
            string qry = GetCBSEQry_BPS(studentId
                , "Scholastic"
                , "PT1And2"
                , "PT3And4"
                , "PT3"
                , "PT4"
                , "SEA 1"
                , "SEA 2"
                , "Half Yearly"
                , "Final Term J"
                , "First Term"
                , "Second Term"
                );



            DataTable dt = site.ExecuteSelect(qry);
            ReportCardCBSEEntity reportCard;

            foreach (DataRow dr in dt.Rows)
            {
                reportCard = new ReportCardCBSEEntity();

                reportCard.StudentMasterId = studentId;
                reportCard.StudentName = util.CheckNull(dr["StudentName"]);
                reportCard.DOB = util.CheckNullDate(dr["dob"]);
                reportCard.Subject = util.CheckNull(dr["SubjectName"]);
                reportCard.PT1 = util.CheckNull(dr["PerTest1"]);
                reportCard.NoteBook1 = util.CheckNull(dr["NoteBook1"]);

                reportCard.SEA1 = util.CheckNull(dr["SEA1"]);
                reportCard.HalfYearly = util.CheckNull(dr["HalfYearly"]);
                reportCard.ClassName = util.CheckNull(dr["ClassName"]);
                reportCard.SectionName = util.CheckNull(dr["SectionName"]);
                reportCard.MotherName = util.CheckNull(dr["MotherName"]);
                reportCard.FatherName = util.CheckNull(dr["FatherName"]);
                reportCard.PT2 = util.CheckNull(dr["PerTest2"]);
                reportCard.NoteBook2 = util.CheckNull(dr["NoteBook2"]);
                reportCard.SEA2 = util.CheckNull(dr["SEA2"]);
                reportCard.Yearly = util.CheckNull(dr["Yearly"]);

                ReportCardList.Add(reportCard);

            }
            return ReportCardList;
        }


        public List<ReportCardCBSEEntity> GetReportCardCBSE_Physical_BPS_Junior(int studentId)
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ReportCardList = new List<ReportCardCBSEEntity>();

            //DataTable dt = site.ExecuteSelect(GetCBSEQry(studentId, "CoScholastic"));

            string qry = GetCBSEQry_BPS(studentId
               , "Physical"
                , "PT1And2"
                , "PT3And4"
                , "PT3"
                , "PT4"
                , "SEA1"
                , "SEA2"
                , "Half Yearly"
                , "Final Term J"
                , "First Term"
                , "Second Term"
               );


            DataTable dt = site.ExecuteSelect(qry);


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



////////////////////////////////Total Result

        public string GetCBSEQry_Total(string className, string subjectGroup
                                                          , string PT1, string PT2
                                                          , string noteBook1, string noteBook2
                                                          , string sea1, string sea2
                                                          , string halfYearly, string yearly
                                                          , string term1, string term2
                                                                                              )
        {
            string qry = "";

            qry = "SELECT "
                        + " StudentName, className "
                        + " , PerTest1, NoteBook1, SEA1, HalfYearly "
                        + " , (PerTest1 + NoteBook1 + SEA1 + HalfYearly) HalfYearly_Total "
                        + " , PerTest2, NoteBook2, SEA2, Yearly "
                        + " , (PerTest2 + NoteBook2 + SEA2 + Yearly) Yearly_Total "
                        + " , PerTest1_MM , NoteBook1_MM, SEA1_MM, HalfYearly_MM"
                        + " , (PerTest1_MM + NoteBook1_MM + SEA1_MM + HalfYearly_MM) HalfYearly_Total_MM"
                        + " , PerTest1_Code , NoteBook1_Code, SEA1_Code, HalfYearly_Code "
                        + " FROM ( "
                        + " SELECT "
                        + " StudentName, className "
                        + " , SUM(CAST(PerTest1 AS DECIMAL))PerTest1  "
                        + " , SUM(CAST(NoteBook1 AS DECIMAL))NoteBook1  "
                        + " , SUM(CAST(SEA1 as DECIMAL))SEA1  "
                        + " , SUM(CAST(HalfYearly AS DECIMAL))HalfYearly  "

                        + " , SUM(CAST(PerTest2 AS DECIMAL))PerTest2  "
                        + " , SUM(CAST(NoteBook2 AS DECIMAL))NoteBook2  "
                        + " , SUM(CAST(SEA2 as DECIMAL))SEA2  "
                        + " , SUM(CAST(Yearly AS DECIMAL))Yearly  "
                        + " , SUM(CAST(PerTest1_MM AS DECIMAL)) PerTest1_MM "
                        + " , SUM(CAST(NoteBook1_MM AS DECIMAL)) NoteBook1_MM "
                        + " , SUM(CAST(SEA1_MM AS DECIMAL)) SEA1_MM "
                        + " , SUM(CAST(HalfYearly_MM AS DECIMAL))HalfYearly_MM  "
                        + " ,  MAX(PerTest1_Code) PerTest1_Code "
                        + " ,  MAX(NoteBook1_Code)NoteBook1_Code  "
                        + " ,  MAX(SEA1_Code)SEA1_Code  "
                        + " ,  MAX(HalfYearly_Code)HalfYearly_Code  "
                        + " ,  MAX(PerTest2_Code)PerTest2_Code  "
                        + " ,  MAX(NoteBook2_Code)NoteBook2_Code  "
                        + " ,  MAX(SEA2_Code)SEA2_Code  "
                        + " ,  MAX(Yearly_Code) Yearly_Code  "

                        + "  FROM ( "
                        + " SELECT  MAX(StudentName) StudentName, SubjectName "
                        + " , Max(FatherName)FatherName, MAX(MotherName)MotherName "
                        + " , MAX(DOB)DOB, MAX(AdmissionNo)AdmNo "
                        + " , MAX(ClassName) className, MAX(SectionName) SectionName "
                        + " , MAX(SubjectGroupName)SubjectGroupName  "
                        + " , MAX(PerTest1)PerTest1  "
                        + " , MAX(NoteBook1)NoteBook1  "
                        + " , MAX(SEA1)SEA1  "
                        + " , MAX(HalfYearly)HalfYearly  "
                        + " , MAX(PerTest2)PerTest2  "
                        + " , MAX(NoteBook2)NoteBook2  "
                        + " , MAX(SEA2)SEA2  "
                        + " , MAX(Yearly) Yearly  "
                        + " , MAX(PerTest1_MM) PerTest1_MM "
                        + " , MAX(NoteBook1_MM)NoteBook1_MM  "
                        + " , MAX(SEA1_MM)SEA1_MM  "
                        + " , MAX(HalfYearly_MM)HalfYearly_MM  "
                        + " , MAX(PerTest2_MM)PerTest2_MM  "
                        + " , MAX(NoteBook2_MM)NoteBook2_MM  "
                        + " , MAX(SEA2_MM)SEA2_MM  "
                        + " , MAX(Yearly_MM) Yearly_MM  "
                        + " , MAX(PerTest1_Code)PerTest1_Code "
                        + " , MAX(NoteBook1_Code)NoteBook1_Code  "
                        + " , MAX(SEA1_Code)SEA1_Code  "
                        + " , MAX(HalfYearly_Code)HalfYearly_Code  "
                        + " , MAX(PerTest2_Code)PerTest2_Code  "
                        + " , MAX(NoteBook2_Code)NoteBook2_Code  "
                        + " , MAX(SEA2_Code)SEA2_Code  "
                        + " , MAX(Yearly_Code) Yearly_Code  "
                        + " FROM  (  "
                        + " SELECT  StudentName, FatherName, MotherName, DOB, AdmissionNo "
                        + " , SubjectGroupName , ClassName, SectionName, SubjectName "
                        + " ,  SubjectOrder "
                        + " ,  (CASE WHEN (ExamName = 'PT1' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest1  "
                        + " ,(CASE WHEN (ExamName = 'NB1' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook1  "
                        + " ,(CASE WHEN (ExamName = 'SEA1' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA1  "
                        + " ,(CASE WHEN (ExamName = 'Half Yearly' )   "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) HalfYearly  "
                        + " ,(CASE WHEN (ExamName = 'PT2' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) PerTest2  "
                        + " ,(CASE WHEN (ExamName = 'NB2' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) NoteBook2  "
                        + " ,(CASE WHEN (ExamName = 'SEA2' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) SEA2  "
                        + " ,(CASE WHEN (ExamName = 'Yearly' )  "
                        + " THEN IsNull(MAX(MarksObtained), '0') END) Yearly  "
                        + " , (CASE WHEN (ExamName = 'PT1' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) PerTest1_MM  "
                        + " ,(CASE WHEN (ExamName = 'NB1' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) NoteBook1_MM  "
                         + " ,(CASE WHEN (ExamName = 'SEA1' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) SEA1_MM  "
                        + " ,(CASE WHEN (ExamName = 'Half Yearly' )   "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) HalfYearly_MM  "
                        + " ,(CASE WHEN (ExamName = 'PT2' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) PerTest2_MM  "
                        + " ,(CASE WHEN (ExamName = 'NB2' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) NoteBook2_MM  "
                        + " ,(CASE WHEN (ExamName = 'SEA2' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) SEA2_MM  "
                        + " ,(CASE WHEN (ExamName = 'Yearly' )  "
                        + " THEN IsNull(MAX(MaxMarks), '0') END) Yearly_MM  "
                        + " , (CASE WHEN (ExamName = 'PT1' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) PerTest1_Code  "
                        + " , (CASE WHEN (ExamName = 'NB1' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) NoteBook1_Code  "
                        + " ,(CASE WHEN (ExamName = 'SEA1' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) SEA1_Code "
                        + " ,(CASE WHEN (ExamName = 'Half Yearly' )   "
                        + " THEN IsNull(MAX(ExamCode), '0') END) HalfYearly_Code "
                        + " ,(CASE WHEN (ExamName = 'PT2' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) PerTest2_Code  "
                        + " ,(CASE WHEN (ExamName = 'NB2' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) NoteBook2_Code  "
                        + " ,(CASE WHEN (ExamName = 'SEA2' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) SEA2_Code  "
                        + " ,(CASE WHEN (ExamName = 'Yearly' )  "
                        + " THEN IsNull(MAX(ExamCode), '0') END) Yearly_Code  "
                        + " FROM(   SELECT   StudentMasterId   , StudentName "
                        + " , FatherName, MotherName, DOB, AdmissionNo, SubjectGroupName "
                        + " , MobileF  , ClassName   , SectionName  , TermName  "
                        + " , SubjectName  , SubjectOrder "
                        + " ,ExamCode, ExamName  "
                        + " , em.MaxMarks "
                        + " ,  IsNull(MAX(CASE WHEN (MarksObtained= -1 OR MarksObtained = -2) "
                        + " THEN 0 ELSE  MarksObtained END ), '0') marksObtained "
                        + " FROM tblExamMarks m   "
                        + " LEFT OUTER JOIN tblStudentMaster st  "
                        + " ON m.studentID = st.StudentMasterID    "
                        + " LEFT OUTER JOIN tblClassMaster c  ON c.ClassMasterId = m.ClassID   "
                        + " LEFT OUTER JOIN tblSectionMaster sm  ON sm.sectionMasterID = m.SectionID   "
                        + " LEFT OUTER JOIN tblExamMaster em  ON em.ExamMasterID = m.ExamID   "
                        + " LEFT OUTER JOIN tblSubjectMaster sub  ON sub.SubjectMasterID = m.SubjectId   "
                        + " LEFT OUTER JOIN tblSubjectGroupMaster sgm  "
                        + " ON sgm.SubjectGroupMasterID = sub.SubjectGroupMasterID   "
                        + " LEFT OUTER JOIN tblTerm tm ON tm.TermId = em.termID  ";
                        qry += Util_BLL.GetUserWhereCondition(Util_BLL.User, "st");
                        qry += " AND ClassName='" +className + "' "
                        + " AND SubjectGroupName='" + subjectGroup + "'"
                        + " GROUP By  StudentMasterId   "
                        + " , StudentName  , FatherName, MotherName, DOB "
                        + " , AdmissionNo, SubjectGroupName , MobileF   "
                        + " , ClassName   , SectionName   , SubjectName "
                        + " , SubjectOrder  , ExamName ,ExamCode  , MaxMarks, TermName  ) as A  "
                        + " GROUP BY StudentName, ExamName  , FatherName, MotherName, DOB "
                        + " , AdmissionNo, SubjectGroupName , ClassName, SectionName  "
                        + " , TermName  , SubjectName  , subjectOrder   ) as b  "
                        + " GROUP BY StudentName, SubjectName  , SubjectOrder  "
                        + " ) "
                        + " AS C "
                        + " GROUP BY StudentName, className "
                        + " ) AS D ";


            return qry;

        }

        public List<ReportCardCBSEEntity> GetResultByClass(string className="V")
        {
            DBSite site = new DBSite();
            List<ReportCardCBSEEntity> ResultList = new List<ReportCardCBSEEntity>();
            string qry = GetCBSEQry_Total(className
                , "Scholastic"
                , "PT1"
                , "PT2"
                , "NB1"
                , "NB2"
                , "SEA1"
                , "SEA2"
                , "Half Yearly"
                , "Yearly"
                , "First Term"
                , "Second Term"
                );

            DataTable dt = site.ExecuteSelect(qry);
            ReportCardCBSEEntity result = new ReportCardCBSEEntity();
           

            
            foreach (DataRow dr in dt.Rows)
            {
                result = new ReportCardCBSEEntity();

                
                result.StudentName = util.CheckNull(dr["StudentName"]);
                //result.DOB = util.CheckNullDate(dr["dob"]);

                result.ClassName = util.CheckNull(dr["ClassName"]);
                //result.SectionName = util.CheckNull(dr["SectionName"]);
                //result.MotherName = util.CheckNull(dr["MotherName"]);
                //result.FatherName = util.CheckNull(dr["FatherName"]);


                result.PT1 = util.CheckNull(dr["PerTest1"]);
                result.NoteBook1 = util.CheckNull(dr["NoteBook1"]);
                result.SEA1 = util.CheckNull(dr["SEA1"]);
                result.HalfYearly = util.CheckNull(dr["HalfYearly"]);
                result.SubjectTotal_HalfYearly = util.CheckNullDecimal(dr["HalfYearly_Total"]);

                result.SubjectTotal_MM_HalfYearly = util.CheckNullDecimal(dr["HalfYearly_Total_MM"]);
                if (result.SubjectTotal_MM_HalfYearly > 0)
                {
                    result.SubjectTotal_Percent_HalfYearly = (result.SubjectTotal_HalfYearly / result.SubjectTotal_MM_HalfYearly) * 100;
                    result.SubjectTotal_Grade_HalfYearly = GetGradeRemarks(result.SubjectTotal_Percent_HalfYearly, true);

                }
                result.PT2 = util.CheckNull(dr["PerTest2"]);
                result.NoteBook2 = util.CheckNull(dr["NoteBook2"]);
                result.SEA2 = util.CheckNull(dr["SEA2"]);
                result.Yearly = util.CheckNull(dr["Yearly"]);

                result.PerTest1_MM = util.CheckNullInt(dr["PerTest1_MM"]);
                result.NoteBook1_MM = util.CheckNullInt(dr["NoteBook1_MM"]);
                result.SEA1_MM = util.CheckNullInt(dr["SEA1_MM"]);
                result.HalfYearly_MM = util.CheckNullInt(dr["HalfYearly_MM"]);

                //result.PerTest2_MM = util.CheckNullInt(dr["PerTest2_MM"]);
                //result.NoteBook2_MM = util.CheckNullInt(dr["NoteBook2_MM"]);
                //result.SEA2_MM = util.CheckNullInt(dr["SEA2_MM"]);
                //result.Yearly_MM = util.CheckNullInt(dr["Yearly_MM"]);


                result.PerTest1_Code = util.CheckNull(dr["PerTest1_Code"]);
                result.NoteBook1_Code = util.CheckNull(dr["NoteBook1_Code"]);
                result.SEA1_Code = util.CheckNull(dr["SEA1_Code"]);
                result.HalfYearly_Code = util.CheckNull(dr["HalfYearly_Code"]);

                //result.PerTest2_Code = util.CheckNull(dr["PerTest2_Code"]);
                //result.NoteBook2_Code = util.CheckNull(dr["NoteBook2_Code"]);
                //result.SEA2_Code = util.CheckNull(dr["SEA2_Code"]);
                //result.Yearly_Code = util.CheckNull(dr["Yearly_Code"]);

                                
                ResultList.Add(result);
            }

            return ResultList;


        }

        public int GetPositionInClass()
        {
            int positionInClass = 0;
            // get list of students with total percent and arrange the list in desending order
            // 

            return positionInClass;
        }

    }
}