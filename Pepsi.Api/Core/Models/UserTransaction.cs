using Coupons.PepsiKSA.Api.Presistence.Models;
using System;

namespace Coupons.PepsiKSA.Api.Core.Models
{
    public class UserTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Sku { get; set; }
        public string RewardRedeemed { get; set; }
        public string PromoCode { get; set; }
        public int OldPointsBalance { get; set; }
        public int NewPointsBalance { get; set; }
        public int Points { get; set; }
        public string UserEmail { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Type { get; set; }
        public string CampaignId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
