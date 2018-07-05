namespace Fanex.Tarus.Security.Models
{
    internal sealed class UserPermission
    {
        internal UserPermission(int groupId, string fileNames, string funcName, byte policy)
        {
            GroupId = groupId;
            FileNames = fileNames;
            FuncName = funcName;
            ActualPolicy = policy;
        }

        internal int GroupId { get; }

        internal string FileNames { get; }

        internal string FuncName { get; }

        internal byte ActualPolicy { get; }
    }
}
