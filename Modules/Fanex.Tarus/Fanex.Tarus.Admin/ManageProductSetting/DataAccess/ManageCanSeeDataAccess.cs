namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    using System.Collections.Generic;
    using DNAide.Core;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageCanSeeDataAccess
    {
        IEnumerable<CanSeeModel> Select(SelectCanSeeCriteria criteria);

        Result Insert(InsertCanSeeCriteria criteria);

        Result Update(UpdateCanSeeCriteria criteria);

        Result Delete(DeleteCanSeeCriteria criteria);

        IEnumerable<IdNameEntity> GetAllSportType();

        IEnumerable<IdNameEntity> GetCurrencies();
    }

    public class ManageCanSeeDataAccess : BaseDataAccess, IManageCanSeeDataAccess
    {
        public IEnumerable<CanSeeModel> Select(SelectCanSeeCriteria criteria)
        {
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    yield return new CanSeeModel
                    {
                        SportType = DataUtils.ToInt32(reader["SportType"]),
                        BetType = DataUtils.ToInt32(reader["BetType"]),
                        BetTypeName = DataUtils.ToString(reader["BetTypeName"]),
                        Currency = DataUtils.ToInt32(reader["Currency"]),
                        CurrencyName = DataUtils.ToString(reader["CurrencyName"]),
                        SiteId = DataUtils.ToInt32(reader["SiteId"]),
                        SiteName = DataUtils.ToString(reader["SiteName"]),
                        IsLaunched = DataUtils.ToBoolean(reader["IsLaunched"]),
                        IsTest = DataUtils.ToBoolean(reader["IsTest"]),
                        LaunchedLevel = DataUtils.ToInt32(reader["LaunchedLevel"])
                    };
                }
            }
        }

        public Result Insert(InsertCanSeeCriteria criteria) => ExecuteNonQuery(criteria);

        public Result Update(UpdateCanSeeCriteria criteria) => ExecuteNonQuery(criteria);

        public Result Delete(DeleteCanSeeCriteria criteria) => ExecuteNonQuery(criteria);

        public IEnumerable<IdNameEntity> GetAllSportType()
        {
            var sportTypes = new List<IdNameEntity>();
            var criteria = new SelectAllSportTypeCriteria();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    sportTypes.Add(new IdNameEntity
                    { Id = CastAide.AsInt32(reader["sporttype"]),
                        Name = reader["sportname"].ToString()
                    });
                }
            }

            return sportTypes;
        }

        public IEnumerable<IdNameEntity> GetCurrencies()
        {
            var currencies = new List<IdNameEntity>();
            var criteria = new SelectAllCurrencyCriteria();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        currencies.Add(new IdNameEntity
                        {
                            Id = CastAide.AsInt32(reader["exchangeid"]),
                            Name = reader["currency"].ToString()
                        });
                    }
                }
            }

            return currencies;
        }
    }
}