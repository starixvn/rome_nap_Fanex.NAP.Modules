using System.Collections.Generic;
using Fanex.Tarus.Admin.ManageProductSetting.Entities;

namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix
{
    public class CheckSbsmCriteria : InsertSbsmCriteria
    {
        public CheckSbsmCriteria(IEnumerable<SbsmEntity> entities, int isNew, int isSingleCheck) : base(entities)
        {
            IsNew = isNew;
            IsSingleCheck = isSingleCheck;
        }

        public int IsNew { get; set; }

        public int IsSingleCheck { get; set; }

        public override string GetSettingKey()
            => "Acc_ReferenceSetting_Check";
    }
}
