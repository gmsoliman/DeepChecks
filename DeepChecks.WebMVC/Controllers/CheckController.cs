using DeepChecks.Models.Check;
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
    public class CheckController : Controller
    {
        // GET: Participant
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CheckService(userId);
            var model = service.GetCheckByRelationship(id);

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            PopulateRelationships();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CheckCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCheckService();

            if (service.CreateCheck(model))
            {
                TempData["SaveResult"] = "Your Deep Check was successfully scheduled.";
                return RedirectToAction("Index", "Check", new { id = model.RelationshipId });
            };

            ModelState.AddModelError("", "Your Deep Check could not be created.");

            return View(model);
        }

        //public ActionResult Details(int id)
        //{
        //    var svc = CreateCheckService();
        //    var model = svc.GetCheckById(id);

        //    return View(model);
        //}
        
        //public ActionResult Details(int id)
        //{
        //    var svc = CreateCheckService();
        //    var model = svc.GetCheckByRelationship(id);

        //    return View(model);
        //}

        public ActionResult Edit(int id)
        {
            var service = CreateCheckService();
            var detail = service.GetCheckById(id);
            var model =
                new CheckListItem
                {
                    CheckId = detail.CheckId,
                    CheckTitle = detail.CheckTitle,
                    CheckDate = detail.CheckDate,
                    RelationshipId = detail.RelationshipId
                };
            PopulateRelationships(detail.RelationshipId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CheckListItem model)
        {
            if (!ModelState.IsValid)
            {
                PopulateRelationships(model.RelationshipId);
                return View(model);
            }

            if (model.CheckId != id)
            {
                PopulateRelationships(model.RelationshipId);
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCheckService();

            if (service.UpdateCheck(model))
            {
                TempData["SaveResult"] = "Your Deep Check was successfully updated.";
                return RedirectToAction("Index", "Check", new { id = model.RelationshipId });
            }

            ModelState.AddModelError("", "Your Deep Check could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCheckService();
            var model = svc.GetCheckById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteACheck(int id)
        {
            var service = CreateCheckService();

            service.DeleteCheck(id);

            TempData["SaveResult"] = "Your Deep Check was deleted";

            return RedirectToAction("Index", "Relationship");
        }

        private CheckService CreateCheckService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CheckService(userId);
            return service;
        }

        private void PopulateRelationships()
        {
            ViewBag.RelationshipId = new SelectList(new RelationshipService(Guid.Parse(User.Identity.GetUserId())).GetRelationships(), "RelationshipId", "RelationshipName");
        }
        private void PopulateRelationships(int id)
        {
            ViewBag.RelationshipId = new SelectList(new RelationshipService(Guid.Parse(User.Identity.GetUserId())).GetRelationships(), "RelationshipId", "RelationshipName", id);
        }
    }
}