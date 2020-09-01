using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.HttpFactory
{
    public interface IHttpFactory
    {
        Task<T> ClientGetAsync<T>(string uri);
        Task<TOut> ClientPostAsync<TIn, TOut>(string uri, TIn content);
        Task<TOut> ClientPostAsync<TIn, TOut>(string uri, TIn content, Dictionary<string, string> customHeader);
    }
}
