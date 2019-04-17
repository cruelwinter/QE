using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models
{
    public class SubjectViewModel
    {
        
        public SelectList TermList { get; set; }
        public int SelectedTerm { get; set; } // store the selected term id

        public List<SubjectAndSSubjects> SubjectAndSSubjects { get; set; }

        public SelectList SubjectList { get; set; }
        public int SelectedSubject { get; set; }  // store the selected subject id
        
        public SSUBJECT NewSubject { get; set; }

        public List<int> InactiveSubjectsList { get; set; } // store the id of subject user want to delete
        public List<int> InactiveSSubjectsList { get; set; } // store the id of subsubject user want to delete
        public SubjectViewModel()
        {
            SubjectAndSSubjects = new List<SubjectAndSSubjects>();
            SelectedTerm = Constant.DEF_INT;
            NewSubject = new SSUBJECT();
            InactiveSubjectsList = new List<int>();
            InactiveSSubjectsList = new List<int>();
        }

    }

    public class SubjectAndSSubjects
    {
        public SUBJECT MainSubject { get; set; }
        public List<SSUBJECT> SSubjects { get; set; }

        public SubjectAndSSubjects()
        {
            MainSubject = new SUBJECT();
            SSubjects = new List<SSUBJECT>();
        }

        public SubjectAndSSubjects(SUBJECT MainSubject, List<SSUBJECT> SSubjects)
        {
            this.MainSubject = MainSubject;
            this.SSubjects = SSubjects;
        }
    }
}