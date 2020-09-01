namespace Coupons.PepsiKSA.Api.Core.ModelDto
{
    public class DealDto
    {
        public decimal Discount { get; set; }
        public string Image { get; set; }
        public string Merchant { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string OfferNumber { get; set; }
        public string OfferType { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public int Externalpoints { get; set; }
        public string Description { get; set; }

    }
}
