using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class BurnReport
    {
        public string EmailAddress { get; set; }
        public string ProductID { get; set; }
        public string Sku { get; set; }
        public string PromoCode { get; set; }
        public int PointsEarned { get; set; }
        public DateTime DateEarned { get; set; }
        public string CampaignID { get; set; }
    }
}