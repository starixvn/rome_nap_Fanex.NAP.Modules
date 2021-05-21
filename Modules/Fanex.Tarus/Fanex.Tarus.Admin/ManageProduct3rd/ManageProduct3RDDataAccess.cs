namespace Fanex.Tarus.Admin.ManageProduct3RD
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Tarus.Admin.DataAccess;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.ManageProduct3RD.Criteria;
    using Fanex.Tarus.Admin.ManageProduct3RD.Models;
    using Fanex.Tarus.Infrastructure.Helper;

    public interface IManageProduct3RDDataAccess
    {
        Product3RDModel Select(SelectProduct3RDCriteria criteria);

        Result Insert(InsertProduct3RDCriteria criteria);

        Result Update(UpdateProduct3RDCriteria criteria);

        Result Delete(DeleteProduct3RDCriteria criteria);
    }

    public class ManageProduct3RDDataAccess : BaseDataAccess, IManageProduct3RDDataAccess
    {
        public Product3RDModel Select(SelectProduct3RDCriteria criteria)
        {
            var entities = new List<Product3RDEntity>();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objDb.ExecuteReader(criteria.GetSpParams());
                while (reader.Read())
                {
                    entities.Add(new Product3RDEntity
                    {
                        RowNum = DataUtils.ToString(reader["rownum"]),
                        ProductId = DataUtils.ToString(reader["productid"]),
                        ProductName = DataUtils.ToString(reader["productname"]),
                        Description = DataUtils.ToString(reader["description"]),
                        ProductPrefix = DataUtils.ToString(reader["productprefix"]),
                        OfferSingle = DataUtils.ToString(reader["offersingle"]),
                        ApplyDCS = DataUtils.ToString(reader["applydcs"]),
                        Enabled = DataUtils.ToString(reader["enabled"]),
                        EndOfSupportDate = reader["EndOfSupportDate"] == DBNull.Value ? string.Empty : DataUtils.ToDateTime(reader["endofsupportdate"]).ToString("MM/dd/yyyy"),
                        OfferSite = DataUtils.ToString(reader["offersite"])
                    });
                }

                reader.Close();
                return new Product3RDModel
                {
                    Product3RDEntities = entities,
                    TotalRows = DataUtils.ToInt32(objDb.GetOuputParams()["totalrows"])
                };
            }
        }

        public Result Insert(InsertProduct3RDCriteria criteria) => ExecuteNonQuery(criteria);

        public Result Update(UpdateProduct3RDCriteria criteria) => ExecuteNonQuery(criteria);

        public Result Delete(DeleteProduct3RDCriteria criteria) => ExecuteNonQuery(criteria);
    }
}
