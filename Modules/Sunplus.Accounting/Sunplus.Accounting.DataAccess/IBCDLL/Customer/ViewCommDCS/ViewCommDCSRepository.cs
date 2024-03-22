namespace Sunplus.Accounting.DataAccess.IBCDLL.Customer
{
    using Fanex.Common;
    using Fanex.Data;
    using Sunplus.Accounting.Entities.Criteria.ViewPT;
    using Sunplus.Accounting.Entities.Customer.PT.ViewPT;
    using System.Collections.Generic;

    public class ViewCommDCSRepository : IViewCommDCSRepository
    {
        public IList<DCSProduct> GetProducts()
        {
            var criteria = new ViewCommDCSCriteria();
            var products = new List<DCSProduct>();
            int[] ListCommSettingIds = new int[] { 25, 3, 5, 14, 19 };
            foreach (int id in ListCommSettingIds)
            {
                criteria.SettingType = id;
                GetProductLists(criteria, products);
            }

            return products;
        }

        private static void GetProductLists(ViewCommDCSCriteria criteria, List<DCSProduct> products)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        products.Add(new DCSProduct
                        {
                            ProductId = Converter.ToInt(reader["ProductID"]),
                            ProductName = Converter.ToString2(reader["Name"])
                        });
                    }
                }
            }
        }
    }
}