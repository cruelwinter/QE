using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class STUDENT_GROUP
    {
        [Key]
        public int ID { get; set; }

        public string NAME { get; set; }
        public int TEACHER { get; set; }
        public int TEACHER_2 { get; set; }
        public int TEACHER_3 { get; set; }
        public int TEACHER_4 { get; set; }
        public int TERM { get; set; }
        public string LANGUAGE { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public STUDENT_GROUP()
        {
            ID = Constant.DEF_INT;
            NAME = Constant.DEF_STRING;
            TEACHER = Constant.DEF_INT;
            TEACHER_2 = Constant.DEF_INT;
            TEACHER_3 = Constant.DEF_INT;
            TEACHER_4 = Constant.DEF_INT;
            TERM = Constant.DEF_INT;
            LANGUAGE = Constant.DEF_STRING;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }

        public STUDENT_GROUP(string NAME, int TEACHER, int TEACHER_2, int TEACHER_3, int TEACHER_4, int TERM, string LANGUAGE, int ADD_BY, DateTime ADD_DATE, int MODIFY_BY, DateTime MODIFY_DATE, bool ACTIVE)
        {
            this.NAME = NAME;
            this.TEACHER = TEACHER;
            this.TEACHER_2 = TEACHER_2;
            this.TEACHER_3 = TEACHER_3;
            this.TEACHER_4 = TEACHER_4;
            this.TERM = TERM;
            this.LANGUAGE = LANGUAGE;
            this.ADD_BY = ADD_BY;
            this.ADD_DATE = ADD_DATE;
            this.MODIFY_BY = MODIFY_BY;
            this.MODIFY_DATE = MODIFY_DATE;
            this.ACTIVE = ACTIVE;
        }
    }
}

