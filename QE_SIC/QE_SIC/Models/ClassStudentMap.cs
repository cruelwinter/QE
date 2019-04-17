using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class ClassStudentMap : QE_CLASS
    {
        public int STUDENT { get; set; }
        public string STUDENT_NAME { get; set; }
        public int CLASS_NUM { get; set; }
        public DateTime DATE_BIRTH { get; set; }
        public string HOUSE { get; set; }
        public string STUDENT_ID { get; set; }

        public ClassStudentMap(QE_CLASS qe_class, STUDENT student, CLASS_STUDENT_LIST map)
        {
            ID = qe_class.ID;
            TERM = qe_class.TERM;
            FORM = qe_class.FORM;
            NAME = qe_class.NAME;
            TEACHER = qe_class.TEACHER;
            TEACHER_2 = qe_class.TEACHER_2;
            CLASSROOM = qe_class.CLASSROOM;
            STUDENT = map.STUDENT;
            CLASS_NUM = map.CLASS_NUM;
            STUDENT_NAME = student.STUDENT_NAME;
            DATE_BIRTH = student.DATE_BIRTH;
            HOUSE = student.HOUSE;
            STUDENT_ID = student.STUDENT_ID;
        }
    }
}

