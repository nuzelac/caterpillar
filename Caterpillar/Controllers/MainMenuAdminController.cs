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
    [Authorize(Roles = "Administrator")]
	public class MainMenuAdminController : Controller
	{
		public MainMenuAdminController()
			: this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
		{
		}

		public MainMenuAdminController(UserManager<ApplicationUser> userManager)
		{
			UserManager = userManager;
		}
		private CaterpillarContext db = new CaterpillarContext();
		//
		// GET: /MainMenuStudent/


		public UserManager<ApplicationUser> UserManager { get; private set; }

		public ActionResult Index()
		{

			var currentAdmin = UserManager.FindByIdAsync(User.Identity.GetUserId());
			var admin = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
			ViewData["Admin"] = admin;



			return View();
		}
	}
}