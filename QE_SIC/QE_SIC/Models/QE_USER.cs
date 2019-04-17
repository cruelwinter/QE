using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class QE_USER
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        [StringLength(30, ErrorMessage = "Cannot be longer than 30 characters.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Invalid character(s).")]
        public string USER_ID { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Invalid character(s).")]
        public string USER_NAME { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Invalid character(s) .")]
        public string USER_NAME_CHI { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid character(s).")]
        public string POSITION { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format.")]
        //[EmailAddress]
        public string EMAIL { get; set; }

        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format.")]
        //[EmailAddress]
        public string EMAIL_2 { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid character(s).")]
        public int PHONE { get; set; }


        [Required(ErrorMessage = "User Id is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid character(s).")]
        public int PHONE_2 { get; set; }

        public string PHOTO_PATH { get; set; }

        public DateTime FIRST_LOGIN { get; set; }

        public DateTime LAST_LOGIN { get; set; }

        public int ADD_BY { get; set; }

        public DateTime ADD_DATE { get; set; }

        public int MODIFY_BY { get; set; }

        public DateTime MODIFY_DATE { get; set; }

        public string PASSWORD { get; set; }

        public bool ACTIVE { get; set; }

        public QE_USER()
        {
            ID = Constant.DEF_INT;
            USER_ID = Constant.DEF_STRING;
            USER_NAME = Constant.DEF_STRING;
            USER_NAME_CHI = Constant.DEF_STRING;
            POSITION = Constant.DEF_STRING;
            EMAIL = Constant.DEF_STRING;
            EMAIL_2 = Constant.DEF_STRING;
            PHONE = Constant.DEF_INT;
            PHONE_2 = Constant.DEF_INT;
            PHOTO_PATH = Constant.DEF_STRING;
            FIRST_LOGIN = Constant.DEF_DATETIME;
            LAST_LOGIN = Constant.DEF_DATETIME;
            ADD_BY = Constant.DEF_INT;
            ADD_DATE = Constant.DEF_DATETIME;
            MODIFY_BY = Constant.DEF_INT;
            MODIFY_DATE = Constant.DEF_DATETIME;
            PASSWORD = Constant.DEF_STRING;
            ACTIVE = Constant.DEF_BOOL;
        }

        public QE_USER(String USER_ID, String USER_NAME, String USER_NAME_CHI, String POSITION, String EMAIL, String EMAIL_2, int PHONE, int PHONE_2, string PHOTO_PATH, DateTime FIRST_LOGIN, DateTime LAST_LOGIN, int ADD_BY, DateTime ADD_DATE, int MODIFY_BY, DateTime MODIFY_DATE, String PASSWORD, bool ACTIVE)
        {

            this.USER_ID = USER_ID;
            this.USER_NAME = USER_NAME;
            this.USER_NAME_CHI = USER_NAME_CHI;
            this.POSITION = POSITION;
            this.EMAIL = EMAIL;
            this.EMAIL_2 = EMAIL_2;
            this.PHONE = PHONE;
            this.PHONE_2 = PHONE_2;
            this.PHOTO_PATH = PHOTO_PATH;
            this.FIRST_LOGIN = FIRST_LOGIN;
            this.LAST_LOGIN = LAST_LOGIN;
            this.ADD_BY = ADD_BY;
            this.ADD_DATE = ADD_DATE;
            this.MODIFY_BY = MODIFY_BY;
            this.MODIFY_DATE = MODIFY_DATE;
            this.PASSWORD = PASSWORD;
            this.ACTIVE = ACTIVE;
        }
    }
}