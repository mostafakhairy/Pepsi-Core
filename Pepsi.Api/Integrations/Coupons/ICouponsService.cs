using Coupons.PepsiKSA.Api.Core.ModelDto;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.Coupons
{
    public interface ICouponsService
    {
        Task<BurnCouponDto> BurnCouponsAsync(string mobileNumber, string offerNumber, string lang);
        //void SubscribeToOfferAsync(string mobileNumber, string offerNumber);
        Task<UserHistoryDto> GetUserHistoryAsync(string mobileNumber, int pageIndex, int pageSize, string lang);
        Task<TiersResponseDto> GetTiers();
        Task<TierOffersResponseDto> GetTierOffers(int userPoints, string lang);
        Task<CouponDetailsDto> SubscribeToOffer(string mobileNumber, string offerNumber, int userPoints, string lang);

    }
}
