using System.ComponentModel;

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
        [Description("Запрошена оценка")]
        RequestedPeerReview,
        [Description("Получена оценка")]
        ReceivedPeerReview
    }
}