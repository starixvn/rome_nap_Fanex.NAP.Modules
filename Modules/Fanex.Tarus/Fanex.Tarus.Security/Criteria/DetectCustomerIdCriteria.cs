using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Security.Criteria
{
    internal class DetectCustomerIdCriteria : CriteriaBase
    {
        public DetectCustomerIdCriteria(string searchText)
        {
            SearchText = searchText;
            CustId = null;
        }

        public string SearchText { get; }

        public int? CustId { get; set; }

        public override string GetSettingKey()
            => "Acc_CustomerDetectCustId";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(SearchText);
    }
}
