namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using Criteria;
    using Entities;
    using Fanex.Module.NG2.Entities;
    using Fanex.Module.NG2.Services;
    using Repositories;

    internal class NG2Service
    {
        private readonly ING2StatusService _ng2StatusService;
        private readonly ING2Repository _ng2Repository;

        public NG2Service(
            ING2StatusService ng2StatusService = null,
            ING2Repository ng2Repository = null)
        {
            _ng2StatusService = ng2StatusService ?? new NG2StatusService();
            _ng2Repository = ng2Repository ?? new NG2Repository();
        }

        public void SyncCustomer(CopyExistingCustomerCriteria criteria, byte systemId)
        {
            var disabled = GetStatus(criteria.ExistingCustId).IsDisabled;

            _ng2StatusService.AutoInsertAccount(
                criteria.CustId,
                criteria.UserName,
                (RoleId)criteria.CustLevel,
                criteria.CustLevel == 4 ? criteria.CustId : criteria.UplineId,
                disabled,
                SiteId.NAP,
                criteria.CurrencyId,
                criteria.Creator,
                systemId);
        }

        public NG2Status GetStatus(int custId)
            => _ng2Repository.GetStatus(new GetNG2StatusCriteria(custId));
    }
}