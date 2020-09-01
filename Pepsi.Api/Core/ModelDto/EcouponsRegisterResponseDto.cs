namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class EcouponsRegisterResponseDto
    {
        public RegisterResponseHeader ResponseHeader { get; set; }
    }
    public class RegisterResponseHeader
    {
        public int ResponseStatus { get; set; }
        public string Message { get; set; }
    }
}
