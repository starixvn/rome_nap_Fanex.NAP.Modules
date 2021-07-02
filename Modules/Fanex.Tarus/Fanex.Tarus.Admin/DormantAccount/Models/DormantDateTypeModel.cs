using Fanex.Tarus.Admin.Enums;

namespace Fanex.Tarus.Admin.DormantAccount.Models
{
    public class DormantDateTypeModel : DormantUsernameModel
    {
        public DormantDateTypeModel()
        {
            ArchivedMaster = new NumberArchivedInfo();
            ArchivedAgent = new NumberArchivedInfo();
            ArchivedMember = new NumberArchivedInfo();
        }

        public int CustHeadlineId { get; set; }

        public string HeadlineGroup { get; set; }

        public int? HeadlineType { get; set; }

        public string HeadlineTypeText
        {
            get
            {
                if (HeadlineType.HasValue)
                {
                    return HeadlineTypeConstant.List.ContainsKey(HeadlineType.Value) ?
                        HeadlineTypeConstant.List[HeadlineType.Value] : string.Empty;
                }

                return string.Empty;
            }
        }
        public NumberArchivedInfo ArchivedMaster { get; set; }

        public NumberArchivedInfo ArchivedAgent { get; set; }

        public NumberArchivedInfo ArchivedMember { get; set; }
    }
}
