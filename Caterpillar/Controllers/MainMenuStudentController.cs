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

        public ActionResult Index()
        {
         //   var currentUMUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
         //   var currentUser = db.Users.Find(currentUMUser.UserID);


            
            var currentstudent =  UserManager.FindByIdAsync(User.Identity.GetUserId());
           // Task<System.Web.Mvc.ActionResult> currentstudent = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //if (User.Identity==null||User.Identity.Name=="")
                var student = db.Users.Where(u=>u.UserName==User.Identity.Name).FirstOrDefault();
            //var student = db.Users.Find(User.Identity.GetUserId());
            //var student = db.Users.Find(4);
            ViewData["Student"] = student;
            

            //var kwlentries = student.KWLentries;
            //ViewData["KWLentries"] = kwlentries.ToList();

            var subjects = student.UserClassCourses;
            ViewData["Subjects"] = subjects.ToList();

            var topics = from a in subjects.ToList()
                         join b in db.CourseTopics
                         on a.CourseId equals b.CourseId
                         select new UserTopicViewModel
                         {
                             Topic = b.Topic
                         };

            ViewData["Topics"] = topics.ToList();
           


            return View();
        }

        
	}
}

