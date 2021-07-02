using System;

namespace Fanex.Tarus.Admin.DormantAccount.Request
{
    public class SelectDormantAccountRequest
    {
        public bool SearchFromArchiveRoleColumn { get; set; }

        public bool SearchUsernameFromFilter { get; set; }

        public string Username { get; set; }

        public string RoleId { get; set; }

        public string HeadlineType { get; set; }

        public string HeadlineGroup { get; set; }

        public bool NotAssignGroupOnly { get; set; }

        public int DateType { get; set; }

        public int ArchiveStage { get; set; }

        public DateTime FromDate { get; set; }

        public string FromDateText => FromDate.ToString("MM/dd/yyyy");

        public DateTime ToDate { get; set; }

        public string ToDateText => ToDate.ToString("MM/dd/yyyy");

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
