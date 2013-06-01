using System.ComponentModel.DataAnnotations;

namespace PSSharp.Models
{
    public class Department
    {
        public virtual int DepartmentId { get; set; }
        [Display(Name = "Название")]
        public virtual string FullName { get; set; }
        [Display(Name = "Сокр. название")]
        public virtual string ShortName { get; set; }
        [Display(Name = "Номер")]
        public virtual int Number { get; set; }
    }
}