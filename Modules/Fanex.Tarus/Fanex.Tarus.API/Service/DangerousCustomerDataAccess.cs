namespace Fanex.Tarus.DataAccess.Implement.Customer
{
    using API.Model.Customer.DangerousCustomer;
    using API.Model.DangerousCustomer;
    using Entities.Criteria.Customer;
    using Data;
    using Interface.Customer;
    using System.Collections.Generic;

    public class DangerousCustomerDataAccess : IDangerousCustomerDataAccess
    {
        public List<UpdateDangerousCustomerResponse> UpdateDangerousCustomer(UpdateDangerousCustomerRequest request)
        {
            List<UpdateDangerousCustomerResponse> response = new List<UpdateDangerousCustomerResponse>();
            var criteria = new UpdateDangerousCustomerCriteria(request);
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = db.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    response = response ?? new List<UpdateDangerousCustomerResponse>();
                    response.Add(new UpdateDangerousCustomerResponse()
                    {
                        UserName = reader["ListUsername"].ToString(),
                        Result = new NapApiResult()
                        {
                            ErrorCode = 100,
                            ErrorMessage = reader["ErrorMsg"].ToString()
                        }
                    });
                }

                return response;
            }
        }
    }
}
