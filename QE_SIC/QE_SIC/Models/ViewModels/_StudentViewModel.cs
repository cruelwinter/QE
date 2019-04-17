using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class _StudentViewModel
    {
        public List<STUDENT> studentList { get; set; }
        public List<CLASS_STUDENT_LIST> ClassStudentMap { get; set; }
        public STUDENT selectedStudent { get; set; }
        public int selectedStudentNum { get; set; }
        public int selectedClass { get; set; }
        public int selectedTerm { get; set; }



        public _StudentViewModel()
        {
            studentList = new List<STUDENT>();
            selectedStudent = new STUDENT();
            ClassStudentMap = new List<CLASS_STUDENT_LIST>();
            selectedStudentNum = Constant.DEF_INT;
            selectedClass = Constant.DEF_INT;
            selectedTerm = Constant.DEF_INT;

        }

        public _StudentViewModel(List<STUDENT> studentList, STUDENT selectedStudent, List<CLASS_STUDENT_LIST> ClassStudentMap, int selectedStudentNum, int selectedClass, int selectedTerm)
        {
            this.studentList = studentList;
            this.selectedStudent = selectedStudent;
            this.ClassStudentMap = ClassStudentMap;
            this.selectedStudentNum = selectedStudentNum;
            this.selectedClass = selectedClass;
            this.selectedTerm = selectedTerm;
        }
    }
}
