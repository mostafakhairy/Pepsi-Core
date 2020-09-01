using Coupons.PepsiKSA.Api.Core.ModelDto;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.SaleForce
{
    public interface ISaleForceService
    {
        Task<SaleForceAuthTokenResponse> SaleForceAuthToken();
        Task<SaleForceSendEmailResponse> SaleForceSendMail(UserRegisterDto registerdUser, string token, string lang = "Arabic", bool mailOPT = true, bool smsOPT = true);
    }
}
