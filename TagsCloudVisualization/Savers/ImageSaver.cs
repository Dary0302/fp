using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Savers;

public class ImageSaver : IImageSaver
{
    public Result<string> SaveImageToFile(Bitmap bitmap, SaveSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Filename))
        {
            return Result.Fail<string>("Filename cannot be null or empty");
        }

        if (!Directory.Exists(settings.FilePath))
        {
            Directory.CreateDirectory(settings.FilePath);
        }

        var path = Path.Combine(settings.FilePath, $"{settings.Filename}.{settings.Format}");
        var result = Result.Ok(path);

        result = result.Then(ValidateExtension)
            .OnFail(error => Result.Fail<string>(error));

        #pragma warning disable CA1416
        bitmap.Save(path, settings.ImageFormat);
        #pragma warning restore CA1416

        Console.WriteLine($"Tag cloud visualization saved to: {Path.GetFullPath(path)}");

        return result;
    }

    private static Result<string> ValidateExtension(string path)
    {
        var extension = Path.GetExtension(path).TrimStart('.').ToLower();
        return extension is "png" or "jpeg" or "jpg" ? Result.Ok(path) : Result.Fail<string>("Invalid extension");
    }
}