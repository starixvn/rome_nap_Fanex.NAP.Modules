namespace Fanex.Tarus.Admin.ManageVendorGlobalSetting.Models
{
    using System.Collections.Generic;

    public class VendorGlobalModel
    {
        public IEnumerable<VenderGlobalEntity> VendorGlobalEntities { get; set; }

        public int TotalRows { get; set; }
    }
}
