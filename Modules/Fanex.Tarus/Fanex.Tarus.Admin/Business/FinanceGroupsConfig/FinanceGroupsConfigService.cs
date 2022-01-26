namespace Fanex.Tarus.Admin.Business.FinanceGroupsConfig
{
    using Fanex.Tarus.Admin.Criteria.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.DataAccess.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.Requests.FinanceGroupsConfig;
    using Fanex.Tarus.Admin.ViewModel.FinanceGroupsConfig;
    using System.Collections.Generic;

    public class FinanceGroupsConfigService : IFinanceGroupsConfigService
    {
        private readonly IFinanceGroupsConfigDataAccess _dataAccess;

        public FinanceGroupsConfigService()
        {
            _dataAccess = new FinanceGroupsConfigDataAccess();
        }

        public IEnumerable<IdNameEntity> GetGroups()
            => _dataAccess.GroupSel(new GetGroupsCriteria());

        public FinanceGroupSelectViewModel Select(FinanceGroupSelCriteria request)
            => _dataAccess.FinanceGroupSel(request);

        public Result Update(FinanceGroupUpdCriteria request)
            => _dataAccess.FinanceGroupUpd(request);

        public Result UpdateIsView(FinanceGroupIsViewUpdCriteria request)
            => _dataAccess.FinanceGroupIsViewUpd(request);

        public Result Remove(FinanceGroupDelCriteria request)
            => _dataAccess.FinanceGroupDel(request);

        public Result RemoveAll(FinanceGroupDeleteAllRequest request)
        {
            var selCriteria = new FinanceGroupSelCriteria
            {
                UsernameList = request.UsernameList,
                GroupIds = request.GroupIds,
                FinanceType = 0,
                IsView = request.IsView,
                Level = request.Level,
                PageSize = request.TotalItems,
                PageIndex = 1
            };
            var selResult = _dataAccess.FinanceGroupSel(selCriteria);

            var delCriteria = new FinanceGroupDelCriteria();
            foreach(var row in selResult.FinanceCodes)
            {
                delCriteria.FinanceGroups.Add(new FinanceGroupDeleteModel
                {
                    UserName = row.FinanceCode,
                    GroupId = row.GroupId,
                    IsView = row.IsView,
                    RoleId = row.RoleId
                });
            }

            return _dataAccess.FinanceGroupDel(delCriteria);
        }

        public FinanceGroupValidateViewModel ValidateGroupInsertion(FinanceGroupInsValidationCriteria request)
            => _dataAccess.FinanceGroupInsValidation(request);

        public FinanceGroupInsertViewModel InsertGroup(FinanceGroupInsCriteria request)
            => _dataAccess.FinanceGroupIns(request);

        public FinanceUserValidateViewModel ValidateUserInsertion(FinanceUserInsValidationCriteria request)
            => _dataAccess.FinanceUserInsValidation(request);

        public FinanceUserInsertViewModel InsertUser(FinanceUserInsCriteria request)
         => _dataAccess.FinanceUserIns(request);

        public FinanceCodeValidateViewModel ValidateCodeInsertion(FinanceCodeInsValidationCriteria request)
            => _dataAccess.FinanceCodeInsValidation(request);

        public FinanceCodeInsertViewModel InsertCode(FinanceCodeInsCriteria request)
            => _dataAccess.FinanceCodeIns(request);
    }
}
