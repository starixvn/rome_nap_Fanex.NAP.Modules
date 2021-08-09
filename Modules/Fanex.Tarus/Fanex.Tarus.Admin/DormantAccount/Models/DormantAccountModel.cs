using System.Collections.Generic;

namespace Fanex.Tarus.Admin.DormantAccount.Models
{
    public class DormantAccountModel
    {
        public DormantAccountModel()
        {
            Breadscrumb = new DormantBreadscrumbModel();
            ByUsername = new List<DormantUsernameModel>();
            ByDateType = new List<DormantDateTypeModel>();
            Balance = new List<DormantBalanceModel>();
        }

        public DormantBreadscrumbModel Breadscrumb { get; set; }

        public IList<DormantUsernameModel> ByUsername { get; set; }

        public IList<DormantDateTypeModel> ByDateType { get; set; }

        public IList<DormantBalanceModel> Balance { get; set; }

        public int TotalItem { get; set; }
    }
}
