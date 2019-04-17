using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class SUBJECT
    {     
        [Key]
        public int ID { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        [RegularExpression("^a-zA-Z0-9", ErrorMessage = "Invalid character(s).")]
        public string EDB_CODE { get; set; }

        [Required(ErrorMessage = "Subject name is required.")]
        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        [RegularExpression("^a-zA-Z0-9", ErrorMessage = "Invalid character(s).")]
        public string NAME { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        [RegularExpression("^a-zA-Z0-9", ErrorMessage = "Invalid character(s).")]
        public string NAME_CHI { get; set; }


        public int TERM { get; set; }
        public int FULL_MARK { get; set; }

        [Required]
        public int ADD_BY { get; set; }

        [Required]
        public DateTime ADD_DATE { get; set; }

        public int MODIFY_BY { get; set; }

        public DateTime MODIFY_DATE { get; set; }

        [Required]
        public bool ACTIVE { get; set; }

        public SUBJECT()
        {
            ID = Constant.DEF_INT;
            EDB_CODE = Constant.DEF_STRING;
            NAME = Constant.DEF_STRING;
            NAME_CHI = Constant.DEF_STRING;
            TERM = Constant.DEF_INT;
            FULL_MARK = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }

        public SUBJECT(string EDB_CODE, string NAME, string NAME_CHI, int FULL_MARK, int ADD_BY, DateTime ADD_DATE, int MODIFY_BY, DateTime MODIFY_DATE, bool ACTIVE)
        {
            this.EDB_CODE = EDB_CODE;
            this.NAME = NAME;
            this.NAME_CHI = NAME_CHI;
            this.FULL_MARK = FULL_MARK;
            this.ADD_BY = ADD_BY;
            this.ADD_DATE = ADD_DATE;
            this.MODIFY_BY = MODIFY_BY;
            this.MODIFY_DATE = MODIFY_DATE;
            this.ACTIVE = ACTIVE;
        }
    }
}