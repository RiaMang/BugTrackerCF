using BugTrackerCF.Helpers;
using BugTrackerCF.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTrackerCF.Hub;
using Microsoft.AspNet.Identity.Owin;


namespace BugTrackerCF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            
           
                return View();
        }

        public ActionResult EditUser(string id= "edf8da98-ba7f-4626-a73f-77c9d2862dd7")
        {
            var user = db.Users.Find(id);
            AdminUserViewModel AdminModel = new AdminUserViewModel();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUserRoles(id);
            AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            AdminModel.User = user;
            
            return View(AdminModel);
        }

        [HttpPost]
        public ActionResult EditUser(AdminUserViewModel model)
        {
            model.User = db.Users.Find(model.User.Id);
            var um = Request.GetOwinContext().Get<ApplicationUserManager>();
            string[] sel = { };
            var SelRoles = model.SelectedRoles ?? sel;
            foreach (var role in db.Roles.ToList())
            {
                if (SelRoles.Contains(role.Name))
                    um.AddToRole(model.User.Id, role.Name);
                else
                    if(!(role.Name == "Admin" && model.User.UserName == "rmanglani@coderfoundry.com" ))
                        um.RemoveFromRole(model.User.Id, role.Name);
            }
            //return RedirectToAction("EditUser", new { Id = model.User.Id });
            
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // :warning: Contact Form 
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}