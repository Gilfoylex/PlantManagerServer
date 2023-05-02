using CommandLine;

namespace ImageToMinio.Models;

public class CommandLineOptions
{
    [Option('r', "root-dir", Required = true, HelpText = "Input image root directory")]
    public string? RootDir { get; set; }

    [Option('l', "host", HelpText = "minio server host, do not need http or https prefix")]
    public string Host { get; set; } = "192.168.101.52";

    [Option('p', "port", HelpText = "minio server api port")]
    public int Port { get; set; } = 19000;

    [Option('b', "bucket", HelpText = "minio bucket name")]
    public string Bucket { get; set; } = "plant-images";
    
    [Option('a', "access-key", HelpText = "minio access-key")]
    public string AccessKey { get; set; } = "plant-access-key-2334";
    
    [Option('s', "secret-key", HelpText = "minio secret-key")]
    public string SecretKey { get; set; } = "plant-secret-key-2334";
    
    [Option('e', "extension", HelpText = "fileExtension")]
    public string FileExtension { get; set; } = ".png";
}