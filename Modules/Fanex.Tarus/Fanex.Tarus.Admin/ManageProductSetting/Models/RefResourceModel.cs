namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class RefResourceModel
    {
        public IEnumerable<RefResourceEntity> RefResources { get; set; }

        public int TotalRows { get; set; }
    }
}
