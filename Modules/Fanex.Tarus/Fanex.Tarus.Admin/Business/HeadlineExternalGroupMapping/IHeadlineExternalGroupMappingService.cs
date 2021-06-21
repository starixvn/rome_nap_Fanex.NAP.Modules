namespace Fanex.Tarus.Admin.Business.HeadlineExternalGroupMapping
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;

    public interface IHeadlineExternalGroupMappingService
    {
        IEnumerable<SelectListItem> GetExternalGroups();

        IEnumerable<SelectListItem> GetPrimaryGroups();

        IEnumerable<SelectListItem> GetCurrencies();

        IEnumerable<SelectListItem> GetHeadline();

        IEnumerable<HeadlineExternalGroupMappingModel> Select(SelExternalGroupHeadlineMappingCriteia request);

        Result Insert(HeadlineExternalGroupMappingModel request, int userId);

        Result Update(HeadlineExternalGroupMappingModel request, int userId);

        ResultResponse Delete(HeadlineExternalGroupMappingModel request, int userId);

        MappingSuperResult GetMappingSuper(string usernames, int groupId);
    }
}
