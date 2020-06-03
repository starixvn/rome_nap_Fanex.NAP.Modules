namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class BettypeModel
    {
        public IEnumerable<BettypeEntity> BettypeEntities { get; set; }

        public int TotalRows { get; set; }
    }
}