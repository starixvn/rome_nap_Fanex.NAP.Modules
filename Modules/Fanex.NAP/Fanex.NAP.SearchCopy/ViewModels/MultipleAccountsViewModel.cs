namespace Fanex.CustomerSetting.Module.SearchCopy.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Entities;

    public class MultipleAccountsViewModel
    {
        public int CustId { get; set; }

        public int ParentId { get; set; }

        public CustomerInfo Customer { get; set; }

        public IEnumerable<SelectListItem> Sites { get; set; }

        public int DefaultMaxInputUsername { get; } = 50;
    }
}