using System.Collections.Generic;
using System.Linq;
using Fanex.Tarus.Admin.DormantAccount.Models;
using Fanex.Tarus.Admin.DormantAccount.Request;
using Fanex.Tarus.Admin.Enums;

namespace Fanex.Tarus.Admin.DormantAccount.ViewModel
{
    public class DormantAccountViewModel
    {
        public DormantAccountViewModel(DormantAccountModel model, SelectDormantAccountRequest request)
        {
            SearchUsernameFromFilter = request.SearchUsernameFromFilter ? 1 : 0;
            TotalItem = model.TotalItem;
            var yesterdayBalance = model.Balance.ToDictionary(i => i.CustId, i => i.YesterdayTotalBalance);
            foreach (var i in model.ByUsername)
            {
                var balance = yesterdayBalance.ContainsKey(i.CustId) ? yesterdayBalance[i.CustId] : 0;
                i.YesterdayBalance = balance;
            }

            foreach (var i in model.ByDateType)
            {
                var balance = yesterdayBalance.ContainsKey(i.CustId) ? yesterdayBalance[i.CustId] : 0;
                i.YesterdayBalance = balance;
            }

            DataByDateType = model.ByDateType.OrderBy(i => i.CustHeadlineId == 0);
            DataByUsername = model.ByUsername;

            Member = new BreadScrumbInfoViewModel(UserLevelConstant.Member, request)
            {
                IsDirectAccount = model.Breadscrumb.IsDirectAccount(model.Breadscrumb.Username),
                Username = model.Breadscrumb.RoleId == UserLevelConstant.Member ? model.Breadscrumb.Username : string.Empty,
                Headline = model.Breadscrumb.HeadlineGroup
            };

            Agent = new BreadScrumbInfoViewModel(UserLevelConstant.Agent, request)
            {
                IsDirectAccount = model.Breadscrumb.IsDirectAccount(model.Breadscrumb.AgentName),
                //IsDummyAccount = Member.IsDirectAccount,
                Username = model.Breadscrumb.AgentName,
                Headline = model.Breadscrumb.HeadlineGroup,
                Downline = Member.Username
            };

            Master = new BreadScrumbInfoViewModel(UserLevelConstant.Master, request)
            {
                IsDirectAccount = model.Breadscrumb.IsDirectAccount(model.Breadscrumb.MasterName),
                //IsDummyAccount = Agent.IsDirectAccount,
                Username = model.Breadscrumb.MasterName,
                Headline = model.Breadscrumb.HeadlineGroup,
                Downline = Agent.Username
            };

            Super = new BreadScrumbInfoViewModel(UserLevelConstant.Super, request)
            {
                IsDirectAccount = model.Breadscrumb.IsDirectAccount(model.Breadscrumb.SuperName),
                //IsDummyAccount = Master.IsDirectAccount,
                Username = model.Breadscrumb.SuperName,
                Headline = model.Breadscrumb.HeadlineGroup,
                Downline = Master.Username
            };

            Headline = new BreadScrumbInfoViewModel(UserLevelConstant.Headline, request)
            {
                Username = request.Username
            };

            BreadScrumb = BuildBreadScrumb();
            Request = request;
        }

        public SelectDormantAccountRequest Request { get; set; }

        public int SearchUsernameFromFilter { get; }

        public IEnumerable<DormantDateTypeModel> DataByDateType { get; }

        public IEnumerable<DormantUsernameModel> DataByUsername { get; }

        public bool HasData => DataByDateType.Any() || DataByUsername.Any();

        public int TotalItem { get; }

        public bool HasBreadScrumb => DataByUsername.Any();

        public IList<BreadScrumbInfoViewModel> BreadScrumb { get; }

        private BreadScrumbInfoViewModel Member { get; }

        private BreadScrumbInfoViewModel Agent { get; }

        private BreadScrumbInfoViewModel Master { get; }

        private BreadScrumbInfoViewModel Super { get; }

        private BreadScrumbInfoViewModel Headline { get; }

        private List<BreadScrumbInfoViewModel> BuildBreadScrumb()
        {
            var breadScrumb = new List<BreadScrumbInfoViewModel>
            {
                Headline,
                Super,
                Master,
                Agent,
                Member
            };

            var roleDirectAccount = FindDirectAccount(breadScrumb);
            if(roleDirectAccount!=0)
            {
                breadScrumb = breadScrumb.Where(i => i.RoleId == UserLevelConstant.Headline || i.RoleId <= roleDirectAccount).ToList();
            }

            return breadScrumb.Where(i => !string.IsNullOrEmpty(i.Text)).ToList();
        }

        private int FindDirectAccount(List<BreadScrumbInfoViewModel> list)
        {
            var dirrectAccount = list.FirstOrDefault(i => i.IsDirectAccount);

            return dirrectAccount != null ? dirrectAccount.RoleId : 0;
        }
    }
}
