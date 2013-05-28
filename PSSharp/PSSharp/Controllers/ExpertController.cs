using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using PSSharp.Models;

namespace PSSharp.Controllers
{
    [Authorize(Roles = "expert")]
    public class ExpertController : Controller
    {
        private readonly PSSContext _db = new PSSContext();

        [HttpGet]
        public ActionResult WritePeerReview(int id)
        {
            var direction = _db.Directions.Where(d => d.DirectionId == id)
                                        .Include(d => d.Suggestion)
                                        .Include(d => d.Suggestion.User).First();
            var peerReview = new PeerReview {Direction = direction};
            return View(peerReview);
        }

        [HttpPost]
        public ActionResult WritePeerReview(PeerReview peerReview)
        {
            //au
            var user = _db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            peerReview.UserId = user.UserId;
            peerReview.When = DateTime.Now;
            _db.Entry(peerReview).State = EntityState.Added;
            _db.SaveChanges();
            AutomatedDecision.MakeDecision(peerReview.DirectionId);
            return RedirectToAction("SelectSuggestion");
        }

        public ActionResult SelectSuggestion()
        {
            //au
            var user = _db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            int userId = user.UserId;
            int departmentId = user.DepartmentId;
            var result = _db.Directions.Where(d => d.DepartmentId == departmentId && d.Status == Statuses.RequestedPeerReview && d.PeerReviews.All(p => p.UserId != userId))
                                        .Include(d => d.Suggestion)
                                        .Include(d => d.Suggestion.User)
                                        .Include(d => d.PeerReviews).ToList();
            return View(result);
        }
    }
}