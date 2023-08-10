namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class UpdateSbsmCriteria : InsertSbsmCriteria
    {
        public UpdateSbsmCriteria(IEnumerable<SbsmEntity> entities, int isSingleUpd) : base(entities)
        {
            IsSingleUpd = isSingleUpd;
        }

        public int IsSingleUpd { get; set; }

        public override string GetSettingKey() => "Acc_ReferenceSetting_Upd";
    }
}
