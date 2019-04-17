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
    public class MeritController : Controller
    {
        private static IDBservice DBS = new DBservice();
        private static GeneralinitializationService GS = new GeneralinitializationService();
        private static GeneralinitializationService.COMMONPARM CP = new GeneralinitializationService.COMMONPARM();
        private SelectList termSelectList;
        private List<STUDENT> studentList;
        private IEnumerable<SelectListItem> classSelectList;


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

        // GET: merits
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Merits(int? term_id, int?class_id)
        {
            checkLogin();

            int displayingTerm = CP.currentTerm.ID;
            
            if(term_id != null && (int)term_id > 0)
            {
                displayingTerm = (int)term_id;
            }

            MeritsViewModel MVM = new MeritsViewModel();
            MVM.TermList = CP.termSelectList;
            MVM.classList = classSelectList = ClassService.getClassSelectList(displayingTerm);
            if (class_id != null && (int)class_id > 0)
            {
                MVM.studentList = studentList = StudentService.GetStudents(displayingTerm, (int)class_id);
            }
            
            return View(MVM);
        }


        // GET: merits
        [HttpGet]
        [AllowAnonymous]
        public ActionResult viewMerits(int term_id, int class_id, int student_id)
        {
            checkLogin();
            
            MeritsViewModel MVM = new MeritsViewModel();
            MVM.TermList = CP.termSelectList;
            MVM.userList = CP.teacherSelectList;
            MVM.classList = classSelectList;
            MVM.studentList = studentList;
            MVM.merits = DBS.findActiveRecordsBySingleParm<MERITS>("STUDENT", student_id);
            
            return View("MERIT",MVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult inactiveMerits(MeritsViewModel MVM, int id)
        {
            checkLogin();
            MVM.TermList = CP.termSelectList;
            MVM.userList = CP.teacherSelectList;
            MVM.classList = classSelectList;

            MVM.merits.Remove(MVM.merits.Where(m => m.ID == id).First());

            if (id > 0)
                MVM.inactiveList.Add(id);

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("MERIT", MVM);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult addMerits(MeritsViewModel MVM)
        {
            checkLogin();
            MVM.TermList = CP.termSelectList;
            MVM.userList = CP.teacherSelectList;
            MVM.classList = classSelectList;

            MVM.newMerit.ID = GS.getNewID();
            MVM.newMerit.ADD_BY = CP.userID;
            MVM.newMerit.ADD_DATE = DateTime.Now;
            MVM.newMerit.ACTIVE = true;

            MVM.merits.Add(MVM.newMerit);
            MVM.newMerit = new MERITS();

            ModelState.Clear();
            TempData["showForm"] = true;
            return View("MERIT", MVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Merit(MeritsViewModel MVM)
        {
            checkLogin();
            MVM.TermList = CP.termSelectList;
            MVM.userList = CP.teacherSelectList;
            MVM.classList = classSelectList;

            // fields checking
            if (!ModelState.IsValid)
                return View("Merit", MVM);

            //edit or create
            bool result = true;

            for (int m = 0; m < MVM.merits.Count; m++)
            {
                if (result)
                {
                    if (MVM.merits[m].ID < 1)
                    {
                        result = false;
                        result = DBS.addRecord(MVM.merits[m]);
                    }
                }
            }

            foreach (var i in MVM.inactiveList)
            {
                if (result && i > 0)
                {
                    result = false;
                    result = DBS.InactiveRecord("MERITS", i, CP.userID);
                }
            }

            ModelState.Clear();
            if (result) { TempData[Constant.msg_success] = Constant.ChangeSucceed; } else { TempData[Constant.msg_error] = Constant.ChangeFailed; }
            return viewMerits(MVM.selectedTerm, MVM.selectedClass, MVM.selectedStudent);
        }
    }
}

