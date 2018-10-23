namespace Fanex.Tarus.Security.Repositories
{
    using System.Collections.Generic;
    using Models;
    using NAP.Framework.Data;

    internal interface IUserPermissionDataAccess
    {
        AuthorizationResult CheckSpecialSitePermission(ICriteria criteria);

        IEnumerable<UserPermission> GetUserPermissions(ICriteria criteria);
    }
}
