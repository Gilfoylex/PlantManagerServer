
using CommandLine.Text;
using CommandLine;

namespace ExcelToSql.Models;
public class CommandLineOptions
{
    [Option('i', "input", Required = true, HelpText = "Input excel file path.")]
    public string? InputFile { get; set; }

    [Option('l', "host", HelpText = "database host")]
    public string Host { get; set; } = "localhost";

    [Option('p', "port", HelpText = "database port")]
    public int Port { get; set; } = 5432;
    
    [Option('d', "dbname", HelpText = "database name")]
    public string DataBaseName { get; set; } = "plant_database";
    
    [Option('u', "dbuser", HelpText = "database user name")]
    public string UserName { get; set; } = "postgres";
    
    [Option('p', "dbuser", HelpText = "database password")]
    public string Password { get; set; } = "1";
}
