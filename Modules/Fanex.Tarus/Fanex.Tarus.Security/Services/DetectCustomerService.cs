using Fanex.Tarus.Security.Criteria;
using Fanex.Tarus.Security.Repositories;

namespace Fanex.Tarus.Security.Services
{
    public interface IDetectCustomerService
    {
        int GetCustId(string searchText);
    }

    public class DetectCustomerService : IDetectCustomerService
    {
        private readonly IDetectCustomerDataAccess customerDataAccess;

        public DetectCustomerService(IDetectCustomerDataAccess customerDataAccess = null)
        {
            this.customerDataAccess = customerDataAccess ?? new DetectCustomerDataAccess();
        }

        public int GetCustId(string searchText)
            => searchText.IndexOfAny(new char[] { ',', ';' }) > -1
            ? 0
            : customerDataAccess.GetCustId(new DetectCustomerIdCriteria(searchText));
    }
}
