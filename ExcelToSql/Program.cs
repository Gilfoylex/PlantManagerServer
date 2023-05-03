using CommandLine;
using ExcelToSql.Models;
using OfficeOpenXml;
using XBase.Logging;
using Shared.Models;
using System.Globalization;

namespace ExcelToSql;

class Program
{
    static void Main(string[] args)
    {
        Logger.Sink = new ConsoleLogSink();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 设置许可模式为非商业模式，在开源项目或非商业应用中使用

        CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(RunWithOptions)
            .WithNotParsed(HandleParseError);
    }

    private static void RunWithOptions(CommandLineOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.InputFile))
        {
            Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log("input file param fail!");
            return;
        }

        using var package = new ExcelPackage(new FileInfo(options.InputFile));
        var worksheet = package.Workbook.Worksheets[0];
        // 找到数据的边界（假设数据从第 2 行开始，第 1 行是字段名称）。
        int startRow = 2;
        int endRow = worksheet.Dimension.End.Row;

        using var dbContext = new Models.PlantDbContext("plant_database", "postgres", "1");

        for (var row = startRow; row < endRow; row++)
        {
            var plant = new PlantTable();
            var properties = plant.GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                object? value = null;
                var col = i + 1;
                var cell = worksheet.Cells[row, col];
                var curProperty = properties[i];

                if (curProperty.Name == nameof(PlantTable.InvestigationTime))
                {
                    try
                    {
                        //调查时间特殊处理
                        // 将字符串转换为 DateTime 对象
                        var date = DateTime.ParseExact(cell.Text, "M/dd/yy H:mm", CultureInfo.InvariantCulture);

                        // 将 DateTime 对象转换为 DateTimeOffset 对象，以获取 Unix 时间戳
                        DateTimeOffset dateTimeOffset = new(date);

                        // 将 DateTimeOffset 对象转换为 Unix 时间戳（以秒为单位）
                        var unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();
                        curProperty.SetValue(plant, unixTimestamp);
                    }
                    catch(Exception ex)
                    {
                        Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log($"第[{row}]行 第[{col}]列, 内容[{cell.Text}]解析错误, {ex.Message}");
                    }

                    continue;
                }

                if (curProperty.PropertyType == typeof(string))
                {
                    value = cell.Text;
                }
                else if (curProperty.PropertyType == typeof(int) || curProperty.PropertyType == typeof(int?))
                {
                    value = cell.Value == null ? (int?)null : Convert.ToInt32(cell.Value);
                }
                else if (curProperty.PropertyType == typeof(long) || curProperty.PropertyType == typeof(long?))
                {
                    value = cell.Value == null ? (long?)null : Convert.ToInt64(cell.Value);
                }
                else if (curProperty.PropertyType == typeof(float) || curProperty.PropertyType == typeof(float?))
                {
                    value = cell.Value == null ? (float?)null : Convert.ToSingle(cell.Value);
                }
                else if (curProperty.PropertyType == typeof(double) || curProperty.PropertyType == typeof(double?))
                {
                    value = cell.Value == null ? (double?)null : Convert.ToDouble(cell.Value);
                }
                else if (curProperty.PropertyType == typeof(bool) || curProperty.PropertyType == typeof(bool?))
                {
                    value = cell.Value == null ? (bool?)null : Convert.ToBoolean(cell.Value);
                }
                else if (curProperty.PropertyType == typeof(float[]))
                {
                    value = cell.Value == null ? null : cell.Value.ToString().Split(',').Select(float.Parse).ToArray();
                }

                curProperty.SetValue(plant, value);
            }

            dbContext.PlantEntities.Add(plant);
        }

        dbContext.SaveChanges();
    }

    private static void HandleParseError(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log(error.ToString());
        }
    }
}
