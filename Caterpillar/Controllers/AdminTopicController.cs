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
    public class AdminTopicController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /Topic/
        public ActionResult Index()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            return View(db.Topics.ToList());
        }

        // GET: /Topic/Details/5
        public ActionResult Details(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: /Topic/Create
        public ActionResult Create()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            return View();
        }

        // POST: /Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Topic topic)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: /Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: /Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Topic topic)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: /Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: /Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
