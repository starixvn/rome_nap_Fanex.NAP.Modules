using System.Collections.Generic;
using Fanex.Tarus.Admin.Entities.ManageProduct;

namespace Fanex.Tarus.Admin.Models.ManageProduct
{
    public class ProductModel
    {
        public IEnumerable<ProductEntity> ProductEntities { get; set; }

        public int TotalRows { get; set; }
    }
}