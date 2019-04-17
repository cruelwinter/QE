using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class CLASS_STUDENT_LIST
    {
        [Key]
        public int ID { get; set; }
        public int QE_CLASS { get; set; }
        public int STUDENT { get; set; }
        public int CLASS_NUM { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public CLASS_STUDENT_LIST()
        {
            ID = Constant.DEF_INT;
            QE_CLASS = Constant.DEF_INT;
            STUDENT = Constant.DEF_INT;
            CLASS_NUM = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }

        public CLASS_STUDENT_LIST(int QE_CLASS, int STUDENT, int CLASS_NUM, int ADD_BY, DateTime ADD_DATE, int MODIFY_BY, DateTime MODIFY_DATE, bool ACTIVE)
        {
            this.QE_CLASS = QE_CLASS;
            this.STUDENT = STUDENT;
            this.CLASS_NUM = CLASS_NUM;
            this.ADD_BY = ADD_BY;
            this.ADD_DATE = ADD_DATE;
            this.MODIFY_BY = MODIFY_BY;
            this.MODIFY_DATE = MODIFY_DATE;
            this.ACTIVE = ACTIVE;
        }

    }
}
