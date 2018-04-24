using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using thursdaySportsday.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace thursdaySportsday.Controllers
{
    public class HomeController : Controller
    {
        private SportsContext _Context;
        public HomeController(SportsContext context)
        {
            _Context = context;
        }


        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        // POST: /Register/
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            System.Console.WriteLine("In Register Route");
            if(ModelState.IsValid)
            {
                // Checks to see whether a User is already registered with this email.
                var existingPerson = _Context.Users.Where(you => you.EmailAddress == model.emailAddress).SingleOrDefault();
                if(existingPerson == null)
                {
                    // Create User object with hashed password.
                    User newUser = new User
                    {
                        FirstName = model.firstName,
                        LastName = model.lastName,
                        EmailAddress = model.emailAddress,
                        Password = model.password
                    };

                    // Add new User to DB.
                    _Context.Users.Add(newUser);
                    _Context.SaveChanges();
                    System.Console.WriteLine("User Saved !!");

                    // Set session variables.
                    HttpContext.Session.SetInt32("CurrentUserId", newUser.UserId);
                    HttpContext.Session.SetString("FirstName", newUser.FirstName);
                    HttpContext.Session.SetString("LastName", newUser.LastName);
                    return RedirectToAction("Dashboard", "Event");
                }
                else
                {
                    System.Console.WriteLine("Not Good ..");
                    ViewBag.errors = "A User with that Email already exists.";
                    return View("Index");
                }
            }

            // Return View to show ASP-Validation errors.
            return View("Index");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            System.Console.WriteLine("In Login Route ..");
            //get the user with the submitted email
            var existing = _Context.Users.Where(u => u.EmailAddress == email).SingleOrDefault();
            if (existing != null)
            {
                var existingPassword = _Context.Users.Where(u => u.Password == password).FirstOrDefault();
                if (existingPassword != null)
                {
                    HttpContext.Session.SetInt32("CurrentUserId", existing.UserId);
                    HttpContext.Session.SetString("Username", existing.FirstName);
                    return RedirectToAction("Dashboard", "Event");
                }
                else
                {
                    System.Console.WriteLine("Incorrect Password ..");
                    ViewBag.passworderror = "Incorrect Password.";
                    return View("Index");
                }
            }
            else
            {
                System.Console.WriteLine("Try Again ..");
                ViewBag.emailerror = "Enter your correct Email";
                return View("Index");
            }
        }

        // GET: /Logout/            
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            System.Console.WriteLine("GoodBye ..");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
