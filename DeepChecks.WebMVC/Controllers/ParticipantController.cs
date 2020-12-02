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

        private ParticipantService CreateParticipantService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParticipantService(userId);
            return service;
        }
    }
}