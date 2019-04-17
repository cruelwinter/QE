using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QE.Models;
using System.ComponentModel.DataAnnotations;

namespace QE.Models.ViewModels
{
    public class HomeworkViewModel
    {
        public HOMEWORK newWork { get; set; }
        public List<int> inactiveList { get; set; }
        public List<HomeworkView> viewList { get; set; }
        public List<Submission_view> submission_list { get; set; }
        public IEnumerable<SelectListItem> groupSelectList { get; set; }
        public IEnumerable<SelectListItem> typeSelectList { get; set; }

        public IEnumerable<SelectListItem> termFilList { get; set; }
        public IEnumerable<SelectListItem> groupFilList { get; set; }
        public IEnumerable<SelectListItem> typeFilList { get; set; }
        public int SelectedTerm { get; set; }
        public int SelectedGroup { get; set; }
        public int SelectedType { get; set; }

        public HomeworkViewModel()
        {
            newWork = new HOMEWORK();
            inactiveList = new List<int>();
            viewList = new List<HomeworkView>();
            submission_list = new List<Submission_view>();
            SelectedTerm = 0;
            SelectedGroup = 0;
            SelectedType = 0;
        }
    }

    public class HomeworkView
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_assign { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_due { get; set; }

        public int type_id { get; set; }
        public string type_name { get; set; }
        public decimal submission_ratio { get; set; }
        public decimal late_ratio { get; set; }
        public int full_mark { get; set; }

        public HomeworkView()
        {
            ID = Constant.DEF_INT;
            name = Constant.DEF_STRING;
            group_id = Constant.DEF_INT;
            group_name = Constant.DEF_STRING;
            date_assign = Constant.DEF_DATETIME;
            date_due = Constant.DEF_DATETIME;
            type_id = Constant.DEF_INT;
            type_name = Constant.DEF_STRING;
            submission_ratio = Constant.DEF_INT;
            late_ratio = Constant.DEF_INT;
        }
    }

    public class Submission_view
    {
        public int homework_id { get; set; }
        public int student_id { get; set; }
        public int group_id { get; set; }
        public int QE_class { get; set; }
        public string work_name { get; set; }
        public string group_name { get; set; }
        public string QE_class_name { get; set; }
        public int class_num { get; set; }
        public string student_name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime submit_date { get; set; }

        public int mark { get; set; }
        public string remark { get; set; }
        public int submission_id { get; set; }
        public bool contain { get; set; }
        

        public Submission_view()
        {
            homework_id = Constant.DEF_INT;
            student_id = Constant.DEF_INT;
            group_id = Constant.DEF_INT;
            QE_class = Constant.DEF_INT;
            work_name = Constant.DEF_STRING;
            group_name = Constant.DEF_STRING;
            QE_class_name = Constant.DEF_STRING;
            class_num = Constant.DEF_INT;
            student_name = Constant.DEF_STRING;
            submit_date = Constant.DEF_DATETIME;
            mark = Constant.DEF_INT;
            remark = Constant.DEF_STRING;
            submission_id = Constant.DEF_INT;
            contain = Constant.DEF_BOOL;
        }
    }
}

