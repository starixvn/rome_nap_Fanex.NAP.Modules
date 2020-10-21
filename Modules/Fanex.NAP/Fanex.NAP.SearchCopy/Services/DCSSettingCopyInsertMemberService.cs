namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System;
    using Core.Models;
    using Criteria;
    using Logging.Extensions.Web;
    using NAP.Framework.Data;
    using Repositories;

    internal class DCSSettingCopyInsertMemberService : IDCSSettingCopyInsertMemberService
    {
        private readonly IDCSSettingCopyInsertMemberRepository _repository;

        public DCSSettingCopyInsertMemberService(IDCSSettingCopyInsertMemberRepository repository = null)
        {
            _repository = repository ?? new DCSSettingCopyInsertMemberRepository();
        }

        public Result CorrectMemberPositionTaking(int custId, int sourceCustId, int actorId, string ip, byte settingStatus)
        {
            ICriteria criteria = new DCSSettingCopyInsertMemberCriteria(
                custId,
                sourceCustId,
                actorId,
                ip, 
                settingStatus);

            try
            {
                return _repository.CorrectMemberPositionTaking(criteria);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return null;
        }
    }
}