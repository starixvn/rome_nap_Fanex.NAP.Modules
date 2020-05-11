namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class ProductModel
    {
        public IEnumerable<ProductEntity> ProductEntities { get; set; }

        public int TotalRows { get; set; }
    }
}