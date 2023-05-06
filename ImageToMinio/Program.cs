using System.ComponentModel;
using CommandLine;
using ImageToMinio.Models;
using Minio;
using XBase.Logging;

namespace ImageToMinio;

class Program
{
    private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);
    static void Main(string[] args)
    {
        Logger.Sink = new ConsoleLogSink();
        CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(RunWithOptions)
            .WithNotParsed(HandleParseError);

        _exitEvent.WaitOne();
    }

    private static async void RunWithOptions(CommandLineOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.RootDir))
        {
            Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log("root directory param fail!");
            return;
        }
        
        if (!Directory.Exists(options.RootDir))
        {
            Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log("root directory not exists!");
            return;
        }

        try
        {
            var minioClient = new MinioClient()
                .WithEndpoint($"{options.Host}:{options.Port}")
                .WithCredentials(options.AccessKey, options.SecretKey)
                .WithSSL(false)
                .Build();

            var bucketName = options.Bucket;
            // Check if the bucket exists
            var args = new BucketExistsArgs().WithBucket(bucketName);
            var found = await minioClient.BucketExistsAsync(args).ConfigureAwait(false);
            if (!found)
            {
                // If not, create a new bucket
                //await minioClient.MakeBucketAsync(bucketName);
                Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log($"bucket={options.Bucket} not exists!");
                return;
            }

            var rootDir = options.RootDir!;
            foreach (var imageDir in Directory.GetDirectories(rootDir))
            {
                var imageKey = XBase.Utils.PathHelper.GetDirectoryLastFolderName(imageDir);
                var startIndex = 1;
                Logger.TryGet(LogLevel.Information, LogArea.ImageToMinio)?.Log(imageDir);
                var tasks = new List<Task>();
                foreach (var imagePath in Directory.GetFiles(imageDir))
                {
                    var fileExtension = Path.GetExtension(imagePath);
                    if (fileExtension != options.FileExtension)
                    {
                        Logger.TryGet(LogLevel.Warning, LogArea.ImageToMinio)?.Log($"{imagePath} extension not {options.FileExtension}, not upload");
                        continue;
                    }
                    var objectName = $"{imageKey}-{startIndex++}{fileExtension}";
                    tasks.Add(UploadFile(minioClient, bucketName, objectName, imagePath, options.ContentType));
                }

                await Task.WhenAll(tasks);
            }
        }
        catch (Exception e)
        {
            Logger.TryGet(LogLevel.Error, LogArea.ImageToMinio)?.Log(e.Message);
        }
        finally
        {
            _exitEvent.Set();
        }
    }

    private static async Task UploadFile(MinioClient client,string bucketName, string objectName, string filePath, string contentType)
    {
        try
        {
            await using var fileStream = File.OpenRead(filePath);
            // Upload the file to the specified bucket and object
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);
            await client.PutObjectAsync(args).ConfigureAwait(false);
            Logger.TryGet(LogLevel.Information, LogArea.ImageToMinio)?.Log($"{filePath} uploaded successfully.");
        }
        catch (Exception ex)
        {
            Logger.TryGet(LogLevel.Error, LogArea.ImageToMinio)?.Log("Error: " + ex.Message);
        }
    }

    private static void HandleParseError(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Logger.TryGet(LogLevel.Error, LogArea.ExcelToSql)?.Log(error.ToString()?? "");
        }

        _exitEvent.Set();
    }
}

