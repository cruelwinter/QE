using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using System.Web.Mvc;

namespace QE.Services
{
    public class StudentGroupService
    {
        public static STUDENT_GROUP GetStudentGroup(int id)
        {
            try { return KennyORM.GetDBSource("STUDENT_GROUP", "select * from STUDENT_GROUP where ID=" + id + " and active=1").Cast<STUDENT_GROUP>().First(); }
            catch { return null; }
        }

        public static List<STUDENT_GROUP> GetStudentGroups()
        {
            try { return KennyORM.GetDBSource("STUDENT_GROUP", "select * from STUDENT_GROUP where TERM='" + TermService.GetCurrentTerm().ID + "' and active=1").Cast<STUDENT_GROUP>().ToList(); }
            catch { return null; }
        }

        public static List<STUDENT_GROUP> GetStudentGroups(int subject)
        {
            try { return KennyORM.GetDBSource("STUDENT_GROUP", "select * from STUDENT_GROUP where TERM='" + TermService.GetCurrentTerm().ID + "' and SUBJECT=" + subject + " and active=1").Cast<STUDENT_GROUP>().ToList(); }
            catch { return null; }
        }


        public static List<STUDENT_GROUP> GetStudentGroupByTerm(int term_id)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                {"TERM", term_id.ToString()},
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("STUDENT_GROUP", WillORM.QueryBuilder(null, "STUDENT_GROUP", Ds, null)).Cast<STUDENT_GROUP>().ToList(); }
            catch (Exception e) { return null; }
        }

        public static bool InactiveGroup(int group_id, int user_id)
        {
            try { return WillORM.InactiveRecord("STUDENT_GROUP", group_id, user_id); }
            catch { return false; }
        }

        public static int CreateGroup(STUDENT_GROUP newGroup) { int newGroup_id = WillORM.InsertRecord(newGroup); return newGroup_id > 0 ? newGroup_id : 0; }

        public static List<STUDENT_GROUP_SUBJECT_LIST> GetGroupSubjectMap(int group_id)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                {"STUDENT_GROUP", group_id.ToString()},
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("STUDENT_GROUP_SUBJECT_LIST", WillORM.QueryBuilder(null, "STUDENT_GROUP_SUBJECT_LIST", Ds, null)).Cast<STUDENT_GROUP_SUBJECT_LIST>().ToList(); }
            catch (Exception e) { return null; }
        }

        public static List<STUDENT_GROUP_STUDENT_LIST> GetGroupStudentMap(int group_id)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                {"STUDENT_GROUP", group_id.ToString()},
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("STUDENT_GROUP_STUDENT_LIST", WillORM.QueryBuilder(null, "STUDENT_GROUP_STUDENT_LIST", Ds, null)).Cast<STUDENT_GROUP_STUDENT_LIST>().ToList(); }
            catch (Exception e) { return null; }
        }

        public static int AddGroupSubjectMap(STUDENT_GROUP_SUBJECT_LIST newMap) { int newMap_id = WillORM.InsertRecord(newMap); return newMap_id > 0 ? newMap_id : 0; }

        public static int AddGroupStudentMap(STUDENT_GROUP_STUDENT_LIST newMap) { int newMap_id = WillORM.InsertRecord(newMap); return newMap_id > 0 ? newMap_id : 0; }

        public static bool InactiveSubjectMap(int map_id, int user_id)
        {
            try { return WillORM.InactiveRecord("STUDENT_GROUP_SUBJECT_LIST", map_id, user_id); }
            catch { return false; }
        }

        public static bool InactiveStudentMap(int map_id, int user_id)
        {
            try { return WillORM.InactiveRecord("STUDENT_GROUP_STUDENT_LIST", map_id, user_id); }
            catch { return false; }
        }
        
        
        public static List<STUDENT_GROUP> GetGroups()
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                { "ACTIVE", "1" }
            };

            try { return WillORM.GetDBSource("STUDENT_GROUP", WillORM.QueryBuilder(null, "STUDENT_GROUP", Ds, null)).Cast<STUDENT_GROUP>().ToList(); }
            catch (Exception e) { return null; }
        }

        public static List<STUDENT_GROUP> GetGroupsByTerm(int term_id)
        {
            Dictionary<string, string> Ds = new Dictionary<string, string>()
            {
                { "ACTIVE", "1" }
            };

            if(term_id > 0) { Ds.Add("TERM", term_id.ToString()); }

            try { return WillORM.GetDBSource("STUDENT_GROUP", WillORM.QueryBuilder(null, "STUDENT_GROUP", Ds, null)).Cast<STUDENT_GROUP>().ToList(); }
            catch (Exception e) { return new List<STUDENT_GROUP>(); }
        }

        public static IEnumerable<SelectListItem> getGroupSelectList(int term_id)
        {
            List<SelectListItem> TermItemList = new List<SelectListItem>();
            SelectListItem selectedValue = new SelectListItem();
            foreach (var group in GetGroupsByTerm(term_id))
            {
                TermItemList.Add(new SelectListItem() { Value = group.ID.ToString(), Text = group.NAME});
            }
            return TermItemList;
        }
    }
}


