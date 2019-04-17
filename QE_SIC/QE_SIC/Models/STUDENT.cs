using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class STUDENT
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid character(s).")]
        public string STUDENT_ID { get; set; }
        
        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Invalid character(s).")]
        public string STUDENT_NAME { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string STUDENT_NAME_CHI { get; set; }
        
        [Required(ErrorMessage = "House is required.")]
        [StringLength(10, ErrorMessage = "Cannot be longer than 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid character(s).")]
        public string HOUSE { get; set; }
        
        [Required(ErrorMessage = "HKID is required.")]
        [StringLength(10, ErrorMessage = "Cannot be longer than 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid character(s).")]
        public string HKID { get; set; }
        
        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10, ErrorMessage = "Cannot be longer than 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid character(s).")]
        public string GENDER { get; set; }


        [Required(ErrorMessage = "Date Birth is required.")]
        public DateTime DATE_BIRTH { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format.")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        public int PHONE { get; set; }

        public int PHONE_2 { get; set; }

        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string PHOTO_PATH { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Invalid character(s).")]
        public string ADDRESS { get; set; }

        [Required(ErrorMessage = "Date admit is required.")]
        public DateTime DATE_ADMIT { get; set; }


        [Required(ErrorMessage = "At least one guardian is required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Invalid character(s).")]
        public string GUARDIAN { get; set; }

        [Required(ErrorMessage = "Guardian's contact number is required.")]
        public int GUARDIAN_PHONE { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string GUARDIAN_2 { get; set; }

        public int GUARDIAN_PHONE_2 { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string GUARDIAN_3 { get; set; }

        public int GUARDIAN_PHONE_3 { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public string GUARDIAN_4 { get; set; }

        public int GUARDIAN_PHONE_4 { get; set; }
        
        public bool CARING_ARG { get; set; }
        public bool ATTENDANCE_ARG { get; set; }
        public bool HOMEWORK_ARG { get; set; }
        public int ADD_BY { get; set; }
        public DateTime ADD_DATE { get; set; }
        public int MODIFY_BY { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public bool ACTIVE { get; set; }

        public STUDENT()
        {
            ID = Constant.DEF_INT;
            STUDENT_ID = Constant.DEF_STRING;
            STUDENT_NAME = Constant.DEF_STRING;
            STUDENT_NAME_CHI = Constant.DEF_STRING;
            HOUSE = Constant.DEF_STRING;
            HKID = Constant.DEF_STRING;
            GENDER = Constant.DEF_STRING;
            DATE_BIRTH = Constant.DEF_DATETIME;
            EMAIL = Constant.DEF_STRING;
            PHONE = Constant.DEF_INT;
            PHONE_2 = Constant.DEF_INT;
            ADDRESS = Constant.DEF_STRING;
            DATE_ADMIT = Constant.DEF_DATETIME;
            GUARDIAN = Constant.DEF_STRING;
            GUARDIAN_PHONE = Constant.DEF_INT;
            GUARDIAN_2 = Constant.DEF_STRING;
            GUARDIAN_PHONE_2 = Constant.DEF_INT;
            GUARDIAN_3 = Constant.DEF_STRING;
            GUARDIAN_PHONE_3 = Constant.DEF_INT;
            GUARDIAN_4 = Constant.DEF_STRING;
            GUARDIAN_PHONE_4 = Constant.DEF_INT;
            CARING_ARG = Constant.DEF_BOOL;
            ATTENDANCE_ARG = Constant.DEF_BOOL;
            HOMEWORK_ARG = Constant.DEF_BOOL;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            ACTIVE = Constant.DEF_BOOL;
        }
    }
}