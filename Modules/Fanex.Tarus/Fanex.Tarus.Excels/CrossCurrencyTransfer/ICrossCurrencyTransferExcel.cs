namespace Fanex.Tarus.Excels.CrossCurrencyTransfer
{
    using System.Data;

    public interface ICrossCurrencyTransferExcel
    {
        ExcelResult ValidateTemplateFile();

        DataTable ExcelToDataTable();
    }
}
