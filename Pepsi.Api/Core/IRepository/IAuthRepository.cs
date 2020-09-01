using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Presistence.Models;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Core.IRepository
{
    public interface IAuthRepository
    {
        Task<User> Register(UserRegisterDto user);
        Task<User> Login(LoginDto user);

        Task<bool> EmailExist(string userEmail);

        Task<bool> UserExist(string mobileNumber);
        AuthDto GenerateToken(User user);
        Task<bool> GenerateOTP(string email);
        Task<bool> ValidateOTP(string email, int otp);
        Task<bool> ResetPassword(ResetPasswordDto req);

    }
}
