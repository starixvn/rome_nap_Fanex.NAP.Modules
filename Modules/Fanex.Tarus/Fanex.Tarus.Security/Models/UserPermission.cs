namespace Fanex.Tarus.Security.Models
{
    public sealed class UserPermission
    {
        public UserPermission(int groupId, string fileNames, string funcName, byte policy)
        {
            GroupId = groupId;
            FileNames = fileNames;
            FuncName = funcName;
            ActualPolicy = policy;
        }

        public int GroupId { get; }

        public string FileNames { get; }

        public string FuncName { get; }

        public byte ActualPolicy { get; }
    }
}
