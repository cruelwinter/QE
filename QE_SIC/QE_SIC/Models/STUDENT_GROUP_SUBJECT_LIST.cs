using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class STUDENT_GROUP_SUBJECT_LIST
    {
        [Key]
        public int ID { get; set; }

        public int STUDENT_GROUP { get; set; }
        public int SUBJECT { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public STUDENT_GROUP_SUBJECT_LIST()
        {
            ID = Constant.DEF_INT;
            STUDENT_GROUP = Constant.DEF_INT;
            SUBJECT = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }

        public STUDENT_GROUP_SUBJECT_LIST(int ID, int STUDENT_GROUP, int SUBJECT, int ADD_BY, DateTime ADD_DATE, int MODIFY_BY, DateTime MODIFY_DATE, bool ACTIVE)
        {
            this.ID = ID;
            this.STUDENT_GROUP = STUDENT_GROUP;
            this.SUBJECT = SUBJECT;
            this.ADD_BY = ADD_BY;
            this.ADD_DATE = ADD_DATE;
            this.MODIFY_BY = MODIFY_BY;
            this.MODIFY_DATE = MODIFY_DATE;
            this.ACTIVE = ACTIVE;
        }
    }
}
