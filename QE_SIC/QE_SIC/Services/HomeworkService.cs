using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using System.Web.Mvc;

namespace QE.Services
{
    public class HomeworkService
    {
        public static HOMEWORK GetHomework(int id)
        {
            try { return KennyORM.GetDBSource("HOMEWORK", "select * from HOMEWORK where ID=" + id + " and active=1").Cast<HOMEWORK>().First(); }
            catch { return null; }
        }

        public static List<HOMEWORK> GetHomeworks()
        {
            try { return KennyORM.GetDBSource("HOMEWORK", "select * from HOMEWORK where STUDENT_GROUP in (select ID from STUDENT_GROUP where TERM='" + TermService.GetCurrentTerm().ID + "' and active=1) and active=1").Cast<HOMEWORK>().ToList(); }
            catch { return null; }
        }

        public static List<HOMEWORK> GetHomeworks(int student_group)
        {
            try { return KennyORM.GetDBSource("HOMEWORK", "select * from HOMEWORK where STUDENT_GROUP='" + student_group + "' and active=1").Cast<HOMEWORK>().ToList(); }
            catch { return null; }
        }

        public static List<HOMEWORK> GetHomeworksByType(int homework_type)
        {
            try { return KennyORM.GetDBSource("HOMEWORK", "select * from HOMEWORK where STUDENT_GROUP in (select ID from STUDENT_GROUP where TERM='" + TermService.GetCurrentTerm().ID + "' and active=1) and HOMEWORK_TYPE=" + homework_type + " and active=1").Cast<HOMEWORK>().ToList(); }
            catch { return null; }
        }

        public static List<HOMEWORK> GetHomeworksByGroupAndType(int student_group, int homework_type)
        {
            try { return KennyORM.GetDBSource("HOMEWORK", "select * from HOMEWORK where STUDENT_GROUP='" + student_group + "' and HOMEWORK_TYPE=" + homework_type + " and active=1").Cast<HOMEWORK>().ToList(); }
            catch { return null; }
        }

        public static List<HOMEWORK> GetWorks()
        {
            try { return WillORM.GetDBSource("HOMEWORK", "select * from HOMEWORK where STUDENT_GROUP in (select ID from STUDENT_GROUP where TERM='" + TermService.GetCurrentTerm().ID + "' and active=1) and active=1").Cast<HOMEWORK>().ToList(); }
            catch { return null; }
        }

        public static List<HOMEWORK> GetFilteredWorks(int term_id, int group_id, int type_id)
        {
            List<MutliQuery> groupPram = new List<MutliQuery>();
            if (term_id > 0) { groupPram.Add(new MutliQuery() { field1 = "TERM", field2 = term_id.ToString(), function = null }); }
            if (group_id > 0) { groupPram.Add(new MutliQuery() { field1 = "ID", field2 = group_id.ToString(), function = null }); groupPram.Add(new MutliQuery() { field1 = "active", field2 = "1", function = null }); }
            
            string typeParm = type_id > 0? WillORM.QueryBuilder("ID", "HOMEWORK_TYPE", new List<MutliQuery>() { new MutliQuery() { field1 = "ID", field2 = type_id.ToString(), function = null } }, null) : null;
            
            List<MutliQuery> workParm = new List<MutliQuery>();
            if (groupPram.Any()){ workParm.Add(new MutliQuery() { field1 = "STUDENT_GROUP", field2 = "(" + WillORM.QueryBuilder("ID", "STUDENT_GROUP", groupPram, null) + ")", function = "in" }); }
            if (!string.IsNullOrEmpty(typeParm)) { workParm.Add(new MutliQuery() { field1 = "HOMEWORK_TYPE", field2 = "(" + typeParm + ")", function = "in" }); }
            workParm.Add(new MutliQuery() { field1 = "ACTIVE", field2 = "1", function = null });
            
            try { return WillORM.GetDBSource("HOMEWORK", WillORM.QueryBuilder(null, "HOMEWORK", workParm, null)).Cast<HOMEWORK>().ToList(); }
            catch { return null; }
        }

        public static HOMEWORK GetWork(int id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ID", id.ToString()},
                {"ACTIVE", "1"}
            };

