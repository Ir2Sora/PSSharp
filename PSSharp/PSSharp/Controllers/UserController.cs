using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using PSSharp.Models;

namespace PSSharp.Controllers
{
    public class UserController : Controller
    {
        private readonly PSSContext _db = new PSSContext();

        [HttpGet]
        public ActionResult SubmitSuggestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitSuggestion(Suggestion sugg)
        {
            //au
            sugg.UserId = 1;
            sugg.DateOfReceipt = DateTime.Now;
            _db.Suggestions.Add(sugg);
            _db.SaveChanges();
            return new EmptyResult();
        }

        public ActionResult ViewOwnSuggestions()
        {
            //au
            var UserId = 1;
            var suggestions = _db.Suggestions.Where(s => s.UserId == UserId).ToList();
            return View(suggestions);
        }

        [HttpGet]
        public ActionResult EditOwnSuggestion(int id)
        {
            Suggestion suggestion = _db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        [HttpPost]
        public ActionResult EditOwnSuggestion(Suggestion sugg)
        {
            _db.Entry(sugg).State = EntityState.Modified;
            _db.SaveChanges();
            return View(sugg);
        }
    }
}