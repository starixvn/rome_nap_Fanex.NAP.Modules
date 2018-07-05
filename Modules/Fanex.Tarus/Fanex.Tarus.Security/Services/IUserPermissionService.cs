namespace Fanex.Tarus.Security.Services
{
    using System.Collections.Generic;
    using Models;

    internal interface IUserPermissionService
    {
        bool CanRead(int userId, string functionName);

        bool CanHost(int userId, string functionName);

        byte? GetActualPolicy(int userId, string functionName);

        UserPermission GetUserPermission(int userId, string functionName);

        IEnumerable<UserPermission> GetUserPermissions(int userId);
    }
}
