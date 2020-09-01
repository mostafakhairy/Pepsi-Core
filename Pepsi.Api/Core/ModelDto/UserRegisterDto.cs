using System.ComponentModel.DataAnnotations;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class UserRegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression("^05[0-9]{8}$")]
        public string MobileNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsSubscribedMail { get; set; }
        public bool IsSubscribedSms { get; set; }
        public string Language { get; set; }
    }
}
