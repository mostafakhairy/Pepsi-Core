using System;

namespace Coupons.PepsiKSA.Api.Core
{
    public interface ILog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(Exception ex, string message);
    }
}
