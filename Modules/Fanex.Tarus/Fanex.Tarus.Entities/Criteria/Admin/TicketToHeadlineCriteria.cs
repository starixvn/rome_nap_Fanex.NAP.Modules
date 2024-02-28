using Fanex.NAP.Framework.Data;
using System;

namespace Fanex.Tarus.Entities.Criteria
{
    [Serializable]
    public class TicketToHeadlineCriteria : CriteriaBase
    {
        public TicketToHeadlineCriteria() { }

        public TicketToHeadlineCriteria(TicketToHeadlineCached cached)
        {
            ExportExcel = cached.ExportExcel;
            UserId = cached.UserId;
            File = cached.File;
            HeadlineType = cached.HeadlineType;
            ProductId = cached.ProductId;
            MatchId = cached.MatchId;
            MessageId = cached.MessageId;
            ActualResult = cached.ActualResult;
            WrongResult = cached.WrongResult;
            PageIndex = cached.PageIndex;
            PageSize = cached.PageSize;
        }
        public int UserId { get; set; }
        public bool ExportExcel { get; set; }

        public string File { get; set; }

        public byte HeadlineType { get; set; }

        public int ProductId { get; set; }

        public int MatchId { get; set; }

        public int MessageId { get; set; }

        public string ActualResult { get; set; }

        public string WrongResult { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelGroupHeadlineByRefno";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
