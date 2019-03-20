using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

namespace AccountingSoftware.BLL
{
    public class BasicSalaryBLL
    {

        Util_BLL util = new Util_BLL();

        public class SalaryEntity
        {
            public int SalaryId { get; set; }
            public int StaffId { get; set; }
            public String StaffName { get; set; }
            public int DesignationId { get; set; }
            public String Designation { get; set; }
            public int YearNo{ get; set; }
            public int MonthNo { get; set; }
            public double BasicSalary { get; set; }
            public double Conveyance { get; set; }
            public double epf { get; set; }
            public double esi { get; set; }
            public double emi { get; set; }

            public int UserID { get; set; }
            public int FYear { get; set; }

        }


        public void SaveBasicSalary(DBSite site, SalaryEntity bs)
        {

            string qry = "INSERT INTO tblSalary(DesignationId, YearNo, MonthNo "
                + " , BasicSalary, Conveyance "
                + " , EPF, ESI, EMI "
                + " , StaffId "
                + ",  UserID, FYear)  VALUES(";

            qry +=   bs.DesignationId  
             + ", " + bs.YearNo
             + ", " + bs.MonthNo 
            +  ", "  + bs.BasicSalary 
            +  ", "  + bs.Conveyance
            + ", " + bs.epf
            + ", " + bs.esi
            + ", " + bs.emi 
            +  ", "  + bs.StaffId
            + ", "   + Util_BLL.User.UserId
            + ", " + Util_BLL.User.fYear 
            + ")";


            site.Execute(qry);

        }



        public void UpdateSalary(DBSite site, SalaryEntity sal)
        {
            string qry = " UPDATE tblSalary SET "
             + " designationId =" + sal.DesignationId
             + ", YearNo = " + sal.YearNo
             + ", MonthNo = " + sal.MonthNo
             + " , BasicSalary =" + sal.BasicSalary
             + " , Conveyance =" + sal.Conveyance
             + " ,  EPF =" + sal.epf
             + " ,  ESI =" + sal.esi
             + " ,  EMI =" + sal.emi
             + " , StaffId =" + sal.StaffId
            + " ,  UserID =" + Util_BLL.User.UserId
            + " , FYear = " + Util_BLL.User.fYear
             + " WHERE SalaryId =" + sal.SalaryId;
            

            site.Execute(qry);

        }


        public void DeleteSalary(DBSite site, string bsIds)
        {

            string qry = " DELETE FROM tblSalary  ";

            qry += " WHERE SalaryID IN (" + bsIds + ")";

            site.Execute(qry);

        }

        public bool isEntryDuplicateInOneMonth(DBSite site, int staffid, int yearNo, int dayNo)
        {
            // there should me only one record per month per staff

            bool isDouble = false;



            return isDouble;

        }


        public List<SalaryEntity> GetSalaries(DBSite site, int salaryID = -1)
        {
            List<SalaryEntity> salList = new List<SalaryEntity>();


            string qry = "";
            qry += " SELECT SalaryID, sa.DesignationID, Designation,  YearNo, MonthNo, BasicSalary ";

            qry += " , conveyance,  EPF, ESI, EMI, StaffID, st.StudentName ";
            qry += " , sa.userID, sa.FYear ";

            qry += " FROM  tblSalary as sa "
                + " LEFT OUTER JOIN tblStudentMaster st on st.StudentMasterID = sa.staffID "
                + " LEFT OUTER JOIN tblDesignation d ON d.designationId = sa.designationId "

            + " WHERE sa.UserID =  " + Util_BLL.User.UserId;


            if (salaryID != -1)
                qry += " AND  SalaryID = " + salaryID;
            else
                qry += " AND SalaryId IN (SELECT MAX(SalaryID) FROM tblSalary GROUP BY StaffID) ";

            DataTable dt = site.ExecuteSelect(qry);

            SalaryEntity sal;

            foreach (DataRow dr in dt.Rows)
            {
                sal = new SalaryEntity();

                sal.SalaryId  = util.CheckNullInt(dr["SalaryID"]);
                sal.StaffId  = util.CheckNullInt(dr["StaffId"]);
                sal.StaffName = util.CheckNull(dr["StudentName"]);
                sal.DesignationId = util.CheckNullInt(dr["DesignationId"]);
                sal.Designation = util.CheckNull(dr["Designation"]);

                sal.YearNo = util.CheckNullInt(dr["YearNo"]);
                sal.MonthNo = util.CheckNullInt(dr["MonthNo"]);
                

                sal.BasicSalary  = util.CheckNullDouble(dr["BasicSalary"]);
                sal.Conveyance  = util.CheckNullDouble(dr["Conveyance"]);
                sal.epf = util.CheckNullDouble(dr["epf"]);
                sal.esi = util.CheckNullDouble(dr["esi"]);
                sal.emi  = util.CheckNullDouble(dr["emi"]);

                sal.UserID = util.CheckNullInt(dr["UserID"]);
                sal.FYear = util.CheckNullInt(dr["FYear"]);


                salList.Add(sal);

            }



            return salList;
        }


    }
}