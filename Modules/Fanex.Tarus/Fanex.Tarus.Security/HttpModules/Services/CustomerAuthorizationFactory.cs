namespace Fanex.Tarus.Security.HttpModules.Services
{
    internal static class CustomerAuthorizationFactory
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
                    return new CN88CustomerAuthorization();

                default:
                    return null;
            }
        }
    }
}
