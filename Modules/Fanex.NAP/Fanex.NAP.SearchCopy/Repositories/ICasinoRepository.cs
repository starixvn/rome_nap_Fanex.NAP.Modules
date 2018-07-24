namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using Core.Models;
    using NAP.Framework.Data;

    internal interface ICasinoRepository
    {
        Result UpdateSyncCasinoForIBC(ICriteria criteria);
    }
}
