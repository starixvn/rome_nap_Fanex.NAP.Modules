using System.Collections.Generic;
using ExcelUtil.Spreadsheet;
using Fanex.Tarus.Admin.Criteria.SMAManagement;
using Fanex.Tarus.Admin.Entities;
using Fanex.Tarus.Admin.Entities.SMAManagement;

namespace Fanex.Tarus.Admin.Business.SMAManagement
{
    public interface ISMAManagementService
    {
        IEnumerable<IdNameEntity> GetSites(int mode = 0);

        IEnumerable<IdNameEntity> GetCurrencies(bool excludeTestCurrencies);

        SMAManagerModel GetSMAGroupHeadlines(GetSMAGroupHeadlineCriteria criteria);

        CN88SMAManagerModel GetCN88SMAGroupHeadlines(GetCN88SMAGroupHeadlineCriteria criteria);

        ExcelFile ExportSMAGroupHeadlinesToExcel(string sheetName, GetSMAGroupHeadlineCriteria criteria);

        ExcelFile ExportCN88SMAGroupHeadlinesToExcel(string sheetName, GetCN88SMAGroupHeadlineCriteria criteria);

        Result UpdateHeadlineType(UpdateHeadlineTypeCriteria criteria);

        Result UpdateOwner(UpdateOwnerCriteria criteria);
    }
}
