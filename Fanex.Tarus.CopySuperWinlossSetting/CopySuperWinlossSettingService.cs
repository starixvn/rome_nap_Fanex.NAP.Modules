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

        private const string WinLossLimitXmlFormat = "<Limit SportType=\"{0}\" BetType=\"{1}\" MaxWinLimit=\"{3}\" MaxLossLimit=\"{4}\" IsAutoReset=\"{5}\" />";

        public CopySuperWinlossSettingService() : this(null)
        { }

        internal CopySuperWinlossSettingService(ICopySuperWinlossSettingRepository repo)
        {
            _repo = repo ?? new CopySuperWinlossSettingRepository();
            _sourceSettingsSettingType6 = new List<WinlossSettingItem>();
            _sourceSettingsSettingType12 = new List<WinlossSettingItem>();
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
            }

            _sourceSettingsSettingType6 = _sourceSettingsSettingType6.Where(settingItem => request.Contains(settingItem.SportTypeId)).ToList();
            _sourceSettingsSettingType12 = _sourceSettingsSettingType12.Where(settingItem => request.Contains(settingItem.SportTypeId)).ToList();


            foreach (var targetCustId in Split(request.TargetCustIds))
            {
                Result resultUpdateSettingType6 = new Result(), resultUpdateSettingType12 = new Result();
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

                results.Add(_sourceSettingsSettingType12.Any() && resultUpdateSettingType12.IsSuccess ? resultUpdateSettingType6 : resultUpdateSettingType12);
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
