using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caterpillar.Models;

namespace Gusjenica.Controllers
{
    public class AdminKWLentryController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /KWLentry/
        public ActionResult Index()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            var kwlentry = db.KWLentries.Include(k => k.User).OrderByDescending(u => u.Type).ThenBy(u => u.Topic.Name).ThenBy(u => u.User.Surname);
            return View(kwlentry.ToList());
        }

        // GET: /KWLentry/Details/5
        public ActionResult Details(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KWLentry kwlentry = db.KWLentries.Find(id);
            if (kwlentry == null)
            {
                return HttpNotFound();
            }
            return View(kwlentry);
        }

        // GET: /KWLentry/Create
        public ActionResult Create()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");
            return View();
        }

        // POST: /KWLentry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Type,Entry")] KWLentry kwlentry)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.KWLentries.Add(kwlentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
            return View(kwlentry);
        }

        // GET: /KWLentry/Edit/5
        public ActionResult Edit(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KWLentry kwlentry = db.KWLentries.Find(id);
            if (kwlentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
            return View(kwlentry);
        }

        // POST: /KWLentry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Type,Entry")] KWLentry kwlentry)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Entry(kwlentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
            return View(kwlentry);
        }

        // GET: /KWLentry/Delete/5
        public ActionResult Delete(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KWLentry kwlentry = db.KWLentries.Find(id);
            if (kwlentry == null)
            {
                return HttpNotFound();
            }
            return View(kwlentry);
        }

        // POST: /KWLentry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            KWLentry kwlentry = db.KWLentries.Find(id);
            db.KWLentries.Remove(kwlentry);
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
