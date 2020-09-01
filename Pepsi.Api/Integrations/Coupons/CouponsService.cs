using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Integrations.HttpFactory;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.Coupons
{
    //Coupons Integration Services
    public class CouponsService : ICouponsService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpFactory _httpFactory;
        public CouponsService(IConfiguration configuration, IHttpFactory httpFactory)
        {
            _configuration = configuration;
            _httpFactory = httpFactory;
        }
        public async Task<BurnCouponDto> BurnCouponsAsync(string mobileNumber, string offerNumber, string lang)
        {
            Boolean.TryParse(_configuration.GetSection("Coupons:SendSms").Value, out bool sendSms);
            Boolean.TryParse(_configuration.GetSection("Coupons:SubscribeSendSMS").Value, out bool subscribeSendSMS);

            var url = $"{_configuration.GetSection("Coupons:CouponsPregeneratedUrl").Value}" +
                $"/api/couponz/BurnPreGeneratedCoupon?customerMsisdn={mobileNumber}&couponNumber={offerNumber}" +
                $"&subscribeSendSMS={sendSms}&autoSubscribeWithOfferDetails=true&subscribeSendSMS={subscribeSendSMS}&lang={lang}";

            return await _httpFactory.ClientGetAsync<BurnCouponDto>(url);
        }

        public async Task<TierOffersResponseDto> GetTierOffers(int userPoints, string lang)
        {
            var url = $"{_configuration.GetSection("Coupons:CouponsUrl").Value}" +
                $"/api/Deals/GetTierDeals?ExternalPoints={userPoints}&lang={lang}";
            return await _httpFactory.ClientGetAsync<TierOffersResponseDto>(url);
        }

        public async Task<TiersResponseDto> GetTiers()
        {
            var url = $"{_configuration.GetSection("Coupons:CouponsUrl").Value}/api/Deals/GetTiers";
            return await _httpFactory.ClientGetAsync<TiersResponseDto>(url);
        }

        public async Task<UserHistoryDto> GetUserHistoryAsync(string mobileNumber, int pageIndex, int pageSize, string lang)
        {
            var url = $"{_configuration.GetSection("Coupons:CouponsUrl").Value}" +
               $"/api/couponz/GetCustomerHistoryJsonResult?msisdn={mobileNumber}&pageIndex={pageIndex}" +
               $"&pageSize={pageSize}&lang={lang}";

            return await _httpFactory.ClientGetAsync<UserHistoryDto>(url);
        }

        public async Task<CouponDetailsDto> SubscribeToOffer(string mobileNumber, string offerNumber, int userPoints, string lang)
        {
            var ratePlan = _configuration.GetSection("Coupons:RatePlan").Value;
            var url = $"{_configuration.GetSection("Coupons:CouponsUrl").Value}" +
               $"/api/couponz/SubscribeToOffer?msisdn={mobileNumber}" +
               $"&offerNumber={offerNumber}&channel=USSD&ratePlan={ratePlan}&forceXmlResult=false" +
               $"&ExternalPoints={userPoints}&subscribeWithOfferDetails=true&languageId={lang}";
            return await _httpFactory.ClientGetAsync<CouponDetailsDto>(url);
        }
    }
}
