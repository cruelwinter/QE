using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using QE.Models.ViewModels;
using System.Web.Mvc;

namespace QE.Services
{
    public class ClassService
    {
        private static IDBservice DBS = new DBservice();

        public static List<QE_CLASS> GetClasses()
        {
            try { return KennyORM.GetDBSource("QE_CLASS", "select * from QE_CLASS where TERM = '" + TermService.GetCurrentTerm().ID + "' and active=1 order by FORM, NAME").Cast<QE_CLASS>().ToList(); }
            catch { return null; }
        }

        public static QE_CLASS GetClass(int form, string name)
        {
            try { return KennyORM.GetDBSource("QE_CLASS", "select * from QE_CLASS where TERM = '" + TermService.GetCurrentTerm().ID + "' and active=1 and FORM = " + form + " and NAME = '" + name + "'").Cast<QE_CLASS>().First(); }
            catch { return null; }
        }

        public static List<QE_CLASS> GetClassesByForm(int form)
        {
            try { return KennyORM.GetDBSource("QE_CLASS", "select * from QE_CLASS where TERM = '" + TermService.GetCurrentTerm().ID + "' and active=1 and FORM = " + form + " order by FORM, NAME").Cast<QE_CLASS>().ToList(); }
            catch { return null; }
        }

        public static QE_CLASS GetClassesByStudent(int student_id)
        {
            try { return WillORM.GetDBSource("QE_CLASS", "select * from QE_CLASS where ID in (select QE_CLASS from CLASS_STUDENT_LIST where STUDENT = " + student_id + " and active = 1) and active = 1").Cast<QE_CLASS>().FirstOrDefault(); }
            catch { return null; }
        }
        public static List<QE_CLASS> GetClassesByTerm(int term)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                {"TERM", term.ToString()},
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("QE_CLASS", WillORM.QueryBuilder(null, "QE_CLASS", Ds, new string[] { "FORM", "NAME" })).Cast<QE_CLASS>().ToList(); }
            catch { return null; }
        }

        public static List<ClassView> getClassViews(int term)
        {
            try
            {
                List<ClassView> list = new List<ClassView>();
                List<QE_USER> users = DBS.findActiveRecords<QE_USER>();

                foreach (var c in GetClassesByTerm(term))
                {
                    string name = users.Exists(u => u.ID == c.TEACHER) ? users.Where(u => u.ID == c.TEACHER).FirstOrDefault().USER_NAME : string.Empty;
                    string name2 = users.Exists(u => u.ID == c.TEACHER_2) ? users.Where(u => u.ID == c.TEACHER_2).FirstOrDefault().USER_NAME : string.Empty;
                    ClassView CV = new ClassView(c,name , name2);
                    list.Add(CV);
                }

                return list;
            }
            catch (Exception e){ return null; }
        }

        public static QE_CLASS GetClass(int class_id)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                {"ID", class_id.ToString()},
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("QE_CLASS", WillORM.QueryBuilder(null, "QE_CLASS", Ds, new string[] { "FORM", "NAME" })).Cast<QE_CLASS>().ToList().FirstOrDefault(); }
            catch { return null; }
        }

        public static List<QE_CLASS> GetClasses(Dictionary<string, string> Ds)
        {
            Ds.Add("TERM", TermService.GetCurrentTerm().ID.ToString());
            Ds.Add("ACTIVE", "1");

            try { return WillORM.GetDBSource("QE_CLASS", WillORM.QueryBuilder(null, "QE_CLASS", Ds, new string[] { "FORM", "NAME" })).Cast<QE_CLASS>().ToList(); }
            catch { return null; }
        }

        public static CLASS_STUDENT_LIST getClassStudentListByStudent(int student_id)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                {"STUDENT", student_id.ToString()},
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("CLASS_STUDENT_LIST", WillORM.QueryBuilder(null, "CLASS_STUDENT_LIST", Ds, null)).Cast<CLASS_STUDENT_LIST>().FirstOrDefault(); }
            catch { return null; }
        }


        public static List<CLASS_STUDENT_LIST> getClassStudentMap(int class_id, int student_id, int term_id)
        {
            try { return WillORM.GetDBSource("CLASS_STUDENT_LIST", "select * from CLASS_STUDENT_LIST where QE_CLASS in (select ID from QE_CLASS where ID = " + class_id + " and TERM = " + term_id + " and active=1) and STUDENT =" + student_id + " and active=1").Cast<CLASS_STUDENT_LIST>().ToList(); }
            catch { return null; }
        }

        public static int CreateClassStudentMap(CLASS_STUDENT_LIST newMap) { int newMap_id = 0; newMap_id = WillORM.InsertRecord(newMap); return newMap_id; }

        public static ClassStudentMap getClassStudentMapByStudent(int student_id)
        {
            try
            {
                QE_CLASS classs = GetClassesByStudent(student_id);
                STUDENT student = StudentService.GetStudent(student_id);
                CLASS_STUDENT_LIST list = getClassStudentListByStudent(student_id);
                ClassStudentMap map = new ClassStudentMap(classs, student, list);

                return map;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static int AddClass(QE_CLASS newClass)
        {
            try { return WillORM.InsertRecord(newClass); }
            catch { return 0; }
        }

        public static bool UpdateClass(QE_CLASS QE_Class)
        {
            QE_Class.MODIFY_BY = ClientSessionService.GetSession.loginedUser.ID;
            QE_Class.MODIFY_DATE = DateTime.Now;

            try { return WillORM.UpdateRecord(QE_Class); }
            catch { return false; }
        }
        
        public static bool InactiveClass(int id)
        {
            try { return WillORM.InactiveRecord("QE_CLASS", id, ClientSessionService.GetSession.loginedUser.ID); }
            catch { return false; }
        }
        
        public static IEnumerable<SelectListItem> getClassSelectList(int term_id)
        {
            List<SelectListItem> classList = new List<SelectListItem>();
            SelectListItem selectedValue = new SelectListItem();
            foreach (var cls in GetClassesByTerm(term_id))
            {
                classList.Add(new SelectListItem() { Value = cls.ID.ToString(), Text = cls.FORM + cls.NAME});
            }
            return classList;
        }
    }
}
