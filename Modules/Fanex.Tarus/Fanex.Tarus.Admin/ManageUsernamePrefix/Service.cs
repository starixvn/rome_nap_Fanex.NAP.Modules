namespace Fanex.Tarus.Admin.ManageUsernamePrefix
{
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageUsernamePrefix.Criteria;
    using Fanex.Tarus.Admin.ManageUsernamePrefix.Models;
    using Fanex.Tarus.Admin.ManageUsernamePrefix.Requests;
    using Fanex.Tarus.Infrastructure.Business;
    using Fanex.Tarus.Infrastructure.Common;
    using Fanex.Tarus.Infrastructure.Helper;
    using System.Collections.Generic;

    public interface IService
    {
        IEnumerable<QueryModel> Query(QueryRequest request);

        Result Insert(InsertRequest request);

        Result Delete(DeleteRequest request);
    }

    public class Service : IService
    {
        private readonly IServiceBase serviceBase;

        public Service()
            : this(null)
        {
        }

        public Service(IServiceBase serviceBase)
        {
            this.serviceBase = serviceBase ?? new ServiceBase();
        }

        public IEnumerable<QueryModel> Query(QueryRequest request)
        {
            var criteria = new QueryCriteria
            {
                GroupName = request.GroupName,
                Prefix = request.Prefix,
                AllowNewSuper = request.AllowNewSuper
            };
            return serviceBase.GetData<QueryModel>(criteria);
        }

        public Result Insert(InsertRequest request)
        {
            var criteria = new InsertCriteria
            {
                GroupName = request.GroupName,
                Prefix = request.Prefix.Trim().ToUpperInvariant(),
                UserID = AppSession.AccId,
                IP = Utils.GetClientIP()
            };
            serviceBase.ExecuteNonQuery(criteria);
            return new Result(criteria.ErrCode, criteria.ErrMsg);
        }

        public Result Delete(DeleteRequest request)
        {
            var criteria = new DeleteCriteria
            {
                GroupName = request.GroupName,
                Prefix = request.Prefix,
                UserID = AppSession.AccId,
                IP = Utils.GetClientIP()
            };
            serviceBase.ExecuteNonQuery(criteria);
            return new Result(criteria.ErrCode, criteria.ErrMsg);
        }
    }
}
