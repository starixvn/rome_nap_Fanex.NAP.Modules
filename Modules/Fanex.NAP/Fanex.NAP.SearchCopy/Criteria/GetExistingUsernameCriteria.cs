namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using Fanex.NAP.Framework.Data;

    internal class GetExistingUsernameCriteria : CriteriaBase
    {
        public GetExistingUsernameCriteria(string usernames)
        {
            Usernames = usernames;
        }

        public string Usernames { get; }

        public override string GetSettingKey()
            => "Acc_CustomerSelExistingUsernames";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(Usernames);
    }
}