namespace Fanex.Tarus.Security.Repositories
{
    using System.Collections.Generic;
    using Models;
    using NAP.Framework.Data;

    internal interface IUserPermissionDataAccess
    {
        IEnumerable<UserPermission> GetUserPermissions(ICriteria criteria);
    }
}
