namespace Fanex.Tarus.Admin.Business.HeadlineExternalGroupMapping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.DataAccess.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;

    public class HeadlineExternalGroupMappingService : IHeadlineExternalGroupMappingService
    {
        private readonly IHeadlineExternalGroupMappingDataAccess _dataAccess;

        public HeadlineExternalGroupMappingService()
        {
            _dataAccess = new HeadlineExternalGroupMappingDataAccess();
        }

        public ResultResponse Delete(HeadlineExternalGroupMappingModel request, int userId)
        {
            var criteria = new DelExternalGroupHeadlineMappingCriteia(request, userId)
            {
                IP = HttpContext.Current.Request.UserHostAddress
            };

            return _dataAccess.Delete(criteria);
        }

        public IEnumerable<SelectListItem> GetCurrencies()
            => _dataAccess.SelCreditCurrency(new GetCreditCurrencyCriteria())
                .Select(cur => new SelectListItem { Text = cur.Name, Value = cur.Id.ToString() }).ToList();

        public IEnumerable<SelectListItem> GetExternalGroups()
            => _dataAccess.SelExternalGroup(new GetExternalGroupCriteria())
                .Select(group => new SelectListItem { Text = group, Value = group }).ToList();

        public IEnumerable<SelectListItem> GetHeadline()
            => _dataAccess.SelHeadline(new GetHeadlineCriteria())
                .Select(headline => new SelectListItem { Text = headline.Name, Value = headline.Id.ToString() }).ToList();

        public MappingSuperResult GetMappingSuper(string usernames, int groupId)
        {
            var criteria = new MappingSuperCriteria(usernames, groupId);

            return _dataAccess.GetMappingSuper(criteria);
        }

        public IEnumerable<SelectListItem> GetPrimaryGroups()
        {
            var groups = _dataAccess.SelPrimaryGroup(new GetExtraPTGroupCriteria())
                .Select(group => new SelectListItem { Text = group.Name, Value = group.Name }).ToList();

            groups.Insert(0, new SelectListItem { Text = "No Extra PT", Value = "No Extra PT" });

            return groups;
        }

        public Result Insert(HeadlineExternalGroupMappingModel request, int userId)
        {
            var criteria = new InsExternalGroupHeadlineMappingCriteia(request, userId)
            {
                IP = HttpContext.Current.Request.UserHostAddress
            };

            return _dataAccess.Insert(criteria);
        }

        public IEnumerable<HeadlineExternalGroupMappingModel> Select(SelExternalGroupHeadlineMappingCriteia request)
            => _dataAccess.SelectMapping(request);

        public Result Update(HeadlineExternalGroupMappingModel request, int userId)
        {
            var criteria = new UpdExternalGroupHeadlineMappingCriteia(request, userId)
            {
                IP = HttpContext.Current.Request.UserHostAddress
            };

            return _dataAccess.Update(criteria);
        }
    }
}
