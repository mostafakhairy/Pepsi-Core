using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class UserReportDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        [JsonProperty("SMSOpt-In")]
        public bool InitialSmsOptIn { get; set; }
        [JsonProperty("EmailOpt-In")]
        public bool InitialEmailOptIn { get; set; }
        public string LanguagePreference { get; set; }
        public string Country { get; set; }
        public string AccountRegistrationDate { get; set; }
        public string CurrentPoints { get; set; }
        public DateTime LastActivity_UpdateDate { get; set; }
    }
}
