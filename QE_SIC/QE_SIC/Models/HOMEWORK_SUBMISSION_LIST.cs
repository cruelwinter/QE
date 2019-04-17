using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class HOMEWORK_SUBMISSION_LIST
    {
        [Key]
        public int ID { get; set; }
        public int HOMEWORK { get; set; }
        public int STUDENT { get; set; }
        public int STUDENT_GROUP { get; set; }
        public int QE_CLASS { get; set; }
        public string GROUP_NAME { get; set; }
        public string QE_CLASS_NAME { get; set; }
        public string STUDENT_NAME { get; set; }
        public int CLASS_NUM { get; set; }
        public int MARK { get; set; }
        public string REMARK { get; set; }
        public int ADD_BY { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ADD_DATE { get; set; }

        public bool ACTIVE { get; set; }
        

        public HOMEWORK_SUBMISSION_LIST()
        {
            ID = Constant.DEF_INT;
            HOMEWORK = Constant.DEF_INT;
            STUDENT = Constant.DEF_INT;
            STUDENT_GROUP = Constant.DEF_INT;
            GROUP_NAME = Constant.DEF_STRING;
            QE_CLASS = Constant.DEF_INT;
            QE_CLASS_NAME = Constant.DEF_STRING;
            CLASS_NUM = Constant.DEF_INT;
            STUDENT_NAME = Constant.DEF_STRING;
            MARK = Constant.DEF_INT;
            REMARK = Constant.DEF_STRING;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }
    }
}
