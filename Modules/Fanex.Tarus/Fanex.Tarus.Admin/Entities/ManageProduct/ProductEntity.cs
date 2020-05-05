namespace Fanex.Tarus.Admin.Entities.ManageProduct
{
    public class ProductEntity
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductPrefix { get; set; }
        public bool Enabled { get; set; }
        public string EndOfSupportDate { get; set; }
        public string BalanceType { get; set; }
        public string SubProductBalanceType { get; set; }
        public string GroupType { get; set; }
        public string CasinoType { get; set; }
        public bool IsOfferPlayForFun { get; set; }
        public string VendorID { get; set; }
        public string OrderNo { get; set; }
    }
}