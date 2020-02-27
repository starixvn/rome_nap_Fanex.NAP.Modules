using Fanex.CustomerSetting.Module.Core;
using Fanex.Data;
using Fanex.NAP.Framework.Data;

namespace Fanex.Tarus.Security.Services
{
    public interface IFinanceAuthorizationService
    {
        bool HasPermission(ICriteria criteria);
    }

    public class FinanceAuthorizationService : IFinanceAuthorizationService
    {
        public bool HasPermission(ICriteria criteria)
        {
            var result = false;
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                reader.Read();
                result = Utils.ToBoolean(reader["IsView"]);
            }

            return result;
        }
    }
}
