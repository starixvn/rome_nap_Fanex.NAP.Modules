namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Models;
    using Keno.KenoSetting.Services;
    using Keno.KenoSetting.ViewModels;
    using Logging.Extensions.Web;

    internal class KenoService
    {
        private readonly IKenoSettingService _kenoService;

        public KenoService(IKenoSettingService kenoService = null)
        {
            _kenoService = kenoService ?? new KenoSettingService();
        }

        public IEnumerable<Result> UpdateMemberData(int custId, int sourceCustId, int actorId)
        {
            try
            {
                var sourceSettings = GetSourceMemberSettings(sourceCustId, actorId);

                return UpdateMemberSetting(custId, sourceSettings);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return Enumerable.Empty<Result>();
        }

        private AgentEditMemberSettingViewModel GetSourceMemberSettings(int sourceCustId, int actorId)
        {
            var criteria = new CustomerInformationViewModel
            {
                CustId = sourceCustId,
                UserId = actorId
            };

            return _kenoService.GetSettingInCaseOfAgentEditMember(criteria);
        }

        private IEnumerable<Result> UpdateMemberSetting(int custId, AgentEditMemberSettingViewModel sourceSettings)
        {
            sourceSettings.CustomerInformation.CustId = custId;

            foreach (var betSetting in sourceSettings.BetSettings)
            {
                betSetting.IsStatusUpdated = true;
                betSetting.IsBetSettingUpdated = true;
            }

            foreach (var positionTaking in sourceSettings.PositionTakings)
            {
                positionTaking.A1 = positionTaking.A1 < positionTaking.A2 ? positionTaking.A1 : positionTaking.A2;
                positionTaking.IsUpdateA1 = true;
            }

            return _kenoService.UpdateMemberCustomerSettingInCaseOfAgentEditMember(sourceSettings);
        }
    }
}