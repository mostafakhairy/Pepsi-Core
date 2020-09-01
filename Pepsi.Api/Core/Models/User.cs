using Coupons.PepsiKSA.Api.Core.Models;
using System;
using System.Collections.Generic;

namespace Coupons.PepsiKSA.Api.Presistence.Models
{
    public class User
    {
        public User()
        {
            this.UserTransactions = new List<UserTransaction>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int Points { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public bool IsEcouponsVerified { get; set; }
        public int? ResetPasswordOTP { get; set; }
        public DateTime? ResetPasswordOTPDate { get; set; }
        public bool IsSubscribedMail { get; set; }
        public bool IsSubscribedSms { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public ICollection<UserTransaction> UserTransactions { get; set; }
    }
}
