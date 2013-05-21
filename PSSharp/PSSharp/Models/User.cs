
namespace PSSharp.Models
{
    public class User
    {
        public virtual int UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Patronymic { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}