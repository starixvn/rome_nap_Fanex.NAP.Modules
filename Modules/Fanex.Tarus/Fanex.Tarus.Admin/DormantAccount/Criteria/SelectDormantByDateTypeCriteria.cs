namespace Fanex.Tarus.Admin.DormantAccount.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.DormantAccount.Request;

    public class SelectDormantByDateTypeCriteria : CriteriaBase
    {
        public SelectDormantByDateTypeCriteria(SelectDormantAccountRequest request)
        {
            FilterRoleId = request.RoleId;
            FilterHeadlineType = request.HeadlineType;
            FilterHeadlineGroup = request.HeadlineGroup;
            NotAssignGroupOnly = request.NotAssignGroupOnly;
            FilterStageID = request.ArchiveStage;
            FilterDateType = request.DateType;
            FromDate = request.FromDate;
            ToDate = request.ToDate;
            PageSize = request.PageSize;
            PageIndex = request.PageIndex;
        }

        public int TotalItems { get; set; }

        public string FilterRoleId { get; set; }

        public string FilterHeadlineType { get; set; }

        public string FilterHeadlineGroup { get; set; }

        public bool NotAssignGroupOnly { get; set; }

        public int FilterDateType { get; set; }

        public int FilterStageID { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_DormantAccountListByHeadline_Sel";
    }
}