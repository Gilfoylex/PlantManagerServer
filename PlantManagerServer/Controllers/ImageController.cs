using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.Exceptions;
using PlantManagerServer.Helpers;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace PlantManagerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly MinioClient _minioClient;
    private readonly string _bucketName;
    private readonly ILogger<ImageController> _logger;

    public ImageController(MinioClient client, IConfiguration configuration, ILogger<ImageController> logger)
    {
        _logger = logger;
        _minioClient = client;
        _bucketName = configuration.GetValue<string>("Minio:BucketName");
    }

    [HttpGet("{imageName}")]
    public async Task<IActionResult> GetImage(string imageName, [FromQuery] int width, [FromQuery] int height)
    {
        // 获取客户端IP地址
        var clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        // 获取User-Agent
        //var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        // 获取请求URL
        var requestUrl = HttpContext.Request.GetDisplayUrl();
        _logger.LogInformation("client address={ClientIpAddress} request, {RequestUrl}", clientIpAddress, requestUrl);
        
        if (string.IsNullOrEmpty(imageName))
        {
            _logger.LogWarning("client address={ClientIpAddress} request imageName Invalid, {RequestUrl}", clientIpAddress, requestUrl);
            return BadRequest("imageName Invalid");
        }

        try
        {
            using var ms = new MemoryStream();
            var args = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(imageName)
                .WithCallbackStream(async stream =>
                {
                    await stream.CopyToAsync(ms);
                });

            var stat = await _minioClient.GetObjectAsync(args);
            ms.Position = 0;
            if (stat != null)
            {
                using var image = await Image.LoadAsync(ms);
                var newSize = Converts.GetScaleSize(image.Width, image.Height, width, height);
                image.Mutate(x => x
                    .Resize(new ResizeOptions
                    {
                        Size = newSize,
                        Mode = ResizeMode.Max
                    })
                );
                using var compressedImageStream = new MemoryStream();
                // Set the compression quality
                var encoder = new JpegEncoder
                {
                    Quality = 80 // Set the compression quality between 1 and 100
                };
                await image.SaveAsync(compressedImageStream, encoder);
                compressedImageStream.Position = 0;
                // 将压缩后的图像返回给网页前端
                return File(compressedImageStream.ToArray(), "image/jpeg");
            }
            
            _logger.LogWarning("client address={ClientIpAddress} request fail, not found, {RequestUrl}", clientIpAddress, requestUrl);
            return BadRequest("imageName not found");
        }
        catch (MinioException e)
        {
            _logger.LogError(e,"client address={ClientIpAddress} request fail, minio error, {RequestUrl}", clientIpAddress, requestUrl);
            // 处理与MinIO服务器的通信中可能出现的异常
            return StatusCode(500, e.Message);
        }
        catch (InvalidImageContentException)
        {
            _logger.LogError("client address={ClientIpAddress} request fail, image error, {RequestUrl}", clientIpAddress, requestUrl);
            // 如果无法解析图像，返回错误消息
            return BadRequest("Invalid image data.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "client address={ClientIpAddress} request fail, ex error, {RequestUrl}", clientIpAddress, requestUrl);
            return BadRequest("Get image error");
        }
    }
}