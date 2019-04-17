using QE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models.ViewModels;

namespace QE.Services
{
    public class StudentService
    {


        public static STUDENT GetStudent(int id)
        {
            try { return KennyORM.GetDBSource("STUDENT", "select * from STUDENT where ID = " + id + " and active=1").Cast<STUDENT>().First(); }
            catch { return new STUDENT(); }
        }

        public static List<STUDENT> GetStudents() // get students by current term
        {
            try { return KennyORM.GetDBSource("STUDENT", "select * from STUDENT where ID in (select STUDENT from CLASS_STUDENT_LIST where ID in (select ID from QE_CLASS where TERM = " + TermService.GetCurrentTerm().ID + " and active=1 order by FORM, NAME) and active=1) and active=1").Cast<STUDENT>().ToList(); }
            catch { return new List<STUDENT>(); }
        }

        public static List<STUDENT> GetStudents(int term) // get students by term
        {
            try { return KennyORM.GetDBSource("STUDENT", "select * from STUDENT where ID in (select STUDENT from CLASS_STUDENT_LIST where ID in (select ID from QE_CLASS where TERM = " + TermService.GetCurrentTerm().ID + " and active=1 order by FORM, NAME) and active=1) and active=1").Cast<STUDENT>().ToList(); }
            catch { return new List<STUDENT>(); }
        }

        public static List<STUDENT> GetStudentsByClass(int qe_class)
        {
            try { return KennyORM.GetDBSource("STUDENT", "select * from STUDENT where ID in (select STUDENT from CLASS_STUDENT_LIST where QE_CLASS = " + qe_class + ") and active=1").Cast<STUDENT>().ToList(); }
            catch { return new List<STUDENT>(); }
        }


        public static List<STUDENT> GetActiveStudentsByClass(int qe_class)
        {
            string first = WillORM.QueryBuilder("STUDENT", "CLASS_STUDENT_LIST", new List<MutliQuery>() { new MutliQuery(){ field1 = "QE_CLASS", field2 = qe_class.ToString(), function = null }}, null);
            string second = WillORM.QueryBuilder(null, "STUDENT", new List<MutliQuery>() { new MutliQuery() { field1 = "ID", field2 = "("+first+")", function = "in" }, new MutliQuery() { field1 = "active", field2 = "1", function = null } }, null);
            
            try { return WillORM.GetDBSource("STUDENT",second).Cast<STUDENT>().ToList(); }
            catch { return new List<STUDENT>(); }
        }

        public static List<STUDENT> GetStudents(int term_id, int class_id) {
            
            string first = WillORM.QueryBuilder(null, "QE_CLASS", new List<MutliQuery>() { new MutliQuery() { field1 = "ID", field2 = class_id.ToString(), function = null }, new MutliQuery() { field1 = "TERM", field2 = term_id.ToString(), function = null }, new MutliQuery() { field1 = "active", field2 = "1", function = null } }, null);
            string second = WillORM.QueryBuilder("STUDENT", "CLASS_STUDENT_LIST", new List<MutliQuery>() { new MutliQuery() { field1 = "QE_CLASS", field2 = "(" + first + ")", function = "in" }, new MutliQuery() { field1 = "active", field2 = "1", function = null } }, null);
            string third = WillORM.QueryBuilder(null, "STUDENT", new List<MutliQuery>() { new MutliQuery() { field1 = "ID", field2 = "(" + second + ")", function = "in" }, new MutliQuery() { field1 = "active", field2 = "1", function = null } }, null);
            
            try { return WillORM.GetDBSource("STUDENT", third).Cast<STUDENT>().ToList(); }
            catch { return new List<STUDENT>(); }
        }

        public static bool UpdateStudent(STUDENT student)
        {
            try { return WillORM.UpdateRecord(student); }
            catch { return false; }
        }
        
        public static bool InactiveStudent(int student_id, int user_id)
        {
            try { return WillORM.InactiveRecord("STUDENT", student_id, user_id); }
            catch { return false; }
        }

        public static int CreateStudent(STUDENT student)
        {
            try { return WillORM.InsertRecord(student); }
            catch { return 0; }
        }
        
        public static List<STUDENT> getStudentByGroup(int group_id)
        {
            string query = "select * from STUDENT where ID in (select STUDENT from STUDENT_GROUP_STUDENT_LIST where STUDENT_GROUP in (select ID from STUDENT_GROUP where ID = " + group_id + " and TERM = " + TermService.GetCurrentTerm().ID + ")) and active=1";
            try { return WillORM.GetDBSource("STUDENT", query).Cast<STUDENT>().ToList(); }
            catch { return null; }
        }

        public static int countStudentByGroup(int group_id)
        {
            try { return (WillORM.CountRecord("STUDENT", "SELECT COUNT(ID) from STUDENT where ID in (select STUDENT from STUDENT_GROUP_STUDENT_LIST where STUDENT_GROUP  in (select ID from STUDENT_GROUP where ID = " + group_id + ")) and ACTIVE = 1")); }
            catch { return 0; }
        }
    }
}
