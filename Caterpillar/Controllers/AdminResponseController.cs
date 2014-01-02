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
    public class AdminResponseController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();
        

        // GET: /AdminResponse/
        public ActionResult Index()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            var responses = db.Responses.Include(r => r.KWLentry).Include(r => r.User);
            return View(responses.ToList());
        }

        // GET: /AdminResponse/Details/5
        public ActionResult Details(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // GET: /AdminResponse/Create
        public ActionResult Create()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            ViewBag.EntryId = new SelectList(db.KWLentries, "Id", "Entry");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: /AdminResponse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EntryId,UserId,Response1,Correction,Points")] Response response)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntryId = new SelectList(db.KWLentries, "Id", "Entry", response.EntryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", response.UserId);
            return View(response);
        }

        // GET: /AdminResponse/Edit/5
        public ActionResult Edit(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntryId = new SelectList(db.KWLentries, "Id", "Entry", response.EntryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", response.UserId);
            return View(response);
        }

        // POST: /AdminResponse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,EntryId,UserId,Response1,Correction,Points")] Response response)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntryId = new SelectList(db.KWLentries, "Id", "Entry", response.EntryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", response.UserId);
            return View(response);
        }

        // GET: /AdminResponse/Delete/5
        public ActionResult Delete(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: /AdminResponse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            Response response = db.Responses.Find(id);
            db.Responses.Remove(response);
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
