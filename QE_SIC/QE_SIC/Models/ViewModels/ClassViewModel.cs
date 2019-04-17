using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class ClassViewModel
    {
        public List<ClassView> classes { get; set; }
        public SelectList TermList { get; set; }
        public int SelectedTerm { get; set; }
        public IEnumerable<SelectListItem> teachers { get; set; }
        public QE_CLASS newClass { get; set; }
        public List<int> inactiveList { get; set; }

        public ClassViewModel()
        {
            classes = new List<ClassView>();
            newClass = new QE_CLASS();
            inactiveList = new List<int>();
        }
    }

    public class ClassView :QE_CLASS
    {
        public string teacher_name { get; set; }
        public string teacher_2_name { get; set; }

        public ClassView(QE_CLASS QE_Class, string name, string name_2)
        {
            ID = QE_Class.ID;
            TERM = QE_Class.TERM;
            FORM = QE_Class.FORM;
            NAME = QE_Class.NAME;
            TEACHER = QE_Class.TEACHER;
            teacher_name = name;
            TEACHER_2 = QE_Class.TEACHER_2;
            teacher_2_name = name_2;
            CLASSROOM = QE_Class.CLASSROOM;
            ADD_BY = QE_Class.ADD_BY;
            ADD_DATE = QE_Class.ADD_DATE;
            MODIFY_BY = QE_Class.MODIFY_BY;
            MODIFY_DATE = QE_Class.MODIFY_DATE;
            ACTIVE = QE_Class.ACTIVE;
        }
        public ClassView()
        {
            ID = Constant.DEF_INT;
            TERM = Constant.DEF_INT;
            FORM = Constant.DEF_INT;
            NAME = Constant.DEF_STRING;
            TEACHER = Constant.DEF_INT;
            teacher_name = Constant.DEF_STRING;
            TEACHER_2 = Constant.DEF_INT;
            teacher_2_name = Constant.DEF_STRING;
            CLASSROOM = Constant.DEF_STRING;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }
    }
}