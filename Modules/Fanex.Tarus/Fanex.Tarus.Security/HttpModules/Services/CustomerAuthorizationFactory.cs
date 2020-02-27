namespace Fanex.Tarus.Security.HttpModules.Services
{
    public static class CustomerAuthorizationFactory
    {
        public static ICustomerAuthorization GetInstance(string type)
        {
            switch (type)
            {
                case "[A]":
                    return new SpecialSiteCustomerAuthorization();

                case "[B]":
                    return new CreditOrDepositCustomerAuthorization();

                case "[C]":
                    return new FinanceCustomerAuthorization();

                default:
                    return null;
            }
        }
    }
}
