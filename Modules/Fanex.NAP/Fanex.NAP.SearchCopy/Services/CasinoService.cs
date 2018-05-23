namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System;
    using System.Diagnostics;
    using Casino.Services.Impl;
    using Casino.Services.Interface;
    using CasinoLibrary.Model.UserException;
    using CasinoLibrary.SBAgent;
    using Core.Models;
    using Criteria;
    using Logging.Extensions.Web;
    using NAP.Framework.Data;
    using Repositories;

    internal class CasinoService
    {
        private readonly Customer _casinoCustomer;
        private readonly CasinoManager _casinoManager;
        private readonly ICasinoMemberWinLimitService _winLimitService;
        private readonly ICasinoRepository _casinoRepository;

        public CasinoService(
            Customer casinoCustomer = null,
            CasinoManager casinoManager = null,
            ICasinoMemberWinLimitService winLimitService = null,
            ICasinoRepository casinoRepository = null)
        {
            _casinoCustomer = casinoCustomer ?? new Customer();
            _casinoManager = casinoManager ?? new CasinoManager();
            _winLimitService = winLimitService ?? new CasinoMemberWinLimitService();
            _casinoRepository = casinoRepository ?? new CasinoRepository();
        }

        public bool UpdateSuperData(CustomerInsMemberCriteria customer, int casinoFromSystem)
        {
            try
            {
                var isSuccessful = _casinoCustomer.AutoInsertSuperFromExisting(
                    "01",
                    customer.CustId,
                    customer.ExistingCustId,
                    customer.UserName,
                    string.Empty,
                    customer.FirstName,
                    customer.LastName,
                    customer.Userpwd,
                    customer.Phone,
                    customer.MobilePhone,
                    customer.Fax,
                    customer.CurrencyId,
                    CasinoManager.Instance.IsEnabledJackpot(customer.CustId),
                    DateTime.Now,
                    customer.IP,
                    customer.Creator,
                    customer.CreatorName,
                    casinoFromSystem);

                UpdateWinLimit(customer.ExistingCustId, customer.CustId, customer.Creator);

                // Update Synced field.
                ICriteria criteria = new UpdateSyncCasinoForIBCCriteria(customer.DisableCasino)
                {
                    CustId = customer.CustId,
                    LoginIP = customer.IP,
                    UserId = customer.Creator
                };
                var result = _casinoRepository.UpdateSyncCasinoForIBC(criteria);

                return isSuccessful && result.IsSuccess;
            }
            catch (ErrorException casinoEx)
            {
                ExceptionHandler.Handle(new Exception(casinoEx.Message), TraceEventType.Warning);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return false;
        }

        public bool UpdateMemberData(CustomerInsMemberCriteria customer, byte systemId, int casinoFromSystem)
        {
            var isSuccessful = false;
            var errorMessage = string.Empty;

            var custInfo = _casinoManager.GetCustInfoIBC(customer.CustId, customer.UplineId);
            var generalInfo = custInfo.GeneralInfo;

            try
            {
                isSuccessful = _casinoCustomer.AutoInsertAccount(
                    "01",
                    generalInfo.IBCCustID,
                    generalInfo.UserName,
                    generalInfo.UserName2,
                    generalInfo.FirstName,
                    generalInfo.LastName,
                    custInfo.UserPwd,
                    generalInfo.Phone,
                    generalInfo.MobilePhone,
                    generalInfo.Currency,
                    generalInfo.Fax,
                    generalInfo.Closed,
                    generalInfo.Suspend,
                    generalInfo.IsEnablePT,
                    custInfo.SIBCCustID,
                    custInfo.MIBCCustID,
                    custInfo.AIBCCustID,
                    generalInfo.RoleId.ToString(),
                    false,
                    false,
                    false,
                    DateTime.Now,
                    customer.IP,
                    customer.Creator,
                    customer.CreatorName,
                    casinoFromSystem);
            }
            catch (ErrorException casinoEx)
            {
                errorMessage = casinoEx.Message;
                ExceptionHandler.Handle(new Exception(errorMessage), TraceEventType.Warning);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            if (isSuccessful)
            {
                _casinoManager.UpdateCSStatus(
                    generalInfo.IBCCustID,
                    true,
                    generalInfo.UplineDisabledCasino || customer.DisableCasino,
                    !generalInfo.CasinoDisabledDownline,
                    generalInfo.UpperCustID,
                    string.Empty,
                    0,
                    systemId,
                    "en",
                    generalInfo.UserName,
                    string.Empty,
                    out errorMessage);

                var settingModel = _casinoManager.MemberCasinoModel(customer.ExistingCustId, generalInfo.UpperCustID);
                _casinoManager.UpdateMemberSettings(
                    generalInfo.IBCCustID,
                    generalInfo.RoleId,
                    generalInfo.UpperCustID,
                    generalInfo.UplineRoleId,
                    customer.IP,
                    customer.Creator,
                    customer.CreatorName,
                    casinoFromSystem,
                    settingModel);

                UpdateWinLimit(customer.ExistingCustId, customer.CustId, customer.Creator);
            }
            else
            {
                _casinoManager.UpdateCSStatus(
                    generalInfo.IBCCustID,
                    false,
                    generalInfo.UplineDisabledCasino || customer.DisableCasino,
                    false,
                    generalInfo.UpperCustID,
                    string.Empty,
                    0,
                    systemId,
                    "en",
                    generalInfo.UserName,
                    string.Empty,
                    out errorMessage);

                _casinoCustomer.RemoveAccount(generalInfo.UserName, customer.IP, generalInfo.IBCCustID, generalInfo.UserName, casinoFromSystem);
            }

            return isSuccessful;
        }

        private Result UpdateWinLimit(int sourceCustId, int custId, int actorId)
        {
            var winLimit = _winLimitService.GetMemberWinLimit(sourceCustId);

            return _winLimitService.UpdateMemberWinLimit(custId, actorId, 0, winLimit);
        }
    }
}