using Coupons.PepsiKSA.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Coupons.PepsiKSA.Api.Controllers
{
    [ApiController]
    [Route("api/{culture:culture}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IStringLocalizer<Resource> _localizer;

        public WeatherForecastController(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;

        }

        [HttpGet]
        public string Get()
        {
            return _localizer["Hello"].Value;
        }
        [HttpPost]
        public string Post()
        {
            return "hello";
        }
    }
}
