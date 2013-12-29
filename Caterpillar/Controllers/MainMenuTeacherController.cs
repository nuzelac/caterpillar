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

		public class UserTopicViewModel
		{
			public Topic Topic { get; set; }
		}

		public class USERSClassCourseViewModel
		{
			public User User { get; set; }

		}

		public class TOPICSClassCourseViewModel
		{
			public Topic Topic { get; set; }
		}

		public ActionResult Index()
		{

			var currentTeacher = UserManager.FindByIdAsync(User.Identity.GetUserId());
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
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
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			ViewData["Teacher"] = teacher;

			if (idClass == null || idCourse == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//    var students = db.UserClassCourse.Find(idClass, idCourse);
			var usersOfClassCourse = from a in db.UserClassCourses
									 where a.CourseId == idCourse && a.ClassId == idClass
									 select new USERSClassCourseViewModel
									 {
										 User = a.User
									 };

			//     var usersOfClassCourse = db.UserClassCourse.Select( s => s.ClassId== idClass);
			ViewData["Users"] = usersOfClassCourse.ToList();
			if (usersOfClassCourse == null)
			{
				return HttpNotFound();
			}
			return View(usersOfClassCourse);

		}

		public ActionResult ViewTopics(int? idClass, int? idCourse)
		{
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
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

			//     var usersOfClassCourse = db.UserClassCourse.Select( s => s.ClassId== idClass);
			ViewData["Topics"] = topicsOfClassCourse.Distinct().ToList();
			if (topicsOfClassCourse == null)
			{
				return HttpNotFound();
			}
			var primljeniClassId = idClass;
			var primljeniCourseId = idCourse;
			Session["PrimljeniClassId"] = primljeniClassId;
			Session["PrimljeniCourseId"] = primljeniCourseId;
			return View(topicsOfClassCourse);

		}

		// GET: /Topic/Edit/5
		public ActionResult Edit(int? id)
		{
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
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
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
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
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
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
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			ViewData["Teacher"] = teacher;

			Topic topic = db.Topics.Find(id);
			db.Topics.Remove(topic);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// GET: /Topic/Create
		public ActionResult CreateNewTopic(int? idClass, int? idCourse)
		{
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			ViewData["Teacher"] = teacher;

			return View();
		}

		// POST: /Topic/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateNewTopic([Bind(Include = "Id,Name")] Topic topic, [Bind(Include = "Id,CourseId,TopicId")] CourseTopic coursetopic, [Bind(Include = "Id,ClassId,TopicId")] ClassTopic classtopic)
		{
			var trenutniUserName = (string)Session["PrimljeniUser-UserName"];
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			ViewData["Teacher"] = teacher;

			if (ModelState.IsValid)
			{

				db.Topics.Add(topic);
				db.SaveChanges();
				coursetopic.Topic = topic;
				coursetopic.CourseId = (int)Session["PrimljeniCourseId"];
				db.CourseTopics.Add(coursetopic);
				db.SaveChanges();
				classtopic.Topic = topic;
				classtopic.ClassId = (int)Session["PrimljeniClassId"];
				db.ClassTopics.Add(classtopic);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(topic);
		}




	}
}

