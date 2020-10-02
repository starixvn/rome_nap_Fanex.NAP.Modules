using System.Collections.Generic;

namespace Fanex.Tarus.Admin.ManageProductSetting.Entities.UMConfig
{
    public class ProductSettingByFunctionEntity
    {
        public string FunctionType { get; set; }

        public IEnumerable<ProductSettingEntity> Products { get; set; }
    }

    public class ProductSettingEntity
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public IEnumerable<SportSettingEntity> Sports { get; set; }
    }

    public class SportSettingEntity
    {
        public string SportType { get; set; }

        public string SportName { get; set; }

        public IEnumerable<UMConfigSettingEntity> Settings { get; set; }
    }

    public class UMConfigSettingEntity
    {
        public string FunctionType { get; set; }

        public string ProductId { get; set; }

        public string SportType { get; set; }

        public string SettingType { get; set; }

        public string BetType { get; set; }

        public string IsLive { get; set; }

        public string ProductName { get; set; }

        public string SportName { get; set; }

        public string SettingName { get; set; }
    }
}
