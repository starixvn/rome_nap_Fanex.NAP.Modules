namespace Fanex.Tarus.Admin.Criteria.CustomerDateLimit
{
    using System.Collections;

    public class GetCustomerDateLimitByIdCriteria
    {
        public int CustId { get; set; }

        public string GetSettingKey() => "EAS_CustomerDateLimitSel";

        public Hashtable GetSPParams() 
            => new Hashtable
            {
                ["custid"] = CustId
            };
    }
}
