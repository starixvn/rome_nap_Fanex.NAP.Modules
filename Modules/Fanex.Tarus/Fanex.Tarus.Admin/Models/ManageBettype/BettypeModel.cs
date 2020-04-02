namespace Fanex.Tarus.Admin.Models.ManageBettype
{
    using Fanex.Tarus.Admin.Entities.ManageBetType;
    using System.Collections.Generic;

    public class BettypeModel
    {
        public IEnumerable<BettypeEntity> BettypeEntities { get; set; }

        public int TotalRows { get; set; }
    }
}
