using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QE.Models
{
    public class Constant
    {
        public static string title = "Queen Elizabeth School Old Students' Association Secondary School";
        public static string DEF_STRING = string.Empty;
        public static int DEF_INT = 0;
        public static decimal DEF_DEC = 0m;
        public static DateTime DEF_DATETIME = new DateTime(1900, 1, 1);
        public static bool DEF_BOOL = false;

        public static string DATETIME_FORMAT = "yyyy-MM-dd HH':'mm':'ss";
        public static string DATE_FORMAT = "yyyy-MM-dd";
        public static string TIME_FORMAT = "HH':'mm':'ss";

        public static string msg_error = "msg_error";
        public static string msg_success = "msg_success";
        public static string ChangeSucceed = "Changes have been saved";
        public static string ChangeFailed = "Changes have not been saved";


        public static string IMGPATH = "/Content/Img/Logo/";
        public static string AdminAndSetupIMGPATH = "/Content/Img/Logo/AdminAndSetup/";
        public static string StudentIMGPATH = "/Content/Img/Logo/Student/";
        public static string HomeworkIMGPATH = "/Content/Img/Logo/Homework/";
        public static string ECAIMGPATH = "/Content/Img/Logo/ECA/";
        public static string ARIMGPATH = "/Content/Img/Logo/AcademicResult/";
        public static string MeritDemeritIMGPATH = "/Content/Img/Logo/MeritDemerit/";
        public static string EnquiryIMGPATH = "/Content/Img/Logo/Enquiry/";
        public static string ProfilePicPATH = "/Content/Img/UserPic/";

        public class BoundaryCondition
        {
            public const int MinYear = 1990;
            public static int MaxYear = DateTime.Today.Year + 10;
            public const decimal MinMeasure = 0;
            public const decimal MaxMeasure = 300;
            public const char Email_Vaildate = '@';
            public const int HKID_Max = 11;
            public const int HKID_Min = 7;
            public const int MaxPhone = 12;
            public const int MinPhone = 8;
            public const int IDMaxNum = 9999999;
        }

        public class LoginViewLabel
        {
            public const string btnLogin = "LOGIN";
            public const string UserName = "USERNAME";
            public const string Password = "PASSWORD";
            public const string RememberMe = "REMEMBER ME";
            public const string ForgotPassword = "Forgot password?";
            public const string Logout = "Logout";
        }

        public class LogoImgPath
        {
            public static string DashBoard = "QE_dashboard_off.png";
            public static string AdminAndSetup = "QE_admin_off.png";
            public static string Student = "QE_student_off.png";
            public static string Homework = "QE_hw_off.png";
            public static string ECA = "QE_eca_off.png";
            public static string AcademicResult = "QE_result_off.png";
            public static string MeritDemerit = "QE_MD_off.png";
            public static string Enquiry = "QE_enquiry_off.png";
            public static string DashBoardOn = "QE_dashboard_on.png";
            public static string AdminAndSetupOn = "QE_admin_on.png";
            public static string StudentOn = "QE_student_on.png";
            public static string HomeworkOn = "QE_hw_on.png";
            public static string ECAOn = "QE_eca_on.png";
            public static string AcademicResultOn = "QE_result_on.png";
            public static string MeritDemeritOn = "QE_MD_on.png";
            public static string EnquiryOn = "QE_enquiry_on.png";
            public static string SearchIcon = "QE_search.png";
            public static string AddIcon = "QE_add.png";
            public static string DeleteIcon = "QE_delete.png";

            public class LoginImg
            {
                public static string SchoolLogo = "QOS_School_Logo1 (1).png";
                public static string UserName = "QE_homepage_id.png";
                public static string Password = "QE_homepage_pw.png";
                public static string Logout = "QE_topbar_logout.png";
            }

            public class DashBoardImg
            {
                public class On
                {
                    public static string TodayLateAbsent = "QE_db_todaylate_on.png";
                    public static string OutstandingHomeworkSubmission = "QE_db_outstandhwsub_on.png";
                    public static string MeritDemerit = "QE_db_MD_on.png";
                    public static string TopLateHomeworkSubmission = "QE_db_top20_on.png";
                    public static string Tardiness = "QE_db_tardiness_on.png";
                }
                public class Off
                {
                    public static string TodayLateAbsent = "QE_db_todaylate_off.png";
                    public static string OutstandingHomeworkSubmission = "QE_db_outstandhwsub_off.png";
                    public static string MeritDemerit = "QE_db_MD_off.png";
                    public static string TopLateHomeworkSubmission = "QE_db_top20_off.png";
                    public static string Tardiness = "QE_db_tardiness_off.png";
                }


            }

            public class AdminAndSetupImg
            {
                public class On
                {

                    public static string AcademicYear = "QE_adm_acayear_on.png";
                    public static string UserAndTeacher = "QE_adm_user_on.png";
                    public static string FormsClassesAndHeadTeachers = "QE_adm_classteacher_on.png";
                    public static string Subjects = "QE_adm_subject_on.png";
                    public static string FormsAndSubjects = "QE_adm_formsubj_on.png";
                    public static string TeachersAndSubject = "QE_adm_teachsubj_on.png";
                    public static string MeritDemrit = "QE_adm_MD_on.png";
                    public static string Houses = "QE_adm_hse_on.png";
                    public static string UserPermissions = "QE_adm_permission_on.png";
                }

                public class Off
                {
                    public static string AcademicYear = "QE_adm_acayear_off.png";
                    public static string UserAndTeacher = "QE_adm_user_off.png";
                    public static string FormsClassesAndHeadTeachers = "QE_adm_classteacher_off.png";
                    public static string Subjects = "QE_adm_subject_off.png";
                    public static string FormsAndSubjects = "QE_adm_formsubj_off.png";
                    public static string TeachersAndSubject = "QE_adm_teachsubj_off.png";
                    public static string MeritDemrit = "QE_adm_MD_off.png";
                    public static string Houses = "QE_adm_hse_off.png";
                    public static string UserPermissions = "QE_adm_permission_off.png";
                }


            }
            public class StudentImg
            {
                public class On
                {

                    public static string Admission = "QE_student_admission_on.png";
                    public static string StudentsAndSubject = "QE_student_studentsubj_on.png";
                    public static string Promotion = "QE_student_promo_on.png";
                    public static string Graduation = "QE_student_grad_on.png";
                }

                public class Off
                {
                    public static string Admission = "QE_student_admission_off.png";
                    public static string StudentsAndSubject = "QE_student_studentsubj_off.png";
                    public static string Promotion = "QE_student_promo_off.png";
                    public static string Graduation = "QE_student_grad_off.png";
                }


            }

            public class HomeworkImg
            {
                public class On
                {

                    public static string Homework = "QE_hw_hw_on.png";
                    public static string Submission = "QE_hw_sub_on.png";
                    public static string HWQuantityAnalysis = "QE_hw_analysis_on.png";
                }

                public class Off
                {
                    public static string Homework = "QE_hw_hw_off.png";
                    public static string Submission = "QE_hw_sub_off.png";
                    public static string HWQuantityAnalysis = "QE_hw_analysis_off.png";
                }


            }
            public class ECAImg
            {
                public class On
                {
                    public static string ECA = "QE_eca_eca_on.png";
                    public static string Members = "QE_eca_members_on.png";
                    public static string Notices = "QE_eca_notices_on.png";
                    public static string Reply = "QE_eca_reply_on.png";
                    public static string Attendance = "QE_eca_attendance_on.png";
                }

                public class Off
                {
                    public static string ECA = "QE_eca_eca_off.png";
                    public static string Members = "QE_eca_members_off.png";
                    public static string Notices = "QE_eca_notices_off.png";
                    public static string Reply = "QE_eca_reply_off.png";
                    public static string Attendance = "QE_eca_attendance_off.png";
                }


            }

            public class AcademicResultImg
            {
                public class On
                {

                    public static string Result = "QE_AcademicReult_result_on.png";
                }

                public class Off
                {
                    public static string Result = "QE_AcademicReult_result_off.png";
                }


            }

            public class MeritDemeritImg
            {
                public class On
                {
                    public static string Students = "QE_MD_student_on.png";
                    public static string Demerit = "QE_MD_demerit_on.png";
                }

                public class Off
                {
                    public static string Students = "QE_MD_student_off.png";
                    public static string Demerit = "QE_MD_demerit_off.png";
                }


            }
            public class EnquiryImg
            {
                public class On
                {

                    public static string FormsAndStudent = "QE_enquiry_overview_on.png";
                    public static string Student = "QE_enquiry_student_on.png";
                    public static string SubjectResult = "QE_enquiry_result_on.png";
                    public static string SubjectComparison = "QE_enquiry_comparison_on.png";
                }

                public class Off
                {
                    public static string FormsAndStudent = "QE_enquiry_overview_off.png";
                    public static string Student = "QE_enquiry_student_off.png";
                    public static string SubjectResult = "QE_enquiry_result_off.png";
                    public static string SubjectComparison = "QE_enquiry_comparison_off.png";
                }


            }

        }

        public class MenubarLabel
        {
            public static string Dashboard = "Dashboard";
            public static string AdminSetup = "Admin";
            public static string Attendance = "Attendance";
            public static string Student = "Student";
            public static string Homework = "Homework";
            public static string ECA = "ECA";
            public static string AcademicResult = "Academic";
            public static string MeritDemerit = "Merit";
            public static string Enquiry = "Enquiry";
            public static string Account = "Account";
            public static string Logout = "Logout";

        }

        public class StudentLabel
        {
            public static string Class = "Class";
            public static string StudentNumber = "Student number";
            public static string StudentName = "Student name";

        }

        public class TeacherLabel
        {
            public static string ID = "ID";
            public static string UserID = "User ID";
            public static string UserName = "Name (English)";
            public static string UserNameChi = "Name (中文)";
            public static string Position = "Title";
            public static string Email = "Email 1";
            public static string Email2 = "Email 2";
            public static string Phone = "Phone 1";
            public static string Phone2 = "Phone 2";
            public static string PhotoPath = "Photo Path";
            public static string FirstLogin = "First Login";
            public static string LastLogin = "Last Login";
            public static string AddBy = "Add By";
            public static string AddDate = "Add Date";
            public static string ModifyBy = "Modify By";
            public static string ModifyDate = "Modify Date";
            public static string password = "Password";
            public static string Language = "Language";

        }

        public class CommonLabel
        {
            public static string Subject = "Subject";
            public static string Record = "Record";
            public static string Status = "Status";
            public static string Assignment = "Assignment";
            public static string Date = "Date";
            public static string Description = "Description";
            public static string DescriptionEng = "Description (English)";
            public static string DescriptionChi = "Description (Chinese)";
            public static string Name_Eng = "Name (English)";
            public static string Name_Chi = "Name (Chinese)";
            public static string SubPaper = "Sub-Paper";
            public static string Save = "Save";
            public static string Cancel = "Cancel";
            public static string Apply = "Apply";
            public static string Student = "Student";
            public static string Form = "Form";
            public static string Class = "Class";
            public static string Remark = "Remark";
            public static string AcademicYear = "Academic Year";
            public static string Rank = "Rank";
            public static string Merit = "Merit";
            public static string Demerit = "Demerit";
            public static string Teacher = "Teacher";
            public static string Code = "Code";
            public static string StudentID = "Student ID";
            public static string StudentName = "Student Name";
            public static string StudentNameChi = "Student Name (Chinese)";
            public static string StudentNameEng = "Student Name (English)";
            public static string DateOfBirth = "Date Of Birth";
            public static string AddDate = "Admission Date";
            public static string Gender = "Gender";
            public static string House = "House";
            public static string ClassNo = "Class No";
            public static string Exemption = "Exemption";
            public static string Name = "Name";
            public static string ContactNo = "Contact Number";
            public static string Address = "Address";
            public static string Email = "Email";
            public static string Addby = "Add by";
            public static string Edit = "Edit";
            public static string Delete = "Delete";
            public static string Import = "Import";
            public static string Preview = "Preview";
            public static string TargetDate = "Target Date";
            public static string DueDate = "Due Date";
            public static string Add = "Add";
            public static string Export = "Export";
            public static string Print = "Print";
            public static string Mark = "Mark";
            public static string Show = "Show";
            public static string From = "From";
            public static string To = "To";
            public static string Type = "Type";
            public static string Grade = "Grade";
            public static string Exit = "Exit";
            public static string Body = "Body";
            public static string Venue = "Venue";
            public static string Load = "Load";
            public static string Search = "Search";
            public static string Chi = "Chi";
            public static string Eng = "Eng";
            public static string ServiceNA = "This service is not available at the moment.";
        }

        public class Msg
        {
            public class Login
            {
                public static string InvalidLogin = "Incorrect ID / Password! Beware of capital letters.";
            }
            public class AdminSetup
            {
                public class Success
                {
                    public static string AddSubject = "Add subject successfully.";
                    public static string DeleteSubject = "Delete subject successfully.";
                    public static string EditSubject = "Edit subject successfully.";
                }

                public class Error
                {
                    public static string AddSubject = "Failed to add subject.";
                    public static string DeleteSubject = "Failed to delete subject.";
                    public static string EditSubject = "Failed to edit subject.";

                }
            }
        }

        public class DashboardViewLabel
        {
            public class IconTitle
            {
                public static string TodayLateAbsent = "Today Late & Absent";
                public static string OutstandingHomeworkSubmission = "Outstanding Homework Submission";
                public static string MeritDemerit = "Merit & Demerit";
                public static string TopLateHomeworkSubmission = "Top 20 Late Home Submission Student";
                public static string Tardiness = "Tardiness";
            }

            public class TodayLateAbsent
            {
                public static string Title = "TODAY LATE & ABSENTS";
                public static string Class = StudentLabel.Class;
                public static string StudentNumber = StudentLabel.StudentNumber;
                public static string StudentName = StudentLabel.StudentName;
                public static string Record = CommonLabel.Record;
                public static string Status = CommonLabel.Status;
                public static string TimeOfArrival = "Time of arrival";
            }

            public class OutstandingHomeworkSubmission
            {
                public static string Title = "OUTSTANDING HOMEWORK SUBMISSION";
                public static string Class = StudentLabel.Class;
                public static string Subject = CommonLabel.Subject;
                public static string Assignment = CommonLabel.Assignment;
                public static string TargetDate = CommonLabel.TargetDate;
                public static string Overdue = "Overdue";
                public static string StudentNumber = StudentLabel.StudentNumber;
                public static string StudentName = StudentLabel.StudentName;
                public static string Record = CommonLabel.Record;
            }

            public class MeritAndDemerit
            {
                public static string Title = "MERIT & DEMERIT RECORD IN LAST 30 DAYS";
                public static string Merit = CommonLabel.Merit;
                public static string Date = CommonLabel.Date;
                public static string Class = StudentLabel.Class;
                public static string StudentNumber = StudentLabel.StudentNumber;
                public static string StudentName = StudentLabel.StudentName;
                public static string Description = CommonLabel.Description;
                public static string GivenBy = "Given By";
            }

            public class LateHomeworkSubmission
            {
                public static string Title = "TOP 20 HOMEWORK LATE SUBMISSION STUDENT";
                public static string Rank = CommonLabel.Rank;
                public static string Class = StudentLabel.Class;
                public static string StudentNumber = StudentLabel.StudentNumber;
                public static string studentName = StudentLabel.StudentName;
                public static string Record = CommonLabel.Record;
            }

            public class Tardiness
            {
                public static string Title = "Tardiness: Arrive late to school over three times";
                public static string Class = StudentLabel.Class;
                public static string StudentNumber = StudentLabel.StudentNumber;
                public static string StudentName = StudentLabel.StudentName;
                public static string LastDayOfTardy = "Last day of tardy";
                public static string Record = CommonLabel.Record;
            }

        }


        public class AdminAndSetupViewLabel
        {
            public class Title
            {
                public static string Term = "Term";
                public static string Users = "Users";
                public static string Classes = "Classes";
                public static string Subjects = "Subjects";
                public static string Merit = "Merit";
                public static string Houses = "Houses";
                public static string Accesses = "Accesses";
            }

            public class AcademicYear
            {
                public static string NewAcademicYear = "New Academic Year";
                public static string Start = "Start";
                public static string New = "New";
                public static string CopyToNew = "Copy To New Year";
            }

            public class UsersAndTeachers
            {
                public static string Title = "Users & Teachers";
                public static string ID = TeacherLabel.ID;
                public static string NameEng = TeacherLabel.UserName;
                public static string NameChi = TeacherLabel.UserNameChi;
                public static string Phone = TeacherLabel.Phone;
                public static string Phone2 = TeacherLabel.Phone2;
                public static string Email = TeacherLabel.Email;
                public static string Email2 = TeacherLabel.Email2;
                public static string DefaultLanguage = TeacherLabel.Language;
                public static string AddNewUserTeacher = "+ Add New User/Teacher";
                public static string UpdateFail = "Failed! User profile is not updated.";
                public static string UpdateSuccess = "Success! User profile updated.";
                public static string AddFail = "Failed! User profile is not added.";
                public static string DeleteFail = "Failed! User profile is not deleted.";
                public static string AddSuccess = "Success! User profile add.";
                public static string DeleteSuccess = "Success! User profile is deleted.";
                public static string EmailMsg = "(Initial password will be sent to this email.)";


            }

            public class FormsClassesAndHeadTeachers
            {
                public static string Title = "Forms, Classes & Head Teachers";
                public static string AcademicYear = CommonLabel.AcademicYear;
                public static string Form = CommonLabel.Form;
                public static string Classes = "Classes";
                public static string ClassTeacher1 = "Class Teacher 1";
                public static string ClassTeacher2 = "Class Teacher 2";
                public static string Remark = CommonLabel.Remark;
                public static string Save = CommonLabel.Save;
                public static string SaveSuccess = "Success! Class teachers setting saved.";
                public static string SaveFail = "Failed! Class teachers setting save failed.";
            }

            public class Subject
            {
                public static string Title = "Subject";
                public static string Save = CommonLabel.Save;
                public static string AcademicYear = CommonLabel.AcademicYear;
                public static string EDB_code = "EDB Code";
                public static string Code = CommonLabel.Code;
                public static string Name_Eng = CommonLabel.Name_Eng;
                public static string Name_Chi = CommonLabel.Name_Chi;
                public static string SubPaper = CommonLabel.SubPaper;
                public static string FullMark = "Full Marks";
            }

            public class FormsAndSubjects
            {
                public static string Title = "Forms & Subjects";
                public static string AcademicYear = CommonLabel.AcademicYear;
                public static string Subject = CommonLabel.Subject;
                public static string Save = CommonLabel.Save;
                public static string Code = CommonLabel.Code;
                public static string SaveSuccess = "Success! Form and Subject setting saved.";
                public static string SaveFail = "Failed! Form and Subject setting save failed.";
            }

            public class TeachersAndSubjects
            {
                public static string Title = "Teachers & Subjects";
                public static string AcademicYear = CommonLabel.AcademicYear;
                public static string Teacher = CommonLabel.Teacher;
                public static string Save = CommonLabel.Save;
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string SaveSuccess = "Success! Teacher and Subject setting saved.";
                public static string SaveFail = "Failed! Teacher and Subject setting save failed.";
                public static string EmptyTeacher = "You haven't select a teacher for this setting";
            }

            public class Merit
            {
                public static string Name = "Name";
                public static string Name_chi = "Name Chi";
                public static string Points = "Points";
                public static string Demerit = "Merit/Demerit";
            }

            public class Houses
            {
                public static string Title = "Houses";
                public static string Save = CommonLabel.Save;
                public static string Code = CommonLabel.Code;
                public static string DescriptionEng = CommonLabel.DescriptionEng;
                public static string DescriptionChi = CommonLabel.DescriptionChi;

            }

            public class Accesses
            {
                public static string UserGroup = "User Group";
                public static string GroupRights = "Group Rights";
                public static string GroupUsers = "Group Users";
                
            }
        }

        public class StudentViewLabel
        {
            public class Title
            {
                public static string Student = "Students";
                public static string StudentGroup = "Student Group";
                public static string StudentGroupAndSubjects = "Student Groups & Subjects";
                public static string StudentGroupAndStudents = "Student Groups & Students";
                public static string Subjects = "Subjects";
                public static string Classes = "Classes";
            }

            public class Admission
            {
                public static string StudentID = CommonLabel.StudentID;
                public static string StudentName = CommonLabel.StudentName;
                public static string StudentNameChi = "Chinese";
                public static string StudentNameEng = CommonLabel.StudentNameEng;
                public static string DateOfBirth = CommonLabel.DateOfBirth;
                public static string AddDate = CommonLabel.AddDate;
                public static string Gender = CommonLabel.Gender;
                public static string House = CommonLabel.House;
                public static string HKID = "HKID";
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string ClassNo = CommonLabel.ClassNo;
                public static string Exemption = CommonLabel.Exemption;
                public static string ErgentContact = "Emergency Contact";
                public static string Name = CommonLabel.Name;
                public static string Contact = CommonLabel.ContactNo;
                public static string Address = CommonLabel.Address;
                public static string Email = CommonLabel.Email;
                public static string Relationship = "Relationship";
                public static string RefStudent = "Ref.Student";
                public static string Date = CommonLabel.Date;
                public static string Remark = CommonLabel.Remark;
                public static string Addby = CommonLabel.Addby;
                public static string Edit = CommonLabel.Edit;
                public static string Delete = CommonLabel.Delete;
                public static string AddNewStudent = "+ Add New Student";
                public static string ArriveLatetoSchool = "Arrive late to school";
                public static string From = CommonLabel.From;
                public static string To = CommonLabel.To;
                public static string LateHomeSubmission = "Late Homework Submission";
                public static string EmergencyContacts = "Emergency Contacts";
                public static string Guardian = "Guardian";
                public static string Guardian_phone = "Guardian's Contact";
                public static string Special_arg = "Special Arrangements";
                public static string CARING_ARG = "Special Caring";
                public static string ATTENDANCE_ARG = "Attendence";
                public static string HOMEWORK_ARG = "Homework";
                public static string create_succeed = "Student record created";
                public static string create_failed = "Student record is not created";
                public static string update_succeed = "Student record updated";
                public static string update_failed = "Student record is not updated";
                public static string delete_succeed = "Student record is deleted";
                public static string delete_failed = "Student record is not deleted";
            }

            public class StudentGroup
            {
                public static string NAME = "Group Name";
                public static string TEACHER = "Teacher 1";
                public static string TEACHER_2 = "Teacher 2";
                public static string TEACHER_3 = "Teacher 3";
                public static string TEACHER_4 = "Teacher 4";
                public static string LANGUAGE = "Language";
            }
                
            

            public class StudentAndSubject
            {

                public static string Bio = "Biology";
                public static string Chem = "Chemistry";
                public static string HistoryCulture = "History and Culture";
                public static string CHistory = "Chinese History";
                public static string Chinese = "Chinese Language";
                public static string ChineseLit = "Chinese Literature";
                public static string Computer = "Computer Literacy";
                public static string LS = "Liberal Studies/ Independent Living";
                public static string Acct = "BAFS (Accounting)";
                public static string Desing = "Design And Technology";
                public static string Econ = "Economics";
                public static string BusinssMgt = "BAFS (Business Management)";
                public static string English = "English Language";
                public static string French = "French";
                public static string Geo = "Geography";
                public static string Japanese = "Japanese";
                public static string Science = "Combined Science";
                public static string MusicDSE = "Music (HKDSE)";
                public static string MathCore = "Mathematics (Compulsory Part)";
                public static string History = "History";
                public static string HomeEcon = "Home Economics";
                public static string MathExtend = "Mathematics (Extended Part – Module 2)";
                public static string Humanities = "Integrated Humanities";
                public static string JuniorScience = "Science (Secondary 1-3)";
                public static string IntegratedStudy = "Integrated Studies";
                public static string Liberal = "Liberal Studies";
                public static string Math = "Mathematics";
                public static string Music = "Music";
                public static string PE = "Physical Education Lessons";
                public static string Phy = "Physics";
                public static string Putonghua = "Putonghua";
                public static string PEDSE = "Physical Education (HKDSE)";
                public static string VA = "Visual Arts";
                public static string THS = "Tourism and Hospitality Studies";
                public static string Commu = "Information & Communication Technology";
                public static string VADSE = "Visual Arts (HKDSE)";
                public static string BasicStudy = "Integrated Basic Studies";
                public static string PSE = "Personal & Social Education";
                public static string EngishAcademic = "English for Academic Purpose";
                public static string IntegratedScience = "Integrated Science";
                public static string ReadingCulture = "Reading & Culture";
                public static string ReadingSelfStudy = "Reading & Self Study";
                public static string ReadingScience = "Reading & Science";
                public static string EBT = "Economics, Business & Tourism";
                public static string Conduct = "Conduct";
            }


            public class Promotion
            {
                public static string Student = CommonLabel.Student;
                public static string Decision = "Promote/Repeat/Dropout";
                public static string NewFrom = "New Form";
                public static string Class = CommonLabel.Class;
                public static string Remark = CommonLabel.Remark;

            }

            public class Graduation
            {
                public static string Student = CommonLabel.Student;
                public static string Decision = "Graduation/Repeat/Dropout";
                public static string NewFrom = "New Form";
                public static string Class = CommonLabel.Class;
                public static string Remark = CommonLabel.Remark;

            }
        }

        public class HomeworkViewLabel
        {
            public class Title
            {
                public static string Homework = "Homework";
                public static string Submission = "Submission";
                public static string HomeworkType = "Homework Type";
                public static string HWAnalysis = "Analysis";
            }

            public class Homework
            {
                public static string Title = "Homework";
                public static string Save = CommonLabel.Save;
                public static string Subject = CommonLabel.Subject;
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string DateAssign = "Date Assign";
                public static string Assignment = CommonLabel.Assignment;
                public static string TargetDate = CommonLabel.TargetDate;
                public static string DueDate = "DueDate";
                public static string Submission = "Submission";
                public static string OnTime = "On Time";
                public static string Late = "Late";
                public static string Outstanding = "Outstanding";
                public static string Closed = "Closed";
            }

            public class Submission
            {
                public static string Title = "Homework Submission";
                public static string Subject = CommonLabel.Subject;
                public static string Save = CommonLabel.Save;
                public static string Import = "Import";
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string Student = CommonLabel.Student;
                public static string Submitted = "Submitted";
                public static string DateSubmit = "Date Submit";
                public static string Reson = "Reson";
            }

            public class Type
            {
                public static string Name = "Name";
                public static string FullMark = "Full Mark";
                public static string Duration = "Due";
                public static string SubRatio = "Submission";
                public static string LateRatio = "Late";
                public static string DateAssign = "Assignation";
                public static string GroupSelect = "Assignee";
                public static string TypeSelect = "Type";
            }

            public class Analysis
            {

            }
        }

        public class ECAViewLabel
        {
            public class Title
            {
                public static string Activities = "Activities";
                public static string Members = "Members";
                public static string Notices = "Notices";
                public static string Reply = "Reply";
                public static string Attendance = "Attendance";
            }

            public class ECA
            {
                public static string Title = "Extra-Curricular Activities";
                public static string Type = CommonLabel.Type;
                public static string DescriptionEng = CommonLabel.DescriptionEng;
                public static string DescriptionChi = CommonLabel.DescriptionChi;
                public static string DutyCount = "Duty/Count";
                public static string TeacherInCharge = "Teacher in Charge";
                public static string SupportTeacher = "Support Teacher";
                public static string Remark = CommonLabel.Remark;
                public static string Save = CommonLabel.Save;
                public static string Exit = CommonLabel.Exit;
            }

            public class Members
            {
                public static string Title = "Extra-Curricular Activities Members";
                public static string Type = CommonLabel.Type;
                public static string Body = CommonLabel.Body;
                public static string Load = CommonLabel.Load;
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string Grade = CommonLabel.Grade;
                public static string StudentNameEng = CommonLabel.StudentNameEng;
                public static string StudentNameChi = CommonLabel.StudentNameChi;
                public static string Save = CommonLabel.Save;
                public static string Exit = CommonLabel.Exit;
            }

            public class Notices
            {
                public static string Title = "Extra-Curricular Activities Notices";
                public static string ActivityID = "Activity ID";
                public static string Type = CommonLabel.Type;

                public static string Body = CommonLabel.Body;
                public static string Search = "Search";
                public static string TitleLabelEng = "Title (Eng)";
                public static string TitleLabelChi = "Title (Chi)";
                public static string Date = CommonLabel.Date;
                public static string Venue = CommonLabel.Venue;
                public static string Quota = "Quota";
                public static string Fee = "Fee";
                public static string Requirement = "Requirement";
                public static string DressCode = "Dress Code";
                public static string Ref = "Ref #";
                public static string Message = "Message";

                public static string Remark = CommonLabel.Remark;
                public static string Footer = "Footer";
                public static string PrintNotice = "Print Notice";
                public static string New = "New";
                public static string Save = CommonLabel.Save;
                public static string Exit = CommonLabel.Exit;

                public static string ActivityDetails = "Activity details";
                public static string From = "From";
                public static string To = "To";
                public static string AssembleTime = "Assemble Time";
                public static string Place = "Place";
                public static string DismissTime = "Dismiss Time";
                public static string Attendance = "Attendance";
                public static string Members = "Members";
                public static string Grade = CommonLabel.Grade;
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string StudentNameEng = CommonLabel.StudentNameEng;
                public static string StudentNamechi = CommonLabel.StudentNameChi;
            }

            public class Reply
            {
                public static string Title = "Extra-Curricular Activities Reply";
                public static string Type = CommonLabel.Type;
                public static string Body = CommonLabel.Body;
                public static string DescriptionEng = CommonLabel.DescriptionEng;
                public static string DescriptionChi = CommonLabel.DescriptionChi;
                public static string Date = CommonLabel.Date;
                public static string Venue = CommonLabel.Venue;
                public static string TimeFrom = "Time (From)";
                public static string TimeTo = "Time (To)";
                public static string Load = CommonLabel.Load;
                public static string Save = CommonLabel.Save;
                public static string Exit = CommonLabel.Exit;
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string StudentNameEng = CommonLabel.StudentNameEng;
                public static string StudentNameChi = CommonLabel.StudentNameChi;
                public static string Yes = "Yes";
                public static string No = "No";

            }

            public class Attendance
            {
                public static string Title = "Extra-Curricular Activities Reply";
                public static string Type = CommonLabel.Type;
                public static string Body = CommonLabel.Body;
                public static string DescriptionEng = CommonLabel.DescriptionEng;
                public static string DescriptionChi = CommonLabel.DescriptionChi;
                public static string Date = CommonLabel.Date;
                public static string Venue = CommonLabel.Venue;
                public static string TimeFrom = "Time (From)";
                public static string TimeTo = "Time (To)";
                public static string Load = CommonLabel.Load;
                public static string Save = CommonLabel.Save;
                public static string Exit = CommonLabel.Exit;
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string StudentNameEng = CommonLabel.StudentNameEng;
                public static string StudentNameChi = CommonLabel.StudentNameChi;
                public static string AbsentType = "Absent type";
                public static string Remark = "Remark";
            }
        }

        public class AcademicResultViewLabel
        {
            public class Title
            {
                public static string AcademicResult = "Academic Result";
                public static string Analysis = "Analysis";
            }

            public class AcademicResult
            {
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string StudentID = CommonLabel.StudentID;
                public static string Code = "Code";
                public static string DescriptionEng = "Description (English)";
                public static string DescriptionChi = "Description (Chinese)";
                public static string FullM = "Full Mark";
                public static string Text = "Text";
                public static string Exam = "Exam";
                public static string Add = CommonLabel.Add;
            }
        }


        public class MeritDemeritViewLabel
        {
            public class Title
            {
                public static string Merit = "Merit";
                public static string Analysis = "Analysis";
            }

            public class Student
            {
                public static string Title = "Merit & Dermerit";
                public static string Save = CommonLabel.Save;
                public static string StudentLabel = CommonLabel.Student;
                public static string Date = CommonLabel.Date;
                public static string Code = CommonLabel.Code;
                public static string Description = CommonLabel.Description;
                public static string GivenBy = "Given by";
            }
        }


        public class EnquiryViewLabel
        {
            public class IconTitle
            {
                public static string Enquiry = "Enquiry";
                public static string FormsAndStudent = "Forms & Student Overview";
                public static string Student = "Student";
                public static string SubjectResult = "Subject Result";
                public static string SubjectComparison = "Subject Comparison";
            }

            public class FormStudentOverview
            {
                public static string TeachersSubjects = "Teachers & Subjects";
                public static string AcademicYear = CommonLabel.AcademicYear;
                public static string Form = CommonLabel.Form;
                public static string Export = CommonLabel.Export;
            }

            public class Student
            {
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string student = CommonLabel.Student;
                public static string StudentID = CommonLabel.StudentID;
                public static string Code = "Code";
                public static string DescriptionEng = "Description (English)";
                public static string DescriptionChi = "Description (Chinese)";
                public static string FullM = "Full Mark";
                public static string Text = "Text";
                public static string Exam = "Exam";
                public static string Export = CommonLabel.Export;
                public static string Print = CommonLabel.Print;
                public static string StudentName = CommonLabel.StudentName;
                public static string StudentNameChi = CommonLabel.StudentNameChi;
                public static string StudentNameEng = CommonLabel.StudentNameEng;
                public static string DateOfBirth = CommonLabel.DateOfBirth;
                public static string AddDate = CommonLabel.AddDate;
                public static string Gender = CommonLabel.Gender;
                public static string House = CommonLabel.House;
                public static string ClassNo = CommonLabel.ClassNo;
                public static string HomeworkStatus = MenubarLabel.Homework;
                public static string MEritDemerit = MenubarLabel.MeritDemerit;
                public static string AcademicResult = MenubarLabel.AcademicResult;
                public static string ECA = MenubarLabel.ECA;
            }

            public class SubjectResult
            {
                public static string Form = CommonLabel.Form;
                public static string Class = CommonLabel.Class;
                public static string StudentID = CommonLabel.StudentID;
                public static string FullM = "Full Mark";
                public static string Text = "Text";
                public static string Exam = "Exam";
                public static string Export = CommonLabel.Export;
                public static string Print = CommonLabel.Print;
                public static string StudentName = CommonLabel.StudentName;
                public static string House = CommonLabel.House;
                public static string ClassNo = CommonLabel.ClassNo;
                public static string Ayear = CommonLabel.AcademicYear;
                public static string Teacher = CommonLabel.Teacher;
                public static string Subject = CommonLabel.Subject;
                public static string Result = "Subject Result";
            }

            public class SubjectComparison
            {
                public static string Form = CommonLabel.Form;
                public static string To = CommonLabel.To;
                public static string Class = CommonLabel.Class;
                public static string StudentID = CommonLabel.StudentID;
                public static string FullM = "Full Mark";
                public static string Text = "Text";
                public static string Exam = "Exam";
                public static string Export = CommonLabel.Export;
                public static string Show = CommonLabel.Show;
                public static string Print = CommonLabel.Print;
                public static string Student = CommonLabel.Student;
                public static string Ayear = CommonLabel.AcademicYear;
                public static string Teacher = CommonLabel.Teacher;
                public static string Subject = CommonLabel.Subject;
                public static string Rank = CommonLabel.Rank;
                public static string Mark = CommonLabel.Mark;
                public static string Result = "Subject Result";
                public static string SubjectCompare = "Subject Comparison";
            }
        }


        public class DBtable
        {
            public class SysParameter
            {
                public static string ParamCode = "ACAMDYR";
            }
        }
    }

}