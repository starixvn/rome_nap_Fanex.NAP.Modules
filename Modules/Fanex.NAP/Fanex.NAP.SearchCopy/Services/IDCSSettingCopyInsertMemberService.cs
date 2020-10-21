namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using Core.Models;

    internal interface IDCSSettingCopyInsertMemberService
    {
        Result CorrectMemberPositionTaking(int custId, int sourceCustId, int actorId, string ip, byte settingStatus);
    }
}
