namespace Fanex.Tarus.Admin.ManageUsernamePrefix
{
    using Fanex.Tarus.Admin.ManageUsernamePrefix.Models;
    using Fanex.Tarus.Admin.ManageUsernamePrefix.Requests;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;
    using Sunplus.Accounting.Business.Security;
    using System.Web.Mvc;

    public class ManageUsernamePrefixController : BaseController
    {
        private readonly IService service;

        public ManageUsernamePrefixController() : this(null)
        { }

        public ManageUsernamePrefixController(IService service)
        {
            this.service = service ?? new Service();
        }

        [AuthenticateRead]
        [HttpGet]
        public ActionResult Index()
        {
            var isHost = Permissions.GetHostPermission("Admin_ManageUsernamePrefix");
            var viewModel = new IndexViewModel
            {
                IsHost = isHost
            };
            return View("~/Views/Admin/ManageUsernamePrefix/Index.cshtml", viewModel);
        }

        [AuthenticateRead]
        [HttpPost]
        public ActionResult Query(QueryRequest request) => Json(service.Query(request));

        [AuthenticateHost]
        [HttpPost]
        public ActionResult Insert(InsertRequest request) => Json(service.Insert(request));

        [AuthenticateHost]
        [HttpPost]
        public ActionResult Delete(DeleteRequest request) => Json(service.Delete(request));
    }
}
