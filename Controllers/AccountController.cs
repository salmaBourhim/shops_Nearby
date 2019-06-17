using projectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectTest.Controllers
{
    public class AccountController : Controller
    {
        private ShopsNearByDbContext _context = new ShopsNearByDbContext();
        // GET: Account
        public ActionResult Index()
        {
            return View(_context.register.ToList());
        }
        //GET:Register
        public ActionResult Register()
        {
            return View();
        }
        //POST:Register
        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                _context.register.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("","Some error occured");
            }
            return View(user);
        }
        //GET:Login
        public ActionResult Login()
        {
            return View();
        }
        //POST:Login
        [HttpPost]
        public ActionResult Login(RegisterModel users)
        {
                    var usr = _context.register.Where(u => u.Username== users.Username && u.Password== users.Password).FirstOrDefault();
                    if (usr != null)
                    {
                        Session["UserId"] = usr.UserId.ToString();
                        Session["Username"] = usr.Username.ToString();
                return RedirectToAction("Index", "Shop");
            }
            return View(users);
        }
    }
}