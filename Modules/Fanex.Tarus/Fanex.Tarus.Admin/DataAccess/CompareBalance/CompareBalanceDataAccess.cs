using Sunplus.Accounting.DataAccess.IBCDLL.Accounting;

namespace Fanex.Tarus.Admin.DataAccess.CompareBalance
{
    using Common;
    using Criteria.CompareBalance;
    using Data;
    using Entities.CompareBalance;
    using Fanex.Tarus.Infrastructure.Common;

    public class CompareBalanceDataAccess
    {
        public CompareBalanceModel GetCompareBalance(GetCompareBalanceCriteria criteria)
        {
            CompareBalanceModel result = new CompareBalanceModel();
            criteria.DeniedList = AccountCredit.Acc_DeniedSiteCurrency_Sel(AppSession.AccId);
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                if (reader.Read())
                {
                    result.UserName = Converter.ToString2(reader["UserName"]);
                    result.TotalCash = Converter.ToDecimal(reader["TotalCash"]);
                    result.OutStanding = Converter.ToDecimal(reader["OutStanding"]);
                    result.CreditBonusWallet = Converter.ToDecimal(reader["CreditBonusWallet"]);
                    result.CreditCasino = Converter.ToDecimal(reader["CreditCasino"]);
                    result.CreditNetPosition = Converter.ToDecimal(reader["CreditNetPosition"]);
                    result.CreditOthers = Converter.ToDecimal(reader["CreditOthers"]);
                    result.CreditSports = Converter.ToDecimal(reader["CreditSports"]);
                    result.CreditTransfer = Converter.ToDecimal(reader["CreditTransfer"]);
                    result.DebitBonusWallet = Converter.ToDecimal(reader["DebitBonusWallet"]);
                    result.DebitCasino = Converter.ToDecimal(reader["DebitCasino"]);
                    result.DebitNetPosition = Converter.ToDecimal(reader["DebitNetPosition"]);
                    result.DebitOthers = Converter.ToDecimal(reader["DebitOthers"]);
                    result.DebitSports = Converter.ToDecimal(reader["DebitSports"]);
                    result.DebitTransfer = Converter.ToDecimal(reader["DebitTransfer"]);
                }
            }

            return result;
        }
    }
}
