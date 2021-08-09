using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Tarus.Admin.Enums;
using Fanex.Tarus.Admin.Resource;

namespace Fanex.Tarus.Admin.DormantAccount.ViewModel
{
    public class DormantAccountFilter
    {
        public DormantAccountFilter()
        {
            HeadlineType = HeadlineTypeConstant.List.Select(i => new SelectListItem { Text = i.Value, Value = i.Key.ToString() }).ToList();
            HeadlineType.Insert(0, new SelectListItem { Text = Language.All, Value = "", Selected = true });

            Level = UserLevelConstant.List.Select(i => new SelectListItem { Text = i.Value, Value = i.Key.ToString() }).ToList();

            ArchiveStage = ArchiveStageConstant.List.Select(i => new SelectListItem { Text = i.Value, Value = i.Key.ToString() }).ToList();
            ArchiveStage.Insert(0, new SelectListItem { Text = Language.All, Value = "0", Selected = true });

            ArchiveDate = ArchiveDateConstant.List.Select(i => new SelectListItem { Text = i.Value, Value = i.Key.ToString() }).ToList();
            ArchiveDate.Insert(0, new SelectListItem { Text = Language.All, Value = "0", Selected = true });
            
            FromDate = DateTime.Today.AddDays(-90);
            ToDate = DateTime.Today;
            MinDate = new DateTime(2020, 1, 1);
            MaxDate = DateTime.Today;
        }

        public IList<SelectListItem> HeadlineType { get; set; }

        public IList<SelectListItem> Level { get; set; }

        public IList<SelectListItem> ArchiveStage { get; set; }

        public IList<SelectListItem> ArchiveDate { get; set; }

        public string HeadlineGroup { get; set; }

        public bool NotAssignedToGroupOnly { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime MinDate { get; set; }

        public DateTime MaxDate { get; set; }

        public string Username { get; set; }
    }
}
