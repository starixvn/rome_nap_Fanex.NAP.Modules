namespace Fanex.Tarus.Admin.Criteria.MaxMemberLimitBySuper
{
    using Fanex.NAP.Framework.Data;
    using Sunplus.Accounting.Core.Common;
    using Sunplus.Accounting.Core.Helper;

    public class UpdateSuperCriteria : CriteriaBase
    {
        public string SuperNames { get; set; }

        public int MaxMemberLimit { get; set; }

        public int UserId
        {
            get
            {
                return AppSession.AccId;
            }
        }

        public string IP
        {
            get
            {
                return Utils.GetClientIP();
            }
        }

        public string Remark { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_MaxMemberLimitBySuperUpd";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(SuperNames)
                && SuperNames.Length <= 3000
                && MaxMemberLimit >= -1
                && MaxMemberLimit <= 999999
                && UserId > 0
                && (string.IsNullOrWhiteSpace(Remark) || Remark.Length > 300);
        }
    }
}
