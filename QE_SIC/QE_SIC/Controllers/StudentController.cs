using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QE.Models;
using QE.Services;
using QE.Models.ViewModels;

namespace QE.Controllers
{
    public class StudentController : Controller
    {
        private static IDBservice DBS = new DBservice();
        private static GeneralinitializationService GS = new GeneralinitializationService();
        private static GeneralinitializationService.COMMONPARM CP = new GeneralinitializationService.COMMONPARM();

        private ActionResult checkLogin()
        {
            if (!ClientSessionService.IsLogined)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return null;
            }
        }

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Student(int? term_id)
        {
            checkLogin();
            StudentViewModel ViewModel = new StudentViewModel();
            ViewModel.TermList = CP.termSelectList;
            ViewModel.ClassList = DBS.findActiveCurrentRecords<QE_CLASS>();
            ViewModel.SelectedTerm = CP.currentTerm.ID;

            if (term_id != null)
            {
                ViewModel.ClassList = DBS.findActiveRecordsByTerm<QE_CLASS>((int)term_id);
                ViewModel.SelectedTerm = (int)term_id;
            }

            return View(ViewModel);
        }

        // GET: Student
        [HttpGet]
        [AllowAnonymous]
        public ActionResult _Student(int? Class_id, int? student_id, int? term_id)
        {
            checkLogin();
            _StudentViewModel SVM = new _StudentViewModel();
            
            //get class num
            List<CLASS_STUDENT_LIST> ClassStudentMap = new List<CLASS_STUDENT_LIST>();
            if (Class_id > 0 && term_id > 0)
            {
                Dictionary<string, string> DS = new Dictionary<string, string>()
                {
                    {"QE_CLASS", Class_id.ToString()},
                    {"ACTIVE", "1" }
                };
                
                ClassStudentMap = DBS.Query<CLASS_STUDENT_LIST>(DS);
            }
            SVM.ClassStudentMap = ClassStudentMap;

            //get student list
            SVM.studentList = Class_id != null && term_id != null? StudentService.GetActiveStudentsByClass((int)Class_id) : new List<STUDENT>();

            //get student chosen
            SVM.selectedStudent = student_id != null && student_id > 0 && SVM.studentList.Exists(s => s.ID == (int)student_id) ? SVM.studentList.Where(s=> s.ID == (int)student_id).First() : new STUDENT();

            try { SVM.selectedStudentNum = ClassStudentMap.Where(c => c.STUDENT == (int)student_id).FirstOrDefault().CLASS_NUM; } catch { }
            SVM.selectedClass = (int)Class_id;
            SVM.selectedTerm = (int)term_id;

            return View("_Student", SVM);
        }

