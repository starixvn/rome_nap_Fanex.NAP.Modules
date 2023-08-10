namespace Fanex.Tarus.Admin.ManageProductSetting.ViewModel
{
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Admin.ViewModel;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ManageProductSettingViewModel
    {
        public IEnumerable<SelectListItem> Products { get; set; }

        public IEnumerable<SelectListItem> SportTypes { get; set; }

        public IEnumerable<SelectListItem> Currencies { get; set; }

        public IEnumerable<SiteViewModel> Sites { get; set; }

        public IEnumerable<SelectListItem> UMCurrencies { get; set; }

        public IEnumerable<SelectListItem> UMSites { get; set; }

        public SbsmFunctionTableModel FunctionTables { get; set; }
        
    }
}
