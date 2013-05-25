﻿using System.Data;
using System.Linq;
using System.Web.Mvc;
using PSSharp.Models;

namespace PSSharp.Controllers
{
    public class ExpertController : Controller
    {
        private readonly PSSContext _db = new PSSContext();

        [HttpGet]
        public ActionResult WritePeerReview(int id)
        {
            var result = _db.Directions.First(d => d.DirectionId == id);
            result.Suggestion = _db.Suggestions.First(s => s.SuggestionId == result.SuggestionId);
            return View(result);
        }

        [HttpPost]
        public ActionResult WritePeerReview(Direction direction)
        {
            direction.Status = Statuses.ReceivedPeerReview;
            _db.Entry(direction).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("SelectSuggestion");
        }

        public ActionResult SelectSuggestion()
        {
            //au
            int departmentID = 1;
            var result = _db.Directions.Where(d => d.DepartmentId == departmentID && d.Status == Statuses.RequestedPeerReview).ToList();
            foreach (var direction in result)
            {
                direction.Suggestion = _db.Suggestions.First(s => s.SuggestionId == direction.SuggestionId);
            }
            return View(result);
        }
    }
}