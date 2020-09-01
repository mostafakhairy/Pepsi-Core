using Coupons.PepsiKSA.Api.Core;
using NLog;
using System;

namespace Coupons.PepsiKSA.Api.Presistence
{
    public class Log : ILog
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(Exception ex, string message)
        {
            logger.Error(ex, message);
        }
    }
}
