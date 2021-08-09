using Fanex.Tarus.Admin.DormantAccount.Request;
using Fanex.Tarus.Admin.Enums;

namespace Fanex.Tarus.Admin.DormantAccount.ViewModel
{
    public class BreadScrumbInfoViewModel
    {
        public string Text
        {
            get
            {
                var text = IsDirectAccount && !string.IsNullOrWhiteSpace(Username) ? $"{Username} ({Headline})" : Username;
                if (Request.SearchFromArchiveRoleColumn && IsDirectAccount && !string.IsNullOrEmpty(text))
                {
                    text += $"'s Archived {UserLevelConstant.List[int.Parse(Request.RoleId)]} Downlines in {ArchiveStageConstant.List[Request.ArchiveStage]} stage";
                }
                else if (RoleId != UserLevelConstant.Member && 
                    string.IsNullOrWhiteSpace(Downline) && 
                    !string.IsNullOrWhiteSpace(Username))
                {
                    text += "'s Archived Downlines";
                }

                if (RoleId == UserLevelConstant.Headline)
                {
                    text = Request.SearchUsernameFromFilter ? string.Empty : "Headlines";
                }


                return text;
            }

        }

        public BreadScrumbInfoViewModel(int roleId, SelectDormantAccountRequest request)
        {
            Request = request;
            RoleId = roleId;
        }
        
        public bool IsLink => RoleId == UserLevelConstant.Headline || !string.IsNullOrWhiteSpace(Downline);

        public string Username { get; set; }

        public string Downline { get; set; }

        public bool IsDirectAccount { get; set; }

        public bool IsDummyAccount { get; set; }

        public string Headline { get; set; }

        public int RoleId { get; set; }

        private SelectDormantAccountRequest Request { get; set; }


    }
}
