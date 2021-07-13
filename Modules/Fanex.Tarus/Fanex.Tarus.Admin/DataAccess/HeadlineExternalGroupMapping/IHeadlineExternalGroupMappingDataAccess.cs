namespace Fanex.Tarus.Admin.DataAccess.HeadlineExternalGroupMapping
{
    using System.Collections.Generic;
    using Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities.SMAManagement;

    public interface IHeadlineExternalGroupMappingDataAccess
    {
        IEnumerable<IdNameEntity> SelCreditCurrency(GetCreditCurrencyCriteria criteria);

        IEnumerable<string> SelExternalGroup(GetExternalGroupCriteria criteria);

        IEnumerable<IdNameEntity> SelPrimaryGroup(GetExtraPTGroupCriteria criteria);

        IEnumerable<IdNameEntity> SelHeadline(GetHeadlineCriteria criteria);

        Result Insert(InsExternalGroupHeadlineMappingCriteia criteria);

        Result Update(UpdExternalGroupHeadlineMappingCriteia criteria);

        ResultResponse Delete(DelExternalGroupHeadlineMappingCriteia criteria);

        IEnumerable<HeadlineExternalGroupMappingModel> SelectMapping(SelExternalGroupHeadlineMappingCriteia criteria);

        MappingSuperResult GetMappingSuper(MappingSuperCriteria criteria);
    }
}
