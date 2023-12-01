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

        private const string ProductGroupRefId = "ProductGroup";

        private const string DummySportRefId = "DummySport";

        private const string SubGroupRefId = "SubGroup";

        private const string SB_Comm_SMA_RefId = "SB_Comm_SMA";

        private const string SB_Comm_Member_RefId = "SB_Comm_Member";

        private const string G_GT_Sport_Bettype_RefId = "G_GT_Sport_Bettype";

        public static string GetBettype(object bettypeID)
        {
            return remotingRefService.GetBetTypeName(bettypeID.ToString(), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetSportName(object sportID)
            => remotingRefService.GetSportName(sportID.ToString(), Thread.CurrentThread.CurrentCulture.Name, null) ?? remotingRefService.GetResourceDataByRefIdAndResourceId(
                DummySportRefId,
                sportID.ToString(),
                Thread.CurrentThread.CurrentCulture.Name,
                string.Empty);

        public static string GetTeamName(object teamID)
            => remotingRefService.GetTeamName(teamID.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty);

        public static string GetLeagueName(object leagueID)
            => remotingRefService.GetLeagueName(leagueID.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty);

        public static string GetHorseTeamName(object CodeID)
            => remotingRefService.GetHorseTeamName(CastAide.AsString(CodeID), Thread.CurrentThread.CurrentCulture.Name);

        public static string GetOddsTypeName(object OddsType)
            => remotingRefService.GetOddsTypeName(CastAide.AsString(OddsType), Thread.CurrentThread.CurrentCulture.Name);

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
            return remotingRefService.GetCasinoBetItem(resultId.ToString(), Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetRouletteBetItem(int itemId)
            => remotingRefService.GetRouletteBetItem(itemId.ToString(), Thread.CurrentThread.CurrentCulture.Name);

        public static string GetBettypeNameSportingSolution(object betypeId, string resourceId)
        {
            string bettypeName = "";
            bettypeName = remotingRefService.GetBetTypeName(betypeId.ToString(), Thread.CurrentThread.CurrentCulture.Name, string.Empty, resourceId);
            return bettypeName;
        }

        public static string GetProductGroupName(int productGroupId, string language = "en-US")
            => remotingRefService.GetResourceDataByRefIdAndResourceId(ProductGroupRefId, productGroupId.ToString(), language, productGroupId.ToString());

        public static string GetSubGroupName(int? subGroupId, string language = "en-US")
            => remotingRefService.GetResourceDataByRefIdAndResourceId(SubGroupRefId, subGroupId?.ToString(), language, subGroupId?.ToString());

        public static string GetSportsbookCommSMAName(int productGroupId, int groupTypeId, int sporttypeId, int bettypeId, string language = "en-US")
        {
            var refKey = CombineRefKey(productGroupId, groupTypeId, sporttypeId, bettypeId);
            return remotingRefService.GetResourceDataByRefIdAndResourceId(SB_Comm_SMA_RefId, refKey, language, refKey);
        }
        public static string GetSportsbookCommMemberName(int productGroupId, int groupTypeId, int sporttypeId, int bettypeId, string language = "en-US")
        {
            var refKey = CombineRefKey(productGroupId, groupTypeId, sporttypeId, bettypeId);
            return remotingRefService.GetResourceDataByRefIdAndResourceId(SB_Comm_Member_RefId, refKey, language, refKey);
        }

        public static string GetNameByGroupSubGroup(int productGroupId, int groupTypeId, int sporttypeId, int bettypeId, string language = "en-US")
        {
            var refKey = CombineRefKey(productGroupId, groupTypeId, sporttypeId, bettypeId);
            return remotingRefService.GetResourceDataByRefIdAndResourceId(G_GT_Sport_Bettype_RefId, refKey, language, refKey);
        }

        private static string CombineRefKey(int productGroupId, int groupTypeId, int sporttypeId, int bettypeId)
            => $"{productGroupId}_{groupTypeId}_{sporttypeId}_{bettypeId}";
    }
}