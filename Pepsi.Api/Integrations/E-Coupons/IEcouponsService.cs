using Coupons.PepsiKSA.Api.Core.ModelDto;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.E_Coupons
{
    public interface IEcouponsService
    {
        Task<EcouponsRegisterResponseDto> EcouponsRegister(UserRegisterDto registeredUser);
        Task<ECouponBurnResponse> EBurn(ECouponBurnRequest body);
    }
}
