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
    public class TopicController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /Topic/
        public ActionResult Index()
        {
            return View(db.Topics.ToList());
        }

        // GET: /Topic/Details/5
        public ActionResult Details(int? id)
        {
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
            ViewBag.Id = new SelectList(db.Courses, "Id", "Name");
            //ViewBag.Class = new SelectList(db.Classes, "Id", "Name");
            return View();
        }

        // POST: /Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                //var pom = ViewBag.Class;
                var coursetopic = new CourseTopic();
                coursetopic.CourseId = topic.Id;
                topic.Id = 0;
                var vrijeme = DateTime.Now;
                topic.StartDate = vrijeme;
                db.Topics.Add(topic);
                db.SaveChanges();
                coursetopic.TopicId = db.Topics.Where(m => m.StartDate == vrijeme).FirstOrDefault().Id;
                db.CourseTopics.Add(coursetopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Courses, "Id", "Name", topic.Id);

            return View(topic);
        }

        // GET: /Topic/Edit/5
        public ActionResult Edit(int? id)
        {
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
        public ActionResult Edit([Bind(Include="Id,Name")] Topic topic)
        {
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
