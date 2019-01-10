namespace Fanex.Tarus.Admin.Criteria.MaxMemberLimitBySuper
{
    using Fanex.NAP.Framework.Data;
    using Infrastructure.Configurations;
    using Sunplus.Accounting.Core.Common;

    public class GetSitesCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string System { get; set; }

        public bool IncludeCN88 => true;

        public override string GetSettingKey()
        {
            return "CustomerSetting_SiteSel";
        }

        public override bool IsValid()
        {
            return UserId > 0;
        }
    }
}
