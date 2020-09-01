using System.Collections.Generic;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class ECouponBurnResponse
    {
        public List<object> Errors { get; set; }
        public Result Result { get; set; }
    }
    public class ResponseHeader
    {
        public int ResponseStatus { get; set; }
        public string Message { get; set; }
    }

    public class ResponseBody
    {
        public string Barcode { get; set; }
    }

    public class Result
    {
        public ResponseHeader ResponseHeader { get; set; }
        public ResponseBody ResponseBody { get; set; }
    }

}
