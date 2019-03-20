using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class StudentBll
    {

        Util_BLL util = new Util_BLL();


        public class StudenEntity
        {

            public int StudentMasterId { get; set; }
            public string AdmNo { get; set; }
            public string StudentName { get; set; }
            
            public int PresentClass { get; set; }
            public String PresentClassName { get; set; }

            public int SectionId { get; set; }
            public String Section { get; set; }

            public String FatherName { get; set; }
            public String MotherName { get; set; }

            public string MobileNoF { get; set; }
            public string MobileNoM { get; set; }
            public String IdCardNo { get; set; }
            public string Email { get; set; }
            public string DOB { get; set; }

            public int UserID { get; set; }
            public int FYear { get; set; }

        }



        public void SaveStudent(DBSite site, StudenEntity st)
        {

            string qry = "INSERT INTO tblStudentMaster(StudentName, AdmissionNo, ClassId, SectionID, MobileF, MobileM, DOB, Email";
            qry += ", IdCardNo, FatherName, MotherName, UserID, SubuserId , FYear)  VALUES(";

            qry += "'" + st.StudentName  + "'";
            qry += ", '" + st.AdmNo + "'";
            qry += ", " + st.PresentClass;
            qry += ", " + st.SectionId;
            qry += ", '" + st.MobileNoF  + "'";
            qry += ", '" + st.MobileNoM  + "'";
            qry += ", '" + st.DOB + "'";
            qry += ", '" + st.Email + "'";
            qry += ", '" + st.IdCardNo  + "'";
            qry += ", '" + st.FatherName + "'";
            qry += ", '" + st.MotherName + "'";

            qry += ", " + util.GetUserInsertQry(Util_BLL.User);

            
            qry += ")";


            site.Execute(qry);

        }



        public void UpdateStudent(DBSite site, StudenEntity st)
        {
            string qry = " UPDATE tblStudentMaster SET ";
            qry += " StudentName='" + st.StudentName  + "'";
            qry += " , AdmissionNo='" + st.AdmNo + "'";
            qry += ", ClassId =" + st.PresentClass ;
            qry += ", SectionId =" + st.SectionId ;
            qry += ", IdCardNo ='" + st.IdCardNo + "'";
            qry += ", FatherName ='" + st.FatherName+ "'";
            qry += ", MotherName ='" + st.MotherName + "'";
            qry += ", MobileF ='" + st.MobileNoF  + "'";
            qry += ", MobileM ='" + st.MobileNoM + "'";
            qry += ", DOB ='" + st.DOB + "'";
            qry += ", Email ='" + st.Email + "'";
            
            qry += ", SubuserId=" + Util_BLL.SubUser.SubuserId;
            
            qry += " WHERE StudentMasterId=" + st.StudentMasterId ;



            site.Execute(qry);

        }

        public void DeleteStudent(DBSite site, string stIds)
        {

            string qry = " DELETE FROM tblStudentMaster  ";
            qry += " WHERE StudentMasterId IN (" + stIds + ")";

            site.Execute(qry);

            // delete from Attendance table also
            qry = " DELETE FROM tblAttendance  ";
            qry += " WHERE StudentMasterId IN (" + stIds + ")";
            
            site.Execute(qry);


        }


        public bool IsDuplicateCard(DBSite site, string cardNo, int studentId )
        {
            string qry = " SELECT IdCardNo FROM tblStudentMaster ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND IdCardNo = '" + cardNo + "'";
            qry += "AND studentMasterId <> " + studentId;
            DataTable dt = null;
            dt = site.ExecuteSelect(qry);

            return dt.Rows.Count > 0;
        }



        public bool IsStudentPresentInAttendance(DBSite site, int stId)
        {
            string qry = " SELECT StudentMasterID FROM tblAttendance ";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);
            qry += " AND StudentMasterID = " + stId;
            DataTable dt = null;
            dt = site.ExecuteSelect(qry);

            return dt.Rows.Count > 0;
        }

        public List<StudenEntity> SearchStudents(DBSite site, string student__class_names = "")
        {
            string ids = "";
            string qry = " SELECT studentMasterID, StudentName FROM tblStudentMaster ";
            qry += " WHERE UserID =  " + Util_BLL.User.UserId;
            qry += " AND ( ";

            string[] student_arr = student__class_names.Split(',');

            string where_condition = "";
            foreach (string str in student_arr)
            {
                where_condition += " OR StudentName LIKE '" + str.Trim() +"%' ";
            }
            if (where_condition.Length > 3)
            {
                where_condition = where_condition.Substring(3, where_condition.Length - 4);
            }
            qry += where_condition + " )";
            DataTable dt = site.ExecuteSelect(qry);

            foreach (DataRow dr in dt.Rows)
            {
                ids += ", " + util.CheckNullInt(dr["StudentMasterId"]);
            }
            if(ids.Length > 2)
                ids = ids.Substring(2, ids.Length-2);

            return GetStudentsByClass(site, ids, "ALL");
        }



        public List<StudenEntity> GetStudents(DBSite site, string id = "")
        {
            return GetStudentsByClass(site, id, "");
        }

        public List<StudenEntity> GetStudentsByClass(DBSite site, string  ids, string classIds="")
        {
            List<StudenEntity> studentList = new List<StudenEntity>();

            string qry = "";
            qry += " SELECT ";
            qry += " StudentMasterId, StudentName, AdmissionNo "
            + " ,  st.ClassId, ClassName "
            + " ,  sc.SectionMasterID, SectionName "
            + " , IDCardNo, FatherName, MotherName, MobileF, MobileM ";
            qry += " , DOB, Email,  st.UserID, st.FYear, st.CreatedAt ";

            qry += " FROM  tblStudentMaster as st "
                + " LEFT OUTER JOIN tblClassMaster c on c.classMasterID = st.classID  and c.userid =  " + Util_BLL.User.UserId
                + " LEFT OUTER JOIN tblSectionMaster sc on sc.SectionMasterID = st.SectionID  and  sc.userId =  " + Util_BLL.User.UserId;

            qry += " WHERE st.UserID =  " + Util_BLL.User.UserId;
            
            if (ids != "")
                qry += " AND  StudentMasterId IN (" + ids + ")";

            if (classIds != "" && classIds !="ALL")
            {
                qry += " AND  st.ClassId In (" + classIds + ")";
            }
            else if(ids == "" && classIds == "")
            {
                classIds = "-999";
                qry += " AND  st.ClassId In (" + classIds + ")";
            }
            else if (classIds == "ALL")
            {
                qry += " ";
            }

            qry += " order by classOrder, StudentName ";
            
            DataTable dt = site.ExecuteSelect(qry);
            StudenEntity st;

            foreach (DataRow dr in dt.Rows)
            {
                st = new StudenEntity() ;

                st.StudentMasterId = util.CheckNullInt(dr["StudentMasterId"]);
                st.StudentName  = util.CheckNull(dr["StudentName"]);
                st.AdmNo = util.CheckNull(dr["AdmissionNo"]);

                st.PresentClass = util.CheckNullInt(dr["ClassID"]);
                st.PresentClassName = util.CheckNull(dr["ClassName"]);

                st.SectionId = util.CheckNullInt(dr["SectionMasterID"]);
                st.Section = util.CheckNull(dr["SectionName"]);

                st.IdCardNo = util.CheckNull(dr["IdCardNo"]);
                st.FatherName = util.CheckNull(dr["FatherName"]);
                st.MotherName = util.CheckNull(dr["MotherName"]);
                st.MobileNoF = util.CheckNull(dr["MobileF"]);
                st.MobileNoM = util.CheckNull(dr["MobileM"]);
                st.Email  = util.CheckNull(dr["Email"]);
                st.DOB = util.CheckNullDate(dr["DOB"]);
                
                

                //DateTime date = Convert.ToDateTime(dr["ProductDate"]);
                //pme.productDate = date;


                
                st.UserID = util.CheckNullInt(dr["UserID"]);
                st.FYear = util.CheckNullInt(dr["FYear"]);

                
                studentList.Add(st);

            }


            return studentList;
        }


        public void changeClass(DBSite site,  int classId, string studentIds)
        {

            string qry = " UPDATE tblStudentMaster SET ";
            qry += "ClassId =" + classId; 
            qry += " WHERE StudentMasterId IN (" + studentIds +")" ;
            site.Execute(qry);


        }






    }
}