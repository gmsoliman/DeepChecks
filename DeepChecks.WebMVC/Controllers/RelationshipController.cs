using DeepChecks.Models.Relationship;
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
    public class RelationshipController : Controller
    {
        // GET: Relationship
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RelationshipService(userId);
            var model = service.GetRelationships();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RelationshipCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRelationshipService();

            if (service.CreateRelationship(model))
            {
                TempData["SaveResult"] = "Your relationship was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Relationship could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRelationshipService();
            var model = svc.GetRelationshipById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRelationshipService();
            var detail = service.GetRelationshipById(id);
            var model =
                new RelationshipListItem
                {
                    RelationshipId = detail.RelationshipId,
                    RelationshipName = detail.RelationshipName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RelationshipListItem model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RelationshipId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRelationshipService();

            if (service.UpdateRelationship(model))
            {
                TempData["SaveResult"] = "Your relationship name has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your relationship name could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRelationshipService();
            var model = svc.GetRelationshipById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteARelationship(int id)
        {
            var service = CreateRelationshipService();

            service.DeleteRelationship(id);

            TempData["SaveResult"] = "Your relationship was deleted";

            return RedirectToAction("Index");
        }

        private RelationshipService CreateRelationshipService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RelationshipService(userId);
            return service;
        }
    }
}