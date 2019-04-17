using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QE.Models;
using QE.Services;
using QE.Models.ViewModels;

namespace QE.Controllers
{
    public class AdminAndSetupController : Controller
    {// GET: Index
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        private static IDBservice DBS = new DBservice();
        private static GeneralinitializationService GS = new GeneralinitializationService();
        private static GeneralinitializationService.COMMONPARM CP = new GeneralinitializationService.COMMONPARM();
        //private Dictionary<int, TERM> termlist = CP.termlist;
        //private List<QE_USER> userList;
        //private List<SubjectAndSSubjects> SubjectAndSSubjectsList;
        //private static SelectList subjectSelectList;
        //private TERM previousT;
        //private TERM currentT;
        //private TERM nextT;

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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Houses()
        {
            return View("Index");
        }
        
        // GET: Term
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Term()
        {
            checkLogin();

            TERM previousT = TermService.GetPreviousTerm();
            TERM currentT = TermService.GetCurrentTerm();
            TERM nextT = TermService.GetNextTerm();
            Session["previousT"] = previousT;
            Session["currentT"] = currentT;
            Session["nextT"] = nextT;


            return View(new TermViewModel(previousT, currentT, nextT));
        }

        // POST: Term
        [HttpPost, ActionName("Term")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult TermPost(TERM next)
        {
            checkLogin();

            TERM previousT = Session["previousT"] != null? (TERM)Session["previousT"]: TermService.GetPreviousTerm();
            TERM currentT = Session["currentT"] != null ? (TERM)Session["currentT"] : TermService.GetCurrentTerm();
            TERM nextT = Session["nextT"] != null ? (TERM)Session["nextT"] : TermService.GetNextTerm();
            
            TermViewModel viewModel = new TermViewModel(previousT, currentT, nextT);
            TempData["showForm"] = false;
            TempData["resultMsg"] = "";

            // fields checking
            if (!ModelState.IsValid)
            {
                TempData["showForm"] = true;
                return View("Term", viewModel); // redirect to form with data
            }
            // logic checking
            bool pass = true;

            if (TermService.GetTermByName(next.TERM_NAME).ID != 0 && TermService.GetTermByName(next.TERM_NAME).ID != next.ID)
            {
                TempData[Constant.msg_error] = "term name already in use";
                pass = false;
            }

            if (pass && next.TERM_START >= next.TERM_END)
            {
                TempData[Constant.msg_error] = "start date > end date";
                pass = false;
            }

            if (pass && next.TERM_START < DateTime.Now.Date)
            {
                TempData[Constant.msg_error] = "start date < today";
                pass = false;
            }

            if (pass && (currentT != null) && currentT.TERM_END >= next.TERM_START)
            {
                TempData[Constant.msg_error] = "current term end date > next term start date";
                pass = false;
            }

            if (!pass)
            {
                TempData["showForm"] = true;
                return View("Term", viewModel);
            }// redirect to form with data

            next.ACTIVE = true;
            bool isSuccess = false;

            if (next.ID == Constant.DEF_INT)
                isSuccess = DBS.addRecord(next);
            else
                isSuccess = DBS.updateRecord(next);

            // some unkown error happened
            if (!isSuccess)
            {
                TempData[Constant.msg_error] = "Ops! something gone wrong.";
                TempData["showForm"] = true;
                return View("Term", viewModel); // redirect to form with data
            }

            TempData[Constant.msg_success] = ((next.ID == Constant.DEF_INT) ? "New term has been created" : "Next term has been edited");

            //regen lists
            previousT = TermService.GetPreviousTerm();
            currentT = TermService.GetCurrentTerm();
            nextT = TermService.GetNextTerm();

            return View(new TermViewModel(previousT, currentT, nextT)); // success
        }

        // GET: UsersAndTeachers
        [HttpGet]
        [AllowAnonymous]
        public ActionResult UsersAndTeachers(int? id) // user id
        {
            checkLogin();

            List<QE_USER> list = DBS.findActiveRecords<QE_USER>();
            Session["userList"] = list;

            QE_USER displayingUser = id != null ? list.Where(l => l.ID == (int)id).FirstOrDefault() : new QE_USER();

            UsersAndTeachersViewModel ViewModel = new UsersAndTeachersViewModel(list, displayingUser);
            return View(ViewModel);
        }

        // POST: UsersAndTeachers
        [HttpPost, ActionName("UsersAndTeachers")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UsersAndTeachersPost(QE_USER user)
        {
            checkLogin();

            //get user list
            List<QE_USER> userList = Session["userList"] != null ? (List<QE_USER>)Session["userList"] : DBS.findActiveRecords<QE_USER>();
            UsersAndTeachersViewModel viewModel = new UsersAndTeachersViewModel(userList, user);

            //string password = Membership.GeneratePassword(8, 4);
            //user.PASSWORD = AccountService.aesEncryptBase64(password, "QEKey");
            string password = "test";
            user.PASSWORD = "test";

            user.ACTIVE = true;
            user.ADD_BY = CP.userID;
            user.ADD_DATE = DateTime.Now;


            // fields checking
            if (!ModelState.IsValid)
                return View("UsersAndTeachers", viewModel); // redirect to form with data

            // business logic check
            //user.ID < 1 means this is a new user
            if (user.ID < 1)
            {
                if (userList != null && userList.Exists(u => u.ID == user.ID))
                {
                    TempData[Constant.msg_error] = "User ID already in use";
                    return View("UsersAndTeachers", viewModel); // redirect to form with data
                }
            }
                
            
            bool isSuccess = false;

            if (user.ID < 1)
            {
                user.ID = DBS.addRecordReturnID(user);
                isSuccess = user.ID > 0 ? true : false;
            }
            else
                isSuccess = DBS.updateRecord(user);

            if (isSuccess)
            {
                //reset user list after user changes
                CP.teacherSelectList = UserService.getTeacherSelectList(); 
                userList = DBS.findActiveRecords<QE_USER>();
                Session["userList"] = userList;

                //MailService.SendNewPassword(user.EMAIL, user.USER_ID, password);
                TempData[Constant.msg_success] = ((user.ID < 1) ? "user has been created" : Constant.ChangeSucceed);
                return UsersAndTeachers(user.ID);
            }
            else
            {
                TempData[Constant.msg_error] = "Ops! somethings gone wrong.";
                return View("UsersAndTeachers", viewModel);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult InactiveUser(int id)
        {
            checkLogin();
            if (id > 0)
            {
                if (DBS.InactiveRecord("QE_USER", id, CP.userID))
                {
                    TempData[Constant.msg_success] = "User has been deleted";
                    List<QE_USER> userList = DBS.findActiveRecords<QE_USER>(); // regen userlist 
                    Session["userList"] = userList;

                    UsersAndTeachersViewModel ViewModel = new UsersAndTeachersViewModel(userList, userList.FirstOrDefault());
                    return View("UsersAndTeachers", ViewModel);
                }
                else
                {
                    TempData[Constant.msg_success] = "Failed to delete user";
                    return UsersAndTeachers(id);
                }
            }
            return UsersAndTeachers(id);
        }

        // GET: Subject
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Subject(int? id) // term id
        {
            checkLogin();

            //if user choose term or not
            TERM displayingTerm = id != null && id != CP.currentTerm.ID? CP.termlist[(int)id] : CP.currentTerm;
            
            SubjectViewModel ViewModel = new SubjectViewModel();
            ViewModel.TermList = CP.termSelectList;
            ViewModel.SelectedTerm = displayingTerm.ID;
            ViewModel.SubjectList = SubjectService.GetSelectList(displayingTerm.ID);
            Session["subjectSelectList"] = ViewModel.SubjectList;
            ViewModel.SubjectAndSSubjects = SubjectService.getSubjectAndSSubjectsList(displayingTerm.ID);
            Session["SubjectAndSSubjectsList"] = ViewModel.SubjectAndSSubjects;
            
            ModelState.Clear();
            return View(ViewModel);
        }

        //save subject
        [HttpPost, ActionName("Subject")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SubjectPost(SubjectViewModel ViewModel)
        {
            checkLogin();
            
            List<SubjectAndSSubjects> SubjectAndSSubjectsList = Session["SubjectAndSSubjectsList"]!= null? 
                (List<SubjectAndSSubjects>)Session["SubjectAndSSubjectsList"]: 
                SubjectService.getSubjectAndSSubjectsList(ViewModel.SelectedTerm);

            bool result = true;

            foreach (var id in ViewModel.InactiveSSubjectsList)
            {
                if (result)
                    result = DBS.InactiveRecord("SSUBJECT", id, CP.userID);
            }

            if (result)
            {
                foreach (var id in ViewModel.InactiveSubjectsList)
                {
                    List<SSUBJECT> ssList = SubjectAndSSubjectsList.Where(s => s.MainSubject.ID == id).FirstOrDefault().SSubjects;
                    foreach (var ss in ssList)
                    {
                        if (result)
                            result = DBS.InactiveRecord("SSUBJECT", ss.ID, CP.userID);
                    }
                    if (result)
                        result = DBS.InactiveRecord("SUBJECT", id, CP.userID);
                }
            }

            if (result)
            {
                foreach (var item in ViewModel.SubjectAndSSubjects)
                {
                    if (result)
                    {
                        if (item.MainSubject.ID < 0)
                        {
                            item.MainSubject.ADD_BY = CP.userID;
                            item.MainSubject.ADD_DATE = DateTime.Now;
                            int insertedRecordId = DBS.addRecordReturnID(item.MainSubject);
                            result = insertedRecordId > 0 ? true : false;

                            if (result && item.SSubjects != null && item.SSubjects.Any())
                            {
                                foreach (var subItem in item.SSubjects)
                                {
                                    if (result)
                                    {
                                        subItem.SUBJECT = insertedRecordId;
                                        subItem.ADD_DATE = DateTime.Now;
                                        subItem.ADD_BY = CP.userID;
                                        subItem.SUBJECT = insertedRecordId;
                                        result = DBS.addRecord(subItem);
                                    }
                                }
                            }
                        }
                        else
                        {
                            result = DBS.updateRecord(item.MainSubject);
                            if (result &&item.SSubjects != null && item.SSubjects.Any())
                            {
                                foreach (var ss in item.SSubjects)
                                {
                                    if (result)
                                    {
                                        if (ss.ID > 0)
                                        {
                                            ss.MODIFY_BY = CP.userID;
                                            ss.MODIFY_DATE = DateTime.Now;
                                            result = DBS.updateRecord(ss);
                                        }
                                        else
                                        {
                                            ss.ADD_BY = CP.userID;
                                            ss.ADD_DATE = DateTime.Now;
                                            result = DBS.addRecord(ss);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (result)
            {//reset common parms after changes
                ViewModel.SubjectList = SubjectService.GetSelectList(ViewModel.SelectedTerm);
                ViewModel.SubjectAndSSubjects = SubjectService.getSubjectAndSSubjectsList(ViewModel.SelectedTerm);
                Session["subjectSelectList"] = ViewModel.SubjectList;
                Session["SubjectAndSSubjects"] = ViewModel.SubjectAndSSubjects;
            }

            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return Subject(ViewModel.SelectedTerm);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveSubject(SubjectViewModel ViewModel, int id)
        {
            checkLogin();
            
            ViewModel.TermList = CP.termSelectList;
            ViewModel.SubjectList = Session["subjectSelectList"] != null? 
                (SelectList)Session["subjectSelectList"]: 
                SubjectService.GetSelectList(ViewModel.SelectedTerm);

            if (id > 0)
            {
                ViewModel.InactiveSubjectsList.Add(id);
            }
            
            ViewModel.SubjectAndSSubjects.Remove(ViewModel.SubjectAndSSubjects.Where(s => s.MainSubject.ID == id).FirstOrDefault());
            ViewModel.SubjectList = SubjectService.GetSelectList(ViewModel.SubjectAndSSubjects);
            Session["subjectSelectList"] = ViewModel.SubjectList;

            if (ViewModel.SelectedSubject == id)
                ViewModel.SelectedSubject = 0;

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Subject", ViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveSSubject(SubjectViewModel ViewModel, int id)
        {
            checkLogin();

            ViewModel.TermList = CP.termSelectList;
            ViewModel.SubjectList = Session["subjectSelectList"] != null ?
                (SelectList)Session["subjectSelectList"] :
                SubjectService.GetSelectList(ViewModel.SelectedTerm);

            //deduct marks of the delete sub-subject from its main subject
            ViewModel.SubjectAndSSubjects.Where(s => s.SSubjects.Exists(ss => ss.ID == id)).First().MainSubject.FULL_MARK -= ViewModel.SubjectAndSSubjects.Where(s => s.SSubjects.Exists(ss => ss.ID == id)).First().SSubjects.Where(ssb => ssb.ID == id).First().FULL_MARK;
            
            if (id > 0)
            {
                ViewModel.InactiveSSubjectsList.Add(id);
            }

            ViewModel.SubjectAndSSubjects.ForEach(s => s.SSubjects.RemoveAll(ss => ss.ID == id));

            //int totalMark = 0;
            //int target_main_subject_id = ViewModel.SubjectAndSSubjects.Where(s => s.SSubjects.Exists(ss => ss.ID == id)).FirstOrDefault().MainSubject.ID;
            //ViewModel.SubjectAndSSubjects.FindAll(s => s.MainSubject.ID == target_main_subject_id).FirstOrDefault().SSubjects.ForEach(s => totalMark += s.FULL_MARK);
            //ViewModel.SubjectAndSSubjects.FindAll(s => s.MainSubject.ID == target_main_subject_id).FirstOrDefault().MainSubject.FULL_MARK = totalMark;

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Subject", ViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InsertSubject(SubjectViewModel ViewModel)
        {
            checkLogin();
            ModelState.Clear();

            ViewModel.TermList = CP.termSelectList;
            ViewModel.SubjectList = Session["subjectSelectList"] != null ?
                (SelectList)Session["subjectSelectList"] :
                SubjectService.GetSelectList(ViewModel.SelectedTerm);

            foreach (var key in ModelState.Keys)
            {
                if (!key.Contains("NewSubject"))
                    ModelState[key].Errors.Clear(); // only need to check new subject fields
            }

            if (!ModelState.IsValid)
                return View("Subject", ViewModel);

            ViewModel.NewSubject.ADD_BY = CP.userID;
            ViewModel.NewSubject.ADD_DATE = DateTime.Now;
            ViewModel.NewSubject.ACTIVE = true;

            if (ViewModel.SelectedSubject == 0)
            {
                SUBJECT subject = SubjectService.SubjectAdapter(ViewModel.NewSubject); // convert ssubject to subject type
                subject.TERM = ViewModel.SelectedTerm;
                subject.ID = GS.getNewID();

                SubjectAndSSubjects sAndss = new SubjectAndSSubjects();
                sAndss.MainSubject = subject;
                ViewModel.SubjectAndSSubjects.Add(sAndss);
            }
            else
            {
                ViewModel.NewSubject.ID = GS.getNewID();
                ViewModel.NewSubject.SUBJECT = ViewModel.SelectedSubject;
                ViewModel.SubjectAndSSubjects.FindAll(s => s.MainSubject.ID == ViewModel.SelectedSubject).FirstOrDefault().SSubjects.Add(ViewModel.NewSubject);
                int totalMark = 0;
                ViewModel.SubjectAndSSubjects.FindAll(s => s.MainSubject.ID == ViewModel.SelectedSubject).FirstOrDefault().SSubjects.ForEach(s => totalMark+=s.FULL_MARK);
                ViewModel.SubjectAndSSubjects.FindAll(s => s.MainSubject.ID == ViewModel.SelectedSubject).FirstOrDefault().MainSubject.FULL_MARK = totalMark;


            }

            //update params and list after changes
            ViewModel.SubjectList = SubjectService.GetSelectList(ViewModel.SubjectAndSSubjects);
            Session["subjectSelectList"] = ViewModel.SubjectList;
            ViewModel.NewSubject = new SSUBJECT();

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Subject", ViewModel);
        }

        // GET: QE_Class
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Classes(int? id) // term id
        {
            checkLogin();

            TERM displayingTerm = id != null ? TermService.GetTerm((int)id) : CP.currentTerm;

            ClassViewModel CVM = new ClassViewModel()
            {
                classes = ClassService.getClassViews(displayingTerm.ID),
                SelectedTerm = displayingTerm.ID,
                TermList = CP.termSelectList,
                teachers = CP.teacherSelectList,
                newClass = new QE_CLASS()
            };

            ModelState.Clear();
            return View(CVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveClass(ClassViewModel CVM, int id)
        {
            checkLogin();

            CVM.TermList = CP.termSelectList;
            CVM.teachers = CP.teacherSelectList;

            CVM.classes.Remove(CVM.classes.Where(c => c.ID == id).FirstOrDefault());

            if(id > 0)
                CVM.inactiveList.Add(id);

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Classes", CVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddClass(ClassViewModel CVM)
        {
            checkLogin();

            CVM.TermList = CP.termSelectList;
            CVM.teachers = CP.teacherSelectList;

            ClassView newView = new ClassView(CVM.newClass, CVM.teachers.Where(t => t.Value == CVM.newClass.TEACHER.ToString()).FirstOrDefault().Text, CVM.teachers.Where(t => t.Value == CVM.newClass.TEACHER_2.ToString()).FirstOrDefault().Text);
            newView.ID = GS.getNewID();
            CVM.classes.Add(newView);
            CVM.newClass = new QE_CLASS();

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Classes", CVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Classes(ClassViewModel CVM)
        {
            checkLogin();

            bool result = true;

            foreach (var c in CVM.inactiveList)
            {
                if (result && c > 0)
                {
                    result = false;
                    result = DBS.InactiveRecord("QE_CLASS", c, CP.userID);
                }
            }

            foreach (var c in CVM.classes)
            {
                if (result && c.ID < 0)
                {
                    QE_CLASS newClass = new QE_CLASS()
                    {
                        TERM = CVM.SelectedTerm,
                        FORM = c.FORM,
                        NAME = c.NAME,
                        TEACHER = c.TEACHER,
                        TEACHER_2 = c.TEACHER_2,
                        CLASSROOM = c.CLASSROOM,
                        ADD_BY = CP.userID,
                        ADD_DATE = DateTime.Now,
                        ACTIVE = true
                    };
                    result = false;
                    result = DBS.addRecord(newClass);
                }
            }

            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return Classes(CVM.SelectedTerm > 0 ? CVM.SelectedTerm : CP.currentTerm.ID);
        }


        // GET Accesses
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Accesses()
        {
            checkLogin();

            AccessViewModel AVM = new AccessViewModel()
            {
                group = DBS.findALLRecords<USER_GROUP>(),
                inactiveList = new List<int>(),
                newGroup = new USER_GROUP(),
                groupRights = new List<USER_GROUP_RIGHT>(),
                groupUserlist = new List<GroupUserView>()
            };


            ModelState.Clear();
            return View(AVM);
        }

        // View Accesses
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult _Accesses(AccessViewModel AVM, int group_id) // term id
        {
            checkLogin();

            AVM.groupUserlist = new List<GroupUserView>();
            AVM.groupRights = new List<USER_GROUP_RIGHT>();
            AVM.SelectedGroup = group_id;

            List<GROUP_RIGHT> rights = DBS.findALLRecords<GROUP_RIGHT>();
            List<QE_USER> user = DBS.findActiveRecords<QE_USER>();

            //gen user list
            List<USER_GROUP_USER_LIST> groupUserList = DBS.findALLRecords<USER_GROUP_USER_LIST>();
            foreach (var u in user)
            {
                GroupUserView view = new GroupUserView(u);
                view.USER_GROUP = group_id;
                view.QE_USER = u.ID;
                view.user_name = u.USER_NAME;
                if (groupUserList != null)
                {
                    if (groupUserList.Exists(g => (g.USER_GROUP == group_id) && (g.QE_USER == u.ID)))
                    {
                        USER_GROUP_USER_LIST foundList = groupUserList.Where(g => (g.USER_GROUP == group_id) && (g.QE_USER == u.ID)).FirstOrDefault();
                        view.ID = foundList.ID;
                        view.QE_USER = foundList.QE_USER;
                        view.user_name = u.USER_NAME;
                        view.USER_GROUP = foundList.USER_GROUP;
                        view.contain = true;
                    }
                }
                AVM.groupUserlist.Add(view);
            }

            //gen right list
            List<USER_GROUP_RIGHT> groupRightList = DBS.findALLRecords<USER_GROUP_RIGHT>();
            foreach (var r in rights)
            {
                USER_GROUP_RIGHT right = new USER_GROUP_RIGHT();
                right.RIGHT_ID = r.ID;
                right.RIGHT_NAME = r.RIGHT_NAME;
                right.USER_GROUP = group_id;
                if (groupRightList != null)
                {
                    if (groupRightList.Exists(g => (g.USER_GROUP == group_id) && (g.RIGHT_ID == r.ID)))
                    {
                        right = groupRightList.Where(g => g.USER_GROUP == group_id && (g.RIGHT_ID == r.ID)).FirstOrDefault();
                    }
                }
                AVM.groupRights.Add(right);
            }


            ModelState.Clear();
            return View("Accesses", AVM);
        }

        //remove access group
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveGroup(AccessViewModel AVM, int group_id)
        {
            checkLogin();

            AVM.group.Remove(AVM.group.Where(g => g.ID == group_id).FirstOrDefault());

            if(group_id>0)
                AVM.inactiveList.Add(group_id);

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Accesses", AVM);
        }

        //add access group
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroup(AccessViewModel AVM)
        {
            checkLogin();

            int newID = AVM.newGroup.ID = GS.getNewID();
            AVM.group.Add(AVM.newGroup);
            AVM.newGroup = new USER_GROUP();

            ModelState.Clear();
            TempData["showForm"] = true;
            return _Accesses(AVM, newID);
        }

        //admend access group
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Accesses(AccessViewModel AVM)
        {
            checkLogin();

            bool result = true;

            //delete unwanted group
            foreach (var g in AVM.inactiveList)
            {
                if (result && g > 0)
                {
                    result = false;
                    result = DBS.removeRecord<USER_GROUP>(g);
                    result = DBS.removeRecord<USER_GROUP_RIGHT>(g);
                    result = DBS.removeRecord<USER_GROUP_USER_LIST>(g);
                }
            }

            //add new group
            if (result)
            {
                foreach (var g in AVM.group)
                {
                    if (result && g.ID < 1)
                    {
                        g.ADD_BY = CP.userID;
                        g.ADD_DATE = DateTime.Now;
                        result = DBS.addRecord(g);
                    }
                }
            }

            //right change for the group
            if (result)
            {
                foreach (var r in AVM.groupRights)
                {
                    if (result)
                    {
                        if (r.ID < 1)
                        {
                            r.ADD_BY = CP.userID;
                            r.ADD_DATE = DateTime.Now;
                            result = DBS.addRecord(r);
                        }
                        else
                        {
                            r.MODIFY_BY = CP.userID;
                            r.MODIFY_DATE = DateTime.Now;
                            result = DBS.updateRecord(r);

                        }
                    }
                }
            }

            //change user for the group
            if (result)
            {
                foreach (var u in AVM.groupUserlist)
                {
                    if (result)
                    {
                        if (u.contain && u.ID < 1)
                        {
                            USER_GROUP_USER_LIST user = new USER_GROUP_USER_LIST()
                            {
                                QE_USER = u.QE_USER,
                                USER_GROUP = u.USER_GROUP
                            };

                            result = DBS.addRecord(user);
                        }
                        else if (!u.contain && u.ID > 0)
                        {
                            result = DBS.removeRecord<USER_GROUP_USER_LIST>(u.ID);
                        }
                    }
                }
            }

            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return _Accesses(AVM, AVM.SelectedGroup);
        }


        // GET: Merit
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Merit()
        {
            checkLogin();

            MeritViewModel MVM = new MeritViewModel() { merits = DBS.findActiveRecords<MERIT>() };

            return View(MVM);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult removeMerit(MeritViewModel MVM, int id)
        {
            checkLogin();

            MVM.merits.Remove(MVM.merits.Where(m => m.ID == id).First());

            if (id > 0)
                MVM.removeList.Add(id);

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Merit", MVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult addMerit(MeritViewModel MVM)
        {
            checkLogin();

            MVM.newMerit.ID = GS.getNewID();

            MVM.merits.Add(MVM.newMerit);
            MVM.newMerit = new MERIT();

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("Merit", MVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Merit(MeritViewModel MVM)
        {
            checkLogin();

            // fields checking
            if (!ModelState.IsValid)
                return View("Merit", MVM);

            //edit or create
            bool result = true;

            for (int m = 0; m < MVM.merits.Count; m++)
            {
                if (result)
                {
                    if (MVM.merits[m].ID < 1) //create
                    {
                        MERIT nm = MVM.merits[m];
                        nm.ADD_BY = CP.userID;
                        nm.ADD_DATE = DateTime.Now;
                        nm.ACTIVE = true;

                        result = false;
                        result = DBS.addRecord(nm);
                    }
                    else if(MVM.merits[m].ID > 0 && MVM.removeList.Exists(r => r == MVM.merits[m].ID)) //edit
                    {
                            result = false;
                            result = DBS.InactiveRecord("MERIT", MVM.merits[m].ID, CP.userID);
                    }
                    else
                    {
                        MVM.merits[m].MODIFY_BY = CP.userID;
                        MVM.merits[m].MODIFY_DATE = DateTime.Now;

                        result = false;
                        result = DBS.updateRecord(MVM.merits[m]);

                    }
                }
            }
            
            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return Merit();
        }
    }
}


