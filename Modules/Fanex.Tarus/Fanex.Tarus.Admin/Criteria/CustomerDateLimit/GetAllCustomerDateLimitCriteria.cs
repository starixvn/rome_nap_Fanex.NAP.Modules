namespace Fanex.Tarus.Admin.Criteria.CustomerDateLimit
{
    using System.Collections;

    public class GetAllCustomerDateLimitCriteria
    {
        public string UserName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRecords { get; set; }

        public string DeniedList { get; set; }

        public string GetSettingKey() => "Acc_CustomerDateLimitSel";

        public Hashtable GetSPParams()
            => new Hashtable
            {
                ["username"] = UserName,
                ["pagesize"] = PageSize,
                ["pageindex"] = PageIndex,
                ["totalrecords"] = TotalRecords,
                ["deniedlist"] = DeniedList
            };
    }
}
