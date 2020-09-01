namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class ECouponBurnRequest
    {
        public string Mobile { get; set; }
        public string Barcode { get; set; }
        public int RegionId => 88;
        public static string CountryISOCode => "SA";

    }
}
