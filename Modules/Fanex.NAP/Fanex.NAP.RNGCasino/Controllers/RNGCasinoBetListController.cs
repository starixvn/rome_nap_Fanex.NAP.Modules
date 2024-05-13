namespace Fanex.Module.RNGCasino.Controllers
{
    using System;
    using System.Web.Mvc;
    using Criteria;
    using CustomerSetting.Module.Core.Web.Controllers;
    using Models;
    using Resources;
    using Services;
    using ViewModels;

    public class RNGCasinoBetListController : BaseController
    {
        private readonly IRNGCasinoService _rngCasinoService;

        public RNGCasinoBetListController()
        {
            _rngCasinoService = new RNGCasinoService();
        }

        [HttpGet]
        [Obsolete]
        public ActionResult BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = GetBetDetail(criteria);
            return View("~/Areas/RNGCasino/Views/BetDetails/Index.cshtml", result);
        }

        [HttpGet]
        public ActionResult BetDetailsPaging(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = GetBetDetail(criteria);
            return PartialView("~/Areas/RNGCasino/Views/BetDetails/_Tickets.cshtml", result);
        }

        public ActionResult JackpotDetails(GetJackpotDetailsCriteria criteria)
        {
            var result = _rngCasinoService.JackpotDetails(criteria);

            return View("~/Areas/RNGCasino/Views/BetDetails/Jackpot.cshtml", new JackpotDetailsViewModel { TicketDetails = result, Title = criteria.Title });
        }

        [NonAction]
        private BetDetail GetBetDetail(GetRNGCasinoBetDetailsCriteria criteria)
        {
            criteria.PageIndex++;
            if (criteria.PageSize == 0)
            {
                criteria.PageSize = 100;
            }

            var ticketDetails = _rngCasinoService.BetDetails(criteria);

            return new BetDetail
            {
                PageIndex = criteria.PageIndex,
                PageSize = criteria.PageSize,
                TotalRows = criteria.TotalRows,
                TicketDetails = ticketDetails
            };
        }
    }
}