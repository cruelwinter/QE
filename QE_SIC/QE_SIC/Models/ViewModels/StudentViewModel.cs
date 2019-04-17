using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class StudentViewModel
    {
        public SelectList TermList { get; set; }
        public int SelectedTerm { get; set; } // store the selected term id
        
        public List<QE_CLASS> ClassList { get; set; }
        public int SelectedClass { get; set; }  // store the selected class id
        
        public StudentViewModel()
        {
            SelectedTerm = Constant.DEF_INT;
            SelectedClass = Constant.DEF_INT;
        }

    }
}