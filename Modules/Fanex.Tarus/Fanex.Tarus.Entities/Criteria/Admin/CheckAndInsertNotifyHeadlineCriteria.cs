using Fanex.NAP.Framework.Data;
using System;

namespace Fanex.Tarus.Entities.Criteria
{
    public class CheckAndInsertNotifyHeadlineCriteria : CriteriaBase
    {
        public string XmlEvent { get; set; }

        public string XmlTicket { get; set; }

        public bool IsNotify { get; set; }

        public DateTime SubmitDate { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_Amendment_CheckAndNotifyNotification";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(XmlEvent) && !string.IsNullOrWhiteSpace(XmlTicket);
        }
    }
}
