using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Presistence.Models;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Core.IRepository
{
    public interface IUserRepository
    {
        Task<User> UpdatePoints(string mobileNummber, int points, bool isRedeem = false);
        Task<User> GetUser(string mobileNumber);
        Task<User> UpdateUserStatus(string mobileNummber, bool isVerified);
        Task<User> UpdateSubsciptions(string mobileNumber, UserSubscriptionsDto userSubscriptions);
        Task GetUsersReport();
    }
}
