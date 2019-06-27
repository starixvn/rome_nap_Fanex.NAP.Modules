namespace Fanex.Tarus.Infrastructure.Constants
{
    using Fanex.Tarus.Infrastructure.Resource;

    public class CN88CustomerStatusConstants
    {
        public const int Active = 4;
        public const int ViewOnly = 3;
        public const int Suspended = 2;
        public const int Inactive = 1;

        public static string StatusName(int statusId)
        {
            switch (statusId)
            {
                case Active:
                    return Labels.CN88Active;
                case ViewOnly:
                    return Labels.CN88ViewOnly;
                case Suspended:
                    return Labels.CN88Suspended;
                case Inactive:
                    return Labels.CN88Inactive;
                default:
                    return string.Empty;
            }
        }
    }
}
