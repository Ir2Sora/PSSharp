using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.SqlClient;
using System.Web.Mvc;
using PSSharp.Models;
using System.Linq;

namespace PSSharp.Controllers
{
    [Authorize(Roles = "ci")]
    public class CiController : Controller
    {
        private readonly PSSContext _db = new PSSContext();

        [HttpGet]
        public ActionResult ManageSuggestion(int id)
        {
            var result = _db.Suggestions.Where(s => s.SuggestionId == id)
                .Include(s => s.Directions)
                .Include(s => s.User)
                .FirstOrDefault();
            ViewData["Departments"] = GenerateDepartments(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult ManageSuggestion(Suggestion sugg)
        {
            var oldSuggestion = _db.Suggestions.First(s => s.SuggestionId == sugg.SuggestionId);
            var oldStatus = (Statuses) oldSuggestion.Status;
            _db.Entry(oldSuggestion).State = EntityState.Detached;
            var newStatus = (Statuses) sugg.Status;
            _db.Entry(sugg).State = EntityState.Modified;
            _db.SaveChanges();
            if (oldStatus != newStatus)
            {
                var newSuggestion = _db.Suggestions.Where(s => s.SuggestionId == sugg.SuggestionId)
                                                   .Include(s => s.User).First();
                newSuggestion.oldStatus = oldStatus;
                EmailController.SendEmail(newSuggestion);
            }
            return RedirectToAction("ManageSuggestion", new { id = sugg.SuggestionId });
        }

        public ActionResult AddDirection(Direction direction)
        {
            //au
            var user = _db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            direction.UserId = user.UserId;
            if (direction.DepartmentId == null)
            {
                return RedirectToAction("ManageSuggestion", new { id = direction.SuggestionId });
            }
            direction.When = DateTime.Now;
            direction.Status = Statuses.RequestedPeerReview;
            _db.Entry(direction).State = EntityState.Added;
            _db.SaveChanges();
            return RedirectToAction("ManageSuggestion", new { id = direction.SuggestionId });
        }

        [HttpGet]
        public ActionResult SelectSuggestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SelectSuggestion(Suggestion sugg)
        {
            var resultList = _db.Suggestions.Where
                (s => (sugg.SuggestionId == null || s.SuggestionId == sugg.SuggestionId) && 
                      (sugg.Status == null || s.Status == sugg.Status) &&
                      (sugg.DateOfConsideration == null || s.DateOfConsideration == sugg.DateOfConsideration) &&
                      (sugg.DateOfReceipt == null || s.DateOfReceipt == sugg.DateOfReceipt))
                .Include(s => s.User);
            return View("ListSuggestion", resultList);
        }

        [HttpGet]
        public ActionResult ViewDirection(int id)
        {
            var result = _db.Directions.Where(d => d.DirectionId == id)
                                       .Include(d => d.PeerReviews.Select(p => p.User)).First();
            return View(result);
        }

        [HttpPost]
        public ActionResult ViewDirection(Direction direction)
        {
            var result = _db.Directions.First(d => d.DirectionId == direction.DirectionId);
            result.Status = direction.Status;
            _db.SaveChanges();
            return RedirectToAction("ManageSuggestion", new { id = result.SuggestionId });
        }

        private List<SelectListItem> GenerateDepartments(int suggestionId)
        {
            var departments = (from dep in _db.Departments
                               where !_db.Directions.Any(dir => dir.DepartmentId == dep.DepartmentId && dir.SuggestionId == suggestionId) 
                               select new SelectListItem
                               {
                                   Text = dep.ShortName,
                                   Value = SqlFunctions.StringConvert((double)dep.DepartmentId).Trim()
                               });
            return departments.ToList();
        }

        public ActionResult ListDepartments()
        {
            var departments = _db.Departments.ToList();
            return View(departments);
        }

        [HttpGet]
        public ActionResult EditDepartment(int id)
        {
            var department = _db.Departments.First(d => d.DepartmentId == id);
            return View(department);
        }

        [HttpPost]
        public ActionResult EditDepartment(Department dep)
        {
            _db.Entry(dep).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("ListDepartments");
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Department dep)
        {
            _db.Entry(dep).State = EntityState.Added;
            _db.SaveChanges();
            return RedirectToAction("ListDepartments");
        }
    }
}