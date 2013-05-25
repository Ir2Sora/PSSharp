using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSSharp.Models
{
    public class PeerReview
    {
        [Display(Name = "Номер оценки")]
        public virtual int PeerReviewId { get; set; }
        [Display(Name = "Номер направления")]
        public virtual int DirectionId { get; set; }
        public virtual Direction Direction { get; set; }
        public virtual int? UserId { get; set; }
        [Display(Name = "Эксперт")]
        public virtual User User { get; set; }
        [Display(Name = "Рекомендовано")]
        public virtual bool Recommended { get; set; }
        [Display(Name = "Заключение")]
        public virtual string Conclusion { get; set; }
        public virtual DateTime When { get; set; }
    }
}