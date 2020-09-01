namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class SaleForceAuthTokenRequest
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string account_id { get; set; }
    }
}
