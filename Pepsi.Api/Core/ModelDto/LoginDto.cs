using System.ComponentModel.DataAnnotations;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class LoginDto
    {
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
