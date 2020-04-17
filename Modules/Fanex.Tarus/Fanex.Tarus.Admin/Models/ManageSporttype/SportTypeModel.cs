namespace Fanex.Tarus.Admin.Models.ManageSporttype
{
    using Fanex.Tarus.Admin.Entities.ManageSporttype;
    using System.Collections.Generic;

    public class SportTypeModel
    {
        public IEnumerable<SportTypeEntity> SportTypeEntities { get; set; }

        public int TotalRows { get; set; }
    }
}
