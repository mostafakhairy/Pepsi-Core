using AutoMapper;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Core.Models;
using Coupons.PepsiKSA.Api.Presistence.Models;

namespace Coupons.PepsiKSA.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserRegisterDto, EcouponRegisterDto>()
                .ForMember(dist => dist.FirstName, source => source.MapFrom(c => c.FirstName))
                .ForMember(dist => dist.LastName, source => source.MapFrom(c => c.LastName))
                .ForMember(dist => dist.Mobile, source => source.MapFrom(c => c.MobileNumber))
                .ForMember(dist => dist.Email, source => source.MapFrom(c => c.Email));

            CreateMap<User, UserReportDto>()
                 .ForMember(dist => dist.FirstName, source => source.MapFrom(c => c.FirstName))
                .ForMember(dist => dist.LastName, source => source.MapFrom(c => c.LastName))
                .ForMember(dist => dist.MobileNumber, source => source.MapFrom(c => c.MobileNumber))
                .ForMember(dist => dist.EmailAddress, source => source.MapFrom(c => c.Email))
            .ForMember(dist => dist.CurrentPoints, source => source.MapFrom(c => c.Points))
            .ForMember(dist => dist.LanguagePreference, source => source.MapFrom(c => c.Language))
            .ForMember(dist => dist.InitialEmailOptIn, source => source.MapFrom(c => c.IsSubscribedMail))
            .ForMember(dist => dist.InitialSmsOptIn, source => source.MapFrom(c => c.IsSubscribedSms))
            .ForMember(dist => dist.LastActivity_UpdateDate, source => source.MapFrom(c => c.LastModificationDate))
            .ForMember(dist => dist.AccountRegistrationDate, source => source.MapFrom(c => c.RegisterDate))
            .ForMember(dist => dist.Country, source => source.MapFrom(c => c.Country));

            CreateMap<UserTransaction, BurnReport>()
                .ForMember(dist => dist.EmailAddress, source => source.MapFrom(c => c.UserEmail))
                .ForMember(dist => dist.ProductID, source => source.MapFrom(c => c.ProductId))
                .ForMember(dist => dist.Sku, source => source.MapFrom(c => c.Sku))
                .ForMember(dist => dist.PromoCode, source => source.MapFrom(c => c.PromoCode))
                .ForMember(dist => dist.PointsEarned, source => source.MapFrom(c => c.Points))
                .ForMember(dist => dist.DateEarned, source => source.MapFrom(c => c.Date))
                .ForMember(dist => dist.CampaignID, source => source.MapFrom(c => c.CampaignId));

            CreateMap<UserTransaction, SubscribeReport>()
                .ForMember(dist => dist.EmailAddress, source => source.MapFrom(c => c.UserEmail))
                .ForMember(dist => dist.RewardRedeemed, source => source.MapFrom(c => c.RewardRedeemed))
                .ForMember(dist => dist.InterestCategory, source => source.MapFrom(c => c.CategoryId))
                .ForMember(dist => dist.RewardPointsRedeemed, source => source.MapFrom(c => c.Points))
                .ForMember(dist => dist.RewardRedeemedDate, source => source.MapFrom(c => c.Date))
                .ForMember(dist => dist.CampaignID, source => source.MapFrom(c => c.CampaignId));
        }
    }
}
