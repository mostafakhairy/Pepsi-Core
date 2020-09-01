using AutoMapper;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Integrations.HttpFactory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.E_Coupons
{
    public class EcouponsService : IEcouponsService
    {
        private readonly IHttpFactory _httpFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public EcouponsService(IHttpFactory httpFactory, IConfiguration configuration, IMapper mapper)
        {
            _httpFactory = httpFactory;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<EcouponsRegisterResponseDto> EcouponsRegister(UserRegisterDto registeredUser)
        {
            var headers = new Dictionary<string, string>
            {
                { _configuration.GetSection("Ecoupons:APIKey").Key, _configuration.GetSection("Ecoupons:APIKey").Value },
                { _configuration.GetSection("Ecoupons:ServiceKey").Key, _configuration.GetSection("Ecoupons:ServiceKey").Value },
                { _configuration.GetSection("Ecoupons:Password").Key, _configuration.GetSection("Ecoupons:Password").Value },
                { _configuration.GetSection("Ecoupons:RequestType").Key, _configuration.GetSection("Ecoupons:RequestType").Value },
                { "ReferenceNo", new Random().Next(0, 10000000).ToString() }
            };
            var url = $"{_configuration.GetSection("Ecoupons:Url").Value}/eCoponAPI/RegisterPepsi";
            var user = _mapper.Map<EcouponRegisterDto>(registeredUser);
            return await _httpFactory.ClientPostAsync<EcouponRegisterDto, EcouponsRegisterResponseDto>(url, user, headers);
        }
        public async Task<ECouponBurnResponse> EBurn(ECouponBurnRequest body)
        {
            var headers = new Dictionary<string, string>
            {
                { _configuration.GetSection("Ecoupons:APIKey").Key, _configuration.GetSection("Ecoupons:APIKey").Value },
                { _configuration.GetSection("Ecoupons:ServiceKey").Key, _configuration.GetSection("Ecoupons:ServiceKey").Value },
                { _configuration.GetSection("Ecoupons:Password").Key, _configuration.GetSection("Ecoupons:Password").Value },
                { _configuration.GetSection("Ecoupons:RequestType").Key, _configuration.GetSection("Ecoupons:RequestType").Value },
                { "ReferenceNo", new Random().Next(0, 10000000).ToString() }
            };
            var url = $"{_configuration.GetSection("Ecoupons:Url").Value}/eCoponAPI/participatepepsi";
            return await _httpFactory.ClientPostAsync<ECouponBurnRequest, ECouponBurnResponse>(url, body, headers);
        }
    }
}
