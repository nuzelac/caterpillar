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
	[Authorize(Roles = "Teacher")]
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
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
			//ViewData["Teacher"] = teacher;
			if (Session["Teacher"] == null)
			{
				Session["Teacher"] = teacher;
			}


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
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;

			if (idClass == null || idCourse == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//    var students = db.UserClassCourse.Find(idClass, idCourse);
			var usersOfClassCourse = from a in db.UserClassCourses
									 where a.CourseId == idCourse && a.ClassId == idClass && a.User.Role.Name == "Student"
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
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;
			ViewData["Class"] = idClass;
			ViewData["Course"] = idCourse;

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
			ViewData["PrimljeniClassId"] = primljeniClassId;
			ViewData["PrimljeniCourseId"] = primljeniCourseId;
			return View(topicsOfClassCourse);

		}

		// GET: /Topic/Edit/5
		public ActionResult EditTopic(int? id, int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
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
		public ActionResult EditTopic([Bind(Include = "Id,Name,CourseId")] Topic topic, int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;

			if (ModelState.IsValid)
			{
				db.Entry(topic).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("ViewTopics", new { idClass = (int)idClass, idCourse = (int)idCourse });
			}
			return View(topic);
		}

		// GET: /Topic/Delete/5
		public ActionResult DeleteTopic(int? id, int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;

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
		[HttpPost, ActionName("DeleteTopic")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteTopicConfirmed(int id, int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;

			Topic topic = db.Topics.Find(id);
			List<CourseTopic> coursetopics = db.CourseTopics.Where(u => u.TopicId == id).ToList();
			foreach(var ct in coursetopics){
				db.CourseTopics.Remove(ct);
			}
			List<ClassTopic> classtopics = db.ClassTopics.Where(u => u.TopicId == id).ToList();
			foreach (var ct in classtopics)
			{
				db.ClassTopics.Remove(ct);
			}
			db.Topics.Remove(topic);
			db.SaveChanges();
			return RedirectToAction("ViewTopics", new { idClass = (int)idClass, idCourse = (int)idCourse });
		}

		// GET: /Topic/Create
		public ActionResult CreateNewTopic(int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;
			ViewData["Class"] = idClass;
			ViewData["Course"] = idCourse;

			return View();
		}

		// POST: /Topic/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateNewTopic([Bind(Include = "Id,Name")] Topic topic, int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;

			if (ModelState.IsValid)
			{
				var classtopic = new ClassTopic();
				var coursetopic = new CourseTopic();
				classtopic.ClassId = (int)idClass;
				coursetopic.CourseId = (int)idCourse;
				var vrijeme = DateTime.Now;
				topic.StartDate = vrijeme;
				db.Topics.Add(topic);
				db.SaveChanges();
				topic.Id = db.Topics.Where(m => m.StartDate == vrijeme).FirstOrDefault().Id;
				classtopic.TopicId = topic.Id;
				coursetopic.TopicId = topic.Id;
				db.ClassTopics.Add(classtopic);
				db.CourseTopics.Add(coursetopic);
				db.SaveChanges();


				return RedirectToAction("ViewTopics", new { idClass = (int)idClass, idCourse = (int)idCourse});
			}

			return View(topic);
		}

		// GET: /Topic/Create
		public ActionResult AddExistingTopic(int? idClass, int? idCourse)
		{
			var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			ViewBag.Id = new SelectList(db.Topics, "Id", "Name");
			Session["Teacher"] = teacher;
			ViewData["Class"] = idClass;
			ViewData["Course"] = idCourse;

			return View();
		}

		// POST: /Topic/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddExistingTopic([Bind(Include = "Id,Name")] Topic topic, int? idClass, int? idCourse)
		{
			/*var trenutniUserName = User.Identity.Name;
			var teacher = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Teacher"] = teacher;*/

			if (ModelState.IsValid)
			{
				var classtopic = new ClassTopic();
				var coursetopic = new CourseTopic();
				classtopic.ClassId = (int)idClass;
				classtopic.TopicId = (int)topic.Id;
				coursetopic.CourseId = (int)idCourse;
				coursetopic.TopicId = (int)topic.Id;
				if (db.ClassTopics.Where(u => u.ClassId == classtopic.ClassId && u.TopicId == classtopic.TopicId).FirstOrDefault() == null)
					db.ClassTopics.Add(classtopic);
				if (db.CourseTopics.Where(u => u.CourseId == coursetopic.CourseId && u.TopicId == coursetopic.TopicId).FirstOrDefault() == null)
					db.CourseTopics.Add(coursetopic);
				db.SaveChanges();


				return RedirectToAction("ViewTopics", new { idClass = (int)idClass, idCourse = (int)idCourse });
			}

			return View(topic);
		}

		public ActionResult ViewEntries(int? idClass, int? idCourse, int? idTopic)
		{
			var kwlentries = db.KWLentries.Include(k => k.Topic).Include(k => k.User).Where(u => u.TopicId == idTopic);
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			return View(kwlentries.ToList());
		}

		// GET: /Entry/Create
		public ActionResult CreateNewEntry(int? idClass, int? idCourse, int? idTopic)
		{
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			return View();
		}

		// POST: /Entry/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateNewEntry([Bind(Include = "Type, Entry")] KWLentry kwlentry, int? idClass, int? idCourse, int? idTopic)
		{
			if (ModelState.IsValid)
			{
				kwlentry.UserId = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;
				kwlentry.TopicId = (int)idTopic;
				db.KWLentries.Add(kwlentry);
				db.SaveChanges();
				return RedirectToAction("ViewEntries", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
			}
			
			return View(kwlentry);
		}

		// GET: /Entry/Edit/5
		public ActionResult EditEntry(int? id, int? idClass, int? idCourse, int? idTopic)
		{
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			KWLentry kwlentry = db.KWLentries.Find(id);
			if (kwlentry == null)
			{
				return HttpNotFound();
			}
			ViewData["PrimljeniTopicId"] = idTopic;
			return View(kwlentry);
		}

		// POST: /Entry/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditEntry([Bind(Include = "Id,UserId,Type,Entry,TopicId")] KWLentry kwlentry, int? idClass, int? idCourse, int? idTopic)
		{
			if (ModelState.IsValid)
			{
				db.Entry(kwlentry).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("ViewEntries", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
			}
			ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", kwlentry.TopicId);
			ViewBag.UserId = new SelectList(db.Users, "Id", "Name", kwlentry.UserId);
			return View(kwlentry);
		}

		// GET: /Entry/Delete/5
		public ActionResult DeleteEntry(int? id, int? idClass, int? idCourse, int? idTopic)
		{
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
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

		// POST: /Entry/Delete/5
		[HttpPost, ActionName("DeleteEntry")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteEntryConfirmed(int id, int? idClass, int? idCourse, int? idTopic)
		{
			KWLentry kwlentry = db.KWLentries.Find(id);
			List<Response> responses = db.Responses.Where(u => u.EntryId == id).ToList();
			foreach (var response in responses)
			{
				db.Responses.Remove(response);
			}
			db.KWLentries.Remove(kwlentry);
			db.SaveChanges();
			return RedirectToAction("ViewEntries", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
		}

		public ActionResult ViewResponses(int? idClass, int? idCourse, int? idTopic)
		{
			var responses = db.Responses.Include(r => r.KWLentry).Include(r => r.User).Where(u => u.KWLentry.TopicId == idTopic);
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			return View(responses.ToList());
		}

		public ActionResult EditResponse(int? id, int? idClass, int? idCourse, int? idTopic)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Response response = db.Responses.Find(id);
			if (response == null)
			{
				return HttpNotFound();
			}
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			ViewBag.Entry = db.KWLentries.Where(u => u.Id == response.EntryId).FirstOrDefault().Entry;
			return View(response);
		}

		// POST: /Default1/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditResponse([Bind(Include = "Id,EntryId,UserId,Response1,Correction,Points")] Response response, int? idClass, int? idCourse, int? idTopic)
		{
			if (ModelState.IsValid)
			{
				db.Entry(response).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("ViewResponses", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
			}
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			ViewBag.Entry = db.KWLentries.Where(u => u.Id == response.EntryId).FirstOrDefault().Entry;
			return View(response);
		}

		// GET: /Default1/Delete/5
		public ActionResult DeleteResponse(int? id, int? idClass, int? idCourse, int? idTopic)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Response response = db.Responses.Find(id);
			if (response == null)
			{
				return HttpNotFound();
			}
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			return View(response);
		}

		// POST: /Default1/Delete/5
		[HttpPost, ActionName("DeleteResponse")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteResponseConfirmed(int id, int? idClass, int? idCourse, int? idTopic)
		{
			Response response = db.Responses.Find(id);
			db.Responses.Remove(response);
			db.SaveChanges();
			return RedirectToAction("ViewResponses", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
		}

		public ActionResult EvaluateResponse(int? id, int? idClass, int? idCourse, int? idTopic)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Response response = db.Responses.Find(id);
			if (response == null)
			{
				return HttpNotFound();
			}
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			ViewBag.Entry = db.KWLentries.Where(u => u.Id == response.EntryId).FirstOrDefault().Entry;
			return View(response);
		}

		// POST: /Default1/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EvaluateResponse([Bind(Include = "Id,EntryId,UserId,Response1,Correction,Points")] Response response, int? idClass, int? idCourse, int? idTopic)
		{
			if (ModelState.IsValid)
			{
				response.Correction = 1;
				db.Entry(response).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("ViewResponses", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
			}
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			ViewBag.Entry = db.KWLentries.Where(u => u.Id == response.EntryId).FirstOrDefault().Entry;
			return View(response);
		}

		// GET: /Default1/Create
		public ActionResult AddResponse(int? id, int? idClass, int? idCourse, int? idTopic)
		{
			var response = new Response();
			response.EntryId = (int)id;
			response.UserId = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			return View(response);
		}

		// POST: /Default1/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddResponse([Bind(Include = "Id,EntryId,UserId,Response1,Correction,Points")] Response response, int? idClass, int? idCourse, int? idTopic)
		{
			if (ModelState.IsValid)
			{
				db.Responses.Add(response);
				db.SaveChanges();
				return RedirectToAction("ViewEntries", new { idClass = (int)idClass, idCourse = (int)idCourse, idTopic = (int)idTopic });
			}
			ViewData["PrimljeniClassId"] = idClass;
			ViewData["PrimljeniCourseId"] = idCourse;
			ViewData["PrimljeniTopicId"] = idTopic;
			return View(response);
		}
	}
}

