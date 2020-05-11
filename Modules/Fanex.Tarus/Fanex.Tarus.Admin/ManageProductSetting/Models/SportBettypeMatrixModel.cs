namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class SportBettypeMatrixModel
    {
        public IEnumerable<SportBettypeMatrixEntity> SportBettypeMatrixEntities { get; set; }

        public int TotalRows { get; set; }
    }
}