﻿using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using PSSharp.Models;

namespace PSSharp.Controllers
{
    [Authorize]
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
            var user = _db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            sugg.UserId = user.UserId;
            sugg.DateOfReceipt = DateTime.Now;
            sugg.Status = Statuses.Processed;
            _db.Suggestions.Add(sugg);
            _db.SaveChanges();
            return new EmptyResult();
        }

        public ActionResult ViewOwnSuggestions()
        {
            //au
            var user = _db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            var suggestions = _db.Suggestions.Where(s => s.UserId == user.UserId).ToList();
            return View(suggestions);
        }

        public ActionResult ViewNeedImvrovement()
        {
            //au
            var user = _db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            var suggestions = _db.Suggestions.Where(s => s.UserId == user.UserId && s.Status == Statuses.RequireImprovement).ToList();
            return View("ViewOwnSuggestions", suggestions);
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
            Suggestion fromDB = _db.Suggestions.First(s => s.SuggestionId == sugg.SuggestionId);
            fromDB.Problem = sugg.Problem;
            fromDB.Result = sugg.Result;
            fromDB.Solution = sugg.Solution;
            fromDB.Status = Statuses.Processed;
            _db.SaveChanges();
            return View(fromDB);
        }
    }
}