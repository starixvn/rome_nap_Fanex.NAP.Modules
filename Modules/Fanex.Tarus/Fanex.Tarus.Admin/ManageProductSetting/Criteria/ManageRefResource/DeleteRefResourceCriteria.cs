namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageRefResource
{
    using Fanex.NAP.Framework.Data;
    
    public class DeleteRefResourceCriteria : CriteriaBase
    {
        public DeleteRefResourceCriteria(string refId, string resourceId, int userId, string loginIP)
        {
            RefId = refId;
            ResourceId = resourceId;
            UserID = userId;
            LoginIP = loginIP;
        }

        public string RefId { get; set; }

        public string ResourceId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public int UserID { get; set; }

        public string LoginIP { get; set; }

        public override string GetSettingKey()
         => "Acc_RefResource_Del";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(RefId) && !string.IsNullOrWhiteSpace(ResourceId);
    }
}
