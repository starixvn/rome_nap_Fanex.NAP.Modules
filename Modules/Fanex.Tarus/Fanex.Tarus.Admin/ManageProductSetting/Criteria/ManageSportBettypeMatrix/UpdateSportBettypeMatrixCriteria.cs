namespace Fanex.Tarus.Admin.ManageProductSetting.Criteria.ManageSportBettypeMatrix
{
    public class UpdateSportBettypeMatrixCriteria : InsertSportBettypeMatrixCriteria
    {
        public bool IsMultiple { get; set; }
        public override string GetSettingKey() => "Acc_SportBettypeMatrix_Upd";
    }
}