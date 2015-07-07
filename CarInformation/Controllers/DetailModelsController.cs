using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInformation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarInformation.Controllers
{
    [Authorize]
    public class DetailModelsController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;
        public DetailModelsController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: DetailModels
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(db.Details.ToList().Where(detailmodels => detailmodels.User.Id == currentUser.Id));
        }

        // GET: DetailModels/Details/5
        public ActionResult Details(int? id)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailModels detailModels = db.Details.Find(id);
            if (detailModels == null)
            {
                return HttpNotFound();
            }
            if (detailModels.User.Id != currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(detailModels);
        }

        // GET: DetailModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetailModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand,Model,Year")] DetailModels detailModels)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                detailModels.User = currentUser;
                db.Details.Add(detailModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detailModels);
        }

        // GET: DetailModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailModels detailModels = db.Details.Find(id);
            if (detailModels == null)
            {
                return HttpNotFound();
            }
            return View(detailModels);
        }

        // POST: DetailModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,Year")] DetailModels detailModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detailModels);
        }

        // GET: DetailModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailModels detailModels = db.Details.Find(id);
            if (detailModels == null)
            {
                return HttpNotFound();
            }
            return View(detailModels);
        }

        // POST: DetailModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailModels detailModels = db.Details.Find(id);
            db.Details.Remove(detailModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
