namespace Fanex.Tarus.Admin.ManageProductSetting.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SbsmFunctionTableModel
    {
        private const string TablePTType = "TablePT";
        private const string TableCommType = "TableComm";
        private const string TableBetSettingType = "TableBetSetting";
        private const string TableBettransType = "TableBettrans";
        public SbsmFunctionTableModel(List<TableModel> tables)
        {
            var nullItem = new SelectListItem() { Text = "N/A", Value = "N/A" };
            TablePT = new List<SelectListItem>() { nullItem };
            TableComm = new List<SelectListItem>() { nullItem };
            TableBetSetting = new List<SelectListItem>() { nullItem };
            TableBettrans = new List<SelectListItem>();

            foreach (var tbl in tables)
            {
                var opt = new SelectListItem() { Text = tbl.TableName, Value = tbl.TableName };
                switch (tbl.TableType)
                {
                    case TablePTType:
                        TablePT.Add(opt);
                        break;
                    case TableCommType:
                        TableComm.Add(opt);
                        break;
                    case TableBetSettingType:
                        TableBetSetting.Add(opt);
                        break;
                    case TableBettransType:
                        TableBettrans.Add(opt);
                        break;
                    default:
                        break;
                }
            }
        }

        public List<SelectListItem> TablePT { get; set; }

        public List<SelectListItem> TableComm { get; set; }

        public List<SelectListItem> TableBetSetting { get; set; }

        public List<SelectListItem> TableBettrans { get; set; }
    }
}
