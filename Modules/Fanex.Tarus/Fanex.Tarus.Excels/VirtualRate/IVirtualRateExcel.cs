namespace Fanex.Tarus.Excels.VirtualRate
{
    using System.Data;

    public interface IVirtualRateExcel
    {
        ExcelResult ValidateTemplateFile();

        DataTable ExcelToDataTable();
    }
}
