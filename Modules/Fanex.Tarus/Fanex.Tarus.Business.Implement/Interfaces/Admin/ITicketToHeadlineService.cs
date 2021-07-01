using Fanex.Tarus.Entities.Criteria;
using Fanex.Tarus.Entities.Criteria.Admin;
using Fanex.Tarus.Web.Models.Admin;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Tarus.Business.Interface.Admin
{
    public interface ITicketToHeadlineService
    {
        List<SelectListItem> GetSuportedProduct(GetTicketToHeadlineSupportedProductCriteria criteria);

        TicketToHeadlineViewModel GetData(HttpPostedFileBase uploadFile, TicketToHeadlineCriteria criteria);

        TicketToHeadlineCriteria GetCachedCriteria();

        TicketToHeadlineViewModel GetDataFromCachedCriteria();

        TicketToHeadlineViewModel GetDataFromCachedCriteria(int? pageSize, int? pageIndex);

        TicketToHeadlineViewModel NotifyHeadline();

        TicketToHeadlineViewModel GetFakeData(int? pageSize, int? pageIndex, int rowNo = 400);
    }
}
