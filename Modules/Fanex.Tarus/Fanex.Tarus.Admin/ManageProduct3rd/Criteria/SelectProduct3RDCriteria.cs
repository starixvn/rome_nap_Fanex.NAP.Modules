namespace Fanex.Tarus.Admin.ManageProduct3RD.Criteria
{
    using Fanex.NAP.Framework.Data;

    public class SelectProduct3RDCriteria :CriteriaBase
    {
        [SpParam(SqlParamName = "Productid")]
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string ProductPrefix { get; set; }

        public bool? OfferSingle { get; set; }

        public bool? ApplyDCS { get; set; }

        public int OfferSite { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey()
            => "Acc_Product3rd_Sel";
    }
}
