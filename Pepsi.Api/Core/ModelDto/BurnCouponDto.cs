namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class BurnCouponDto
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public OfferDetailsDto AutosubscribeResult { get; set; }
    }
}
