namespace Fanex.Tarus.Admin.Controller
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.Tarus.Admin.ManageProduct3RD;
    using Fanex.Tarus.Admin.ManageProduct3RD.Criteria;
    using Fanex.Tarus.Admin.ManageProduct3RD.Models;
    using Fanex.Tarus.Web.Controllers;
    using Fanex.Tarus.Web.Controllers.Filters;

    public class ManageProduct3RDController : BaseController
    {
        private readonly ManageProduct3RDService _manageProduct3RD;

        public ManageProduct3RDController()
        {
            _manageProduct3RD = new ManageProduct3RDService();
        }

        [AuthenticateRead]
        [HttpPost]
        public JsonResult Select(SelectProduct3RDRequest request) => Json(_manageProduct3RD.Select(new SelectProduct3RDCriteria
        {
            ProductId = request.ProductId,
            ProductName = request.ProductName,
            ApplyDCS = request.ApplyDCS,
            Description = request.Description,
            OfferSingle = request.OfferSingle,
            ProductPrefix = request.ProductPrefix,
            OfferSite = request.OfferSite,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        }), JsonRequestBehavior.AllowGet);

        [AuthenticateHost]
        [HttpPost]
        public JsonResult Insert(HttpPostedFileBase uploadFile) => Json(_manageProduct3RD.Insert(uploadFile));

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(Product3RDEntity request) => Json(_manageProduct3RD.Update(request));

        [AuthenticateHost]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(string productId) => Json(_manageProduct3RD.Delete(productId));
    }
}
