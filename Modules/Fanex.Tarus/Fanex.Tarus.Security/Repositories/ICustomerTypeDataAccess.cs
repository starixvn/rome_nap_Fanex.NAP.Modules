namespace Fanex.Tarus.Security.DataAccess
{
    using NAP.Framework.Data;

    internal interface ICustomerTypeDataAccess
    {
        bool GetCustomerTypes(ICriteria criteria);
    }
}
