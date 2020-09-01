using System.Collections.Generic;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class SaleForceSendEmailResponse
    {
        public string requestId { get; set; }
        public List<Response> responses { get; set; }
    }
    public class Response
    {
        public string recipientSendId { get; set; }
        public bool hasErrors { get; set; }
        public List<string> messages { get; set; }
    }

}
