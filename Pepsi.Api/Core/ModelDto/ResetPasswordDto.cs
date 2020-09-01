using System.ComponentModel.DataAnnotations;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int OTP { get; set; }
    }
}
