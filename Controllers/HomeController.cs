using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DojoMeetUP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace DojoMeetUP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext dbContext;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        public IActionResult Index()
        {
            var userInDb = dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));

            ViewBag.UserLoggedIn = userInDb;
            return View();
        }

        [HttpGet("meetups")]
        public IActionResult MeetUps()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .Include(e => e.CreatedMeets)
                .Include(e => e.JoinedMeets)
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;
            
            // All MeetUps
            var allMeetUps = dbContext.Meets
                .Include(m => m.Creator)
                .Include(m => m.Guests)
                .ThenInclude(g => g.JoinedUser)
                .OrderByDescending(m => m.Date)
                .ToList();
            
            ViewBag.allMeetUps = allMeetUps;

            return View(allMeetUps);
        }
        
        [HttpGet("meetups/new")]
        public IActionResult NewMeet()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            return View();
        }
        
        [HttpPost("meetups/new")]
        public IActionResult CreateMeet(Meet meet)
        {
            if (ModelState.IsValid)
            {
                dbContext.Meets.Add(meet);
                dbContext.SaveChanges();
                return RedirectToAction("MeetUps");
            }
            else
            {
                var userInDb = dbContext.Users
                    .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                ViewBag.UserLoggedIn = userInDb;
                return View("NewMeet");
            }
        }
        
        [HttpGet("meetups/{id}")]
        public IActionResult ShowMeet(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .Include(e => e.CreatedMeets)
                .Include(e => e.JoinedMeets)
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            var meet = dbContext.Meets
                .Include(m => m.Creator)
                .Include(m => m.Guests)
                .ThenInclude(g => g.JoinedUser)
                .FirstOrDefault(m => m.MeetId == id);
            ViewBag.meet = meet;

            return View(meet);
        }
        
        [HttpGet("meetups/{id}/delete")]
        public IActionResult DeleteMeet(int id)
        {
            Console.WriteLine("Delete Meet");
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                Console.WriteLine("Delete Meet, no user");
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }
            
            
            Console.WriteLine("Delete Meet, process");

            var userInDb = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            var meet = dbContext.Meets
                .FirstOrDefault(m => m.MeetId == id);

            if (meet.Creator.UserId != userInDb.UserId)
            {
                TempData["AuthError"] = "You must be the Creator to delete this Meet";
                return RedirectToAction("MeetUps");
            }
            dbContext.Meets.Remove(meet);
            dbContext.SaveChanges();
            return RedirectToAction("MeetUps");
        }

        [HttpGet("JoinMeet/{meetId}")]
        public IActionResult JoinMeet(int meetId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            var meet = dbContext.Meets
                .Include(m => m.Creator)
                .Include(m => m.Guests)
                .ThenInclude(g => g.JoinedUser)
                .FirstOrDefault(m => m.MeetId == meetId);
            ViewBag.meet = meet;

            Guest guest = new Guest();
            guest.JoinedUserId = userInDb.UserId;
            guest.EventId = meetId;
            dbContext.Guests.Add(guest);
            dbContext.SaveChanges();

            return RedirectToAction("MeetUps");
        }
        [HttpGet("LeaveMeet/{meetId}")]
        public IActionResult LeaveMeet(int meetId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            var meet = dbContext.Meets
                .Include(m => m.Creator)
                .Include(m => m.Guests)
                .ThenInclude(g => g.JoinedUser)
                .FirstOrDefault(m => m.MeetId == meetId);
            ViewBag.meet = meet;

            Guest guest = dbContext.Guests
                .FirstOrDefault(g => g.JoinedUserId == userInDb.UserId && g.EventId == meetId);
            dbContext.Guests.Remove(guest);
            dbContext.SaveChanges();

            return RedirectToAction("MeetUps");
        }
        [HttpGet("JoinMeet/{meetId}/ind")]
        public IActionResult JoinMeetind(int meetId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            var meet = dbContext.Meets
                .Include(m => m.Creator)
                .Include(m => m.Guests)
                .ThenInclude(g => g.JoinedUser)
                .FirstOrDefault(m => m.MeetId == meetId);
            ViewBag.meet = meet;

            Guest guest = new Guest();
            guest.JoinedUserId = userInDb.UserId;
            guest.EventId = meetId;
            dbContext.Guests.Add(guest);
            dbContext.SaveChanges();

            return RedirectToAction("ShowMeet", new { id = meetId });
        }
        [HttpGet("LeaveMeet/{meetId}/ind")]
        public IActionResult LeaveMeetind(int meetId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["AuthError"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            var userInDb = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserLoggedIn = userInDb;

            var meet = dbContext.Meets
                .Include(m => m.Creator)
                .Include(m => m.Guests)
                .ThenInclude(g => g.JoinedUser)
                .FirstOrDefault(m => m.MeetId == meetId);
            ViewBag.meet = meet;

            Guest guest = dbContext.Guests
                .FirstOrDefault(g => g.JoinedUserId == userInDb.UserId && g.EventId == meetId);
            dbContext.Guests.Remove(guest);
            dbContext.SaveChanges();

            return RedirectToAction("ShowMeet", new { id = meetId });
        }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}