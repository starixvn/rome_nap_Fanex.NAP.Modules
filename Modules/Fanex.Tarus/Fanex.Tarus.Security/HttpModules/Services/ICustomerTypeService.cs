namespace Fanex.Tarus.Security.HttpModules.Services
{
    internal interface ICustomerTypeService
    {
        bool IsDepositByParam(string custIdOrUserName);

        bool IsDeposit(int custId);

        bool IsDeposit(string userName);
    }
}
