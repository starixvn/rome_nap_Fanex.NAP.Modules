namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SbsmModel
    {
        public IEnumerable<SbsmEntity> SbsmEntities { get; set; }

        public int TotalRows { get; set; }

        public IEnumerable<SelectListItem> DummyProducts { get; set; }
    }
}
