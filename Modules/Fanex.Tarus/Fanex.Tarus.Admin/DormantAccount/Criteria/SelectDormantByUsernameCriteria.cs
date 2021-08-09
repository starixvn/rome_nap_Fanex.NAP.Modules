namespace Fanex.Tarus.Admin.DormantAccount.Criteria
{
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.DormantAccount.Request;

    public class SelectDormantByUsernameCriteria : CriteriaBase
    {
        public SelectDormantByUsernameCriteria(SelectDormantAccountRequest request)
        {
            FilterUsername = request.Username;
            FilterHeadlineType = request.HeadlineType;
            FilterHeadlineGroup = request.HeadlineGroup;
            NotAssignGroupOnly = request.NotAssignGroupOnly;
            PageSize = request.PageSize;
            PageIndex = request.PageIndex;
        }

        public string FilterUsername { get; set; }

        public string FilterHeadlineType { get; set; }

        public string FilterHeadlineGroup { get; set; }

        public bool NotAssignGroupOnly { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_DormantDownlineListByHeadline_Sel";
    }
}