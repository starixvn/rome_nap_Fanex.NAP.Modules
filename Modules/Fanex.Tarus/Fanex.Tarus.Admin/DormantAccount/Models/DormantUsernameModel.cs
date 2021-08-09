using System;
using System.Collections.Generic;
using DNAide.Core;
using Fanex.Tarus.Admin.Enums;

namespace Fanex.Tarus.Admin.DormantAccount.Models
{
    public class DormantUsernameModel
    {
        private const string DateFormat = "MM/dd/yyyy";

        public int CustId { get; set; }

        public string Username { get; set; }

        public int RoleId { get; set; }

        public string RoleText
        => UserLevelConstant.List.ContainsKey(RoleId) ? UserLevelConstant.List[RoleId] : string.Empty;

        public int StatusId { get; set; }

        public string StatusText
        {
            get
            {
                var accountStatusList = new Dictionary<int, string>
                {
                    { 1, "Open" },
                    { 2, "Suspended" },
                    { 3, "Closed" },
                    { 4, "Disabled" },
                    { 5, "Deleted "}
                };
                var status = accountStatusList.ContainsKey(StatusId) ? accountStatusList[StatusId] : string.Empty;

                return status;
            }
        }

        public int StageId { get; set; }

        public string StageText
            => ArchiveStageConstant.List.ContainsKey(StageId) ? ArchiveStageConstant.List[StageId] : string.Empty;

        public decimal? ToBeClearedGivenCredit { get; set; }

        public string ToBeClearedGivenCreditText
            => ToBeClearedGivenCredit.HasValue ?
            DataFormat.FormatNumber20(ToBeClearedGivenCredit.Value) : string.Empty;

        public decimal? ClearedGivenCredit { get; set; }

        public string ClearedGivenCreditText
            => ClearedGivenCredit.HasValue ? DataFormat.FormatNumber20(ClearedGivenCredit.Value) : string.Empty;

        public decimal? YesterdayBalance { get; set; }

        public string YesterdayBalanceText
            => YesterdayBalance.HasValue ?
            DataFormat.FormatNumber20(YesterdayBalance.Value) : string.Empty;

        public DateTime? LastLogged { get; set; }

        public string LastLoggedText => LastLogged.HasValue ? LastLogged.Value.ToString(DateFormat) : string.Empty;

        public DateTime? ClosedDate { get; set; }

        public string ClosedDateText => ClosedDate.HasValue ? ClosedDate.Value.ToString(DateFormat) : string.Empty;

        public DateTime? DisabledDate { get; set; }

        public string DisabledDateText => DisabledDate.HasValue ? DisabledDate.Value.ToString(DateFormat) : string.Empty;

        public DateTime? SettingDeletedDate { get; set; }

        public string SettingDeletedDateText => SettingDeletedDate.HasValue ? SettingDeletedDate.Value.ToString(DateFormat) : string.Empty;

        public bool IsWarningTicket { get; set; }

        public int RowNumber { get; set; }

        public DateTime? AccountDeletedDate { get; set; }

        public string AccountDeletedDateText => AccountDeletedDate.HasValue ? AccountDeletedDate.Value.ToString(DateFormat) : string.Empty;
    }
}
