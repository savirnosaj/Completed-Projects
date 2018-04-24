using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using thursdaySportsday.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace thursdaySportsday.Controllers
{
    public class EventController : Controller
    {
        private SportsContext _Context;
        public EventController(SportsContext context)
        {
            _Context = context;
        }


        // GET: /Dashboard/
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard(int id)
        {
            System.Console.WriteLine("We're in the Dashboard ..");
            int? userID = HttpContext.Session.GetInt32("CurrentUserId");
            if(userID == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.userid = userID;
            ViewBag.firstname = HttpContext.Session.GetString("FirstName");
            ViewBag.lastname = HttpContext.Session.GetString("LastName");
            // 
            ViewBag.guests = _Context.Guests.Where(g => g.SportId == id).Include(g => g.User);

            // Grab all Sports to show on DB.
            ViewBag.weddings = _Context.Sports.Include(w => w.Guests).ToList();

            // Create a list of all the Weddings IDs the current User is attending.
            List<Guest> Guest = _Context.Guests.Where(g => g.UserId == userID).ToList();
            List<int> weddingsAttending = new List<int>();
            foreach(Guest g in Guest)
            {
                weddingsAttending.Add(g.SportId);
            }

            ViewBag.attending = weddingsAttending;
            return View();
        }


        // GET: /Add/
        [HttpGet]
        [Route("add")]
        public IActionResult ShowAdd()
        {
            System.Console.WriteLine("Create your Sports Activity !");            
            int? userID = HttpContext.Session.GetInt32("CurrentUserId");
            if(userID == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.userid = userID;
            ViewBag.username = HttpContext.Session.GetString("Username");
            return View(viewName: "Add");
        }


        // POST: /Create/        
        [HttpPost]
        [Route("create")]
        // Validate the submission and add the Wedding to the DB
        public IActionResult CreatePlayDate(Sport wed)
        {
            // Check whether User is logged in, and if so get session variables (User Id/Name)
            int? userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                // Set current User as Wedding creator and add to DB
                wed.UserId = (int)userId;
                _Context.Add(wed);
                _Context.SaveChanges();
                System.Console.WriteLine("Congratulations !!");
                return RedirectToAction("Dashboard");
            }
            // Show Add page with errors
            return View("Add");
        }


        // GET: /Show/
        [HttpGet]
        [Route("show/{id}")]
        // Show a specific Wedding with details and map
        public IActionResult Show(int id)
        {
            // Check whether User is logged in, and if so get session variables (User Id/Name)
            int? userID = HttpContext.Session.GetInt32("CurrentUserId");
            if(userID == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.thursdaySportsday = _Context.Sports.Where(w => w.SportId == id).SingleOrDefault();
            ViewBag.guests = _Context.Guests.Where(g => g.SportId == id).Include(g => g.User);
            System.Console.WriteLine("We're on the Show page ..");
            return View();
        }


        // GET: /Delete/
        [HttpGet]
        [Route("delete/{id}")]
        // Delete a given Wedding from the DB -- UPDATE TO POST ROUTE
        public IActionResult DeleteEvent(int id)
        {
            Sport wed = _Context.Sports.Where(w => w.SportId == id).SingleOrDefault();
            int? USERID = HttpContext.Session.GetInt32("CurrentUserId");
            // Validate the User deleting is the User who created the Wedding
            if(USERID == wed.UserId)
            {
                _Context.Sports.Remove(wed);
                _Context.SaveChanges();
                System.Console.WriteLine("You just deleted your Wedding ..");            
            }
            return RedirectToAction("Dashboard");
        }


        // GET: /RSVP/               
        [HttpGet]
        [Route("rsvp/{wedid}")]
        // Add User to Guest list for the given Wedding -- UPDATE TO POST ROUTE
        public IActionResult RSVP(int wedid)
        {
            int? uId = HttpContext.Session.GetInt32("CurrentUserId");
            // Check whether User is logged in, and if so get session variables (User Id/Name)
            if (uId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Create Guest and add to DB
            Guest newGuest = new Guest
            {
                SportId = wedid,
                UserId = (int)uId
            };
            _Context.Guests.Add(newGuest);
            _Context.SaveChanges();
            System.Console.WriteLine("You just RSVP");            
            return RedirectToAction("Dashboard");
        }


        // GET: /Un-RSVP/                       
        [HttpGet]
        [Route("unrsvp/{wedid}")]
        // Remove User from Guest list for the given Wedding -- UPDATE TO POST ROUTE
        public IActionResult UNRSVP(int wedid)
        {
            int? userId = HttpContext.Session.GetInt32("CurrentUserId");
            // Check whether User is logged in, and if so get session variables (User Id/Name)
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Instantiate Guest to then Delete from DB
            Guest toDelete = _Context.Guests.Where(g => g.SportId == wedid && g.UserId == userId).SingleOrDefault();
            _Context.Guests.Remove(toDelete);
            _Context.SaveChanges();
            System.Console.WriteLine("You just Un-RSVP");            
            return RedirectToAction("Dashboard");
        }
    }
}