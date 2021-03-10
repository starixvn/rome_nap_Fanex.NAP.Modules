using Fanex.NAP.Framework.Data;
using System;

namespace Fanex.Tarus.Entities.Criteria
{
    [Serializable]
    public class TicketToHeadlineCached 
    {
        public TicketToHeadlineCached()
        {
        }

        public TicketToHeadlineCached(TicketToHeadlineCriteria criteria)
        {
            ExportExcel = criteria.ExportExcel;
            File = criteria.File;
            HeadlineType = criteria.HeadlineType;
            ProductId = criteria.ProductId;
            MatchId = criteria.MatchId;
            ActualResult = criteria.ActualResult;
            WrongResult = criteria.WrongResult;
            PageIndex = criteria.PageIndex;
            PageSize = criteria.PageSize;
        }

        public bool ExportExcel { get; set; }

        public string File { get; set; }

        public byte HeadlineType { get; set; }

        public int ProductId { get; set; }

        public int MatchId { get; set; }

        public string ActualResult { get; set; }

        public string WrongResult { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }
    }
}
