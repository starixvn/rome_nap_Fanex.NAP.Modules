namespace Fanex.Tarus.Admin.Criteria.MaxMemberLimitBySuper
{
    using Fanex.NAP.Framework.Data;

    public class GetSitesCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string System { get; set; }

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
