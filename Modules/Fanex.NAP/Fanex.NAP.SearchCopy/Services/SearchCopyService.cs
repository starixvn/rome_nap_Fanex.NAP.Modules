namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using AutoMapper;
    using Casino.Services.Impl;
    using Casino.Services.Interface;
    using Core.Web;
    using Criteria;
    using Entities;
    using GoldDeluxe.CoreSetting.CommandResult;
    using GoldDeluxe.CoreSetting.ParameterObjects;
    using GoldDeluxe.CoreSetting.Services;
    using GoldDeluxe.CoreSetting.ViewModels;
    using Keno.KenoSetting.Services;
    using Keno.KenoSetting.ViewModels;
    using Logging.Extensions.Web;
    using NAP.Framework.Data;
    using PTEngine.BusinessObjects;
    using Repositories;

    public class SearchCopyService : ISearchCopyService
    {
        private static ISearchCopyRepository _searchCopyRepository;
        private readonly CasinoServiceBase casinoServiceBase;

        public SearchCopyService() : this(null, null)
        {
        }

        public SearchCopyService(
            ISearchCopyRepository searchCopyRepository,
            CasinoServiceBase casinoServiceBase)
        {
            _searchCopyRepository = searchCopyRepository ?? new SearchCopyRepository();
            this.casinoServiceBase = casinoServiceBase ?? new CasinoServiceBaseImpl();
        }

        public SearchResult SearchExistingUser(SearchExistingUserCriteria criteria)
        {
            var searchResult = _searchCopyRepository.SearchExistingUser(criteria);

            if (searchResult.CustId.Equals(-1))
            {
                return searchResult;
            }

            var result = _searchCopyRepository.CheckEnableCustomer(new CheckCustomerEnabledCriteria
            {
                Username = criteria.Username
            });

            if (result.Code.Equals(600))
            {
                return result.Code;
            }

            return searchResult;
        }

        public HideSports GetSportsStatusByUserName(string username)
        {
            return _searchCopyRepository.GetSportsStatusByUserName(username);
        }

        public IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria)
        {
            return _searchCopyRepository.GetSitesExistingSuper(criteria);
        }

        public CustomerInfo GetCustomerInfoByUserId(GetCustomerInfoByUserIdCriteria criteria)
        {
            return _searchCopyRepository.GetCustomerInfoByUserId(criteria);
        }

        public NAP.Framework.Data.CommandResult CopyExistingUser(ref CustomerInsMemberCriteria criteria)
        {
            return _searchCopyRepository.CopyExistingUser(ref criteria);
        }

        public NAP.Framework.Data.CommandResult CheckEnableCustomer(CheckCustomerEnabledCriteria criteria)
        {
            return _searchCopyRepository.CheckEnableCustomer(criteria);
        }

        public NAP.Framework.Data.CommandResult UpdateSync(ICriteria criteria)
        {
            return _searchCopyRepository.UpdateSync(criteria);
        }

        public NAP.Framework.Data.CommandResult InsertMember(Hashtable criteria, int existingCustId)
        {
            var result = _searchCopyRepository.InsertMember(criteria);
            var ipAddress = CurrentContext.Instance.Ip4Address;

            if (result.IsSuccessful)
            {
                // copy keno
                CopyKeno(criteria, existingCustId);

                // copy gold deluxe
                CopyGoldDeluxe(criteria, existingCustId, ipAddress);

                // copy dcs
                CopyDCS(criteria, existingCustId, ipAddress);
            }

            return result;
        }

        public DataTable GetSportBookSettingMember(int custId)
        {
            return _searchCopyRepository.GetSportBookSettingMember(custId);
        }

        private void CheckConstraintData(ref ICustomer oldCustomerSettings)
        {
            //check status
            oldCustomerSettings.StatusSetting.DisableVirtualSports =
                oldCustomerSettings.StatusSetting.DisableUplineVirtualSports ? true :
                oldCustomerSettings.StatusSetting.DisableVirtualSports;

            //check betting limit
            var index = 0;
            if (oldCustomerSettings.BettingLimit != null)
            {
                foreach (BettingLimitItem bettingLimitItem in oldCustomerSettings.BettingLimit.BettingLimitItems)
                {
                    var minBetMax = bettingLimitItem.MinBetMax;
                    var maxBetMin = bettingLimitItem.MaxBetMin;
                    var maxPerMatchMax = bettingLimitItem.MaxPerMatchMax;
                    var uplineIsUnlimited = minBetMax == 0 && maxBetMin == 0 && maxPerMatchMax == 0;

                    var minBet = bettingLimitItem.MinBet;
                    var maxBet = bettingLimitItem.MaxBet;
                    var maxPerMatch = bettingLimitItem.MaxPerMatch;

                    oldCustomerSettings.BettingLimit.BettingLimitItems[index].MinBet = minBet > minBetMax ?
                                                                                        minBet : minBetMax;

                    oldCustomerSettings.BettingLimit.BettingLimitItems[index].MaxBet = (maxBet < maxBetMin || uplineIsUnlimited) ?
                                                                                        maxBet : maxBetMin;

                    oldCustomerSettings.BettingLimit.BettingLimitItems[index].MaxPerMatch = (maxPerMatch < maxPerMatchMax || uplineIsUnlimited) ?
                                                                                            maxPerMatch : maxPerMatchMax;
                    index++;
                }
            }

            //check PT
            var sportTypeIndex = 0;
            foreach (SportType sport in oldCustomerSettings.PtSetting.SportTypes)
            {
                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt.A1 =
                                                                                                                        bettype.Pt.A1 < bettype.Pt.A2 ? bettype.Pt.A1 : bettype.Pt.A2;
                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }
        }

        private IList<Core.Models.Result> CopyKeno(Hashtable criteria, int existingCustId)
        {
            IList<Core.Models.Result> result = null;
            try
            {
                var kenoService = new KenoSettingService();
                var childSetting = kenoService.GetSettingInCaseOfAgentEditMember(new CustomerInformationViewModel { CustId = existingCustId, UserId = (int)criteria["userid"] });
                childSetting.CustomerInformation.CustId = (int)criteria["custid"];

                foreach (var betSetting in childSetting.BetSettings)
                {
                    betSetting.IsStatusUpdated = true;
                    betSetting.IsBetSettingUpdated = true;
                }

                foreach (var positionTaking in childSetting.PositionTakings)
                {
                    positionTaking.A1 = positionTaking.A1 < positionTaking.A2 ? positionTaking.A1 : positionTaking.A2;
                    positionTaking.IsUpdateA1 = true;
                }

                result = kenoService.UpdateMemberCustomerSettingInCaseOfAgentEditMember(childSetting);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return result;
        }

        private IMultipleCommandResults CopyGoldDeluxe(Hashtable criteria, int existingCustId, string ipAddress)
        {
            IMultipleCommandResults result = null;            
            int custId = (int)criteria["custid"];
            int actorId = (int)criteria["userid"];

            var goldDeluxeService = new SettingService();

            result = goldDeluxeService.UpdateSettingFromExistingMember(new UpdateSettingFromExistingMemberParameters
            {
                CustId = custId,
                FromCustId = existingCustId,
                UpLineId = (int)criteria["userid"],
                ActorId = actorId,
                SubAccId = 0,
                Ip = ipAddress,
                Language = CultureInfo.CurrentCulture.Name
            });

            var model = new EditSettingViewModel
            {
                CustId = existingCustId,
                UpLineId = (int)criteria["uplineid"],
                Level = GoldDeluxe.CoreSetting.Enumerations.CustomerLevel.Member,
                UpLineLevel = GoldDeluxe.CoreSetting.Enumerations.CustomerLevel.Agent,
                ActorId = actorId,
                Language = "en-US",
                FormType = GoldDeluxe.CoreSetting.Enumerations.FormType.Edit
            };

            var param = Mapper.Map<EditSettingViewModel, GetSettingParameters>(model);
            var setting = goldDeluxeService.GetSetting(param);
            goldDeluxeService.UpdateGameSetting(new UpdateGameSettingParameters { ActorId = actorId, CustId = custId, GameSetting = setting.GameSetting, Ip = ipAddress, Language = "en-US" });

            if (result.IsSuccessful)
            {
                    var custInfoIBC = casinoServiceBase.GetCustInfoIBC(existingCustId, (int)criteria["uplineid"]);

                result.CommandResultsList.Add(goldDeluxeService.UpdateStatus(new UpdateDisableStatusParameters()
                {
                    CustId = custId,
                    Disable = custInfoIBC.GeneralInfo.GoldDeluxeDisabled,
                    ActorId = actorId,
                    SubAccId = 0,
                    Ip = ipAddress
                }));
            }

            return result;
        }

        private Core.Models.Result CopyDCS(Hashtable criteria, int existingCustId, string ipAddress)
        {
            Core.Models.Result result = null;
            try
            {
                IDCSSettingCopyInsertMemberService _dcsSettingService = new DCSSettingCopyInsertMemberService();
                result = _dcsSettingService.CorrectMemberPositionTaking(new DCSSettingCopyInsertMemberCriteria
                {
                    CustID = (int)criteria["custid"],
                    ExistingCustID = existingCustId,
                    UserID = (int)criteria["userid"],
                    IpAddress = ipAddress
                });
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    ExceptionHandler.Handle(ex);
                }
            }

            return result;
        }

        public dynamic GetProductInfo(int custId, int productId)
        {
            return _searchCopyRepository.GetProductInfo(custId, productId);
        }
    }
}
