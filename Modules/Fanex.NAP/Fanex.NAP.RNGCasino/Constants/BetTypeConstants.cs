namespace Fanex.Module.RNGCasino.Constants
{
    public static class BetTypeConstants
    {
        public static bool IsJackpotContribution(int betType)
            => betType == 2198 || betType == 2398 || betType == 2998;

        public static bool IsProgressiveJackpot(int bettype)
            => bettype == 2199 || bettype == 2399 || bettype == 2999;
    }
}
