using CommandLine;
using ExcelToSql.Models;
using OfficeOpenXml;
using XBase.Logging;

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
        for (var row = 1; row < worksheet.Dimension.Rows; row++)
        {
            for (var col = 1; col < worksheet.Dimension.Columns; col++)
            {
                Console.Write($"{worksheet.Cells[row, col].Value} ");
            }
            Console.WriteLine();
        }
    }

    private static void HandleParseError(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log(error.ToString());
        }
    }
}
