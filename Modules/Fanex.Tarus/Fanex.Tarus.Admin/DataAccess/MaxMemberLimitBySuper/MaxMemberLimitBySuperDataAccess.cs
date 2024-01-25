namespace Fanex.Tarus.Admin.DataAccess.MaxMemberLimitBySuper
{
    using Common;
    using Criteria.MaxMemberLimitBySuper;
    using Data;
    using Entities;
    using Entities.MaxMemberLimitBySuper;
    using System.Collections.Generic;

    public class MaxMemberLimitBySuperDataAccess : IMaxMemberLimitBySuperDataAccess
    {
        public List<SiteEntity> GetCreditSites(GetSitesCriteria criteria)
        {
            List<SiteEntity> result = new List<SiteEntity>();
            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    if (!Converter.ToBool(reader["isLicensee"]))
                    {
                        result.Add(new SiteEntity()
                        {
                            SiteId = Converter.ToInt(reader["siteid"]),
                            SiteName = Converter.ToString2(reader["site"]),
                            SiteLabel = Converter.ToString2(reader["sitelabel"])
                        });
                    }
                }
            }

            return result;
        }

        public MaxMemberLimitBySuperSearchResult SearchSuper(SearchSuperCriteria criteria)
        {
            MaxMemberLimitBySuperSearchResult result = new MaxMemberLimitBySuperSearchResult();
            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    result.ListSuper.Add(new MaxMemberLimitBySuperEntity()
                    {
                        RowNum = Converter.ToInt(reader["RowNum"]),
                        SuperName = Converter.ToString2(reader["UserName"]),
                        MaxMembersLimit = Converter.ToInt(reader["MaxMembersLimit"]),
                        NumberOfMember = Converter.ToInt(reader["NumberOfMember"]),
                        Remark = Converter.ToString2(reader["Remark"]),
                        Site = Converter.ToString2(reader["Site"])
                    });
                }

                reader.Close();
                result.TotalRow = Converter.ToInt(objDb.GetOuputParams()["totalrow"]);
            }

            return result;
        }

        public MaxMemberLimitBySuperUpdateResultModel UpdateSuper(UpdateSuperCriteria criteria)
        {
            var result = new List<MaxMemberLimitBySuperUpdateResultEntity>();
            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new MaxMemberLimitBySuperUpdateResultEntity()
                    {
                        CustId = Converter.ToInt(reader["CustId"]),
                        UserName = Converter.ToString2(reader["UserName"]),
                        ErrCode = Converter.ToInt(reader["ErrCode"]),
                        ErrMsg = Converter.ToString2(reader["ErrMessage"])
                    });
                }

                return new MaxMemberLimitBySuperUpdateResultModel
                {
                    Data = result,
                    ErrorCode = Converter.ToInt(objDb.GetOuputParams()["errcode"]),
                    ErrorMessage = objDb.GetOuputParams()["errmsg"].ToString(),
                };
            }
        }
    }
}
