using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class SubscribeReport
    {
        public string EmailAddress { get; set; }
        public string RewardRedeemed { get; set; }
        public string InterestCategory { get; set; }
        public string RewardRedeemedDate { get; set; }
        public string RewardPointsRedeemed { get; set; }
        public string CampaignID { get; set; }
    }
}
