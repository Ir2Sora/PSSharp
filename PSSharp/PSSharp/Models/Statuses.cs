using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PSSharp.Models
{
    public enum Statuses
    {
        [Description("Рассматривается")]
        Processed,
        [Description("Требует доработки")]
        RequireImprovement,
        [Description("Принята")]
        Adoption,
        [Description("Отклонена")]
        Rejected,
        [Description("Отклонена автоматически")]
        AutomateRejected,
        [Description("Запрошена оценка")]
        RequestedPeerReview,
        [Description("Получена оценка")]
        ReceivedPeerReview
    }

    public static class EnumExtension
    {
        public static string GetDescription(object o)
        {
            Enum enumValue = (Enum) o;
            object[] attr = enumValue.GetType().GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr.Length > 0
               ? ((DescriptionAttribute)attr[0]).Description
               : enumValue.ToString();
        }
    }
}