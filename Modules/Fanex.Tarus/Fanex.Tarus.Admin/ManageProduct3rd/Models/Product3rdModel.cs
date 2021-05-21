namespace Fanex.Tarus.Admin.ManageProduct3RD.Models
{
    using System.Collections.Generic;

    public class Product3RDModel
    {
        public IEnumerable<Product3RDEntity> Product3RDEntities { get; set; }

        public int TotalRows { get; set; }
    }
}
