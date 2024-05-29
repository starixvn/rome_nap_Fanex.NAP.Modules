namespace Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping
{
    using Fanex.NAP.Framework.Data;

    public class GetCreditCurrencyCriteria : CriteriaBase
    {
        public int UserID { get; set; }

        public override string GetSettingKey()
            => "Acc_CurrencyList_Sel";
    }
}
