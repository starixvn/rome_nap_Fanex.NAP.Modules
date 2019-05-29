using System;
using System.Collections.Generic;

namespace Fanex.Tarus.Admin.Entities.SMAManagement
{
    [Serializable]
    public class CN88SMAGroupHeadline
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string Owner { get; set; }

        public IList<CN88SMAEntity> CN88SMAEntities { get; set; }
    }
}
