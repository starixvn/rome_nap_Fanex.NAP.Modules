using System.Collections.Generic;

namespace Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper
{
    public class MaxMemberLimitBySuperUpdateResultEntity
    {
        public int CustId { get; set; }

        public string UserName { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }

    public class MaxMemberLimitBySuperUpdateResultModel
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public List<MaxMemberLimitBySuperUpdateResultEntity> Data { get; set; }
    }
}
