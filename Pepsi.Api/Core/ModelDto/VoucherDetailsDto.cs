using System;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class VoucherDetailsDto
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public string BurnDate { get; set; }
        public string ExpiryDate { get; set; }
        public string VoucherNumber { get; set; }
        public string Status { get; set; }
        public string OfferName { get; set; }
        public string BranchName { get; set; }
        public double Discount { get; set; }
        public string SubscriptionDate { get; set; }
        public int RemainingUsage { get; set; }
        public DateTime LastModifiedAction { get; set; }
        public string OfferTitle { get; set; }
        public string OfferNumber { get; set; }
        public string OfferDescription { get; set; }
        public string MerchantImage { get; set; }
        public string MerchantCategory { get; set; }
        public string Actor { get; set; }
    }
}
