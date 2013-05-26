using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PSSharp.Models
{
    public class Direction
    {
        [Display(Name = "Номер направления")]
        public virtual int DirectionId { get; set; }
        [Display(Name = "Номер инициативы")]
        public virtual int SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        [Display(Name = "Номер отдела")]
        public virtual int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Display(Name = "Статус")]
        public virtual Statuses Status { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime When { get; set; }
        public virtual List<PeerReview> PeerReviews { get; set; }

        public bool ReceivedPeerReview()
        {
            return Status == Statuses.ReceivedPeerReview;
        }

        public virtual int Approved()
        {
            return PeerReviews.Count(peerReview => peerReview.Recommended);
        }

        public virtual int Rejected()
        {
            return PeerReviews.Count(peerReview => !peerReview.Recommended);
        }
    }
}