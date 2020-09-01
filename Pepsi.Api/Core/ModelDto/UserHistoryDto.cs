using System.Collections.Generic;

namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class UserHistoryDto
    {
        public int TotalCount { get; set; }
        public List<VoucherDetailsDto> VoucherDetails { get; set; }
    }
}
