using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class TERM
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Term name is required")]
        [StringLength(20, ErrorMessage = "Term name can be no larger than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Term name format invalid")]
        public string TERM_NAME { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TERM_START { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TERM_END { get; set; }

        public bool ACTIVE { get; set; }

        public TERM()
        {
            ID = Constant.DEF_INT;
            TERM_NAME = Constant.DEF_STRING;
            TERM_START = Constant.DEF_DATETIME;
            TERM_END = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;

        }
    }
}