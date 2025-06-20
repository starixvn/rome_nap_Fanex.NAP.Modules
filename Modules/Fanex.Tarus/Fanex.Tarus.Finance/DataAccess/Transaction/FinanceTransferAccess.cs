namespace Fanex.Tarus.Finance.DataAccess.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Finance.Criteria.Transaction;
    using Fanex.Tarus.Finance.Entities;
    using Fanex.Tarus.Infrastructure.Extensions;

    internal interface IFinanceTransferAccess
    {
        IEnumerable<CustomerTransfer> GetCustomerTransferList(FinanceTransferMulUpdCriteria criteria);

        IEnumerable<CustomerCrossTransfer> GetCustomerCrossTransferList(FinanceCrossTransferMulUpdCriteria criteria);

        Result ExecuteNonQuery(ICriteria criteria);

        Result ExecuteNonQuery(ICriteria criteria, string agIp);

        ResultRefNo ExecuteNonQueryWithRefNo(ICriteria criteria, string agIp);
    }

    internal class FinanceTransferAccess : IFinanceTransferAccess
    {
        public IEnumerable<CustomerTransfer> GetCustomerTransferList(FinanceTransferMulUpdCriteria criteria)
        {
            Func<IDataReader, CustomerTransfer> buildCustomerTransfer = reader =>
                new CustomerTransfer(
                    Convert.ToInt32(reader["fromcustid"]),
                    reader["fromusername"].ToString(),
                    Convert.ToInt32(reader["tocustid"]),
                    reader["tousername"].ToString(),
                    Convert.ToDecimal(reader["amount"]),
                    Convert.ToDouble(reader["exchangerate"]),
                    reader["description"].ToString(),
                    Convert.ToBoolean(reader["iscollection"]),
                    reader["buid"].ToString(),
                    Convert.ToInt32(reader["userid"]),
                    reader["ip"].ToString());

            var customerTransferList = ExecuteReader(criteria, buildCustomerTransfer, out var result);
            criteria.ErrCode = result.ErrorCode;
            criteria.ErrMsg = result.ErrorMessage;

            return customerTransferList;
        }

        public IEnumerable<CustomerCrossTransfer> GetCustomerCrossTransferList(FinanceCrossTransferMulUpdCriteria criteria)
        {
            Func<IDataReader, CustomerCrossTransfer> buildCustomerTransfer = reader =>
                   new CustomerCrossTransfer(
                       Convert.ToInt32(reader["fromcustid"]),
                       reader["fromusername"].ToString(),
                       Convert.ToInt32(reader["tocustid"]),
                       reader["tousername"].ToString(),
                       Convert.ToDecimal(reader["fromamount"]),
                       Convert.ToDecimal(reader["toamount"]),
                       reader["remark"].ToString(),
                       Convert.ToBoolean(reader["iscollection"]),
                       reader["forexusername"].ToString(),
                       Convert.ToInt32(reader["userid"]),
                       reader["ip"].ToString());

            var customerTransferList = ExecuteReader(criteria, buildCustomerTransfer, out var result);
            criteria.ErrCode = result.ErrorCode;
            criteria.ErrMsg = result.ErrorMessage;

            return customerTransferList;
        }

        private static IEnumerable<T> ExecuteReader<T>(ICriteria criteria, Func<IDataReader, T> buildT, out Result result)
        {
            IList<T> customerTransferList = new List<T>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        customerTransferList.Add(buildT(reader));
                    }
                }

                result = ToResult(db);
            }

            return customerTransferList;
        }

        public Result ExecuteNonQuery(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQuery(criteria.GetSpParams());

                    return ToResult(db);
                }
            }
            catch (Exception ex)
            {
                LogExtensions.Error(string.Empty, criteria.GetSettingKey(), criteria.GetSpParams(), ex);
                return Result.ServerError(ex.Message);
            }
        }

        public Result ExecuteNonQuery(ICriteria criteria, string agIp)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQueryScaleoutDB(criteria.GetSpParams(), agIp);

                    return ToResult(db);
                }
            }
            catch (Exception ex)
            {
                LogExtensions.Error(agIp, criteria.GetSettingKey(), criteria.GetSpParams(), ex);
                return Result.ServerError(ex.Message);
            }
        }

        private static Result ToResult(ObjectDb db)
        {
            var outputs = db.GetOuputParams();
            return new Result(Convert.ToInt32(outputs["errcode"]), outputs["errmsg"].ToString());
        }

        public ResultRefNo ExecuteNonQueryWithRefNo(ICriteria criteria, string agIp)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    db.ExecuteNonQueryScaleoutDB(criteria.GetSpParams(), agIp);

                    return ToResultTransfer(db);
                }
            }
            catch (Exception ex)
            {
                LogExtensions.Error(agIp, criteria.GetSettingKey(), criteria.GetSpParams(), ex);
                return new ResultRefNo(1, ex.Message);
            }
        }

        private static ResultRefNo ToResultTransfer(ObjectDb db)
        {
            var outputs = db.GetOuputParams();
            var refNo = outputs["refno"];

            return new ResultRefNo(
                Convert.ToInt32(outputs["errcode"]),
                outputs["errmsg"].ToString(),
                refNo == null || refNo == DBNull.Value ? default(long?) : Convert.ToInt64(refNo));
        }
    }
}
