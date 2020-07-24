namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;

    public class CSSportBettypeMatrixModel
    {
        public IEnumerable<CSSportBettypeMatrixEntity> CSSportBettypeMatrixEntities { get; set; }

        public int TotalRows { get; set; }
    }
}
