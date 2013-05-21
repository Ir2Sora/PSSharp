using System.ComponentModel.DataAnnotations;

namespace PSSharp.Models
{
    public class Direction
    {
        [Display(Name = "Номер направления")]
        public virtual int DirectionId { get; set; }
        [Display(Name = "Номер инициативы")]
        public virtual int SuggestionId { get; set; }
        [Display(Name = "Номер отдела")]
        public virtual int DepartmentId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        public virtual Department Department { get; set; }
        [Display(Name = "Заключение")]
        public virtual string Conclusion { get; set; }
        [Display(Name = "Статус")]
        public virtual Statuses Status { get; set; }

        public bool ReceivedPeerReview()
        {
            return Status == Statuses.ReceivedPeerReview;
        }
    }
}