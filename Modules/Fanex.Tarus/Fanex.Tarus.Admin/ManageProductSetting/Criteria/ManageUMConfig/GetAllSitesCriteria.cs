namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;

    public class GetAllSitesCriteria : CriteriaBase
    {
        public GetAllSitesCriteria(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }

        public override string GetSettingKey()
            => "CustomerSetting_SiteSel";

        public override bool IsValid()
            => UserId > 0;
    }
}
