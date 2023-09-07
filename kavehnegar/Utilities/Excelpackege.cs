using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using OfficeOpenXml;

namespace kavehnegar.CacheStack
{
    public static class ExcelPackageExtensions
    {
        public static DataTable ToDataTable(this ExcelPackage package)
        {
            ExcelWorksheet _worksheet = package.Workbook.Worksheets.First();
            DataTable dt= new DataTable();
            foreach (var item in _worksheet.Cells[1,1,1,_worksheet.Dimension.End.Column])
            {
                dt.Columns.Add(item.Text);
            }

            for (int i = 2; i <= _worksheet.Dimension.End.Row; i++)
            {
                var row = _worksheet.Cells[i, 1, i, _worksheet.Dimension.End.Column];
                var newRow=dt.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(newRow);
            }
            return dt;
        }
        public static List<string> ToStringList(this ExcelPackage package)
        {
            ExcelWorksheet _worksheet = package.Workbook.Worksheets.First();
            List<string> list = new List<string>();
            foreach (var item in _worksheet.Cells[1, 1, 1, _worksheet.Dimension.End.Column])
            {
                list.Add(item.Text);
            }

            for (int i = 2; i <= _worksheet.Dimension.End.Row; i++)
            {
                var row = _worksheet.Cells[i, 1, i, _worksheet.Dimension.End.Column];
                foreach (var cell in row)
                {
                    list.Add(cell.Text);
                }
            }
            return list;
        }

    }
}