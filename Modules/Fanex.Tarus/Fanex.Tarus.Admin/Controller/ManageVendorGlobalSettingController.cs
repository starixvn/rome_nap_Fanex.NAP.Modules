namespace Fanex.Tarus.Admin.Controller
{
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Criteria;
    using Fanex.Tarus.Admin.ManageVendorGlobalSetting.Models;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;

    public class ManageVendorGlobalSettingController : BaseController
    {
        private readonly ManageVendorGlobalSettingService _manageVendorService;

        public ManageVendorGlobalSettingController()
        {
            _manageVendorService = new ManageVendorGlobalSettingService();
        }

        [AuthenticateRead]
        [HttpPost]
        public JsonResult Select(SelectVendorRequest request) => Json(_manageVendorService.Select(new SelectVendorGlobalCriteria
        {
            VendorId = request.VendorId,
            VName = request.VendorName,
            NumberValue = request.NumberValue,
            BetType = request.BetType,
            SportType = request.SportType,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Insert(HttpPostedFileBase uploadFile) => Json(_manageVendorService.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(VendorGlobalEntity request) => Json(_manageVendorService.Update(request));

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(VendorGlobalEntity request) => Json(_manageVendorService.Delete(request));
    }
}
