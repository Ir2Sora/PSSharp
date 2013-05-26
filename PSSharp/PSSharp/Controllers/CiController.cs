using System;
using System.Data;
using System.Data.Entity;
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
            var result = _db.Suggestions.Where(s => s.SuggestionId == id)
                .Include(s => s.Directions)
                .Include(s => s.User)
                .FirstOrDefault();
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
        public ActionResult AddDirection(int id)
        {
            ViewData["SuggestionId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddDirection(Direction direction)
        {
            //au
            direction.UserId = 1;
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
    }
}