
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Integrations.HttpFactory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.SaleForce
{
    public class SaleForceService : ISaleForceService
    {
        private readonly IHttpFactory _httpFactory;
        private readonly IConfiguration _configuration;
        public SaleForceService(IHttpFactory httpFactory, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _configuration = configuration;
        }
        public async Task<SaleForceAuthTokenResponse> SaleForceAuthToken()
        {
            var url = $"{_configuration.GetSection("SaleForce:AuthUrl").Value}";
            var requestBody = new SaleForceAuthTokenRequest()
            {
                grant_type = $"{_configuration.GetSection("SaleForce:grant_type").Value}",
                client_id = $"{_configuration.GetSection("SaleForce:client_id").Value}",
                client_secret = $"{_configuration.GetSection("SaleForce:client_secret").Value}",
                account_id = $"{_configuration.GetSection("SaleForce:account_id").Value}",
            };
            return await _httpFactory.ClientPostAsync<SaleForceAuthTokenRequest, SaleForceAuthTokenResponse>(url, requestBody);

        }

        public async Task<SaleForceSendEmailResponse> SaleForceSendMail(UserRegisterDto registerdUser, string token, string lang = "Arabic", bool mailOPT = true, bool smsOPT = true)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + token }
            };
            var url = $"{_configuration.GetSection("SaleForce:SendEmailUrl").Value}";
            var requestBody = new SaleForceSendEmailRequest
            {
                To = new To
                {
                    Address = registerdUser.Email,
                    SubscriberKey = registerdUser.Email,
                    ContactAttributes = new ContactAttributes
                    {
                        SubscriberAttributes = new SubscriberAttributes
                        {
                            FirstName = registerdUser.FirstName,
                            LastName = registerdUser.LastName,
                            MobileNumber = registerdUser.MobileNumber,
                            Country = "Saudi Arabia",
                            InitialEmailOptin = mailOPT,
                            SMSOptin = smsOPT,
                            LanguagePreference = lang,
                            AccountRegistrationDate = DateTime.Now
                        }
                    }
                }
            };

            return await _httpFactory.ClientPostAsync<SaleForceSendEmailRequest, SaleForceSendEmailResponse>(url, requestBody, headers);
        }
    }
}
