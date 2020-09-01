namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class EcouponRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public static int RegionId => 88;
        public static string CountryISOCode => "SA";
    }
}
