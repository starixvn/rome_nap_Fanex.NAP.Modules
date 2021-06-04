namespace Fanex.Tarus.Admin.ManageProduct3RD.Models
{
    public class SelectProduct3RDRequest
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string ProductPrefix { get; set; }

        public bool? OfferSingle { get; set; }

        public bool? ApplyDCS { get; set; }

        public int OfferSite { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
