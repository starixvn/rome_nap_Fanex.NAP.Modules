namespace Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper
{
    using System.Collections.Generic;

    public class MaxMemberLimitBySuperSearchResult
    {
        public MaxMemberLimitBySuperSearchResult()
        {
            ListSuper = new List<MaxMemberLimitBySuperEntity>();
        }

        public int TotalRow { get; set; }

        public List<MaxMemberLimitBySuperEntity> ListSuper { get; set; }
    }
}
