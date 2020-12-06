//using DeepChecks.Models.Note;
//using DeepChecks.Service;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace DeepChecks.WebMVC.Controllers
//{
//    [Authorize]
//    public class NoteController : Controller
//    {
//        // GET: Participant
//        public ActionResult Index()
//        {
//            var userId = Guid.Parse(User.Identity.GetUserId());
//            var service = new NoteService(userId);
//            var model = service.GetNotes();

//            return View(model);
//        }

//        //GET
//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(NoteCreate model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            var service = CreateNoteService();

//            if (service.CreateNote(model))
//            {
//                TempData["SaveResult"] = "Your note was created.";
//                return RedirectToAction("Index");
//            };

//            ModelState.AddModelError("", "Your note could not be created.");

//            return View(model);
//        }

//        public ActionResult Details(int id)
//        {
//            var svc = CreateNoteService();
//            var model = svc.GetNoteById(id);

//            return View(model);
//        }

//        public ActionResult Edit(int id)
//        {
//            var service = CreateNoteService();
//            var detail = service.GetNoteById(id);
//            var model =
//                new NoteDetail
//                {
//                    NoteId = detail.NoteId,
//                    NoteTitle = detail.NoteTitle,
//                    NoteContent = detail.NoteContent,
//                    CheckId = detail.CheckId,
//                    ParticipantId = detail.ParticipantId
//                };
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, NoteDetail model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            if (model.NoteId != id)
//            {
//                ModelState.AddModelError("", "Id Mismatch");
//                return View(model);
//            }

//            var service = CreateNoteService();

//            if (service.UpdateNote(model))
//            {
//                TempData["SaveResult"] = "Your note was successfully updated.";
//                return RedirectToAction("Index");
//            }

//            ModelState.AddModelError("", "Your note could not be updated.");
//            return View(model);
//        }

//        [ActionName("Delete")]
//        public ActionResult Delete(int id)
//        {
//            var svc = CreateNoteService();
//            var model = svc.GetNoteById(id);

//            return View(model);
//        }

//        [HttpPost]
//        [ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteANote(int id)
//        {
//            var service = CreateNoteService();

//            service.DeleteNote(id);

//            TempData["SaveResult"] = "Your note was deleted";

//            return RedirectToAction("Index");
//        }

//        private NoteService CreateNoteService()
//        {
//            var userId = Guid.Parse(User.Identity.GetUserId());
//            var service = new NoteService(userId);
//            return service;
//        }
//    }
//}