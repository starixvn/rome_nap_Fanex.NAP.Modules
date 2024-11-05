namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageResetWinning
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class InsertDwLListCriteria : CriteriaBase
    {
        public InsertDwLListCriteria()
        {
            DwLList = new List<DwLListEntity>();
        }

        [SpParam(Ignored = true)]
        public List<DwLListEntity> DwLList { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var DwLListItem in DwLList)
                {
                    builder.Append("<File ");
                    builder.Append("CustID = \"" + DwLListItem.CustId + "\" ");
                    builder.Append("Seq = \"" + DwLListItem.Seq + "\" ");
                    builder.Append("ProductID = \"" + DwLListItem.ProductId + "\" ");
                    builder.Append("Product = \"" + DwLListItem.Product + "\" ");
                    builder.Append("IsDisabled = \"" + DwLListItem.IsDisabled + "\" ");
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
            => "Acc_DWLList_Ins";

        public override bool IsValid()
            => UserId > 0;
    }
}
