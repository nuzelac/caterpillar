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
    [Authorize(Roles="Student")]
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

        public ActionResult K()
        {
            object obj = Session["OdabraniTopicId"];
            if (obj == null)
            {
                return RedirectToAction("Index", "MainMenuStudent");
            }

            int idTopic = (int)obj;
			var kwlentries = db.KWLentries.Include(k => k.Topic).Include(k => k.User).Where(u => u.TopicId == idTopic && u.Type == 1);

			return View(kwlentries.ToList());
        }

        public ActionResult W()
        {
            object obj = Session["OdabraniTopicId"];
            if (obj == null)
            {
                return RedirectToAction("Index", "MainMenuStudent");
            }

            int idTopic = (int)obj;
            var kwlentries = db.KWLentries.Include(k => k.Topic).Include(k => k.User).Where(u => u.TopicId == idTopic && u.Type == 0);

            return View(kwlentries.ToList());
        }

        public ActionResult L()
        {
            object obj = Session["OdabraniTopicId"];
            if (obj == null)
            {
                return RedirectToAction("Index", "MainMenuStudent");
            }

            int idTopic = (int)obj;
            var responses = db.Responses.Include(k => k.KWLentry.Topic).Include(k => k.User).Where(u => u.KWLentry.TopicId == idTopic && u.KWLentry.Type == 0);

            return View(responses.ToList());
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
        public ActionResult CreateNewEntry(string Entry)
        {

            if (ModelState.IsValid)
            {
                var entry = new KWLentry();
                var trenutniUserName = User.Identity.Name.ToString();
                var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();

                entry.Entry = Entry;
                entry.TopicId = (int)Session["OdabraniTopicId"];
                entry.User = student;
                entry.UserId = student.Id;
                entry.Type = 0;

                db.KWLentries.Add(entry);
                db.SaveChanges();
                return Json(new { success = true, id = entry.Id, entry = entry.Entry });
            }

            return PartialView("_NewEntryModal", Entry);
        }

        [HttpPost]
        public ActionResult CreateNewKEntry(string Entry)
        {

            if (ModelState.IsValid)
            {
                var entry = new KWLentry();
                var trenutniUserName = User.Identity.Name.ToString();
                var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();

                entry.Entry = Entry;
                entry.TopicId = (int)Session["OdabraniTopicId"];
                entry.User = student;
                entry.UserId = student.Id;
                entry.Type = 1;

                db.KWLentries.Add(entry);
                db.SaveChanges();
                return Json(new { success = true, id = entry.Id, entry = entry.Entry });
            }

            return PartialView("_NewKEntryModal", Entry);
        }

        // POST: /Caterpillar/Delete/5
        [HttpPost]
        public ActionResult DeleteKEntry(int KEntryId)
        {
            var trenutniUserName = User.Identity.Name.ToString();
            var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();

            KWLentry kwlentry = db.KWLentries.Find(KEntryId);
            if (kwlentry != null && kwlentry.User == student)
            {
                db.KWLentries.Remove(kwlentry);
                db.SaveChanges();
                return Json(new { success = true, id = kwlentry.Id });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        // POST: /Caterpillar/Delete/5
        [HttpPost]
        public ActionResult DeleteResponse(int ResponseId)
        {
            var trenutniUserName = User.Identity.Name.ToString();
            var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();

            Response response = db.Responses.Find(ResponseId);
            if (response != null && response.User == student && response.Correction != 1)
            {
                db.Responses.Remove(response);
                db.SaveChanges();
                return Json(new { success = true, id = response.Id });
            }
            else
            {
                return Json(new { success = false });
            }
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
