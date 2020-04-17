namespace Fanex.Tarus.Admin.DataAccess
{
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Tarus.Admin.Entities;

    public class BaseDataAccess
    {
        public Result ExecuteNonQuery(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null)
                {
                    return new Result((int)outputParams["errcode"], outputParams["errmsg"].ToString());
                }
            }

            return null;
        }
    }
}
