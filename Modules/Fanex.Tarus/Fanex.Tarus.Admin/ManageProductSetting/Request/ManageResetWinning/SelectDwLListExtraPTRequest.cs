namespace Fanex.Tarus.Admin.ManageProductSetting.Request.ManageResetWinning
{
    public class SelectDwLListExtraPTRequest
    {
        public int? CustID { get; set; }

        public string Username { get; set; }

        public int? GroupID { get; set; }

        public string GroupName { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsTest { get; set; }
    }
}
