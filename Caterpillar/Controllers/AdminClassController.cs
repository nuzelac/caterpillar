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
    public class AdminClassController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /Class/
        public ActionResult Index()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            var StudentName = Session["PrimljeniUser-Name"];
            var StudentSurname = Session["PrimljeniUser-Surname"];
            return View(db.Classes.ToList());
        }

        // GET: /Class/Details/5
        public ActionResult Details(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: /Class/Create
        public ActionResult Create()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            return View();
        }

        // POST: /Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Year")] Class @class)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@class);
        }

        // GET: /Class/Edit/5
        public ActionResult Edit(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: /Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Year")] Class @class)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // GET: /Class/Delete/5
        public ActionResult Delete(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: /Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
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
