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

    public ImageController(MinioClient client, IConfiguration configuration)
    {
        _minioClient = client;
        _bucketName = configuration.GetValue<string>("Minio:BucketName");
    }

    [HttpGet("{imageName}")]
    public async Task<IActionResult> GetImage(string imageName, [FromQuery] int width, [FromQuery] int height)
    {
        if (string.IsNullOrEmpty(imageName))
            return BadRequest("imageName Invalid");
        
        try
        {
            using var ms = new MemoryStream();
            await _minioClient.GetObjectAsync(_bucketName, imageName, stream => stream.CopyTo(ms));
            ms.Position = 0;
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
        catch (MinioException e)
        {
            // 处理与MinIO服务器的通信中可能出现的异常
            return StatusCode(500, e.Message);
        }
        catch (InvalidImageContentException)
        {
            // 如果无法解析图像，返回错误消息
            return BadRequest("Invalid image data.");
        }
        catch (Exception e)
        {
            return BadRequest("Get image error");
        }
    }
}