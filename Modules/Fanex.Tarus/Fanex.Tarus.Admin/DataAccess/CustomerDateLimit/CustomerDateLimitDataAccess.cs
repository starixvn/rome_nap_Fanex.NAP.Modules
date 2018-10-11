namespace Fanex.Tarus.Admin.DataAccess.CustomerDateLimit
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Fanex.Data;
    using Fanex.Tarus.Admin.Criteria.CustomerDateLimit;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.CustomerDateLimit;

    public interface ICustomerDateLimitDataAccess
    {
        IEnumerable<CustomerDateLimitModel> GetCustomers(GetAllCustomerDateLimitCriteria criteria);

        CustomerDateLimitModel GetById(int custId);

        Result Update(UpdateCustomerDateLimitCriteria criteria);
    }

    public class CustomerDateLimitDataAccess : ICustomerDateLimitDataAccess
    {
        public IEnumerable<CustomerDateLimitModel> GetCustomers(GetAllCustomerDateLimitCriteria criteria)
        {
            var customers = new List<CustomerDateLimitModel>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSPParams());
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new CustomerDateLimitModel
                        {
                            CustId = Convert.ToInt32(reader["CustId"]),
                            UserName = reader["UserName"].ToString(),
                            LimitDays = reader["LimitDays"] == DBNull.Value ? (int?)null : Converter.ToInt(reader["LimitDays"]),
                            MinDate = reader["MinDate"] == DBNull.Value ? (DateTime?)null : Converter.ToDateTime(reader["MinDate"]),
                        });
                    }


                }

                var outputResult = db.GetOuputParams();
                if (outputResult != null)
                {
                    criteria.TotalRecords = Convert.ToInt32(outputResult["totalrecords"]);
                }
            }

            return customers;
        }

        public CustomerDateLimitModel GetById(int custId)
        {
            var criteria = new GetCustomerDateLimitByIdCriteria { CustId = custId };
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSPParams());
                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CustomerDateLimitModel
                        {
                            CustId = Convert.ToInt32(reader["CustId"]),
                            LimitDays = reader["LimitDays"] == DBNull.Value ? (int?)null : Converter.ToInt(reader["LimitDays"]),
                            MinDate = reader["MinDate"] == DBNull.Value ? (DateTime?)null : Converter.ToDateTime(reader["MinDate"])
                        };
                    }
                }
            }

            return null;
        }

        public Result Update(UpdateCustomerDateLimitCriteria criteria)
        {
            var result = new Result();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSPParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    result = new Result((int)outputParams["errcode"], outputParams["errmsg"].ToString());
                }
            }

            return result;
        }
    }
}
