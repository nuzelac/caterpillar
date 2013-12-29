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
    [Authorize]
    public class CaterpillarController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /Caterpillar/
        public ActionResult Index()
        {
            var kwlentries = db.KWLentries.Include(k => k.User);
            return View(kwlentries.ToList());
            
        }

        // GET: /Caterpillar/Details/5
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

        // GET: /Caterpillar/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: /Caterpillar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserId,Type,Entry")] KWLentry kwlentry)
        {
            if (ModelState.IsValid)
            {
                db.KWLentries.Add(kwlentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            return View(kwlentry);
        }

        [HttpPost]
        public ActionResult CreateNewResponse([Bind(Include = "EntryId,Response1")] Response response)
        {

            if (ModelState.IsValid)
            {
                var trenutniUserName = User.Identity.Name.ToString();
                var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();

                response.User = student;
                
                db.Responses.Add(response);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView("_NewResponseModal", response);
        }

        [HttpPost]
        public ActionResult CreateNewEntry([Bind(Include = "Entry")] KWLentry entry)
        {

            if (ModelState.IsValid)
            {
                var trenutniUserName = User.Identity.Name.ToString();
                var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();

                entry.User = student;
                entry.UserId = student.Id;
                entry.Type = 0;

                db.KWLentries.Add(entry);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView("_NewEntryModal", entry);
        }


        // GET: /Caterpillar/Edit/5
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            return View(kwlentry);
        }

        // POST: /Caterpillar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserId,Type,Entry")] KWLentry kwlentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kwlentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
            return View(kwlentry);
        }

        // GET: /Caterpillar/Delete/5
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

        // POST: /Caterpillar/Delete/5
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
