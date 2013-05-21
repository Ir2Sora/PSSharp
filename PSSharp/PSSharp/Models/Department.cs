namespace PSSharp.Models
{
    public class Department
    {
        public virtual int DepartmentId { get; set; }
        public virtual string FullName { get; set; }
        public virtual string ShortName { get; set; }
        public virtual int Number { get; set; }
    }
}