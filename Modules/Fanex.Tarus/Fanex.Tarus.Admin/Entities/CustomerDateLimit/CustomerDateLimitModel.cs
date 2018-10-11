namespace Fanex.Tarus.Admin.Entities.CustomerDateLimit
{
    using System;
    using Enums;

    public class CustomerDateLimitModel
    {
        public int CustId { get; set; }

        public string UserName { get; set; }

        public DateTime? MinDate { get; set; }

        public int? LimitDays { get; set; }

        public string DataAvailable
        {
            get
            {
                if (MinDate.HasValue)
                {
                    return MinDate.Value.ToString("MM/dd/yyyy");
                }

                if (LimitDays.HasValue)
                {
                    return LimitDays.Value.ToString();
                }

                return string.Empty;
            }
        }

        public int Type
        {
            get
            {
                if(MinDate.HasValue)
                {
                    return CustomerDataRangeType.MinDate;
                }

                if(LimitDays.HasValue)
                {
                    return CustomerDataRangeType.TimePeriod;
                }

                return CustomerDataRangeType.None;
            }
        }
    }
}
