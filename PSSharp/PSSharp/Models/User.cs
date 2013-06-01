
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PSSharp.Models
{
    public class User
    {
        public virtual int UserId { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public virtual string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public virtual string Surname { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public virtual string Patronymic { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public virtual string Login { get; set; }

        [NotMapped]
        [Required]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public virtual string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронная почта")]
        public virtual string Email { get; set; }

        [Required]
        [Display(Name = "Отдел")]
        public virtual int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public string[] Roles { get; set; }

        public string View()
        {
            var sb = new StringBuilder();
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