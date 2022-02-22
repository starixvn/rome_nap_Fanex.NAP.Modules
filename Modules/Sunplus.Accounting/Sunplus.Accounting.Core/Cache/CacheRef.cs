namespace Sunplus.Accounting.Core.Cache
{
    using DNAide.Core;
    using Fanex.RemotingRef;
    using System.Threading;

    /// <summary>
    /// Summary description for CacheRef
    /// </summary>
    public static class CacheRef
    {
        private static readonly IRemotingRefService remotingRefService = new RemotingRefService();

        public static string GetBettype(object bettypeID)
        {
            return remotingRefService.GetBetTypeName(bettypeID.ToString(), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetSportName(object sportID)
            => remotingRefService.GetSportName(sportID.ToString(), Thread.CurrentThread.CurrentCulture.Name, null) ?? remotingRefService.GetResourceDataByRefIdAndResourceId(
                "DummySport",
                sportID.ToString(),
                Thread.CurrentThread.CurrentCulture.Name,
                string.Empty);

        public static string GetTeamName(object teamID)
        {
            return remotingRefService.GetTeamName(teamID.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }

        public static string GetLeagueName(object leagueID)
        {
            return remotingRefService.GetLeagueName(leagueID.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty);
        }

        public static string GetHorseTeamName(object CodeID)
        {
            return remotingRefService.GetHorseTeamName(CastAide.AsString(CodeID), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetOddsTypeName(object OddsType)
        {
            return remotingRefService.GetOddsTypeName(CastAide.AsString(OddsType), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetCombinationName(string betteam)
        {
            string name = string.Empty;
            switch (betteam)
            {
                case "110":
                    name = "Trixie";
                    break;

                case "1110":
                    name = "Yankee";
                    break;

                case "11110":
                    name = "Canadian";
                    break;

                case "111110":
                    name = "Heinz";
                    break;

                case "1111110":
                    name = "Super Heinz";
                    break;

                case "11111110":
                    name = "Goliath";
                    break;
            }
            return name;
        }

        public static string GetLiveCasinoResult(int resultId)
        {
            string result = string.Empty;
            /*switch(resultId)
            {
                case 1:
                    result = "Banker";
                    break;

                case 2:
                    result = "Player";
                    break;

                case 3:
                    result = "Tie";
                    break;

                case 4:
                    result = "Big";
                    break;

                case 5:
                    result = "Small";
                    break;

                case 6:
                    result = "B.Pair";
                    break;

                case 7:
                    result = "P.Pair";
                    break;
            }*/
            return remotingRefService.GetCasinoBetItem(resultId.ToString(), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetRouletteBetItem(int itemId)
        {
            return remotingRefService.GetRouletteBetItem(itemId.ToString(), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetBettypeNameSportingSolution(object betypeId, string resourceId)
        {
            string bettypeName = "";
            bettypeName = remotingRefService.GetBetTypeName(betypeId.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty, resourceId);
            return bettypeName;
        }
    }
}