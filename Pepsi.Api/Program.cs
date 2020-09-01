using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sentry;

namespace Coupons.PepsiKSA.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Information("Starting");
            using (SentrySdk.Init("https://6d534ef99d87418cbdacef6f18630917@o396981.ingest.sentry.io/5320634"))
            {
                CreateHostBuilder(args).Build().Run();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSentry();
                });
    }
}
