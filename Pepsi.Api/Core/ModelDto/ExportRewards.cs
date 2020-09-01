using System;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class ExportRewards
    {
        public string EmailAddress { get; set; }
        public string ProductID { get; set; }

        public string SKU { get; set; }

        public string PromoCode { get; set; }
        public int PointsEarned { get; set; }
        public DateTime DateEarned { get; set; }
        public string CampaignID { get; set; }

    }
}
