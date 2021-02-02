namespace Fanex.Tarus.Admin.Controller
{
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using System.Web.Mvc;

    public class MoveBetScheduleController : BaseController
    {
        [AuthenticateRead]
        public ActionResult Index()
            => View("~/Views/Admin/MoveBetSchedule/Index.cshtml");
    }
}