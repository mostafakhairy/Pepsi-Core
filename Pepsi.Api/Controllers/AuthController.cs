using Coupons.PepsiKSA.Api.Core;
using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Api.Integrations.E_Coupons;
using Coupons.PepsiKSA.Api.Integrations.SaleForce;
using Coupons.PepsiKSA.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Controllers
{
    [Route("api/{culture:culture}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IEcouponsService _ecouponsService;
        private readonly IUserRepository _userRepository;
        private readonly ISaleForceService _saleForceService;
        private readonly ILog _logger;

        public AuthController(IAuthRepository authRepo, IStringLocalizer<Resource> localizer,
            IEcouponsService ecouponsService, IUserRepository userRepository, ISaleForceService saleForceService, ILog logger)
        {
            _authRepo = authRepo;
            _localizer = localizer;
            _ecouponsService = ecouponsService;
            _userRepository = userRepository;
            _saleForceService = saleForceService;
            _logger = logger;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterDto registerdUser)
        {
            if (await _authRepo.EmailExist(registerdUser.Email)) return BadRequest(_localizer["EmailExist"].Value);
            if (await _authRepo.UserExist(registerdUser.MobileNumber)) return BadRequest(_localizer["UserExist"].Value);
            var lang = Request.Path.Value.Split('/')[2]?.ToString() == "ar-SA" ? "Arabic" : "English";
            var newUser = await _authRepo.Register(registerdUser);

            _ = _saleForceService.SaleForceAuthToken().ContinueWith((res) =>
            {
                if (res.Exception == null)
                {
                    _logger.Information($"Sales Force Respons Status Expiration: {res.Result.expires_in}");
                    _saleForceService.SaleForceSendMail(registerdUser, res.Result.access_token, lang).ContinueWith(res =>
                    {
                        if (res.Exception != null)
                            _logger.Error(res.Exception, $"Sales Force Send Mail Exception: {res.Exception.Message}");
                        else
                            _logger.Information($"Sales Force Send Mail Repsonse: {res.Result.responses.FirstOrDefault()}");
                    });
                }
                else
                {
                    _logger.Error(res.Exception, $"Sales Force Auth Token Exception: {res.Exception.Message}");
                }
            });
            try
            {
                var registerResponse = await _ecouponsService.EcouponsRegister(registerdUser);
                if (registerResponse.ResponseHeader.ResponseStatus == 0)
                {
                    await _userRepository.UpdateUserStatus(newUser.MobileNumber, true);
                }
                _logger.Information($"Ecoupons Respons Status: {registerResponse.ResponseHeader.ResponseStatus}," +
                   $" Message: {registerResponse.ResponseHeader.Message} ");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ecoupons Error: {ex.Message}");
            }
            var token = _authRepo.GenerateToken(newUser);
            return Ok(token);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginUser)
        {
            var user = await _authRepo.Login(loginUser);
            if (user == null) return Unauthorized();

            var token = _authRepo.GenerateToken(user);
            return Ok(token);

        }
        [HttpGet]
        [Route("isUserRegistered")]
        public async Task<IActionResult> IsUserRegistered(string mobileNumber)
        {
            return Ok(await _authRepo.UserExist(mobileNumber));
        }

        [HttpGet]
        [Route("GenerateOTP")]
        public async Task<IActionResult> GenerateOTP(string email)
        {
            var generated = await _authRepo.GenerateOTP(email);

            if (generated) return Ok(generated);
            else return BadRequest(_localizer["GenerateOTP"].Value);
        }
        [HttpGet]
        [Route("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP(string email, int otp)
        {
            var isOTPValid = await _authRepo.ValidateOTP(email, otp);
            if (!isOTPValid) return BadRequest(_localizer["ValidateOTP"].Value);

            return Ok(isOTPValid);
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto req)
        {

            var result = await _authRepo.ResetPassword(req);
            if (!result) return BadRequest(_localizer["ResetPassword"].Value);

            return Ok(result);
        }

    }
}
