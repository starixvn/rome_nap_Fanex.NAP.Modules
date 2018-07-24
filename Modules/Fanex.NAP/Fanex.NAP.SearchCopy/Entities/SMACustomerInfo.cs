namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    public class SMACustomerInfo
    {
        public SMACustomerInfo(int custId, string username, byte roleId)
        {
            CustId = custId;
            Username = username;
            RoleId = roleId;
        }

        public int CustId { get; }

        public string Username { get; }

        public byte RoleId { get; }
    }
}