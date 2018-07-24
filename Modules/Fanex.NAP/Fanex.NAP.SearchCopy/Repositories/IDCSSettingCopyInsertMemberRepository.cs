namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using Core.Models;
    using Fanex.NAP.Framework.Data;

    internal interface IDCSSettingCopyInsertMemberRepository
    {
        Result CorrectMemberPositionTaking(ICriteria criteria);
    }
}
