using System;

namespace Fanex.Tarus.Admin.DormantAccount.Models
{
    public class DormantBreadscrumbModel
    {
        public int CustId { get; set; }

        public int DirectCustId { get; set; }

        public string DirectUsername { get; set; }

        public int HeadlineType { get; set; }

        public int CustHeadlineId { get; set; }

        public string HeadlineGroup { get; set; }

        public string AgentName { get; set; }

        public string MasterName { get; set; }

        public string SuperName { get; set; }

        public string Username { get; set; }

        public int RoleId { get; set; }

        public bool IsDirectAccount(string userName)
            => !string.IsNullOrWhiteSpace(DirectUsername) &&
            DirectUsername.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
    }
}
