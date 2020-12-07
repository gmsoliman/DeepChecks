using DeepChecks.Data;
using DeepChecks.Models.Entry;
using DeepChecks.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeepChecks.WebMVC.Controllers
{
    [Authorize]
    public class EntryController : Controller
    {
        // GET: Participant
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EntryService(userId);
            var model = service.GetEntriesByParticipant(id);

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            PopulateParticipants();
            PopulateCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EntryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateEntryService();

            if (service.CreateEntry(model))
            {
                TempData["SaveResult"] = "Entry was created.";
                return RedirectToAction("Index", "Entry", new { id = model.ParticipantId });
            };

            ModelState.AddModelError("", "Entry could not be created.");

            return View(model);
        }

        //public ActionResult Details(int id)
        //{
        //    var svc = CreateEntryService();
        //    var model = svc.GetEntryById(id);

        //    return View(model);
        //}

        public ActionResult Edit(int id)
        {
            var service = CreateEntryService();
            var detail = service.GetEntryById(id);
            var model =
                new EntryListItem
                {
                    EntryId = detail.EntryId,
                    EntryContent = detail.EntryContent,
                    CategoryId = detail.CategoryId,
                    ParticipantId = detail.ParticipantId
                };
            PopulateCategories(detail.CategoryId);
            PopulateParticipants(detail.ParticipantId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EntryListItem model)
        {
            if (!ModelState.IsValid)
            {
                PopulateCategories(model.CategoryId);
                PopulateParticipants(model.ParticipantId);
                return View(model);
            }

            if (model.EntryId != id)
            {
                PopulateCategories(model.CategoryId);
                PopulateParticipants(model.ParticipantId);
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateEntryService();

            if (service.UpdateEntry(model))
            {
                TempData["SaveResult"] = "Your entry was successfully updated.";
                return RedirectToAction("Index", "Entry", new { id = model.ParticipantId });
            }

            ModelState.AddModelError("", "Your entry could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateEntryService();
            var model = svc.GetEntryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnEntry(int id)
        {
            var service = CreateEntryService();

            service.DeleteEntry(id);

            TempData["SaveResult"] = "Your entry was deleted";

            return RedirectToAction("Index", "Relationship");
        }

        private EntryService CreateEntryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EntryService(userId);
            return service;
        }

        private void PopulateCategories()
        {
            ViewBag.CategoryId = new SelectList(new CategoryService(Guid.Parse(User.Identity.GetUserId())).GetCategories(), "CategoryId", "CategoryTitle");
        }
        private void PopulateCategories(int categoryId)
        {
            ViewBag.CategoryId = new SelectList(new CategoryService(Guid.Parse(User.Identity.GetUserId())).GetCategories(), "CategoryId", "CategoryTitle", categoryId);
        }

        private void PopulateParticipants()
        {
            ViewBag.ParticipantId = new SelectList(new ParticipantService(Guid.Parse(User.Identity.GetUserId())).GetParticipants(), "ParticipantId", "FirstName", "LastName");
        }
        private void PopulateParticipants(int participantId)
        {
            ViewBag.ParticipantId = new SelectList(new ParticipantService(Guid.Parse(User.Identity.GetUserId())).GetParticipants(), "ParticipantId", "FirstName", "LastName", participantId);
        }
    }
}