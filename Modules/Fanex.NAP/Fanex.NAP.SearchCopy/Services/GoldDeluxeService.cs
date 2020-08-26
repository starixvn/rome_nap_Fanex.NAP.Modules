namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System.Globalization;
    using AutoMapper;
    using Casino.Services.Impl;
    using Casino.Services.Interface;
    using GoldDeluxe.CoreSetting.CommandResult;
    using GoldDeluxe.CoreSetting.Enumerations;
    using GoldDeluxe.CoreSetting.ParameterObjects;
    using GoldDeluxe.CoreSetting.Services;
    using GoldDeluxe.CoreSetting.ViewModels;

    internal class GoldDeluxeService
    {
        private readonly ISettingService _goldDeluxeService;
        private readonly string _language;
        private readonly CasinoServiceBase casinoServiceBase;

        public GoldDeluxeService(
            ISettingService goldDeluxeService = null,
            CasinoServiceBase casinoServiceBase = null,
            string language = null)
        {
            _goldDeluxeService = goldDeluxeService ?? new SettingService();
            this.casinoServiceBase = casinoServiceBase ?? new CasinoServiceBaseImpl();
            _language = language ?? CultureInfo.CurrentCulture.Name;
        }

        public IMultipleCommandResults UpdateMemberData(int custId, int uplineId, int sourceCustId, int actorId, string ip)
        {
            // Update WinLimit, PT and Comm.
            var result = UpdateSettingFromExistingMember(custId, sourceCustId, actorId, ip);
            var commandResults = result.CommandResultsList;

            var gameSettingResult = UpdateGameSettings(custId, uplineId, sourceCustId, actorId, ip);
            commandResults.Add(gameSettingResult);

            if (result.IsSuccessful)
            {
                var statusResult = UpdateStatus(custId, uplineId, sourceCustId, actorId, ip);
                commandResults.Add(statusResult);
            }

            return result;
        }

        private IMultipleCommandResults UpdateSettingFromExistingMember(int custId, int sourceCustId, int actorId, string ip)
        {
            var criteria = new UpdateSettingFromExistingMemberParameters
            {
                CustId = custId,
                FromCustId = sourceCustId,
                UpLineId = actorId,
                ActorId = actorId,
                SubAccId = 0,
                Ip = ip,
                Language = _language
            };

            return _goldDeluxeService.UpdateSettingFromExistingMember(criteria);
        }

        private ICommandResults UpdateGameSettings(int custId, int uplineId, int sourceCustId, int actorId, string ip)
        {
            var editSettingViewModel = new EditSettingViewModel
            {
                CustId = sourceCustId,
                UpLineId = uplineId,
                Level = CustomerLevel.Member,
                UpLineLevel = CustomerLevel.Agent,
                ActorId = actorId,
                Language = _language,
                FormType = FormType.Edit
            };

            var settingParams = Mapper.Map<EditSettingViewModel, GetSettingParameters>(editSettingViewModel);
            var settingDTO = _goldDeluxeService.GetSetting(settingParams);

            var criteria = new UpdateGameSettingParameters
            {
                ActorId = actorId,
                CustId = custId,
                GameSetting = settingDTO.GameSetting,
                Ip = ip,
                Language = _language
            };

            return _goldDeluxeService.UpdateGameSetting(criteria);
        }

        private ICommandResults UpdateStatus(int custId, int uplineId, int sourceCustId, int actorId, string ip)
        {
            var custInfoIBC = casinoServiceBase.GetCustInfoIBC(sourceCustId, uplineId);
            var criteria = new UpdateDisableStatusParameters
            {
                CustId = custId,
                Disable = custInfoIBC.GeneralInfo.GoldDeluxeDisabled,
                ActorId = actorId,
                SubAccId = 0,
                Ip = ip,
                IsSC = true
            };

            return _goldDeluxeService.UpdateStatus(criteria);
        }
    }
}
