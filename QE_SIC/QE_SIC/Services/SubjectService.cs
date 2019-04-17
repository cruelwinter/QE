using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QE.Models;
using System.Web.Mvc;

namespace QE.Services
{
    public class SubjectService
    {
        private static IDBservice DBS = new DBservice();
        

        public static List<SUBJECT> GetSubjects(int term)
        {
            Dictionary<string, string> D = new Dictionary<string, string>()
            {
                {"TERM",term.ToString()},
                {"ACTIVE","1"}
            };

            try { return DBS.Query<SUBJECT>(D); }
            catch { return new List<SUBJECT>(); }
        }

        public static List<SSUBJECT> GetSSubjects(int subject)
        {
            Dictionary<string, string> D = new Dictionary<string, string>()
            {
                {"SUBJECT",subject.ToString()},
                {"ACTIVE","1"}
            };

            try { return DBS.Query<SSUBJECT>(D);}
            catch { return new List<SSUBJECT>(); }
        }

        public static SUBJECT GetSubject(int id)
        {
            try { return KennyORM.GetDBSource("SUBJECT", "select * from SUBJECT where ID="+id+" and active=1").Cast<SUBJECT>().First(); }
            catch { return new SUBJECT(); }
        }

        public static SSUBJECT GetSSubject(int id)
        {
            try { return KennyORM.GetDBSource("SSUBJECT", "select * from SSUBJECT where ID=" + id + " and active=1").Cast<SSUBJECT>().First(); }
            catch { return new SSUBJECT(); }
        }

        public static SelectList GetSelectList(int id) // term id
        {
            List<SelectListItem> SubjectItemList = new List<SelectListItem>();
            SelectListItem selectedValue = new SelectListItem();

            SubjectItemList.Add(new SelectListItem() { Value = "0", Text = "New main subject" });
            foreach (var subject in GetSubjects(id))

            {
                SubjectItemList.Add(new SelectListItem() { Value = subject.ID.ToString(), Text = subject.NAME});
            }
            return new SelectList(SubjectItemList, "Value", "Text");
        }

        public static SelectList GetSelectList(List<SubjectAndSSubjects> subjectAndSSubject)
        {
            List<SelectListItem> SubjectItemList = new List<SelectListItem>();
            SubjectItemList.Add(new SelectListItem() { Value = "0", Text = "New main subject" });
            foreach (var item in subjectAndSSubject)
            {
                SubjectItemList.Add(new SelectListItem() { Value = item.MainSubject.ID.ToString(), Text = item.MainSubject.NAME });
            }
            return new SelectList(SubjectItemList, "Value", "Text");            
        }

        public static List<SubjectAndSSubjects> getSubjectAndSSubjectsList(int term_id)
        {
            List<SubjectAndSSubjects> SubjectAndSSubjectsList = new List<SubjectAndSSubjects>();
            List<SUBJECT> subjects = DBS.findActiveRecordsBySingleParm<SUBJECT>("TERM", term_id);
            List<SSUBJECT> ssubjects = DBS.findActiveRecords<SSUBJECT>();

            foreach (var subject in subjects)
            {
                List<SSUBJECT> ss = ssubjects.FindAll(s => s.SUBJECT == subject.ID);
                SubjectAndSSubjectsList.Add(new SubjectAndSSubjects(subject, ss));
            }
            return SubjectAndSSubjectsList;
        }
        
        public static SUBJECT SubjectAdapter(SSUBJECT ss)
        {
            SUBJECT s = new SUBJECT();
            s.EDB_CODE = ss.EDB_CODE;
            s.NAME = ss.NAME;
            s.NAME_CHI = ss.NAME_CHI;
            s.FULL_MARK = ss.FULL_MARK;
            s.ADD_BY = ss.ADD_BY;
            s.ADD_DATE = ss.ADD_DATE;
            s.MODIFY_BY = ss.MODIFY_BY;
            s.MODIFY_DATE = ss.MODIFY_DATE;
            s.ACTIVE = ss.ACTIVE;
            return s;          
        }
    }
}