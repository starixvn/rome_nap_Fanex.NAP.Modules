namespace Fanex.Tarus.Admin.Models.ManageSportBettypeMatrix
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.Entities.ManageSportBettypeMatrix;

    public class SportBettypeMatrixModel
    {
        public IEnumerable<SportBettypeMatrixEntity> SportBettypeMatrixEntities { get; set; }

        public int TotalRows { get; set; }
    }
}