using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models
{
    public class StudentGroupViewModel
    {
        public SelectList TermList { get; set; }
        public SelectList UserList { get; set; }

        public int SelectedTerm { get; set; }
        public List<STUDENT_GROUP> groupList { get; set; }
        public List<QE_USER> AllUserList { get; set; }
        public STUDENT_GROUP NewGroup { get; set; }
        public List<int> InactiveGroupsList { get; set; }

        public StudentGroupViewModel()
        {
            SelectedTerm = Constant.DEF_INT;
            groupList = new List<STUDENT_GROUP>();
            AllUserList = new List<QE_USER>();
            NewGroup = new STUDENT_GROUP();
            InactiveGroupsList = new List<int>();
        }
    }
}