using Fanex.Tarus.Admin.Enums;
using Sunplus.Accounting.Core.Common;
using System;
using System.Collections;

namespace Fanex.Tarus.Admin.Criteria.SMAManagement
{
    public class GetCN88SMAGroupHeadlineCriteria
    {
        public int Currency { get; set; }

        public bool IsActive { get; set; }

        public bool IsSuspended { get; set; }

        public bool IsViewOnly { get; set; }

        public bool IsInactive { get; set; }

        public bool IsDisabled { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }

        public string GroupHeadline { get; set; }

        public string HeadlineType { get; set; }

        public string Owner { get; set; }

        public bool ExcludeTestAccounts { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string GetSettingKey()
        {
            return "CS_CustomerSelByGroupHeadline";
        }

        public Hashtable GetSPParams()
        {
            if (!string.IsNullOrEmpty(HeadlineType))
            {
                HeadlineType = ((int)Enum.Parse(typeof(HeadlineType), HeadlineType, true)).ToString();
            }

            return new Hashtable
            {              
                { "currency", Currency },
                { "active", IsActive },
                { "viewonly", IsViewOnly },
                { "suspended", IsSuspended },
                { "inactive", IsInactive },
                { "disabled", IsDisabled },
                { "roleid", RoleId },
                { "username", Username },
                { "groupheadline", GroupHeadline },
                { "headlinetype", HeadlineType },
                { "owner", Owner },
                { "istest", !ExcludeTestAccounts },
                { "userid", AppSession.AccId }
            };
        }

        public bool IsValid()
        {
            return RoleId >=0;
        }
    }
}
