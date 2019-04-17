using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class SSUBJECT
    {
        [Key]
        public int ID { get; set; }
        public int SUBJECT { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        public string EDB_CODE { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid character(s).")]
        public string NAME { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        public string NAME_CHI { get; set; }

        [Required(ErrorMessage = "Full mark is required.")]
        public int FULL_MARK { get; set; }

        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }


        public SSUBJECT()
        {
            ID = Constant.DEF_INT;
            SUBJECT = Constant.DEF_INT;
            EDB_CODE = Constant.DEF_STRING;
            NAME = Constant.DEF_STRING;
            NAME_CHI = Constant.DEF_STRING;
            FULL_MARK = Constant.DEF_INT;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;

        }

    }
}