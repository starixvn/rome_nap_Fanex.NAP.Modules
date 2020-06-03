namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class SportTypeModel
    {
        public IEnumerable<SportTypeEntity> SportTypeEntities { get; set; }

        public int TotalRows { get; set; }
    }
}