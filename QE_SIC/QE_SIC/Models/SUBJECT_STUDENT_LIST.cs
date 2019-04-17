using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class SUBJECT_STUDENT_LIST
    {
        [Key]
        public int ID { get; set; }
        public int SUBJECT { get; set; }
        public int STUDENT { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public SUBJECT_STUDENT_LIST()
        {
            ID = Constant.DEF_INT;
            SUBJECT = Constant.DEF_INT;
            STUDENT = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;

        }
    }
}