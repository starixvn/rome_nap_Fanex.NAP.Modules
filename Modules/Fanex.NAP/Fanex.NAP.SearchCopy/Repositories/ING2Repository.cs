namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using Entities;
    using NAP.Framework.Data;

    internal interface ING2Repository
    {
        NG2Status GetStatus(ICriteria criteria);
    }
}
