namespace Fanex.Tarus.Admin.DormantAccount.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.DormantAccount.Request;

    public class DrilldownDormantByDateTypeCriteria : CriteriaBase
    {
        public DrilldownDormantByDateTypeCriteria(SelectDormantAccountRequest request)
        {
            FilterHeadlineType = request.HeadlineType;
            FilterHeadlineGroup = request.HeadlineGroup;
            NotAssignGroupOnly = request.NotAssignGroupOnly;
            FilterUsername = request.Username;
            FilterRoleId = request.RoleId;
            FilterStageID = request.ArchiveStage;
            FilterDateType = request.DateType;
            FromDate = request.FromDate;
            ToDate = request.ToDate;
            PageSize = request.PageSize;
            PageIndex = request.PageIndex;
        }

        public string FilterUsername { get; set; }

        public string FilterHeadlineType { get; set; }

        public string FilterHeadlineGroup { get; set; }

        public bool NotAssignGroupOnly { get; set; }

        public string FilterRoleId { get; set; }

        public int FilterDateType { get; set; }

        public int FilterStageID { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public override string GetSettingKey() => "Acc_DormantDownlineListByHeadline_Sel";
    }
}