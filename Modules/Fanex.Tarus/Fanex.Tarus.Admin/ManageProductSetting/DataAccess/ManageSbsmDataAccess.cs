using Fanex.Data;
using Fanex.NAP.Framework.Data;
using Fanex.Tarus.Admin.DataAccess;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeSettingMatrix;
using Fanex.Tarus.Admin.ManageProductSetting.Entities;
using Fanex.Tarus.Admin.ManageProductSetting.Models;
using Fanex.Tarus.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Tarus.Admin.ManageProductSetting.DataAccess
{
    public interface IManageSbsmDataAccess
    {
        SbsmModel Select(SelectSbsmCriteria criteria);

        Result Insert(InsertSbsmCriteria criteria);
        Result Check(CheckSbsmCriteria criteria);

        Result Update(ICriteria criteria);

        Result Delete(ICriteria criteria);

        IEnumerable<SelectListItem> GetProduct(ICriteria criteria);

        IEnumerable<SelectListItem> GetSportType(ICriteria criteria);

        IEnumerable<SelectListItem> GetBetType(ICriteria criteria);

        SbsmFunctionTableModel GetAllFunctionTable();
    }

    public class ManageSbsmDataAccess : BaseDataAccess, IManageSbsmDataAccess
    {
        public Result Check(CheckSbsmCriteria criteria)
            => base.ExecuteNonQuery(criteria);

        public Result Delete(ICriteria criteria)
            => base.ExecuteNonQuery(criteria);

        public SbsmFunctionTableModel GetAllFunctionTable()
        {
            var criteria = new ReferenceSettingTableSelCriteria
            {
                TableType = 0
            };

            var entities = new List<TableModel>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new TableModel
                        {
                            TableType = DataUtils.ToString(reader["TableType"]),
                            TableName = DataUtils.ToString(reader["TableName"]),
                            OfferSite = DataUtils.ToInt32(reader["OfferSite"])
                        });
                    }
                }
            }

            return new SbsmFunctionTableModel(entities);
        }

        public IEnumerable<SelectListItem> GetBetType(ICriteria criteria)
        {
            var entities = new List<SelectListItem>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new SelectListItem
                        {
                            Value = DataUtils.ToString(reader["BetType"]),
                            Text = string.Format("[{0}] {1}", DataUtils.ToString(reader["BetType"]), DataUtils.ToString(reader["typenamee"]))
                        });
                    }
                    return entities;
                }
            }
        }

        public IEnumerable<SelectListItem> GetProduct(ICriteria criteria)
        {
            var entities = new List<SelectListItem>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new SelectListItem
                        {
                            Value = DataUtils.ToString(reader["ProductID"]),
                            Text = string.Format("[{0}] {1}", DataUtils.ToString(reader["ProductID"]), DataUtils.ToString(reader["ProductName"])) 
                        });
                    }
                    return entities;
                }
            }
        }

        public IEnumerable<SelectListItem> GetSportType(ICriteria criteria)
        {
            var entities = new List<SelectListItem>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        entities.Add(new SelectListItem
                        {
                            Value = DataUtils.ToString(reader["SportType"]),
                            Text = string.Format("[{0}] {1}", DataUtils.ToString(reader["SportType"]), DataUtils.ToString(reader["SportName"]))
                        });
                    }
                    return entities;
                }
            }
        }

        public Result Insert(InsertSbsmCriteria criteria)
            => base.ExecuteNonQuery(criteria);

        public SbsmModel Select(SelectSbsmCriteria criteria)
        {
            var entities = new List<SbsmEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new SbsmEntity
                    {
                        RealProductId = DataUtils.ToString(reader["RealProductID"]),
                        RealProductName = DataUtils.ToString(reader["RealProductName"]),
                        DummyProductId = DataUtils.ToString(reader["DummyProductID"]),
                        DummyProductName = DataUtils.ToString(reader["DummyProductName"]),
                        SportType = DataUtils.ToString(reader["SportType"]),
                        SportTypeName = DataUtils.ToString(reader["SportName"]),
                        BetType = DataUtils.ToString(reader["BetType"]),
                        BetTypeName = DataUtils.ToString(reader["BettypeName"]),
                        IsLive = DataUtils.ToBoolean(reader["IsLive"]) ? "1" : "0",
                        IsLicensee = DataUtils.ToBoolean(reader["IsLicensee"]) ? "1" : "0",
                        IsActive = DataUtils.ToBoolean(reader["IsActive"]) ? "1" : "0",
                        Site = DataUtils.ToString(reader["Site"]),
                        BetTypeRefPT = DataUtils.ToString(reader["BetTypeRefPT"]),
                        BetTypeRefBetSetting = DataUtils.ToString(reader["BetTypeRefBetSetting"]),
                        BetTypeRefComm = DataUtils.ToString(reader["BetTypeRefComm"]),
                        BettypeRefWinlossLimit = DataUtils.ToString(reader["BettypeRefWinlossLimit"]),
                        SportTypeRefPT = DataUtils.ToString(reader["SportTypeRefPT"]),
                        SportTypeRefBetSetting = DataUtils.ToString(reader["SportTypeRefBetSetting"]),
                        SportTypeRefComm = DataUtils.ToString(reader["SportTypeRefComm"]),
                        SporttypeRefWinlossLimit = DataUtils.ToString(reader["SporttypeRefWinlossLimit"]),
                        TableBettrans = DataUtils.ToString(reader["TableBettrans"]),
                        IsLivePT = DataUtils.ToBoolean(reader["IsLivePT"]) ? "1" : "0",
                        TableBetSetting = DataUtils.ToString(reader["TableBetSetting"]),
                        TableComm = DataUtils.ToString(reader["TableComm"]),
                        TablePT = DataUtils.ToString(reader["TablePT"])
                    });
                }

                reader.Close();
                return new SbsmModel
                {
                    SbsmEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Update(ICriteria criteria)
            => base.ExecuteNonQuery(criteria);
    }
}
