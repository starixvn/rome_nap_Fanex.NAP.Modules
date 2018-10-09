namespace Fanex.Tarus.Admin.Entities.MaxMemberLimitBySuper
{
    using System.Collections.Generic;

    public class MaxMemberLimitBySuperEntity
    {
        public int RowNum { get; set; }

        public string SuperName { get; set; }

        public string Site { get; set; }

        public int NumberOfMember { get; set; }

        public int MaxMembersLimit { get; set; }

        public string Remark { get; set; }
    }
}
