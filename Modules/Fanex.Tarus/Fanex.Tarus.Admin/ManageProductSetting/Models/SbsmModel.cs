namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using System.Collections.Generic;

    public class SbsmModel
    {
        public IEnumerable<SbsmEntity> SbsmEntities { get; set; }

        public int TotalRows { get; set; }
    }
}
