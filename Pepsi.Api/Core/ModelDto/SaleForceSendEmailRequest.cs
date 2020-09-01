using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class SaleForceSendEmailRequest
    {
        public To To { get; set; }
    }
    public class SubscriberAttributes
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonProperty("EmailOpt-in")]
        public bool InitialEmailOptin { get; set; }
        public string MobileNumber { get; set; }
        [JsonProperty("SMSOpt-in")]
        public bool SMSOptin { get; set; }
        public string LanguagePreference { get; set; }
        public string Country { get; set; }
        public DateTime AccountRegistrationDate { get; set; }
    }

    public class ContactAttributes
    {
        public SubscriberAttributes SubscriberAttributes { get; set; }
    }

    public class To
    {
        public string Address { get; set; }
        public string SubscriberKey { get; set; }
        public ContactAttributes ContactAttributes { get; set; }
    }

}
