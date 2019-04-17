using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class STUDENT_GROUP_STUDENT_LIST
    {
        [Key]
        public int ID { get; set; }

        public int STUDENT_GROUP { get; set; }
        public int STUDENT { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public STUDENT_GROUP_STUDENT_LIST()
        {
            ID = Constant.DEF_INT;
            STUDENT_GROUP = Constant.DEF_INT;
            STUDENT = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }

        public STUDENT_GROUP_STUDENT_LIST(int ID, int STUDENT_GROUP, int STUDENT, int ADD_BY, DateTime ADD_DATE, int MODIFY_BY, DateTime MODIFY_DATE, bool ACTIVE )
        {
            this.ID = ID;
            this.STUDENT_GROUP = STUDENT_GROUP;
            this.STUDENT = STUDENT;
            this.ADD_BY = ADD_BY;
            this.ADD_DATE = ADD_DATE;
            this.MODIFY_BY = MODIFY_BY;
            this.MODIFY_DATE = MODIFY_DATE;
            this.ACTIVE = ACTIVE;
        }
    }
}