        // POST: Student
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult _Student(_StudentViewModel SVM)
        {
            checkLogin();

            // fields checking
            if (!ModelState.IsValid)
                return View("_Student", SVM);

            //edit or create
            bool result = false;
            if (SVM.selectedStudent.ID > 0)
            {//ediit
                SVM.selectedStudent.MODIFY_BY = CP.userID;
                SVM.selectedStudent.MODIFY_DATE = DateTime.Now;
                result = DBS.updateRecord(SVM.selectedStudent);
            }
            else
            {//create
                SVM.selectedStudent.ACTIVE = true;
                SVM.selectedStudent.ADD_BY = CP.userID;
                SVM.selectedStudent.ADD_DATE = DateTime.Now;
                SVM.selectedStudent.ID = DBS.addRecordReturnID(SVM.selectedStudent);

                CLASS_STUDENT_LIST newMap = new CLASS_STUDENT_LIST(SVM.selectedClass, SVM.selectedStudent.ID, SVM.selectedStudentNum, CP.userID, DateTime.Now, 0, Constant.DEF_DATETIME, true);
                int newMap_id = DBS.addRecordReturnID(newMap); //add new map linking up student and class

                if (SVM.selectedStudent.ID > 0 && newMap_id > 0)
                    result = true;
            }
            
            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return _Student(SVM.selectedClass, SVM.selectedStudent.ID, SVM.selectedTerm);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult InactiveStudent(int? Class_id, int? student_id, int? term_id)
        {
            checkLogin();

            bool result = (Class_id > 0 && student_id > 0 && term_id > 0) ? DBS.InactiveRecord("STUDENT", (int)student_id, CP.userID) : false;

            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return _Student((int)Class_id, 0, (int)term_id);
        }

        // GET: Subject
        [HttpGet]
        [AllowAnonymous]
        public ActionResult StudentGroup(int? term_id) // term id
        {
            checkLogin();

            StudentGroupViewModel SGVM = new StudentGroupViewModel();
            SGVM.TermList = CP.termSelectList;
            SGVM.UserList = CP.userSelectList;
            SGVM.SelectedTerm = CP.currentTerm.ID;
            SGVM.groupList = DBS.findActiveCurrentRecords<STUDENT_GROUP>();
            Session["currentStudentGroups"] = SGVM.groupList;
        SGVM.AllUserList = CP.AllUser;

            if (term_id != null)
            {
                SGVM.groupList = DBS.findActiveRecordsBySingleParm<STUDENT_GROUP>("TERM", (int)term_id);
                SGVM.SelectedTerm = (int)term_id;
            }

            ModelState.Clear();
            return View(SGVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult StudentGroup(StudentGroupViewModel SGVM)
        {
            checkLogin();
            
            bool result = true;

            foreach (var group in SGVM.InactiveGroupsList) //remove groups wanted to remove
            {
                if (result && group > 0)
                {
                    result = false;
                    result = DBS.InactiveRecord("STUDENT_GROUP", group, CP.userID);
                }
            }

            if (result)
            {
                foreach (var group in SGVM.groupList) //add new groups
                {
                    if (result)
                    {
                        if (group.ID < 1)
                        {
                            result = false;
                            group.ADD_BY = CP.userID;
                            group.ADD_DATE = DateTime.Now;
                            group.ACTIVE = true;
                            result = DBS.addRecord(group);
                        }
                        else
                        {
                            result = false;
                            group.MODIFY_BY = CP.userID;
                            group.MODIFY_DATE = DateTime.Now;
                            result = DBS.updateRecord(group);
                        }
                    }
                }
            }

            ModelState.Clear();
            if (result) {
                TempData[Constant.msg_success] = Constant.ChangeSucceed;
                if(SGVM.SelectedTerm == CP.currentTerm.ID) {
                    Session["currentStudentGroups"] = DBS.findActiveCurrentRecords<STUDENT_GROUP>(); //reset current group
                }
            } else {
                TempData[Constant.msg_error] = Constant.ChangeFailed;
            }

            return StudentGroup(SGVM.SelectedTerm);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InsertGroup(StudentGroupViewModel SGVM, int term_id)
        {
            checkLogin();

            SGVM.NewGroup.TERM = term_id;
            SGVM.NewGroup.ID = GS.getNewID();

            foreach (var key in ModelState.Keys)
            {
                if (!key.Contains("NewGroup"))
                    ModelState[key].Errors.Clear(); // only need to check new subject fields
            }

            if (!ModelState.IsValid)
                return View("StudentGroup", SGVM); // redirect to form with data
            
            SGVM.groupList.Add(SGVM.NewGroup);
            SGVM.NewGroup = new STUDENT_GROUP(); //gen new group
            SGVM.TermList = CP.termSelectList;
            SGVM.UserList = CP.userSelectList;
            SGVM.AllUserList = CP.AllUser;

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("StudentGroup", SGVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveGroup(StudentGroupViewModel SGVM, int? group_id)
        {
            checkLogin();

            SGVM.groupList.Remove(SGVM.groupList.Where(g => g.ID == group_id).First());

            if(group_id>0)
                SGVM.InactiveGroupsList.Add((int)group_id);

            SGVM.TermList = CP.termSelectList;
            SGVM.UserList = CP.userSelectList;
            SGVM.AllUserList = CP.AllUser;

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("StudentGroup", SGVM);
        }


        // GET: StudentGroupAndSubjects
        [HttpGet]
        [AllowAnonymous]
        public ActionResult StudentGroupAndSubjects(int? term_id, int? group_id)
        {
            checkLogin();


            GroupSubjectViewModel GSVM = new GroupSubjectViewModel();
            GSVM.TermList = CP.termSelectList;
            GSVM.Teachers = CP.AllUser;

            TERM displayingTerm = term_id != null ? CP.termlist[(int)term_id] : CP.currentTerm;
            if (displayingTerm != CP.currentTerm)
            {
                GSVM.Groups = DBS.findActiveRecordsBySingleParm<STUDENT_GROUP>("TERM", displayingTerm.ID);
                GSVM.SelectedTerm = displayingTerm.ID;
            }
            else
            {
                GSVM.Groups = Session["currentStudentGroups"] != null? (List<STUDENT_GROUP>)Session["currentStudentGroups"]: DBS.findActiveCurrentRecords<STUDENT_GROUP>();
                GSVM.SelectedTerm = CP.currentTerm.ID;
            }

            List<GroupAndSubject> GnSs = new List<GroupAndSubject>(); //subject with bool list
            List<SUBJECT> subject = DBS.findActiveRecordsBySingleParm<SUBJECT>("TERM", displayingTerm.ID);// get subject per term
            List<STUDENT_GROUP_SUBJECT_LIST> Map = group_id != null && group_id > 0? DBS.findActiveRecordsBySingleParm<STUDENT_GROUP_SUBJECT_LIST>("STUDENT_GROUP", (int)group_id) : new List<STUDENT_GROUP_SUBJECT_LIST>(); //get group subject map
            
            if (subject != null && subject.Any())
            {
                for (int s = 0; s < subject.Count; s++) //check each subject if they mapped to chosen group
                {
                    GroupAndSubject GS = new GroupAndSubject();
                    GS.SubjectID = subject[s].ID;
                    GS.EBD_CODE = subject[s].EDB_CODE;
                    GS.Subject_Name = subject[s].NAME;
                    GS.GroupID = group_id != null? (int)group_id: 0;
                    if (Map != null && Map.Any() && Map.Exists(m => (m.SUBJECT == subject[s].ID) && (m.STUDENT_GROUP == (int)group_id))) // check if any map record matches chosen group
                    {
                        GS.Contain = true;
                        GS.MapID = Map.Where(m => (m.SUBJECT == subject[s].ID) && (m.STUDENT_GROUP == (int)group_id)).FirstOrDefault().ID;
                    }
                    GnSs.Add(GS);
                }
            }
            GSVM.GSs = GnSs;

            ModelState.Clear();
            ViewData["SelectedTerm"] = GSVM.SelectedTerm;
            return View(GSVM);
        }




        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult _StudentGroupAndSubjects(GroupSubjectViewModel GSVM)
        {
            checkLogin();

            bool result = true;

            for (int i = 0; i < GSVM.GSs.Count; i++)
            {
                if (result)
                {
                    if (GSVM.GSs[i].Contain && GSVM.GSs[i].MapID < 1) // constains a map ID means its an existing record
                    {
                        STUDENT_GROUP_SUBJECT_LIST newMap = new STUDENT_GROUP_SUBJECT_LIST(0, GSVM.GSs[i].GroupID, GSVM.GSs[i].SubjectID,CP.userID, DateTime.Now, 0, Constant.DEF_DATETIME, true);
                        result = false;
                        result = DBS.addRecord(newMap);
                    }
                    else if (!GSVM.GSs[i].Contain && GSVM.GSs[i].MapID > 0) //not contain but have existing id means need to remove
                    {
                        result = false;
                        result = DBS.InactiveRecord("STUDENT_GROUP_SUBJECT_LIST", GSVM.GSs[i].MapID, CP.userID);
                    }
                }
            }

            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return RedirectToAction("StudentGroupAndSubjects","Student",new { term_id = GSVM.SelectedTerm, group_id = GSVM.GSs.FirstOrDefault().GroupID });
        }

        // GET: StudentGroupAndSubjects
        [HttpGet]
        [AllowAnonymous]
        public ActionResult StudentGroupAndStudents(int? term_id)
        {
            checkLogin();
            
            GroupStudentViewModel GSVM = new GroupStudentViewModel();
            GSVM.TermList = CP.termSelectList;
            GSVM.Map = new List<STUDENT_GROUP_STUDENT_LIST>(); //gen empty map list
            GSVM.Students = new List<STUDENT>(); //gen empty student list
            GSVM.Student = new List<GroupAndStudent>(); //gen empty student with bool list

            GSVM.Teachers = CP.AllUser;
            GSVM.SelectedTerm = term_id != null ? (int)term_id: CP.currentTerm.ID;
            GSVM.Groups = GSVM.SelectedTerm != CP.currentTerm.ID ? DBS.findActiveRecordsBySingleParm<STUDENT_GROUP>("TERM", GSVM.SelectedTerm) : DBS.findActiveCurrentRecords<STUDENT_GROUP>(); ;
            Session["currentStudentGroups"] = GSVM.Groups;
            ModelState.Clear();
            return View(GSVM);
        }



        // GET: StudentGroupAndSubjects
        [HttpGet]
        [AllowAnonymous]
        public ActionResult _StudentGroupAndStudents(int term_id, int group_id, int? class_id)
        {
            checkLogin();

            GroupStudentViewModel GSVM = new GroupStudentViewModel();
            GSVM.TermList = CP.termSelectList;
            GSVM.SelectedTerm = term_id;
            
            GSVM.Groups = term_id == CP.currentTerm.ID? DBS.findActiveCurrentRecords<STUDENT_GROUP>() : DBS.findActiveRecordsBySingleParm<STUDENT_GROUP>("TERM", term_id);
            GSVM.Classes = term_id == CP.currentTerm.ID ? DBS.findActiveCurrentRecords<QE_CLASS>() : DBS.findActiveRecordsBySingleParm<QE_CLASS>("TERM", term_id);
            GSVM.Teachers = CP.AllUser;
            GSVM.SelectedGroup = group_id;
            GSVM.SelectedClass = class_id != null ? (int)class_id : 0;
            
                                //--------------------//
                                //   gen bool list    // 
            //------------------//--------------------//-------------------------------------------------------------
            
            // get student per class
            List<STUDENT> students = GSVM.Students = class_id != null && (int)class_id > 0? StudentService.GetStudentsByClass((int)class_id): new List<STUDENT>();

            //get group student map
            List<STUDENT_GROUP_STUDENT_LIST> Map = DBS.findActiveRecordsBySingleParm<STUDENT_GROUP_STUDENT_LIST>("STUDENT_GROUP", group_id);

            //student with bool list
            List<GroupAndStudent> GnSs = new List<GroupAndStudent>();
            if (students != null && students.Any())
            {
                for (int s = 0; s < students.Count; s++) //check each subject if they mapped to chosen group
                {
                    GroupAndStudent GS = new GroupAndStudent();
                    GS.StudentID = students[s].ID;
                    GS.Student_Name = students[s].STUDENT_NAME;
                    GS.GroupID = group_id;
                    if (Map != null && Map.Any() && Map.Exists(m => (m.STUDENT == students[s].ID) && (m.STUDENT_GROUP == group_id))) // check if any map record matches chosen group
                    {
                        GS.Contain = true; //bool
                        GS.MapID = Map.Where(m => (m.STUDENT == students[s].ID) && (m.STUDENT_GROUP == group_id)).First().ID;
                    }
                    GnSs.Add(GS);
                }
            }
            //--------------------------------------------------------------------------------------------------------------------

            GSVM.Student = GnSs;
            
            ModelState.Clear();
            return View("StudentGroupAndStudents", GSVM);
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult StudentGroupAndStudents(GroupStudentViewModel GSVM)
        {
            checkLogin();
            
            bool result = true;
            
            for (int i = 0; i < GSVM.Student.Count; i++)
            {
                if (result)
                {
                    if (GSVM.Student[i].Contain && GSVM.Student[i].MapID < 1) // constains a map ID means its an existing record
                    {
                        STUDENT_GROUP_STUDENT_LIST newMap = new STUDENT_GROUP_STUDENT_LIST(0, GSVM.Student[i].GroupID, GSVM.Student[i].StudentID,CP.userID, DateTime.Now, 0, Constant.DEF_DATETIME, true);
                        result = false;
                        result = DBS.addRecord(newMap);
                    }
                    else if (!GSVM.Student[i].Contain && GSVM.Student[i].MapID > 0) //not contain but have existing id means need to remove
                    {
                        result = false;
                        result = DBS.InactiveRecord("STUDENT_GROUP_STUDENT_LIST", GSVM.Student[i].MapID, CP.userID);
                    }
                }
            }

            GSVM.TermList = CP.termSelectList;
            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return RedirectToAction("_StudentGroupAndStudents", new{term_id = GSVM.SelectedTerm, group_id = GSVM.SelectedGroup, class_id = GSVM.SelectedClass });
        }

    }
}




