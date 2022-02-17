namespace Fanex.Tarus.Admin.Business.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.Requests.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig;
    using System.Collections.Generic;

    public interface IFinanceGroupsConfigService
    {
        IEnumerable<IdNameEntity> GetGroups();
        FinanceGroupSelectViewModel Select(FinanceGroupSelCriteria request);
        Result Update(FinanceGroupUpdCriteria request);
        Result UpdateIsView(FinanceGroupIsViewUpdCriteria request);
        Result Remove(FinanceGroupDelCriteria request);
        Result RemoveAll(FinanceGroupDeleteAllRequest request);
        FinanceGroupValidateViewModel ValidateGroupInsertion(FinanceGroupInsValidationCriteria request);
        FinanceGroupInsertViewModel InsertGroup(FinanceGroupInsCriteria request);
        FinanceUserValidateViewModel ValidateUserInsertion(FinanceUserInsValidationCriteria request);
        FinanceUserInsertViewModel InsertUser(FinanceUserInsCriteria request);
        FinanceCodeValidateViewModel ValidateCodeInsertion(FinanceCodeInsValidationCriteria request);
        FinanceCodeInsertViewModel InsertCode(FinanceCodeInsCriteria request);
    }
}
