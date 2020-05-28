namespace Fanex.Tarus.Infrastructure.Business
{
    using Fanex.Common.Helper;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class ServiceBase : IServiceBase
    {
        protected readonly IDynamicRepository DynamicRepository;

        public ServiceBase(IDynamicRepository repository = null)
        {
            DynamicRepository = repository ?? new DynamicRepository();
        }

        protected IDynamicRepository Repository
        {
            get { return DynamicRepository; }
        }

        public virtual IEnumerable<T> GetData<T>(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                return DynamicRepository.Fetch<T>(criteria);
            }

            return new List<T>();
        }

        public virtual void ExecuteNonQuery(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (!criteria.IsValid())
            {
                throw new InvalidOperationException("Invalid criteria");
            }

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();
                if (outputParams != null)
                {
                    Map(criteria, outputParams);
                }
            }
        }

        private void Map(ICriteria criteria, Hashtable outputParams)
        {
            foreach(string key in outputParams.Keys)
            {
                var propertyInfos = criteria.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
                foreach(var propertyInfo in propertyInfos)
                {
                    if (propertyInfo.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                    {
                        propertyInfo.SetValue(criteria, outputParams[key]);
                    }
                }
            }
        }
    }
}
