using System;
using System.Data;
using System.Web.Mvc;
using PSSharp.Models;
using System.Linq;

namespace PSSharp.Controllers
{
    public class CiController : Controller
    {
        private readonly PSSContext _db = new PSSContext();

        [HttpGet]
        public ActionResult ManageSuggestion(int id)
        {
            var result = _db.Suggestions.First(s => s.SuggestionId == id);
            result.Directions = _db.Directions.Where(d => d.SuggestionId == id).ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult ManageSuggestion(Suggestion sugg)
        {
            _db.Entry(sugg).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("ManageSuggestion", new { id = sugg.SuggestionId });
        }

        [HttpGet]
        public ActionResult AddDirection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDirection(Direction direction)
        {
            _db.Entry(direction).State = EntityState.Added;
            _db.SaveChanges();
            return RedirectToAction("ManageSuggestion", new { id = direction.SuggestionId });
        }

        [HttpGet]
        public ActionResult SelectSuggestion()
        {
            var statuses = from Statuses s in Enum.GetValues(typeof(Statuses))
                           select new { ID = s, Name = s.ToString() };
            
            ViewData["statuses"] = new SelectList(statuses, "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult SelectSuggestion(Suggestion sugg)
        {
            var resultList = _db.Suggestions.Where
                (s => (sugg.SuggestionId == null || s.SuggestionId == sugg.SuggestionId) && 
                      (sugg.Status == null || s.Status == sugg.Status) &&
                      (sugg.DateOfConsideration == null || s.DateOfConsideration == sugg.DateOfConsideration) &&
                      (sugg.DateOfReceipt == null || s.DateOfReceipt == sugg.DateOfReceipt));
            return View("ListSuggestion", resultList);
        }
    }
}