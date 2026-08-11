using System;
using System.Data;
using System.IO;
using System.Text;

namespace MdbFdbExporter
{
    public static class CsvWriter
    {
        public static void SaveToCsv(DataTable dt, string outputPath, bool useShiftJis = true)
        {
            // Register CodePages for Shift-JIS support in .NET Core/9
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = useShiftJis ? Encoding.GetEncoding("shift-jis") : new UTF8Encoding(true);

            using (var writer = new StreamWriter(outputPath, false, encoding))
            {
                // Write Header
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.Write(EscapeCsv(dt.Columns[i].ColumnName));
                    if (i < dt.Columns.Count - 1)
                        writer.Write(",");
                }
                writer.WriteLine();

                // Write Rows
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        object value = row[i];
                        string formattedValue = FormatValue(dt.Columns[i].ColumnName, value);
                        writer.Write(EscapeCsv(formattedValue));
                        if (i < dt.Columns.Count - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }

        private static string FormatValue(string columnName, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return "";
            }

            // 1. Format DateTime types
            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue.ToString("yyyy/MM/dd");
            }

            // 2. Format custom integer dates like 20251023 (CREDATE or GCREDATE or DATE)
            string colNameUpper = columnName.ToUpper();
            if ((colNameUpper.Contains("DATE") || colNameUpper.Contains("CREDATE")) && (value is int || value is long || value is short))
            {
                string? dateStr = value.ToString();
                if (dateStr != null && dateStr.Length == 8 && DateTime.TryParseExact(dateStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate.ToString("yyyy/MM/dd");
                }
            }

            // 3. Format Floating-Point types to 3 decimal places (coordinates, heights, sizes)
            if (value is double dblValue)
            {
                // Format floating points to 3 decimal places
                return dblValue.ToString("F3");
            }
            if (value is float fltValue)
            {
                return fltValue.ToString("F3");
            }
            if (value is decimal decValue)
            {
                return decValue.ToString("F3");
            }

            return value.ToString() ?? "";
        }

        private static string EscapeCsv(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");
            if (mustQuote)
            {
                var sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in field)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return field;
        }
    }
}
