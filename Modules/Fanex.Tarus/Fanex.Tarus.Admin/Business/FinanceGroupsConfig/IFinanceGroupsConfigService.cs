namespace Fanex.Tarus.Admin.Business.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using System.Collections.Generic;

    public interface IFinanceGroupsConfigService
    {
        IEnumerable<IdNameEntity> GetGroups();
        FinanceGroupSelectResult Select(FinanceGroupSelCriteria request);
        Result Update(FinanceGroupUpdCriteria request);
        Result UpdateIsView(FinanceGroupIsViewUpdCriteria request);
        Result Remove(FinanceGroupDelCriteria request);
        Result RemoveAll(FinanceGroupDeleteAllRequest request);
        FinanceGroupValidateResult ValidateGroupInsertion(FinanceGroupInsValidationCriteria request);
        FinanceGroupInsertResult InsertGroup(FinanceGroupInsCriteria request);
        FinanceUserValidateResult ValidateUserInsertion(FinanceUserInsValidationCriteria request);
        FinanceUserInsertResult InsertUser(FinanceUserInsCriteria request);
        FinanceCodeValidateResult ValidateCodeInsertion(FinanceCodeInsValidationCriteria request);
        FinanceCodeInsertResult InsertCode(FinanceCodeInsCriteria request);
    }
}
