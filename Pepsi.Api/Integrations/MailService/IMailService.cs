using Coupons.PepsiKSA.Api.Core.ModelDto;

namespace Coupons.PepsiKSA.Api.Integrations.MailService
{
    public interface IMailService
    {
        void Send(EmailProperties email);
    }
}
