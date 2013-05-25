using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PSSharp.Models
{
    public class Suggestion
    {
        [Display(Name = "Номер инициативы")]
        public virtual int? SuggestionId { get; set; }
        public virtual int? UserId { get; set; }
        [Display(Name = "Инициатор")]
        public virtual User User { get; set; }
        [Display(Name = "Проблема")]
        public virtual string Problem { get; set; }
        [Display(Name = "Решение")]
        public virtual string Solution { get; set; }
        [Display(Name = "Результат")]
        public virtual string Result { get; set; }
        [Display(Name = "Дата подачи")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime? DateOfReceipt { get; set; }
        [Display(Name = "Дата рассмотрения")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public virtual DateTime? DateOfConsideration { get; set; }
        public virtual List<Direction> Directions { get; set; }
        [Display(Name = "Статус")]
        public virtual Statuses? Status { get; set; }

        public string DateOfConsiderationView()
        {
            return String.Format("{0:yyyy/MM/dd}", DateOfConsideration);
        }

        public string DateOfReceiptView()
        {
            return String.Format("{0:yyyy/MM/dd}", DateOfReceipt);
        }

        public bool NeedImrovement()
        {
            return Status == Statuses.RequireImprovement;
        }

        public string View()
        {
            var sb = new StringBuilder();
            sb.Append(LocalView());
            sb.Append(", Инициатор ").Append(User.View());
            return sb.ToString();
        }

        public string LocalView()
        {
            var sb = new StringBuilder();
            sb.Append("№ ").Append(SuggestionId);
            sb.Append(", Дата подачи ").Append(DateOfReceiptView());
            return sb.ToString();
        }
    }
}