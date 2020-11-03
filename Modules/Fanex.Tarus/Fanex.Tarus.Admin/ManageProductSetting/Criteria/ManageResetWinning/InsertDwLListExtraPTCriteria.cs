namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class InsertDwLListExtraPTCriteria : CriteriaBase
    {
        public InsertDwLListExtraPTCriteria()
        {
            DwLListExtraPT = new List<DwLListExtraPTEntity>();
        }

        [SpParam(Ignored = true)]
        public List<DwLListExtraPTEntity> DwLListExtraPT { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var DwLListExtraPTItem in DwLListExtraPT)
                {
                    builder.Append("<File ");
                    builder.Append("CustID = \"" + DwLListExtraPTItem.CustId + "\" ");
                    builder.Append("GroupID = \"" + DwLListExtraPTItem.GroupId + "\" ");
                    builder.Append("GroupName = \"" + DwLListExtraPTItem.GroupName + "\" ");
                    builder.Append("IsDisabled = \"" + DwLListExtraPTItem.IsDisabled + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");

                return builder.ToString();
            }
        }

        public string LoginIP { get; set; }

        public int UserId { get; set; }

        public bool IsTest { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
            => "Acc_DWLList_ExtraPT_Ins";

        public override bool IsValid()
            => UserId > 0;
    }
}
