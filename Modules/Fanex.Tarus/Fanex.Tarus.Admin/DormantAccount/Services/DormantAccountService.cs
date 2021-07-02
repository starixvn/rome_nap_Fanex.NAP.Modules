using Fanex.Tarus.Admin.DormantAccount.Criteria;
using Fanex.Tarus.Admin.DormantAccount.DataAccess;
using Fanex.Tarus.Admin.DormantAccount.Request;
using Fanex.Tarus.Admin.DormantAccount.ViewModel;

namespace Fanex.Tarus.Admin.DormantAccount.Services
{
    public interface IDormantAccountService
    {
        DormantAccountViewModel DrillDownByDateType(SelectDormantAccountRequest request);

        DormantAccountViewModel GetByDateType(SelectDormantAccountRequest request);

        DormantAccountViewModel GetByUsername(SelectDormantAccountRequest request);
    }

    public class DormantAccountService : IDormantAccountService
    {
        private readonly IDormantAccountDataAccess dataAccess;

        public DormantAccountService()
        {
            dataAccess = new DormantAccountDataAccess();
        }

        public DormantAccountViewModel GetByDateType(SelectDormantAccountRequest request)
        {
            var criteria = new SelectDormantByDateTypeCriteria(request);
            var model = dataAccess.GetByDateType(criteria);

            return new DormantAccountViewModel(model, request);
        }

        public DormantAccountViewModel GetByUsername(SelectDormantAccountRequest request)
        {
            var criteria = new SelectDormantByUsernameCriteria(request);

            var model = dataAccess.GetByUserNameMode(criteria);

            return new DormantAccountViewModel(model, request);
        }

        public DormantAccountViewModel DrillDownByDateType(SelectDormantAccountRequest request)
        {
            var criteria = new DrilldownDormantByDateTypeCriteria(request);

            var model = dataAccess.GetByUserNameMode(criteria);

            return new DormantAccountViewModel(model, request);
        }

    }
}
