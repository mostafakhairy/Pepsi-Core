
using AutoMapper.QueryableExtensions;
using Coupons.PepsiKSA.Api.Core;
using Coupons.PepsiKSA.Api.Core.Enums;
using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Integrations.Coupons;
using Coupons.PepsiKSA.Api.Integrations.E_Coupons;
using Coupons.PepsiKSA.Api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Controllers
{
    [Route("api/{culture:culture}/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICouponsService _couponsService;
        private readonly IEcouponsService _ecouponsService;
        private readonly IAuthRepository _authRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILog _logger;
        public CouponsController(IUserRepository userRepository, ICouponsService couponsService,
            IStringLocalizer<Resource> localizer, ILog logger, ITransactionRepository transactionRepository,
            IAuthRepository authRepository, IEcouponsService ecouponsService)
        {
            _userRepository = userRepository;
            _couponsService = couponsService;
            _authRepository = authRepository;
            _ecouponsService = ecouponsService;
            _localizer = localizer;
            _logger = logger;
            _transactionRepository = transactionRepository;
        }
        [HttpPost]
        [Route("Burn")]
        public async Task<IActionResult> BurnCoupon(string offerNumber)
        {
            var lang = Request.Path.Value.Split('/')[2]?.ToString() == "ar-SA" ? "ar" : "en";
            var mobileNumber = User.Claims.FirstOrDefault(c => c.Type == "mobileNumber").Value;
            var burnResponse = await _couponsService.BurnCouponsAsync(mobileNumber, offerNumber, lang == "ar"? "2": "1");
            if (burnResponse.ErrorCode != "0")
            {
                _logger.Information($"Coupons Burn Response Message: {burnResponse.ErrorMessage}, ErrorCode: {burnResponse.ErrorCode}");
                burnResponse.ErrorMessage  = _localizer["CouponsError"];
                if (burnResponse.ErrorCode == "4")
                    burnResponse.ErrorMessage = _localizer["CouponBurnedBefore"];
                if (burnResponse.ErrorCode == "2")
                    burnResponse.ErrorMessage = _localizer["WorngCouponNumber"];

                return BadRequest(burnResponse.ErrorMessage);
            }
            _ = _ecouponsService.EBurn(new ECouponBurnRequest { Mobile = mobileNumber, Barcode = offerNumber })
                 .ContinueWith(res =>
                 {
                     if (res.Exception != null)
                         _logger.Error(res.Exception, $"Ecoupons Error: {res.Exception.Message}");
                     else
                         _logger.Information($"Ecoupons Burn Response Message: {res.Result.Result.ResponseHeader.Message}");
                 });

            var user = await _userRepository.UpdatePoints(mobileNumber, 1);
            var isAdded = await _transactionRepository.AddTransaction(user, TransactionType.Burn, user.Points - 1, offerNumber, "", "");
            if (!isAdded)
            {
                _logger.Information($"Add Transaction Failed For Mobile: {user.MobileNumber}, Type: Burn, OfferNumber: {offerNumber}");
            }
            var token = _authRepository.GenerateToken(user);
            var res = await _couponsService.GetTierOffers(user.Points, lang);
            var offers = res.Deals;
            return Ok(new { token, burnResponse, offers });
        }
        [HttpGet]
        [Route("GetUserHistory")]
        public async Task<IActionResult> GetUserHistory(int pageIndex, int pageSize)
        {
            var mobileNumber = User.Claims.FirstOrDefault(c => c.Type == "mobileNumber").Value;
            var lang = Request.Path.Value.Split('/')[2]?.ToString() == "ar-SA" ? "ar" : "en";
            var historyResult = await _couponsService.GetUserHistoryAsync(mobileNumber, pageIndex, pageSize, lang);
            return Ok(historyResult);
        }
        [HttpGet]
        [Route("GetTiers")]
        public async Task<IActionResult> GetTiers()
        {
            var res = await _couponsService.GetTiers();
            return Ok(res.Tiers);
        }
        [HttpGet]
        [Route("Subscribe")]
        public async Task<IActionResult> Subscribe(string offerNumber, int externalPoints, string categoryId)
        {
            if (externalPoints < 1)
                return BadRequest(_localizer["InvalidRequest"].Value);

            var mobileNumber = User.Claims.FirstOrDefault(c => c.Type == "mobileNumber").Value;
            var points = _userRepository.GetUser(mobileNumber).Result.Points;
            var user = await _userRepository.UpdatePoints(mobileNumber, externalPoints, true);
            var lang = Request.Path.Value.Split('/')[2]?.ToString() == "ar-SA" ? "2" : "1";
            var res = new CouponDetailsDto();
            try
            {
                 res = await _couponsService.SubscribeToOffer(mobileNumber, offerNumber, points, lang);
                if (res.ErrorCode == 0)
                {
                    _logger.Information($"Coupons Subscribe Response Message: {res.Message}, ErrorCode: {res.ErrorCode}");

                    var isAdded = await _transactionRepository.AddTransaction(user, TransactionType.Subscription,
                        user.Points + externalPoints, res.CouponNumber, categoryId, res.OfferName);
                    if (!isAdded)
                    {
                        _logger.Information($"Add Transaction Failed For Mobile: {user.MobileNumber}," +
                            $" Type: Subscription, OfferNumber: {offerNumber}, CategoryId: {categoryId}");
                    }
                }
                else if (res.ErrorCode >= 1 && res.ErrorCode != 11)
                {
                    _logger.Information($"Rollback User Redemption points, UserPoints to rollback: {externalPoints}");
                    user = await _userRepository.UpdatePoints(mobileNumber, externalPoints);
                }
                _logger.Information($"Coupons Subscribe Response Message: {res.Message}, ErrorCode: {res.ErrorCode}");

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Coupons Subscribe Response Error Message: {ex.Message}");

            }

            var token = _authRepository.GenerateToken(user);
            return Ok(new { coupon = res, token });
        }
        [HttpGet]
        [Route("GetTierDeals")]
        public async Task<IActionResult> GetTierDeals()
        {
            var lang = Request.Path.Value.Split('/')[2]?.ToString() == "ar-SA" ? "ar" : "en";
            var mobileNumber = User.Claims.FirstOrDefault(c => c.Type == "mobileNumber").Value;
            var points = _userRepository.GetUser(mobileNumber).Result.Points;
            var res = await _couponsService.GetTierOffers(points, lang);
            return Ok(res.Deals);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ExportRewards")]
        public async Task<IActionResult> ExportRewards(string type)
        {
            var lowerType = type.ToLower();
            if (lowerType != "burn"
                && lowerType != "subscription") return BadRequest();

            var result = await _transactionRepository.ExportRewards(type);

            if (!result) return BadRequest();

            return Ok();
        }


    }

}
