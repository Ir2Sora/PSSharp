namespace PSSharp.Models
{
    public class Direction
    {
        public virtual int DirectionId { get; set; }
        public virtual int SuggestionId { get; set; }
        public virtual int DepartmentId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        public virtual Department Department { get; set; }
        public virtual string Conclusion { get; set; }
        public virtual Statuses Status { get; set; }
    }
}