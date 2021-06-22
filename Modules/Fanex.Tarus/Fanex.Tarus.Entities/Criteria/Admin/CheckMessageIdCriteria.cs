namespace Fanex.Tarus.Entities.Criteria.Admin
{
    using Fanex.NAP.Framework.Data;

    public class CheckMessageIdCriteria : CriteriaBase
    {
        public int MessageId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_Msg_Check";
        }

        public override bool IsValid()
        {
            return MessageId > 0;
        }
    }
}
