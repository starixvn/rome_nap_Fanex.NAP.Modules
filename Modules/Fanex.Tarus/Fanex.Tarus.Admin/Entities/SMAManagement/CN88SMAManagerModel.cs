using System;
using System.Collections.Generic;

namespace Fanex.Tarus.Admin.Entities.SMAManagement
{
    [Serializable]
    public class CN88SMAManagerModel
    {
        public int TotalGroups { get; set; }

        public int TotalMemberNotAssignedToGroup { get; set; }

        public IList<CN88SMAGroupHeadline> CN88SMAGroupHeadlines { get; set; }
    }
}
