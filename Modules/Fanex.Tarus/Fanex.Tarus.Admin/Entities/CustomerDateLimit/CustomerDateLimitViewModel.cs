namespace Fanex.Tarus.Admin.Entities.CustomerDateLimit
{
    using System.Collections.Generic;

    public class CustomerDateLimitViewModel
    {
        public CustomerDateLimitViewModel(
            IEnumerable<CustomerDateLimitModel> customers,
            int totalRecords,
            int pageSize,
            int pageIndex)
        {
            Customers = customers;
            TotalRecords = totalRecords;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public IEnumerable<CustomerDateLimitModel> Customers { get; set; }

        public int TotalRecords { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
