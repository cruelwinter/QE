using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class HOMEWORK
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public int STUDENT_GROUP { get; set; }
        public int HOMEWORK_TYPE { get; set; }
        
        [Required(ErrorMessage = "Due date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DUE_DATE { get; set; }

        public int FULL_MARK { get; set; }
        public decimal SUBMISSION_RATIO { get; set; }
        public decimal LATE_RATIO { get; set; }
        public int ADD_BY { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ADD_DATE { get; set; }

        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public HOMEWORK()
        {
            ID = Constant.DEF_INT;
            NAME = Constant.DEF_STRING;
            STUDENT_GROUP = Constant.DEF_INT;
            HOMEWORK_TYPE = Constant.DEF_INT;
            DUE_DATE = Constant.DEF_DATETIME;
            FULL_MARK = Constant.DEF_INT;
            SUBMISSION_RATIO = Constant.DEF_DEC;
            LATE_RATIO = Constant.DEF_DEC;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }
    }
}