using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QE.Models.ViewModels
{
    public class GroupSubjectViewModel
    {
        public List<STUDENT_GROUP> Groups { get; set; }
        public List<QE_USER> Teachers { get; set; }
        public List<GroupAndSubject> GSs { get; set; }
        public List<STUDENT_GROUP_SUBJECT_LIST> Map { get; set; }

        public SelectList TermList { get; set; }
        public int SelectedTerm { get; set; }

        public GroupSubjectViewModel()
        {
            Groups = new List<STUDENT_GROUP>();
            Teachers = new List<QE_USER>();
            GSs = new List<GroupAndSubject>();
            Map = new List<STUDENT_GROUP_SUBJECT_LIST>();
            SelectedTerm = Constant.DEF_INT;
        }

        public GroupSubjectViewModel(List<STUDENT_GROUP> Groups, List<QE_USER> Teachers, List<GroupAndSubject> GSs, List<STUDENT_GROUP_SUBJECT_LIST> Map, SelectList TermList, int SelectedTerm)
        {
            this.Groups = Groups;
            this.Teachers = Teachers;
            this.GSs = GSs;
            this.Map = Map;
            this.SelectedTerm = SelectedTerm;
            this.TermList = TermList;
        }
    }
    
    public class GroupAndSubject
    {
        public int GroupID { get; set; }
        public int SubjectID { get; set; }
        public int MapID { get; set; }
        public string EBD_CODE { get; set; }
        public string Subject_Name { get; set; }
        public bool Contain { get; set; }

        public GroupAndSubject()
        {

            GroupID = Constant.DEF_INT;
            SubjectID = Constant.DEF_INT;
            MapID = Constant.DEF_INT;
            EBD_CODE = Constant.DEF_STRING;
            Subject_Name = Constant.DEF_STRING;
            Contain = Constant.DEF_BOOL;
        }

        public GroupAndSubject(int GroupID, int SubjectID, int MapID, string EBD_CODE, string Subject_Name, bool Contain) {

            this.GroupID = GroupID;
            this.SubjectID = SubjectID;
            this.MapID = MapID;
            this.EBD_CODE = EBD_CODE;
            this.Subject_Name = Subject_Name;
            this.Contain = Contain;
        }
    }
}

