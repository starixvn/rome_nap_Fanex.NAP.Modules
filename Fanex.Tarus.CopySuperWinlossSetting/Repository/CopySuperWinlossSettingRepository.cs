namespace Fanex.Tarus.CopySuperWinlossSetting.Repository
{
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.CopySuperWinlossSetting.Model;
    using System.Collections.Generic;

    public interface ICopySuperWinlossSettingRepository
    {
        IEnumerable<WinlossSettingItem> GetCustomerWinLimit(ICriteria criteria);

        Result UpdateCustomerWinLossLimit(ICriteria criteria);
    }

    internal class CopySuperWinlossSettingRepository: ICopySuperWinlossSettingRepository
    {
        public IEnumerable<WinlossSettingItem> GetCustomerWinLimit(ICriteria criteria)
        {
            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());

                while (reader.Read())
                {
                    yield return new WinlossSettingItem()
                    {
                        SportTypeId = Utils.ToInt32(reader["SportType"]),
                        BetTypeId = Utils.ToInt32(reader["BetType"]),
                        WinLimit = reader.GetName(2).Equals("MaxWinLimit") ? Utils.ToDecimal(reader["MaxWinLimit"]) : 0,
                        MaxWinRange = reader.GetName(3).Equals("MaxWinRange") ? Utils.ToDecimal(reader["MaxWinRange"]) : 0,
                        LossLimit = Utils.ToDecimal(reader["MaxLossLimit"]),
                        MinLossRange = Utils.ToDecimal(reader["MinLossRange"]),
                        MaxLossRange = Utils.ToDecimal(reader["MaxLossRange"]),
                        IsAutoReset = Utils.ToBoolean(reader["IsAutoReset"]),
                        IsAutoResetUpline = Utils.ToBoolean(reader["IsAutoResetUpline"])
                    };
                }
            }
        }

        public Result UpdateCustomerWinLossLimit(ICriteria criteria)
        {
            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.ExecuteNonQuery(criteria.GetSpParams());
                var outputs = objDb.GetOuputParams();
                return new Result
                {
                    ErrorCode = Utils.ToInt32(outputs["errcode"]),
                    ErrorMessage = Utils.ToString(outputs["errmsg"])
                };
            }
        }
    }
}
