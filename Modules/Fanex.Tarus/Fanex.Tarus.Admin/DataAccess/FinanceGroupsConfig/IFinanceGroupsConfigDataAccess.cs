namespace Fanex.Tarus.Admin.DataAccess.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using System.Collections.Generic;

    public interface IFinanceGroupsConfigDataAccess
    {
        IEnumerable<IdNameEntity> GroupSel(GetGroupsCriteria criteria);
        FinanceGroupSelectResult FinanceGroupSel(FinanceGroupSelCriteria criteria);
        Result FinanceGroupUpd(FinanceGroupUpdCriteria criteria);
        Result FinanceGroupIsViewUpd(FinanceGroupIsViewUpdCriteria criteria);
        Result FinanceGroupDel(FinanceGroupDelCriteria criteria);
        FinanceGroupValidateResult FinanceGroupInsValidation(FinanceGroupInsValidationCriteria critera);
        FinanceGroupInsertResult FinanceGroupIns(FinanceGroupInsCriteria critera);
        FinanceUserValidateResult FinanceUserInsValidation(FinanceUserInsValidationCriteria critera);
        FinanceUserInsertResult FinanceUserIns(FinanceUserInsCriteria critera);
        FinanceCodeValidateResult FinanceCodeInsValidation(FinanceCodeInsValidationCriteria critera);
        FinanceCodeInsertResult FinanceCodeIns(FinanceCodeInsCriteria critera);
    }
}
