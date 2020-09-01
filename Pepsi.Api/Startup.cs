using AspNetCoreRateLimit;
using AutoMapper;
using Coupons.PepsiKSA.Api.Core;
using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Extensions;
using Coupons.PepsiKSA.Api.Filters.ErrorHandlerFilter;
using Coupons.PepsiKSA.Api.Integrations.Coupons;
using Coupons.PepsiKSA.Api.Integrations.E_Coupons;
using Coupons.PepsiKSA.Api.Integrations.HttpFactory;
using Coupons.PepsiKSA.Api.Integrations.MailService;
using Coupons.PepsiKSA.Api.Integrations.SaleForce;
using Coupons.PepsiKSA.Api.Presistence;
using Coupons.PepsiKSA.Api.Presistence.Repository;
using Coupons.PepsiKSA.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Coupons.PepsiKSA.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("DefaultCon")));
            services.AddSingleton<ILog, Log>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IHttpFactory, HttpFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICouponsService, CouponsService>();
            services.AddScoped<IEcouponsService, EcouponsService>();
            services.AddScoped<ISaleForceService, SaleForceService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IFileExporter, FileExporter>();
            services.AddScoped<ISFTPUploader, SFTPUploader>();
            //services.AddScoped<IWinSCP, WinSCP>();

            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen();

            #region Localization
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                 options =>
                 {
                     var supportedCultures = new List<CultureInfo>
                     {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar-SA")
                     };

                     options.DefaultRequestCulture = new RequestCulture(culture: "ar-SA", uiCulture: "ar-SA");
                     options.SupportedCultures = supportedCultures;
                     options.SupportedUICultures = supportedCultures;
                     options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider { IndexOfCulture = 2, IndexofUICulture = 2 } };
                 });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });
            #endregion
            #region requestLimiter
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion
            #region TokenValidationRoles
            var key = Encoding.ASCII.GetBytes
                        (Configuration.GetSection("AppSetting:Token").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(optyion =>
                {
                    optyion.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true

                    };
                });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pepsi V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseCors(options =>
            {
                options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials();
            });


            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();


            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseIpRateLimiting();
            app.UseClientRateLimiting();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{culture:culture}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
