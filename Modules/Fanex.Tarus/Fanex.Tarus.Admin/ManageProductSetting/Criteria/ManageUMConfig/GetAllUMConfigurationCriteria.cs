namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageUMConfig
{
    using Fanex.NAP.Framework.Data;
    using System;

    public class GetAllUMConfigurationCriteria : CriteriaBase
    {
        public GetAllUMConfigurationCriteria(int functionType, string beginTimeString, string endTimeString)
        {
            FunctionType = functionType;
            if (DateTime.TryParse(beginTimeString, out DateTime beginTime))
            {
                BeginTime = beginTime;
            }
            if (DateTime.TryParse(endTimeString, out DateTime endTime))
            {
                EndTime = endTime.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
        }

        public int FunctionType { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public override string GetSettingKey()
            => "Acc_UMTool_Configuration_Sel";

        public override bool IsValid()
            => true;
    }
}
