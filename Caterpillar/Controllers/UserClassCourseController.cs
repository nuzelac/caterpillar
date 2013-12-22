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
    public class UserClassCourseController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /UserClassCourse/
        public ActionResult Index()
        {
            var userclasscourses = db.UserClassCourses.Include(u => u.Class).Include(u => u.Course).Include(u => u.User);
            return View(userclasscourses.ToList());
        }

        // GET: /UserClassCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClassCourse userclasscourse = db.UserClassCourses.Find(id);
            if (userclasscourse == null)
            {
                return HttpNotFound();
            }
            return View(userclasscourse);
        }

        // GET: /UserClassCourse/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: /UserClassCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserId,ClassId,CourseId")] UserClassCourse userclasscourse)
        {
            if (ModelState.IsValid)
            {
                db.UserClassCourses.Add(userclasscourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", userclasscourse.ClassId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", userclasscourse.CourseId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userclasscourse.UserId);
            return View(userclasscourse);
        }

        // GET: /UserClassCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClassCourse userclasscourse = db.UserClassCourses.Find(id);
            if (userclasscourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", userclasscourse.ClassId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", userclasscourse.CourseId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userclasscourse.UserId);
            return View(userclasscourse);
        }

        // POST: /UserClassCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserId,ClassId,CourseId")] UserClassCourse userclasscourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userclasscourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", userclasscourse.ClassId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", userclasscourse.CourseId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userclasscourse.UserId);
            return View(userclasscourse);
        }

        // GET: /UserClassCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClassCourse userclasscourse = db.UserClassCourses.Find(id);
            if (userclasscourse == null)
            {
                return HttpNotFound();
            }
            return View(userclasscourse);
        }

        // POST: /UserClassCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserClassCourse userclasscourse = db.UserClassCourses.Find(id);
            db.UserClassCourses.Remove(userclasscourse);
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
