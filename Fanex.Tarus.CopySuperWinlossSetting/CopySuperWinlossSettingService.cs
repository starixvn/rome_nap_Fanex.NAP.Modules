namespace Fanex.Tarus.CopySuperWinlossSetting
{
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.Tarus.CopySuperWinlossSetting.Constant;
    using Fanex.Tarus.CopySuperWinlossSetting.Criteria;
    using Fanex.Tarus.CopySuperWinlossSetting.Model;
    using Fanex.Tarus.CopySuperWinlossSetting.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CopySuperWinlossSettingService
    {
        private readonly ICopySuperWinlossSettingRepository _repo;

        private List<WinlossSettingItem> _sourceSettingsSettingType6;
        private List<WinlossSettingItem> _sourceSettingsSettingType12;
        private List<WinlossSettingItem> _sourceSettingsSettingType22;

        private const string WinLossLimitXmlFormat = "<Limit SportType=\"{0}\" BetType=\"{1}\" MaxWinLimit=\"{3}\" MaxLossLimit=\"{4}\" IsAutoReset=\"{5}\" />";

        public CopySuperWinlossSettingService() : this(null)
        { }

        internal CopySuperWinlossSettingService(ICopySuperWinlossSettingRepository repo)
        {
            _repo = repo ?? new CopySuperWinlossSettingRepository();
            _sourceSettingsSettingType6 = new List<WinlossSettingItem>();
            _sourceSettingsSettingType12 = new List<WinlossSettingItem>();
            _sourceSettingsSettingType22 = new List<WinlossSettingItem>();
        }

        public IEnumerable<Result> Copy(CopySuperWinlossSettingRequest request)
        {
            if (request.SourceCustId <= 0)
            {
                throw new ArgumentOutOfRangeException("request", "Invalid SourceCustId.");
            }

            if (Split(request.TargetCustIds).Length <= 0)
            {
                throw new ArgumentException("Invalid TargetCustIds.", "request");
            }

            if (request.Products.Count <= 0)
            {
                throw new ArgumentException("Invalid Products.", "request");
            }

            var results = new List<Result>();

            foreach (var product in request.Products)
            {
                _sourceSettingsSettingType6.AddRange(_repo.GetCustomerWinLimit(new GetWinlossSettingCriteria
                {
                    CustId = request.SourceCustId,
                    ProductId = product.ProductId,
                    SettingType = DCSSettingType.WinlossLimit
                }));

                _sourceSettingsSettingType12.AddRange(_repo.GetCustomerWinLimit(new GetWinlossSettingCriteria
                {
                    CustId = request.SourceCustId,
                    ProductId = product.ProductId,
                    SettingType = DCSSettingType.LossLimit
                }));

                _sourceSettingsSettingType22.AddRange(_repo.GetCustomerWinLimit(new GetWinlossSettingCriteria
                {
                    CustId = request.SourceCustId,
                    ProductId = product.ProductId,
                    SettingType = DCSSettingType.WinlossLimit22
                }));
            }

            _sourceSettingsSettingType6 = _sourceSettingsSettingType6.Where(settingItem => request.Contains(settingItem.SportTypeId)).ToList();
            _sourceSettingsSettingType12 = _sourceSettingsSettingType12.Where(settingItem => request.Contains(settingItem.SportTypeId)).ToList();
            _sourceSettingsSettingType22 = _sourceSettingsSettingType22.Where(settingItem => request.Contains(settingItem.SportTypeId)).ToList();

            foreach (var targetCustId in Split(request.TargetCustIds))
            {
                Result resultUpdateSettingType6 = new Result(), resultUpdateSettingType12 = new Result(), resultUpdateSettingType22 = new Result();
                if (_sourceSettingsSettingType6.Any())
                {
                    resultUpdateSettingType6 = _repo.UpdateCustomerWinLossLimit(new UpdateWinlossSettingCriteria
                    {
                        CustId = targetCustId,
                        IP = request.IP,
                        UserId = request.UserId,
                        SourceUsername = request.SourceUsername,
                        Limit = ComposeUpdateXml(_sourceSettingsSettingType6)
                    });
                }

                if (_sourceSettingsSettingType12.Any())
                {
                    resultUpdateSettingType12 = _repo.UpdateCustomerWinLossLimit(new UpdateWinlossSettingCriteria
                    {
                        CustId = targetCustId,
                        IP = request.IP,
                        UserId = request.UserId,
                        SourceUsername = request.SourceUsername,
                        SettingType = DCSSettingType.LossLimit,
                        Limit = ComposeUpdateXml(_sourceSettingsSettingType12)
                    });
                }

                if (_sourceSettingsSettingType22.Any())
                {
                    resultUpdateSettingType22 = _repo.UpdateCustomerWinLossLimit(new UpdateWinlossSettingCriteria
                    {
                        CustId = targetCustId,
                        IP = request.IP,
                        UserId = request.UserId,
                        SourceUsername = request.SourceUsername,
                        SettingType = DCSSettingType.WinlossLimit22,
                        Limit = ComposeUpdateXml(_sourceSettingsSettingType22)
                    });
                }

                // Settingtype6: WinLoss limit calculate for each sporttype (level sporttypebettype)
                // Settingtype12: Loss limit only
                // Settingtype22: WinLoss limit sum of all sporttype in product (level product use dummy sporttype but sum all WL into this dummy sporttype)
                // Product41 - Live Casino uses 6, 12
                // Each product can use both 6 & 12 but can not mixed with 22 due to bussiness view
                // This function should  be refactored later.
                Result setting6And12Result = (_sourceSettingsSettingType12.Any() && resultUpdateSettingType12.IsSuccess ? resultUpdateSettingType6 : resultUpdateSettingType12);
                results.Add(_sourceSettingsSettingType22.Any() && resultUpdateSettingType22.IsSuccess ?
                    setting6And12Result :
                    resultUpdateSettingType22);
            }

            return results;
        }

        internal int[] Split(string strCustIds)
        {
            return strCustIds.Split(',').Select(m => Utils.ToInt32(m.Trim())).Where(m => Utils.ToInt32(m) > 0).ToArray();
        }

        internal string ComposeUpdateXml(IEnumerable<WinlossSettingItem> settingItems)
            => "<ROOT>" + string.Join(string.Empty, settingItems.Select(settingItem => settingItem.ToXml())) + "</ROOT>";
    }
}
