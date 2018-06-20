namespace Fanex.Tarus.Excels.AutomateTransfer
{
    using System.Data;

    public interface IAutomateTransferExcel
    {
        ExcelResult ValidateTemplateFile();

        DataTable ExcelToDataTable();
    }
}
