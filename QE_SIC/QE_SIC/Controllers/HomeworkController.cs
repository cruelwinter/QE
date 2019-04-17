using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QE.Models;
using QE.Models.ViewModels;
using QE.Services;

namespace QE.Controllers
{
    public class HomeworkController : Controller
    {
        private static IDBservice DBS = new DBservice();
        private static GeneralinitializationService GS = new GeneralinitializationService();
        private static GeneralinitializationService.COMMONPARM CP = new GeneralinitializationService.COMMONPARM();
        //private SelectList termSelectList;
        //private static IEnumerable<SelectListItem> groupSelectList;
        //private static IEnumerable<SelectListItem> typeSelectList;
        //private static IEnumerable<SelectListItem> termFilList;
        //private static IEnumerable<SelectListItem> groupFilList;
        //private static IEnumerable<SelectListItem> typeFilList;

        private IEnumerable<SelectListItem>genSelectList(IEnumerable<SelectListItem> SL, int selected)
        {
            List<SelectListItem> templist = SL.ToList();
            templist.Add(new SelectListItem() { Value = "0", Text = "All" });

            foreach (var item in templist)
            {
                if (item.Value == selected.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }

            SL = templist;
            return SL;
        }

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

        // GET: Homework
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Homework()
        {
            checkLogin();

            HomeworkViewModel HVM = new HomeworkViewModel();
            HVM.groupSelectList = StudentGroupService.getGroupSelectList(CP.currentTerm.ID);
            HVM.typeSelectList = HomeworkService.getTypeSelectList();
            Session["groupSelectList"] = HVM.groupSelectList;
            Session["typeSelectList"] = HVM.typeSelectList;
            
            HVM.termFilList = genSelectList(CP.termSelectList, 0);
            HVM.groupFilList  = genSelectList(HVM.groupSelectList, 0);
            HVM.typeFilList = genSelectList(HVM.typeSelectList, 0);
            Session["termFilList"] = HVM.termFilList;
            Session["groupFilList"] = HVM.groupFilList;
            Session["typeFilList"] = HVM.typeFilList;
            
            HVM.viewList = new List<HomeworkView>();

            ModelState.Clear();
            TempData["showForm2"] = false;
            TempData["showForm"] = false;
            return View(HVM);
        }

        // GET: Homework
        [HttpPost, ActionName("viewHomework")]
        [AllowAnonymous]
        public ActionResult Homework(int SelectedTerm, int SelectedGroup, int SelectedType, HomeworkViewModel HVM )
        {
            checkLogin();

            HVM.groupSelectList = SelectedTerm == CP.currentTerm.ID ?
                (IEnumerable<SelectListItem>)Session["groupSelectList"] :
                StudentGroupService.getGroupSelectList(HVM.SelectedTerm);
            Session["groupSelectList"] = HVM.groupSelectList;
            HVM.typeSelectList = (IEnumerable<SelectListItem>)Session["typeSelectList"];
            HVM.SelectedTerm = SelectedTerm;
            HVM.SelectedGroup = SelectedGroup;
            HVM.SelectedType =  SelectedType;
            HVM.termFilList  = genSelectList(CP.termSelectList, HVM.SelectedTerm);
            HVM.groupFilList = genSelectList(HVM.groupSelectList, HVM.SelectedGroup);
            HVM.typeFilList = genSelectList(HVM.typeSelectList, HVM.SelectedType);
            Session["termFilList"] = HVM.termFilList;
            Session["groupFilList"] = HVM.groupFilList;
            Session["typeFilList"] = HVM.typeFilList;

            List<HOMEWORK> works = HomeworkService.GetFilteredWorks(HVM.SelectedTerm, HVM.SelectedGroup, HVM.SelectedType);//homeworks
            List<HomeworkView> viewList = new List<HomeworkView>(); //homework in display format
            if (works != null && works.Any())
            {
                for (int h = 0; h < works.Count; h++) //put homeworks into display format
                {
                    HomeworkView view = new HomeworkView();
                    view.ID = works[h].ID;
                    view.name = works[h].NAME;
                    view.date_assign = works[h].ADD_DATE;
                    view.date_due = works[h].DUE_DATE;
                    view.full_mark = works[h].FULL_MARK;
                    view.group_id = works[h].STUDENT_GROUP;
                    view.type_id = works[h].HOMEWORK_TYPE;
                    view.submission_ratio = works[h].SUBMISSION_RATIO;
                    view.late_ratio = works[h].LATE_RATIO;
                    view.group_name = view.group_id > 0 ? DBS.findRecordByID<STUDENT_GROUP>(works[h].STUDENT_GROUP).NAME : string.Empty;
                    view.type_name = view.type_id > 0 ? DBS.findRecordByID<HOMEWORK_TYPE>(works[h].HOMEWORK_TYPE).NAME : string.Empty;
                    
                    viewList.Add(view);
                }
            }

            HVM.viewList = viewList;

            ModelState.Clear();
            TempData["showForm2"] = false;
            TempData["showForm"] = false;
            return View("Homework", HVM);
        }


        // GET: Homework
        [HttpPost]
        [AllowAnonymous]
        public ActionResult viewSubmission(HomeworkViewModel HVM, int HWV_id)
        {
            HVM.groupSelectList = (IEnumerable<SelectListItem>)Session["groupSelectList"];
            HVM.typeSelectList = (IEnumerable<SelectListItem>)Session["typeSelectList"];
            HVM.groupFilList = (IEnumerable<SelectListItem>)Session["groupFilList"];
            HVM.typeFilList = (IEnumerable<SelectListItem>)Session["typeFilList"];
            HVM.termFilList = (IEnumerable<SelectListItem>)Session["termFilList"];
            HVM.submission_list = new List<Submission_view>();

            HomeworkView view = HVM.viewList.Where(v => v.ID == HWV_id).First();
            List<HOMEWORK_SUBMISSION_LIST> submissions = HWV_id > 0? DBS.findRecordsBySingleParm<HOMEWORK_SUBMISSION_LIST>("HOMEWORK", HWV_id): new List<HOMEWORK_SUBMISSION_LIST>();
            List<STUDENT> students = StudentService.getStudentByGroup(view.group_id);
            if (students != null && students.Any())
            {
                for (int s = 0; s < students.Count; s++)
                {
                    Submission_view SV = new Submission_view();
                    SV.homework_id = (int)HWV_id;
                    SV.student_id = students[s].ID;
                    SV.group_id = view.group_id;
                    SV.work_name = view.name;
                    SV.group_name = view.group_name;
                    SV.student_name = students[s].STUDENT_NAME;

                    if (submissions != null && submissions.Exists(sub => sub.STUDENT == SV.student_id))
                    {
                        HOMEWORK_SUBMISSION_LIST HSL = submissions.Where(sub => sub.STUDENT == SV.student_id).FirstOrDefault();
                        SV.QE_class = HSL.QE_CLASS;
                        SV.QE_class_name = HSL.QE_CLASS_NAME;
                        SV.class_num = HSL.CLASS_NUM;
                        SV.mark = HSL.MARK;
                        SV.remark = HSL.REMARK;
                        SV.submit_date = HSL.ADD_DATE;
                        SV.submission_id = HSL.ID;
                        SV.contain = true;
                    }
                    else
                    {
                        ClassStudentMap map = ClassService.getClassStudentMapByStudent(students[s].ID);
                        SV.QE_class = map.ID;
                        SV.QE_class_name = map.FORM.ToString() + map.NAME;
                        SV.class_num = map.CLASS_NUM;
                        SV.contain = false;
                    }
                    HVM.submission_list.Add(SV);
                }
            }

            ModelState.Clear();
            TempData["showForm2"] = true;
            TempData["showForm"] = false;
            return View("Homework", HVM);
        }
        
        


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveWork(HomeworkViewModel HVM, int HVM_id)
        {
            checkLogin();

            HVM.groupSelectList = (IEnumerable<SelectListItem>)Session["groupSelectList"];
            HVM.typeSelectList = (IEnumerable<SelectListItem>)Session["typeSelectList"];
            HVM.groupFilList = (IEnumerable<SelectListItem>)Session["groupFilList"];
            HVM.typeFilList = (IEnumerable<SelectListItem>)Session["typeFilList"];
            HVM.termFilList = (IEnumerable<SelectListItem>)Session["termFilList"];

            HVM.viewList.Remove(HVM.viewList.Where(v => v.ID == HVM_id).FirstOrDefault());

            if(HVM_id > 0)
                HVM.inactiveList.Add(HVM_id);
            
            ModelState.Clear();
            TempData["showForm"] = true;
            TempData["showForm2"] = false;
            return View("Homework", HVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddWork(HomeworkViewModel HVM)
        {
            checkLogin();

            HVM.groupSelectList = (IEnumerable<SelectListItem>)Session["groupSelectList"];
            HVM.typeSelectList = (IEnumerable<SelectListItem>)Session["typeSelectList"];
            HVM.groupFilList = (IEnumerable<SelectListItem>)Session["groupFilList"];
            HVM.typeFilList = (IEnumerable<SelectListItem>)Session["typeFilList"];
            HVM.termFilList = (IEnumerable<SelectListItem>)Session["termFilList"];

            HVM.viewList.Add(new HomeworkView()
            {
                ID = GS.getNewID(),
                name = HVM.newWork.NAME,
                group_id = HVM.newWork.STUDENT_GROUP,
                group_name = HVM.groupSelectList.Where(l => l.Value == HVM.newWork.STUDENT_GROUP.ToString()).FirstOrDefault().Text,
                date_assign = DateTime.Now,
                date_due = HVM.newWork.DUE_DATE,
                type_id = HVM.newWork.HOMEWORK_TYPE,
                type_name = HVM.typeSelectList.Where(l => l.Value == HVM.newWork.HOMEWORK_TYPE.ToString()).FirstOrDefault().Text,
                submission_ratio = 0,
                late_ratio = 0,
                full_mark = HVM.newWork.FULL_MARK
            });

            HVM.newWork = new HOMEWORK();

            ModelState.Clear();
            TempData["showForm"] = true;
            TempData["showForm2"] = false;
            return View("Homework", HVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Homework(HomeworkViewModel HVM)
        {
            checkLogin();

            HVM.groupSelectList = (IEnumerable<SelectListItem>)Session["groupSelectList"];
            HVM.typeSelectList = (IEnumerable<SelectListItem>)Session["typeSelectList"];
            HVM.groupFilList = (IEnumerable<SelectListItem>)Session["groupFilList"];
            HVM.typeFilList = (IEnumerable<SelectListItem>)Session["typeFilList"];
            HVM.termFilList = (IEnumerable<SelectListItem>)Session["termFilList"];

            bool result = true;

            //inactive deleted view item
            foreach (var i in HVM.inactiveList)
            {
                if (result && i > 1)
                {
                    result = false;
                    result = DBS.InactiveRecord("HOMEWORK", i, CP.userID);
                }
            }

            //add new work from new view item
            foreach (var n in HVM.viewList)
            {
                if (result && n.ID < 1)
                {
                    result = false;
                    HOMEWORK newWork = new HOMEWORK()
                    {
                        NAME = n.name,
                        STUDENT_GROUP = n.group_id,
                        HOMEWORK_TYPE = n.type_id,
                        DUE_DATE = n.date_due,
                        FULL_MARK = n.full_mark,
                        ADD_BY = CP.userID,
                        ADD_DATE = DateTime.Now,
                        ACTIVE = true
                    };

                    result = DBS.addRecord(newWork);
                }
            }
            
            ModelState.Clear();
            TempData["showForm"] = true;
            TempData["showForm2"] = false;
            if (result) { TempData[Constant.msg_success] = "Changes have been saved"; } else { TempData[Constant.msg_error] = "Changes have not been saved"; }
            return Homework(HVM.SelectedTerm, HVM.SelectedGroup, HVM.SelectedType, HVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSubmissions(HomeworkViewModel HVM)
        {
            checkLogin();

            HVM.groupSelectList = (IEnumerable<SelectListItem>)Session["groupSelectList"];
            HVM.typeSelectList = (IEnumerable<SelectListItem>)Session["typeSelectList"];
            HVM.groupFilList = (IEnumerable<SelectListItem>)Session["groupFilList"];
            HVM.typeFilList = (IEnumerable<SelectListItem>)Session["typeFilList"];
            HVM.termFilList = (IEnumerable<SelectListItem>)Session["termFilList"];

            bool result = true;

            foreach (var s in HVM.submission_list)
            {
                if (result)
                {
                    if (s.submission_id > 0 && !s.contain)
                    {
                        //delete submission record
                        result = false;
                        result = DBS.removeRecord<HOMEWORK_SUBMISSION_LIST>(s.submission_id);
                    }
                    else if (s.submission_id < 1 && s.contain)
                    {
                        s.submit_date = DateTime.Now;//for late ratio cal

                        //add submission record
                        HOMEWORK_SUBMISSION_LIST newList = new HOMEWORK_SUBMISSION_LIST()
                        {
                            HOMEWORK = s.homework_id,
                            STUDENT = s.student_id,
                            STUDENT_GROUP = s.group_id,
                            QE_CLASS = s.QE_class,
                            QE_CLASS_NAME = s.QE_class_name,
                            GROUP_NAME = s.group_name,
                            STUDENT_NAME = s.student_name,
                            CLASS_NUM = s.class_num,
                            REMARK = s.remark,
                            MARK = s.mark,
                            ADD_BY = CP.userID,
                            ADD_DATE = DateTime.Now,
                            ACTIVE = true,
                        };

                        result = false;
                        result = DBS.addRecord(newList);
                        
                    }
                    else if (s.submission_id > 0 && s.contain)
                    {
                        // changes other than submission
                        Dictionary<string, string> parms = new Dictionary<string, string>()
                        {
                            {"REMARK", string.IsNullOrEmpty(s.remark) ? string.Empty : s.remark},
                            {"MARK", s.mark.ToString()},
                            {"MODIFY_BY", CP.userID.ToString()},
                            {"MODIFY_DATE", DateTime.Now.ToString()}
                        };
                        
                        result = false;
                        result = DBS.updateRecordByID<HOMEWORK_SUBMISSION_LIST>(parms, s.submission_id);
                    }
                }
            }


            if (result)
            {
                //update ratios
                HOMEWORK work = DBS.findActiveRecordByID<HOMEWORK>(HVM.submission_list.FirstOrDefault().homework_id);
                if (work != null)
                {
                    int studentCount = StudentService.countStudentByGroup(work.STUDENT_GROUP);
                    work.SUBMISSION_RATIO = (decimal)HVM.submission_list.Count(sm => sm.contain == true) / (decimal)studentCount;
                    work.LATE_RATIO = (decimal)(HVM.submission_list.Count(sm => ((sm.contain == true) && (sm.submit_date > work.DUE_DATE)))) / (decimal)studentCount;
                    work.MODIFY_BY = CP.userID;
                    work.MODIFY_DATE = DateTime.Now;
                    result = DBS.updateRecord(work);
                    if (result)
                    {
                        HVM.viewList.Where(h => h.ID == work.ID).First().submission_ratio = work.SUBMISSION_RATIO;
                        HVM.viewList.Where(h => h.ID == work.ID).First().late_ratio = work.LATE_RATIO;
                    }
                }
            }


            ModelState.Clear();
            TempData["showForm"] = false;
            TempData["showForm2"] = true;
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return viewSubmission(HVM, HVM.submission_list.FirstOrDefault().homework_id);
        }

        // GET: Homework
        public ActionResult HomeworkAnalysis()
        {
            return View();
        }

        // GET: tpye
        [HttpGet]
        [AllowAnonymous]
        public ActionResult HomeworkType()
        {
            checkLogin();

            HomeworkTypeViewModel HTVM = new HomeworkTypeViewModel() { types = DBS.findActiveRecords<HOMEWORK_TYPE>() };

            return View(HTVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InactiveType(HomeworkTypeViewModel HTVM, int type_id)
        {
            checkLogin();

            HTVM.types.Remove(HTVM.types.Where(h => h.ID == type_id).FirstOrDefault());

            if (type_id > 0)
                HTVM.inactiveList.Add(type_id);
            
            ModelState.Clear();
            TempData["showForm"] = true;
            return View("HomeworkType", HTVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddType(HomeworkTypeViewModel HTVM)
        {
            checkLogin();

            HTVM.newType.ID = GS.getNewID();
            HTVM.newType.ADD_BY = CP.userID;
            HTVM.newType.ADD_DATE = DateTime.Now;
            HTVM.newType.ACTIVE = true;

            HTVM.types.Add(HTVM.newType);
            HTVM.newType = new HOMEWORK_TYPE();

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("HomeworkType", HTVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult HomeworkType(HomeworkTypeViewModel HTVM)
        {
            checkLogin();

            // fields checking
            if (!ModelState.IsValid)
                return View("HomeworkType", HTVM);

            //edit or create
            bool result = true;

            for (int h = 0; h < HTVM.types.Count; h++)
            {
                if (result)
                {
                    if (HTVM.types[h].ID < 1)
                    {
                        result = false;
                        result = DBS.addRecord(HTVM.types[h]);
                    }
                }
            }

            foreach (var r in HTVM.inactiveList)
            {
                if (result && r > 0)
                {
                    result = false;
                    result = DBS.InactiveRecord("HOMEWORK_TYPE", r, CP.userID);
                }
            }

            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return HomeworkType();
        }
    }
}

