namespace Fanex.CustomerSetting.Module.SearchCopy.ViewModels
{
    using Fanex.NAP.Framework.Data;

    public class CommandResult : CommandResultBase
    {
        public override bool IsSuccessful
        {
            get
            {
                string errCode = this.Code.ToString();

                return string.IsNullOrWhiteSpace(errCode) || errCode == "0";
            }
        }
    }
}