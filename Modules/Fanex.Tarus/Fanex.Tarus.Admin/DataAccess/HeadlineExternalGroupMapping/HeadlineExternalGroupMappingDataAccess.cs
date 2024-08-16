namespace Fanex.Tarus.Admin.DataAccess.HeadlineExternalGroupMapping
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DNAide.Core;
    using Fanex.Data;
    using Fanex.Tarus.Admin.Criteria.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Sunplus.Accounting.Core.Common;
    using Sunplus.Accounting.DataAccess.IBCDLL.Accounting;

    public class HeadlineExternalGroupMappingDataAccess : IHeadlineExternalGroupMappingDataAccess
    {
        public IEnumerable<IdNameEntity> SelCreditCurrency(GetCreditCurrencyCriteria criteria)
        {
            var currencies = new List<IdNameEntity>();
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

        public IEnumerable<IdNameEntity> SelPrimaryGroup(GetExtraPTGroupCriteria criteria)
        {
            var groups = new List<IdNameEntity>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        groups.Add(new IdNameEntity
                        {
                            Id = CastAide.AsInt32(reader["groupid"]),
                            Name = reader["groupname"].ToString()
                        });
                    }
                }
            }

            return groups;
        }

        public IEnumerable<string> SelExternalGroup(GetExternalGroupCriteria criteria)
        {
            var groups = new List<string>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        groups.Add(reader["groupname"].ToString());
                    }
                }
            }

            return groups;
        }

        public Result Insert(InsExternalGroupHeadlineMappingCriteia criteria)
            => ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSpParams());

        public Result Update(UpdExternalGroupHeadlineMappingCriteia criteria)
            => ExecuteNonQuery(criteria.GetSettingKey(), criteria.GetSpParams());

        public ResultResponse Delete(DelExternalGroupHeadlineMappingCriteia criteria)
            => ExecuteNonQueryWithAffectedSuper(criteria.GetSettingKey(), criteria.GetSpParams());

        private Result ExecuteNonQuery(string settingKey, Hashtable inputParams)
        {
            using (var db = new ObjectDb(settingKey))
            {
                db.ExecuteNonQuery(inputParams);
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    return new Result((int)outputParams["errcode"], outputParams["errmsg"].ToString());
                } else
                {
                    return new Result(0, string.Empty);
                }
            }
        }

        private ResultResponse ExecuteNonQueryWithAffectedSuper(string settingKey, Hashtable inputParams)
        {
            using (var db = new ObjectDb(settingKey))
            {
                db.ExecuteNonQuery(inputParams);
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    return new ResultResponse((int)outputParams["errcode"], outputParams["errmsg"].ToString())
                    {
                        AffectedSupers = outputParams["affectedsupers"].ToString()
                    };
                }
                else
                {
                    return new ResultResponse(0, string.Empty);
                }
            }
        }

        public IEnumerable<HeadlineExternalGroupMappingModel> SelectMapping(SelExternalGroupHeadlineMappingCriteia criteria)
        {
            IList<HeadlineExternalGroupMappingModel> mappings = new List<HeadlineExternalGroupMappingModel>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        var mapping = new HeadlineExternalGroupMappingModel
                        {
                            GroupHeadlineId = Convert.ToInt32(reader["groupheadlineid"]),
                            HeadlineName = reader["headlinename"].ToString(),
                            SiteId = Convert.ToInt32(reader["siteid"]),
                            Site = reader["site"].ToString(),
                            ExchangeId = Convert.ToInt32(reader["exchangeid"]),
                            Currency = reader["currency"].ToString(),
                            NormalGroup = reader["normalgroup"].ToString(),
                            ExtraPTGroup = reader["extraptgroup"].ToString(),
                            PrimaryExtraPTGroup = reader["primaryextraptgroup"].ToString(),
                            CurrentPrimaryExtraPTGroup = reader["currentprimaryextraptgroup"].ToString(),
                            CurrentEffectiveDate = Convert.IsDBNull(reader["currenteffectivedate"]) ? string.Empty : Convert.ToDateTime(reader["currenteffectivedate"]).ToString("MM/dd/yyyy")
                        };

                        mappings.Add(mapping);
                    }
                }
            }

            return mappings;
        }

        public IEnumerable<IdNameEntity> SelHeadline(GetHeadlineCriteria criteria)
        {
            criteria.DeniedList = AccountCredit.Acc_DeniedSiteCurrency_Sel(AppSession.AccId);
            var headlines = new List<IdNameEntity>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        headlines.Add(new IdNameEntity
                        {
                            Id = CastAide.AsInt32(reader["groupheadlineid"]),
                            Name = reader["headlinename"].ToString()
                        });
                    }
                }
            }

            return headlines;
        }

        public MappingSuperResult GetMappingSuper(MappingSuperCriteria criteria)
        {
            var result = new MappingSuperResult();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                IList<ExternalGroupHeadlineMappingSuper> mappings = new List<ExternalGroupHeadlineMappingSuper>();
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        var mapping = new ExternalGroupHeadlineMappingSuper
                        {
                            Username = reader["username"].ToString(),
                            SiteId = Convert.ToInt32(reader["siteid"]),
                            Site = reader["site"].ToString(),
                            ExchangeId = Convert.ToInt32(reader["exchangeid"]),
                            Currency = reader["currency"].ToString(),
                            NormalGroup = reader["normalgroup"].ToString(),
                            ExtraPTGroup = reader["extraptgroup"].ToString(),
                            PrimaryExtraPTGroup = reader["primaryextraptgroup"].ToString()
                        };

                        mappings.Add(mapping);
                    }
                }

                result.Supers = mappings;
            }

            return result;
        }
    }
}