            try { return WillORM.GetDBSource("HOMEWORK", WillORM.QueryBuilder(null, "HOMEWORK", DS, null)).Cast<HOMEWORK>().FirstOrDefault(); }
            catch { return null; }
        }

        public static List<HOMEWORK_TYPE> GetTypes()
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                { "ACTIVE", "1" }
            };
            
            try {return WillORM.GetDBSource("HOMEWORK_TYPE", WillORM.QueryBuilder(null, "HOMEWORK_TYPE", Ds, null)).Cast<HOMEWORK_TYPE>().ToList();}
            catch { return null; }
        }

        public static int AddType(HOMEWORK_TYPE newType)
        {
            try { return WillORM.InsertRecord(newType); }
            catch { return 0; }
        }

        public static bool InactiveType(int type_id, int user_id)
        {
            try { return WillORM.InactiveRecord("HOMEWORK_TYPE", type_id, user_id); }
            catch { return false; }
        }


        public static IEnumerable<SelectListItem> getTypeSelectList()
        {

            List<SelectListItem> TermItemList = new List<SelectListItem>();
            SelectListItem selectedValue = new SelectListItem();
            List<HOMEWORK_TYPE> typeList = GetTypes();

            if (typeList != null) {
                foreach (var type in typeList)
                {
                    TermItemList.Add(new SelectListItem() { Value = type.ID.ToString(), Text = type.NAME });
                }
            }
            return TermItemList;
        }

        public static List<HOMEWORK_SUBMISSION_LIST> getSubmission(int work_id)
        {
            Dictionary<string, string> condistions = new Dictionary<string, string>()
            {
                {"HOMEWORK", work_id.ToString()}
            };

            try { return WillORM.GetDBSource("HOMEWORK_SUBMISSION_LIST", WillORM.QueryBuilder(null, "HOMEWORK_SUBMISSION_LIST", condistions, null)).Cast<HOMEWORK_SUBMISSION_LIST>().ToList(); }
            catch { return null; }
        }

        public static HOMEWORK_TYPE getType(int type_id)
        {
            Dictionary<string, string> DS = new Dictionary<string, string>()
            {
                {"ID",type_id.ToString()}
            };

            try { return WillORM.GetDBSource("HOMEWORK_TYPE", WillORM.QueryBuilder(null, "HOMEWORK_TYPE", DS, null)).Cast<HOMEWORK_TYPE>().FirstOrDefault(); }
            catch { return null; }
        }

        public static bool InactiveWork(int work_id, int user_id)
        {
            try { return WillORM.InactiveRecord("HOMEWORK", work_id, user_id); }
            catch { return false; }
        }

        public static int AddWork(HOMEWORK newWork)
        {
            try { return WillORM.InsertRecord(newWork); }
            catch { return 0; }
        }

        public static bool InactiveSubmssion(int Submission_id, int user_id)
        {
            try { return WillORM.InactiveRecord("HOMEWORK_SUBMISSION_LIST", Submission_id, user_id); }
            catch { return false; }
        }

        public static int AddSubmission(HOMEWORK_SUBMISSION_LIST newSubmission)
        {
            try { return WillORM.InsertRecord(newSubmission); }
            catch { return 0; }
        }

        public static bool UpdateWork(HOMEWORK work)
        {
            work.MODIFY_BY = ClientSessionService.GetSession.loginedUser.ID;
            work.MODIFY_DATE = DateTime.Now;

            try { return WillORM.UpdateRecord(work); }
            catch { return false; }
        }

        public static bool UpdateSubmissionMarkRemark(HOMEWORK_SUBMISSION_LIST list)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                { "REMARK", list.REMARK},
                { "MARK", list.MARK.ToString()},
                {"MODIFY_BY", ClientSessionService.GetSession.loginedUser.ID.ToString()},
                {"MODIFY_DATE", DateTime.Now.ToString()}
            };
            
            try { return WillORM.UpdateRecord("HOMEWORK_SUBMISSION_LIST", parms, list.ID); }
            catch { return false; }
        }

        public static bool DeleteSubmission(int id)
        {
            try { return WillORM.DeleteRecord("HOMEWORK_SUBMISSION_LIST", id); }
            catch { return false; }
        }
    }
}
