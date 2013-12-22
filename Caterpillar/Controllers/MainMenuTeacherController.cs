using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Caterpillar.Models;
using Microsoft.AspNet.Identity;

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;


namespace Caterpillar.Controllers
{
    public class MainMenuTeacherController : Controller
    {
        public MainMenuTeacherController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public MainMenuTeacherController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        private CaterpillarContext db = new CaterpillarContext();
        //
        // GET: /MainMenuStudent/


        public UserManager<ApplicationUser> UserManager { get; private set; }

        public ActionResult Index()
        {
           
            var currentTeacher = UserManager.FindByIdAsync(User.Identity.GetUserId());
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();          
            ViewData["Teacher"] = teacher;


            //var kwlentries = student.KWLentries;
            //ViewData["KWLentries"] = kwlentries.ToList();

            var subjects = teacher.UserClassCourses;
            ViewData["Subjects"] = subjects.ToList();



            var topics = from a in subjects.ToList()
                         join b in db.CourseTopics
                         on a.CourseId equals b.CourseId
                         select new UserTopicViewModel
                         {
                             Topic = b.Topic

                         };

            ViewData["TopicsTOTAL"] = topics.ToList();


            var teacherclasscourses = teacher.UserClassCourses;
            ViewData["TeacherClassCourses"] = teacherclasscourses.ToList();

            return View();
        }

        public ActionResult ViewStudents(int? idClass, int? idCourse)
        {
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

            if (idClass == null || idCourse==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var students = db.UserClassCourses.Find(idClass, idCourse);
            var usersOfClassCourse = from a in db.UserClassCourses
                           where a.CourseId == idCourse && a.ClassId == idClass
                           select new USERSClassCourseViewModel
                           {
                               User = a.User
                           };
 
       //     var usersOfClassCourse = db.UserClassCourses.Select( s => s.ClassId== idClass);
            ViewData["Users"] = usersOfClassCourse.ToList();
            if (usersOfClassCourse == null)
            {
                return HttpNotFound();
            }
            return View(usersOfClassCourse );
            
        }

        public ActionResult ViewTopics(int? idClass, int? idCourse)
        {
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

            if (idClass == null || idCourse == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var topicsOfClassCourse = from a in db.UserClassCourses
                                      join b in db.CourseTopics
                                      on a.CourseId equals b.CourseId
                                     where a.CourseId == idCourse && a.ClassId == idClass
                                     
                                     select new TOPICSClassCourseViewModel
                                     {
                                         Topic = b.Topic
                                     };

            //     var usersOfClassCourse = db.UserClassCourses.Select( s => s.ClassId== idClass);
            ViewData["Topics"] = topicsOfClassCourse.Distinct().ToList();
            if (topicsOfClassCourse == null)
            {
                return HttpNotFound();
            }
            var primljeniClassId = idClass;
            var primljeniCourseId = idCourse;
            ViewData["PrimljeniClassId"] = primljeniClassId;
            ViewData["PrimljeniCourseId"] = primljeniCourseId;
            return View(topicsOfClassCourse);

        }

        // GET: /Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

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
        public ActionResult Edit([Bind(Include = "Id,Name,CourseId")] Topic topic)
        {
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

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
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

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
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Topic/Create
        public ActionResult CreateNewTopic(int? idClass, int? idCourse)
        {
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");

            return View();
        }

        // POST: /Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewTopic(int? idCourse, int? idClass, int? idTopic, [Bind(Include = "Id,Name,CourseId")] Topic topic, [Bind(Include = "Id,CourseId,TopicId")] CourseTopic coursetopic)
        {
            var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewData["Teacher"] = teacher;

            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                db.CourseTopics.Add(coursetopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = idCourse;
            ViewBag.TopicId = idTopic;
            return View(topic);
        }
        



    }
}

