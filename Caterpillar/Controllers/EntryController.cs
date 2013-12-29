using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caterpillar.Models;

namespace Caterpillar.Controllers
{
    public class EntryController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /Entry/
        public ActionResult Index()
        {
            var kwlentries = db.KWLentries.Include(k => k.Topic).Include(k => k.User);
            return View(kwlentries.ToList());
        }

        // GET: /Entry/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: /Entry/Create
        public ActionResult Create()
        {
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: /Entry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserId,Type,Entry,TopicId")] KWLentry kwlentry)
        {
            if (ModelState.IsValid)
            {
                db.KWLentries.Add(kwlentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            return View(kwlentry);
        }

        // GET: /Entry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KWLentry kwlentry = db.KWLentries.Find(id);
            if (kwlentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            return View(kwlentry);
        }

        // POST: /Entry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserId,Type,Entry,TopicId")] KWLentry kwlentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kwlentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            return View(kwlentry);
        }

        // GET: /Entry/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: /Entry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
