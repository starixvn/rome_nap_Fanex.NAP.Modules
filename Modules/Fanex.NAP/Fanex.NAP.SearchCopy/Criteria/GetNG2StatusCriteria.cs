namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using Fanex.NAP.Framework.Data;

    internal class GetNG2StatusCriteria : CriteriaBase
    {
        public GetNG2StatusCriteria(int custId, byte settingStatus)
        {
            CustId = custId;
            SettingStatus = settingStatus;
        }

        public int CustId { get; }

        public int ProductId { get; } = 23;

        public byte SettingStatus { get; set; }

        public override string GetSettingKey()
            => "Acc_CustSetting_DisableGameStatusSelect";

        public override bool IsValid()
            => CustId > 0;
    }
}