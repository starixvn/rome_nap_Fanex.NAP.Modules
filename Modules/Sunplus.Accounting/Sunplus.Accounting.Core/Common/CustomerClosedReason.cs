using System;
using System.Drawing;

namespace Sunplus.Accounting.Core.Common
{
    [Serializable]
    public sealed class CustomerClosedReason
    {
        private const byte ByAdmin = 1; // purple: #b3a2c7
        private const byte ByUpline = 2; // orange: #f79646
        private const byte ByArchive = 4; // dark grey hi-light: #969696

        public CustomerClosedReason(object closedReason)
        {
            if (closedReason != null && closedReason != DBNull.Value)
            {
                AssignParameters(Convert.ToByte(closedReason));
            }
        }

        public CustomerClosedReason(byte closedReason)
        {
            AssignParameters(closedReason);
        }

        public string ClassName { get; private set; }

        public string ClosedReason { get; private set; }

        public Color ClosedColorForExcel { get; private set; }

        private void AssignParameters(byte closedReason)
        {
            ClassName = "closed-";

            switch (closedReason)
            {

                case ByAdmin:
                    ClassName += "byAdmin";
                    ClosedReason = "This account was closed by admin.";
                    ClosedColorForExcel = Color.FromArgb(179, 162, 199);
                    break;

                case ByUpline:
                    ClassName += "byUpline";
                    ClosedReason = "This account was closed by upline.";
                    ClosedColorForExcel = Color.FromArgb(247, 150, 70);
                    break;

                case ByArchive:
                    ClassName += "byArchive";
                    ClosedReason = "This account was closed by inactivity.";
                    ClosedColorForExcel = Color.FromArgb(150, 150, 150);
                    break;

                default:
                    ClassName = string.Empty;
                    ClosedReason = string.Empty;
                    ClosedColorForExcel = Color.FromArgb(204, 199, 186);
                    break;
            }
        }
    }
}
