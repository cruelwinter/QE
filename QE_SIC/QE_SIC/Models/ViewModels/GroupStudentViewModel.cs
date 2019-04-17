using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class GroupStudentViewModel
    {
        public List<STUDENT_GROUP> Groups { get; set; }
        public List<QE_CLASS> Classes { get; set; }
        public List<STUDENT> Students { get; set; }
        public List<QE_USER> Teachers { get; set; }
        public List<STUDENT_GROUP_STUDENT_LIST> Map { get; set; }
        public List<GroupAndStudent> Student { get; set; }

        public SelectList TermList { get; set; }
        public int SelectedTerm { get; set; }
        public int SelectedGroup { get; set; }
        public int SelectedClass { get; set; }

        public GroupStudentViewModel()
        {
            Groups = new List<STUDENT_GROUP>();
            Classes = new List<QE_CLASS>();
            Students = new List<STUDENT>();
            Teachers = new List<QE_USER>();
            Map = new List<STUDENT_GROUP_STUDENT_LIST>();
            SelectedTerm = Constant.DEF_INT;
            SelectedGroup = Constant.DEF_INT;
            SelectedClass = Constant.DEF_INT;
            Student = new List<GroupAndStudent>();
        }

        public GroupStudentViewModel(List<STUDENT_GROUP> Groups, List<QE_CLASS> Classes, List<STUDENT> Students, List<QE_USER> Teachers, List<STUDENT_GROUP_STUDENT_LIST> Map, List<GroupAndStudent> Student,SelectList TermList, int SelectedTerm, int SelectedGroup, int SelectedClass)
        {
            this.Groups = Groups;
            this.Classes = Classes;
            this.Students = Students;
            this.Teachers = Teachers;
            this.Student = Student;
            this.Map = Map;
            this.SelectedTerm = SelectedTerm;
            this.TermList = TermList;
            this.SelectedGroup = SelectedGroup;
            this.SelectedClass = SelectedClass;
        }
    }

    public class GroupAndStudent
    {
        public int GroupID { get; set; }
        public int StudentID { get; set; }
        public int MapID { get; set; }
        public string Student_Name { get; set; }
        public bool Contain { get; set; }

        public GroupAndStudent()
        {
            GroupID = Constant.DEF_INT;
            StudentID = Constant.DEF_INT;
            MapID = Constant.DEF_INT;
            Student_Name = Constant.DEF_STRING;
            Contain = Constant.DEF_BOOL;
        }

        public GroupAndStudent(int GroupID, int StudentID, int MapID, string Student_Name, bool Contain)
        {
            this.GroupID = GroupID;
            this.StudentID = StudentID;
            this.MapID = MapID;
            this.Student_Name = Student_Name;
            this.Contain = Contain;
        }
    }
}

