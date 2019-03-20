using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AccountingSoftware.BLL
{
    public class ClassMasterBLL
    {
        Util_BLL util = new Util_BLL();

        public class ClassMasterEntity
        {
            public int ClassMasterId { get; set; }
            public string ClassName { get; set; }
            public int ClassOrder { get; set; }
            public int UserID { get; set; }
            public int FYear { get; set; }

        }


        public void SaveClassMaster(DBSite site, ClassMasterEntity cls)
        {


            string qry = "INSERT INTO tblClassMaster(ClassName, ClassOrder ";
            qry += " , UserID, FYear)  VALUES(";
            qry += "'" + cls.ClassName + "'";
            qry += ", " + cls.ClassOrder;
            qry += ", " + util.GetUserInsertQryMaster(Util_BLL.User);
            
            //qry += ", " + UserBLL.userID;
            //qry += ", " + UserBLL.fYear;


            qry += " )";


            site.Execute(qry);

        }



        public void UpdateClassFrm(DBSite site, ClassMasterEntity  cls)
        {

            string qry = "UPDATE tblClassMaster SET ";
            qry += " ClassName ='" + cls.ClassName + "'";
            qry += ", ClassOrder=" + cls.ClassOrder + "";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User);  //-------------  gwt user where condition --------------
            qry += " AND ClassMasterID=" + cls.ClassMasterId ;

            site.Execute(qry);

        }

        public void DeleteClasses(DBSite site, string ids)
        {

            string qry = "DELETE FROM  tblStudentMaster";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); // --------get user where condition  -----------------
            qry += " AND ClassID IN (" + ids + ")";

            site.Execute(qry);
            
            
            qry = "DELETE FROM  tblClassMaster";
            qry += Util_BLL.GetUserWhereCondition(Util_BLL.User); // --------get user where condition  -----------------
            qry += " AND ClassMasterID IN (" + ids + ")";

            site.Execute(qry);

            



        }


        public List<ClassMasterEntity> GetClassList(DBSite site, int userId, string id = "")
        {
            return GetClassListBySubUser(site, userId, id, false);
        }
        

        public List<ClassMasterEntity> GetClassListBySubUser(DBSite site, int userId, string id, bool BySubUser)
        {
            List<ClassMasterEntity> classList = new List<ClassMasterEntity>();

            string qry = "";

            if (BySubUser)
            {
                qry = " SELECT  ClassMasterID, ClassName, ClassOrder, cl.UserID, cl.FYear  "
                + " FROM tblClassMaster cl   "
                + " INNER JOIN tblTeacherClasses tc ON tc.classId = cl.classMasterID  "
                + " AND tc.userID = cl.userID AND tc.subuserID =" + Util_BLL.SubUser.SubuserId;
            }
            else
            {

                qry = " SELECT  ClassMasterID, ClassName, ClassOrder, UserID, FYear  "
                     + " FROM tblClassMaster cl  ";
            }

            qry += " WHERE cl.UserId = " + Util_BLL.User.UserId;

            if (id != string.Empty)
                qry += "AND  ClassMasterID = " + id;


            qry += " ORDER BY ClassOrder ";

            DataTable dt = site.ExecuteSelect(qry);
            ClassMasterEntity cls;

            foreach (DataRow dr in dt.Rows)
            {
                cls = new ClassMasterEntity();

                cls.ClassMasterId = util.CheckNullInt(dr["ClassMasterID"].ToString());
                cls.ClassName = util.CheckNull(dr["ClassName"]);
                cls.ClassOrder = util.CheckNullInt(dr["ClassOrder"]);
                                              
                cls.UserID = util.CheckNullInt(dr["UserID"]);
                cls.FYear = util.CheckNullInt(dr["FYear"]);

                classList.Add(cls);

            }

            return classList;
        }



    }
}