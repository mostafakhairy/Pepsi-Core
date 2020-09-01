using Coupons.PepsiKSA.Api.Core.Enums;
using Coupons.PepsiKSA.Api.Presistence.Models;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Core.IRepository
{
    public interface ITransactionRepository
    {
        Task<bool> AddTransaction(User user, TransactionType type, int oldPoints, string promoCode, string catgoryId, string rewardRedeemed);

        Task<bool> ExportRewards(string type);
    }
}
