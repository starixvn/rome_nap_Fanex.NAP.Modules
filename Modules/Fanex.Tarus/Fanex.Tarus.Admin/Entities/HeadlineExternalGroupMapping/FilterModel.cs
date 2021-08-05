namespace Fanex.Tarus.Admin.Entities.HeadlineExternalGroupMapping
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class FilterModel
    {
        public IEnumerable<SelectListItem> Sites { get; set; }

        public IEnumerable<SelectListItem> Currencies { get; set; }

        public IEnumerable<SelectListItem> ExternalGroups { get; set; }

        public IEnumerable<SelectListItem> PrimaryGroups { get; set; }

        public IEnumerable<SelectListItem> Headlines { get; set; }
    }
}
