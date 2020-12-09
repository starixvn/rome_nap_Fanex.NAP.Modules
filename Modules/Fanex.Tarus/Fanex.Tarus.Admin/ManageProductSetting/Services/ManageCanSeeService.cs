namespace Fanex.Tarus.Admin.ManageProductSetting.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using Fanex.RemotingRef;
    using Fanex.Tarus.Admin.Entities;
    using Fanex.Tarus.Admin.Entities.SMAManagement;
    using Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSporttype;
    using Fanex.Tarus.Admin.ManageProductSetting.DataAccess;
    using Fanex.Tarus.Admin.ManageProductSetting.Entities;
    using Fanex.Tarus.Admin.ManageProductSetting.Models;
    using NPOI.SS.UserModel;
    using Fanex.Tarus.Infrastructure.Extensions;

    public class ManageCanSeeService
    {
        private readonly IManageCanSeeDataAccess _manageCanSeeDataAccess;
        private readonly IRemotingRefService _remotingRefService;

        public ManageCanSeeService() : this(null, null)
        {
        }

        public ManageCanSeeService(IManageCanSeeDataAccess manageSporttypeDataAccess, IRemotingRefService remotingRefService)
        {
            _manageCanSeeDataAccess = manageSporttypeDataAccess ?? new ManageCanSeeDataAccess();
            _remotingRefService = remotingRefService ?? new RemotingRefService();
        }

        public IEnumerable<CanSeeModel> Select(SelectCanSeeCriteria criteria)
        {
            var canSeeModel = _manageCanSeeDataAccess.Select(criteria).ToList();

            foreach (var model in canSeeModel)
            {
                model.SportTypeName = _remotingRefService.GetSportName(model.SportType.ToString(), Thread.CurrentThread.CurrentCulture.Name);
            }

            return canSeeModel;
        }

        public Result Insert(HttpPostedFileBase uploadFile)
        {
            var dataRow = new List<CanSeeEntity>();
            int rowCount = 1;
            try
            {
                IWorkbook workbook = WorkbookFactory.Create(uploadFile.InputStream);
                ISheet sheet = workbook.GetSheetAt(0);
                IEnumerator rows = sheet.GetRowEnumerator();
                rows.MoveNext();
                while (rows.MoveNext())
                {
                    rowCount++;
                    var row = (IRow)rows.Current;
                    dataRow.Add(new CanSeeEntity
                    {
                        SportType = HttpUtility.HtmlEncode(row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString()),
                        BetType = HttpUtility.HtmlEncode(row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString()),
                        Currency = HttpUtility.HtmlEncode(row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString()),
                        SiteId = HttpUtility.HtmlEncode(row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString()),
                        IsLaunched = HttpUtility.HtmlEncode(row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString()),
                        IsTest = HttpUtility.HtmlEncode(row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString()),
                        LaunchedLevel = HttpUtility.HtmlEncode(row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString())
                    });
                }
                var criteria = new InsertCanSeeCriteria
                {
                    Entities = dataRow
                };

                return _manageCanSeeDataAccess.Insert(criteria);
            }
            catch (Exception ex)
            {
                var result = new Result(1, "Invalid file uploaded. Error at line: " + rowCount.ToString());
                LogExtensions.ExceptionHandler(ex);

                return result;
            }
        }

        public Result Update(UpdateCanSeeCriteria request)
            => _manageCanSeeDataAccess.Update(request);

        public Result Delete(DeleteCanSeeCriteria request)
            => _manageCanSeeDataAccess.Delete(request);

        public IEnumerable<IdNameEntity> GetAllSportType()
            => _manageCanSeeDataAccess.GetAllSportType().ToList();

        public IEnumerable<IdNameEntity> GetCurrencies()
        {
            var currencies = _manageCanSeeDataAccess.GetCurrencies().ToList();
            currencies.Insert(0, new IdNameEntity { Id = 0, Name = "All Currencies" });

            return currencies;
        }
    }
}