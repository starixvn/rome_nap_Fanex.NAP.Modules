namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageResetWinning
{
    public class SelectDwLListRequest
    {
        public int? CustID { get; set; }

        public string Username { get; set; }

        public int? ProductID { get; set; }

        public string Product { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsTest { get; set; }
    }
}
