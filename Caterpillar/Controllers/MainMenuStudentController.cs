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
	[Authorize(Roles = "Student")]
	public class MainMenuStudentController : Controller
	{
		public MainMenuStudentController()
			: this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
		{
		}

		public MainMenuStudentController(UserManager<ApplicationUser> userManager)
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

        public class TOPICSClassCourseViewModel
        {
            public Topic Topic { get; set; }
        }

		public ActionResult Index()
		{
			//   var currentUMUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			//   var currentUser = db.User.Find(currentUMUser.UserID);



			var currentstudent = UserManager.FindByIdAsync(User.Identity.GetUserId());
			// Task<System.Web.Mvc.ActionResult> currentstudent = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			//if (User.Identity==null||User.Identity.Name=="")
			var trenutniUserName = User.Identity.Name;
			var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
			Session["Student"] = student;
			var studentClassCourses = db.UserClassCourses.Where(u => u.UserId == student.Id).ToList();
			var studentClass = studentClassCourses[0].Class;
			Session["Class"] = studentClass;
			List<Course> studentCourses = new List<Course>();
            List<int> studentCourseId=new List<int>();
			foreach (var elem in studentClassCourses)
			{
				studentCourses.Add(elem.Course);
                studentCourseId.Add(elem.Course.Id);
			}
            


			//var kwlentries = student.KWLentries;
			//ViewData["KWLentries"] = kwlentries.ToList();

			var subjects = student.UserClassCourses;
			ViewData["Subjects"] = subjects.ToList();
			/*
			var topics = from a in subjects.ToList()
						 join b in db.CourseTopics
						 on a.CourseId equals b.CourseId
						 select new UserTopicViewModel
						 {
							 Topic = b.Topic
						 };*/
            var foundtopics = from a in db.ClassTopics
                         join b in db.CourseTopics
                         on a.TopicId equals b.TopicId
                         where a.ClassId == studentClass.Id && studentCourseId.Contains(b.CourseId)
                         select new UserTopicViewModel
                         {
                            Topic = b.Topic
                         };
            var topics = foundtopics.Distinct().ToList();
			
			var studentTopics = db.ClassTopics.Where(u => u.ClassId == studentClass.Id).ToList();


			ViewData["Topics"] = topics;
			ViewData["StudentTopics"] = studentTopics;

			return View();
		}

        public ActionResult ViewTopics(int? idClass, int? idCourse)
        {
            var trenutniUserName = User.Identity.Name;
            var student = db.Users.Where(u => u.UserName == trenutniUserName).FirstOrDefault();
            Session["Student"] = student;
            ViewData["PrimljeniClassId"] = idClass;
            ViewData["PrimljeniCourseId"] = idCourse;

            if (idClass == null || idCourse == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var topics = from a in db.ClassTopics
                         join b in db.CourseTopics
                         on a.TopicId equals b.TopicId
                         where a.ClassId == idClass && b.CourseId == idCourse
                         select new TOPICSClassCourseViewModel
                         {
                            Topic = b.Topic
                         };
            

            //     var usersOfClassCourse = db.UserClassCourse.Select( s => s.ClassId== idClass);
            // ViewData["Topics"] = topicsOfClassCourse.Distinct().ToList();
            ViewData["Topics"] = topics.Distinct().ToList();
            if (topics == null)
            {
                return HttpNotFound();
            }
            return View(topics);

        }

        public ActionResult SelectTopic(int? idClass, int? idCourse, int? idTopic)
        {
            Session["OdabraniTopicId"] = idTopic;
            return RedirectToAction("K", "Caterpillar");
        }


	}
}

