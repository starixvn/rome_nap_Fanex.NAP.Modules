namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    using Fanex.NAP.Framework.Data;
    
    public class SelectSbsmCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "pMutilProductID")]
        public string ProductIds { get; set; }

        [SpParam(SqlParamName = "pMutilSportType")]
        public string SportTypes { get; set; }

        [SpParam(SqlParamName = "pMutilBetType")]
        public string BetTypes { get; set; }

        public bool IsLicensee { get; set; }

        public bool IsLive { get; set; }

        public int ExportData { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRows { get; set; }

        public override string GetSettingKey() => "Acc_ReferenceSetting_Sel";
    }
}
