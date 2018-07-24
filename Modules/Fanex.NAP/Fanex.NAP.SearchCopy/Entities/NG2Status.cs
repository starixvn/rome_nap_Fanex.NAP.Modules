namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    internal class NG2Status
    {
        public NG2Status()
            : this(false, true)
        {
        }

        public NG2Status(bool isSynced, bool isDisabled)
        {
            IsSynced = isSynced;
            IsDisabled = isDisabled;
        }

        public bool IsSynced { get; }

        public bool IsDisabled { get; }
    }
}