﻿using System;
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
    public class AdminCourseTopicController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /CourseTopic/
        public ActionResult Index()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            var coursetopic = db.CourseTopics.Include(c => c.Course).Include(c => c.Topic);
            return View(coursetopic.ToList());
        }

        // GET: /CourseTopic/Details/5
        public ActionResult Details(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic coursetopic = db.CourseTopics.Find(id);
            if (coursetopic == null)
            {
                return HttpNotFound();
            }
            return View(coursetopic);
        }

        // GET: /CourseTopic/Create
        public ActionResult Create()
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");
            return View();
        }

        // POST: /CourseTopic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,TopicId")] CourseTopic coursetopic)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.CourseTopics.Add(coursetopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", coursetopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", coursetopic.TopicId);
            return View(coursetopic);
        }

        // GET: /CourseTopic/Edit/5
        public ActionResult Edit(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic coursetopic = db.CourseTopics.Find(id);
            if (coursetopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", coursetopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", coursetopic.TopicId);
            return View(coursetopic);
        }

        // POST: /CourseTopic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,TopicId")] CourseTopic coursetopic)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (ModelState.IsValid)
            {
                db.Entry(coursetopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", coursetopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", coursetopic.TopicId);
            return View(coursetopic);
        }

        // GET: /CourseTopic/Delete/5
        public ActionResult Delete(int? id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic coursetopic = db.CourseTopics.Find(id);
            if (coursetopic == null)
            {
                return HttpNotFound();
            }
            return View(coursetopic);
        }

        // POST: /CourseTopic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Admin"] = admin;
            CourseTopic coursetopic = db.CourseTopics.Find(id);
            db.CourseTopics.Remove(coursetopic);
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
