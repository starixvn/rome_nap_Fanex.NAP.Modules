namespace Fanex.Tarus.Security.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constant;
    using Criteria;
    using Models;
    using Repositories;

    internal sealed class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionDataAccess permissionDataAccess;

        internal UserPermissionService(IUserPermissionDataAccess permissionDataAccess = null)
        {
            this.permissionDataAccess = permissionDataAccess ?? new UserPermissionDataAccess();
        }

        public bool CanRead(int userId, string functionName)
            => GetActualPolicy(userId, functionName).HasValue;

        public bool CanHost(int userId, string functionName)
            => GetActualPolicy(userId, functionName) == PolicyConst.Host;

        public byte? GetActualPolicy(int userId, string functionName)
            => GetUserPermission(userId, functionName)?.ActualPolicy;

        public UserPermission GetUserPermission(int userId, string functionName)
            => GetUserPermissions(userId)
                .FirstOrDefault(f => f.FuncName.Equals(functionName, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<UserPermission> GetUserPermissions(int userId)
            => permissionDataAccess.GetUserPermissions(new GetUserPermissionCriteria(userId));
    }
}
