namespace Fanex.Tarus.Admin.Controller
{
    using Business.MoveBetSchedule;
    using Criteria.MoveBetSchedule;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using System.Web.Mvc;

    public class MoveBetScheduleController : BaseController
    {
        private readonly IMoveBetScheduleService moveBetScheduleService;

        public MoveBetScheduleController() : this(null)
        {
        }

        public MoveBetScheduleController(IMoveBetScheduleService moveBetScheduleService)
        {
            this.moveBetScheduleService = moveBetScheduleService ?? new MoveBetScheduleService();
        }

        [AuthenticateRead]
        public ActionResult Index()
            => View("~/Views/Admin/MoveBetSchedule/Index.cshtml",
                moveBetScheduleService.GetMoveBetSchedule(new GetDefaultMoveBetScheduleCriteria()));

        [AuthenticateRead]
        public JsonResult GetMoveBetSchedule(GetMoveBetScheduleCriteria criteria)
            => Json(moveBetScheduleService.GetMoveBetSchedule(criteria), JsonRequestBehavior.DenyGet);

        [AuthenticateHost]
        public JsonResult UpdateMoveBetSchedule(UpdateMoveBetScheduleCriteria criteria)
            => Json(moveBetScheduleService.UpdateMoveBetSchedule(criteria), JsonRequestBehavior.DenyGet);
    }
}