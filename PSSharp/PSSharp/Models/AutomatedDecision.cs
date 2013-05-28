using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Http;

namespace PSSharp.Models
{
    public class AutomatedDecision
    {
        private static readonly PSSContext _db = new PSSContext();

        public static void MakeDecision(int directionId)
        {
            Direction direction = _db.Directions.Where(d => d.DirectionId == directionId)
                                               .Include(d => d.PeerReviews).First();
            string rawNumberExpertsOnDepartment = ConfigurationManager.AppSettings["NumberExpertsOnDepartment"];
            string rawThresholdForAutomaticDecision = ConfigurationManager.AppSettings["ThresholdForAutomaticDecision"];
            int numberExpertsOnDepartment = Convert.ToInt32(rawNumberExpertsOnDepartment);
            double thresholdForAutomaticDecision = Convert.ToDouble(rawThresholdForAutomaticDecision);

            if (direction.Rejected() * 1.0 / numberExpertsOnDepartment > thresholdForAutomaticDecision)
            {
                direction.Status = Statuses.AutomateRejected;
                _db.SaveChanges();
            }
            else if (direction.PeerReviews.Count == numberExpertsOnDepartment)
            {
                direction.Status = Statuses.ReceivedPeerReview;
                _db.SaveChanges();
            }
        }
    }
}