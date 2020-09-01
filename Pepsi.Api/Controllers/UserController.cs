using Coupons.PepsiKSA.Api.Core.IRepository;
using Coupons.PepsiKSA.Api.Core.ModelDto;
using Coupons.PepsiKSA.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Controllers
{
    [Route("api/{culture:culture}/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        public UserController(IUserRepository userRepository, IAuthRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }
        [HttpPatch]
        [Route("UpdateSubscription")]
        public async Task<IActionResult> UpdateSubscriptions(UserSubscriptionsDto userSubscriptions)
        {
            var mobileNumber = User.Claims.FirstOrDefault(c => c.Type == "mobileNumber").Value;
            var user = await _userRepository.UpdateSubsciptions(mobileNumber, userSubscriptions);
            var token = _authRepository.GenerateToken(user);

            return Ok(token);
        }
        [HttpGet]
        [Route("UsersReportJob")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersReport()
        {
            //WinSCP.UploadFiles();
            await _userRepository.GetUsersReport();

            return Ok();
        }
    }
}
