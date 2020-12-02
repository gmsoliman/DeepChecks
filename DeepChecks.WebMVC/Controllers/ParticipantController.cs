using DeepChecks.Models.Participant;
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
    public class ParticipantController : Controller
    {
        // GET: Participant
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParticipantService(userId);
            var model = service.GetParticipants();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParticipantCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateParticipantService();

            if (service.CreateParticipant(model))
            {
                TempData["SaveResult"] = "The participant was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Participant could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateParticipantService();
            var model = svc.GetParticipantById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateParticipantService();
            var detail = service.GetParticipantById(id);
            var model =
                new ParticipantDetail
                {
                    ParticipantId = detail.ParticipantId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Email = detail.Email
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParticipantDetail model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.ParticipantId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateParticipantService();

            if (service.UpdateParticipant(model))
            {
                TempData["SaveResult"] = "The participant was successfully updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The participant could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateParticipantService();
            var model = svc.GetParticipantById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAParticipant(int id)
        {
            var service = CreateParticipantService();

            service.DeleteParticipant(id);

            TempData["SaveResult"] = "The participant was deleted";

            return RedirectToAction("Index");
        }

        private ParticipantService CreateParticipantService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParticipantService(userId);
            return service;
        }
    }
}