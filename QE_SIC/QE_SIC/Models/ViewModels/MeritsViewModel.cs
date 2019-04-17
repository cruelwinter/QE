using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class MeritsViewModel
    {
        public List<MERITS> merits { get; set; }
        public MERITS newMerit { get; set; }
        public List<int> inactiveList { get; set; }

        public SelectList TermList { get; set; }
        public int selectedTerm { get; set; } // store the selected term id

        public IEnumerable<SelectListItem> userList { get; set; }
        public int selectedUser { get; set; }

        public IEnumerable<SelectListItem> classList { get; set; }
        public int selectedClass { get; set; }

        public List<STUDENT> studentList { get; set; }
        public int selectedStudent { get; set; }
        
        public MeritsViewModel()
        {
            merits = new List<MERITS>();
            newMerit = new MERITS();
            inactiveList = new List<int>();
            selectedTerm = Constant.DEF_INT;
            selectedUser = Constant.DEF_INT;
            selectedClass = Constant.DEF_INT;
            selectedStudent = Constant.DEF_INT;
            studentList = new List<STUDENT>();
        }
    }
}
