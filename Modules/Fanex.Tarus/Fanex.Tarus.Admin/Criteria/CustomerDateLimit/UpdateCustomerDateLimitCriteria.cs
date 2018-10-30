namespace Fanex.Tarus.Admin.Criteria.CustomerDateLimit
{
    using System;
    using System.Collections;
    using SiteManagement;

    public class UpdateCustomerDateLimitCriteria : CriteriaBase
    {
        public string CustIds { get; set; }

        public string UserNames { get; set; }

        public DateTime? MinDate { get; set; }

        public int? LimitDays { get; set; }

        public int Choice { get; set; }

        public int UserId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public int EffectedRow { get; set; }

        public string NoChangeUser { get; set; }

        public string GetSettingKey() => "EAS_CustomerDateLimitMultipleUpd";

        public Hashtable GetSPParams()
            => new Hashtable
            {
                ["listusername"] = UserNames,
                ["listcustid"] = CustIds,
                ["mindate"] = MinDate,
                ["timeperiod"] = LimitDays,
                ["choice"] = Choice,
                ["userid"] = UserId,
                ["errcode"] = ErrCode,
                ["errmsg"] = ErrMsg,
                ["effectedrow"] = EffectedRow,
                ["nochangeuser"] = NoChangeUser
            };
    }
}
