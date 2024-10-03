namespace Fanex.Tarus.Admin.DataAccess.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig;
    using System.Collections.Generic;

    public interface IFinanceGroupsConfigDataAccess
    {
        IEnumerable<IdNameEntity> GroupSel(GetGroupsCriteria criteria);
        FinanceGroupSelectViewModel FinanceGroupSel(FinanceGroupSelCriteria criteria);
        Result FinanceGroupUpd(FinanceGroupUpdCriteria criteria);
        Result FinanceGroupIsViewUpd(FinanceGroupIsViewUpdCriteria criteria);
        Result FinanceGroupDel(FinanceGroupDelCriteria criteria);
        FinanceGroupValidateViewModel FinanceGroupInsValidation(FinanceGroupInsValidationCriteria criteria);
        FinanceGroupInsertViewModel FinanceGroupIns(FinanceGroupInsCriteria criteria);
        FinanceUserValidateViewModel FinanceUserInsValidation(FinanceUserInsValidationCriteria criteria);
        FinanceUserInsertViewModel FinanceUserIns(FinanceUserInsCriteria criteria);
        FinanceCodeValidateViewModel FinanceCodeInsValidation(FinanceCodeInsValidationCriteria criteria);
        FinanceCodeInsertViewModel FinanceCodeIns(FinanceCodeInsCriteria criteria);
        string GetValidFinanceCodes();

    }
}
