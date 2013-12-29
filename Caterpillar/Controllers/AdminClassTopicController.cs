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
    public class AdminClassTopicController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /ClassTopic/
        public ActionResult Index()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            var classtopic = db.ClassTopics.Include(c => c.Class).Include(c => c.Topic);
            return View(classtopic.ToList());
        }

        // GET: /ClassTopic/Details/5
        public ActionResult Details(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTopic classtopic = db.ClassTopics.Find(id);
            if (classtopic == null)
            {
                return HttpNotFound();
            }
            return View(classtopic);
        }

        // GET: /ClassTopic/Create
        public ActionResult Create()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");
            return View();
        }

        // POST: /ClassTopic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClassId,TopicId")] ClassTopic classtopic)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.ClassTopics.Add(classtopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", classtopic.ClassId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", classtopic.TopicId);
            return View(classtopic);
        }

        // GET: /ClassTopic/Edit/5
        public ActionResult Edit(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTopic classtopic = db.ClassTopics.Find(id);
            if (classtopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", classtopic.ClassId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", classtopic.TopicId);
            return View(classtopic);
        }

        // POST: /ClassTopic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClassId,TopicId")] ClassTopic classtopic)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Entry(classtopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", classtopic.ClassId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", classtopic.TopicId);
            return View(classtopic);
        }

        // GET: /ClassTopic/Delete/5
        public ActionResult Delete(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTopic classtopic = db.ClassTopics.Find(id);
            if (classtopic == null)
            {
                return HttpNotFound();
            }
            return View(classtopic);
        }

        // POST: /ClassTopic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            ClassTopic classtopic = db.ClassTopics.Find(id);
            db.ClassTopics.Remove(classtopic);
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
