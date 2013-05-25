
using System.Text;

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

        public string View()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Surname)
              .Append(" ")
              .Append(FirstName.Substring(0, 1))
              .Append(".")
              .Append(Patronymic.Substring(0, 1))
              .Append(".");
            return sb.ToString();
        }
    }
}