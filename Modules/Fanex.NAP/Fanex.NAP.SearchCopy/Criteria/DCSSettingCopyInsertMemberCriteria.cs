namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using Fanex.NAP.Framework.Data;

    internal class DCSSettingCopyInsertMemberCriteria : CriteriaBase
    {
        public DCSSettingCopyInsertMemberCriteria(int custId, int existingCustId, int userId, string ip, byte settingStatus)
        {
            CustId = custId;
            ExistingCustId = existingCustId;
            UserId = userId;
            IP = ip;
            SettingStatus = settingStatus;
        }

        public int CustId { get; }

        public int ExistingCustId { get; }

        public int UserId { get; }

        public string IP { get; }

        public int SubAccId { get; } = 0;

        public byte SettingStatus { get; set; }

        public int ErrCode { get; } = 0;

        public string ErrMsg { get; } = string.Empty;

        public override string GetSettingKey()
            => "Acc_CustSetting_DCSSettingCopyInsertMember";

        public override bool IsValid()
            => CustId > 0 && ExistingCustId > 0;
    }
}
