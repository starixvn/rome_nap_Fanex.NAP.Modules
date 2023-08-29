using System.Linq;

namespace Fanex.Tarus.Admin.ManageProductSetting.Entities
{
    public class BettypeEntity
    {
        public string TypeId { get; set; }
        public string TypeNamee { get; set; }
        public string TypeNamec { get; set; }
        public string TypeDesc { get; set; }
        public string TypeNamec_uni { get; set; }
        public string TypeNamej { get; set; }
        public string TypeNamcn { get; set; }
        public string TypeNamecn { get; set; }
        public string TypeNameth { get; set; }
        public string TypeNameit { get; set; }
        public string TypeNameko { get; set; }
        public string TypeNameES { get; set; }
        public string TypeNameVN { get; set; }
        public string TypeNameEL { get; set; }
        public string TypeNamee_Eu { get; set; }
        public string TypeNameRU { get; set; }
        public string TypeNameID { get; set; }
        public string TypeNamePT { get; set; }
        public string TypeNameFR { get; set; }
        public string ParentTypeid { get; set; }
        public string TypeNameMMR { get; set; }
        public string TypeNameHI { get; set; }
        public string DisplayedTypeID { get; set; }
        public string LicSpreadBetTypeGroupId { get; set; }
        public string Is3rdParty { get; set; }
        public string TypeNamePTBR { get; set; }
        public string TypeNameDE { get; set; }
        public string TypeNameBENG { get; set; }
        public string TypeNameURD { get; set; }
        public string TypeNameMSA { get; set; }
        public string TypeNameLAO { get; set; }
        public string TypeNameKH { get; set; }
        public string TypeNameAR { get; set; }
        public string TypeNameTAM { get; set; }

        public bool IsEmptyData
            => !GetType().GetProperties()
            .Where(pi => pi.PropertyType == typeof(string))
            .Select(pi => (string)pi.GetValue(this))
            .Any(value => !string.IsNullOrWhiteSpace(value));
    }
}