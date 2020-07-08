namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageRefResource
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class InsertRefResourceCriteria : CriteriaBase
    {
        public InsertRefResourceCriteria()
        {
            RefResources = new List<RefResourceEntity>();
        }

        [SpParam(Ignored = true)]
        public List<RefResourceEntity> RefResources { get; set; }

        public string File
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("<ROOT>");
                foreach (var RefResource in RefResources)
                {
                    builder.Append("<File ");
                    builder.Append("RefId = \"" + RefResource.RefId + "\" ");
                    builder.Append("ResourceId = \"" + RefResource.ResourceId + "\" ");
                    builder.Append("Resource_en = \"" + RefResource.ResourceEN + "\" ");
                    builder.Append("Resource_ch = \"" + RefResource.ResourceCH + "\" ");
                    builder.Append("Resource_cs = \"" + RefResource.ResourceCS + "\" ");
                    builder.Append("Resource_jp = \"" + RefResource.ResourceJP + "\" ");
                    builder.Append("Resource_it = \"" + RefResource.ResourceIT + "\" ");
                    builder.Append("Resource_th = \"" + RefResource.ResourceTH + "\" ");
                    builder.Append("Resource_ko = \"" + RefResource.ResourceKO + "\" ");
                    builder.Append("Resource_es = \"" + RefResource.ResourceES + "\" ");
                    builder.Append("Sort1 = \"" + RefResource.Sort1 + "\" ");
                    builder.Append("Sort2 = \"" + RefResource.Sort2 + "\" ");
                    builder.Append("Resource_el = \"" + RefResource.ResourceEL + "\" ");
                    builder.Append("Resource_vn = \"" + RefResource.ResourceVN + "\" ");
                    builder.Append("Resource_pl = \"" + RefResource.ResourcePL + "\" ");
                    builder.Append("Resource_id = \"" + RefResource.ResourceID + "\" ");
                    builder.Append("Resource_hi = \"" + RefResource.ResourceHI + "\" ");
                    builder.Append("Resource_mm = \"" + RefResource.ResourceMM + "\" ");
                    builder.Append("Resource_fr = \"" + RefResource.ResourceFR + "\" ");
                    builder.Append("Resource_ru = \"" + RefResource.ResourceRU + "\" ");
                    builder.Append("Resource_pt = \"" + RefResource.ResourcePT + "\" ");
                    builder.Append("Resource_de = \"" + RefResource.ResourceDE + "\" ");
                    builder.Append("/>");
                }

                builder.Append("</ROOT>");

                return builder.ToString();
            }
        }
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public int UserId { get; set; }

        public string LoginIP { get; set; }

        public override string GetSettingKey()
            => "Acc_RefResource_Ins";

        public override bool IsValid()
            => true;
    }
}
