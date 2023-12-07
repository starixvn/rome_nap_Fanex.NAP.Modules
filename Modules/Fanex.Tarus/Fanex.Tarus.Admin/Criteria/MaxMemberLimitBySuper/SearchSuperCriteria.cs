namespace Fanex.Tarus.Admin.Criteria.MaxMemberLimitBySuper
{
    using Fanex.NAP.Framework.Data;
    using Sunplus.Accounting.Core.Common;

    public class SearchSuperCriteria : CriteriaBase
    {
        public int UserId
        {
            get
            {
                return AppSession.AccId;
            }
        }

        public string SuperName { get; set; }

        public string Site { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_MaxMemberLimitBySuperSel";
        }

        public override bool IsValid()
        {
            return PageSize > 0 && PageIndex > 0 && !string.IsNullOrWhiteSpace(Site);
        }
    }
}
