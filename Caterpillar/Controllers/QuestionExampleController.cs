﻿using System;
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
    public class QuestionExampleController : Controller
    {
        private CaterpillarContext db = new CaterpillarContext();

        // GET: /QuestionExample/
        public ActionResult Index()
        {
            var kwlentries = db.KWLentries.Include(k => k.User);
            return View(kwlentries.ToList());
        }

        // GET: /QuestionExample/Details/5
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

        // GET: /QuestionExample/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: /QuestionExample/Create
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

        // GET: /QuestionExample/Edit/5
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

        // POST: /QuestionExample/Edit/5
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

        // GET: /QuestionExample/Delete/5
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

        // POST: /QuestionExample/Delete/5
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
