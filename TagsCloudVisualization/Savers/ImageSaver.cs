using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Savers;

public class ImageSaver : IImageSaver
{
    public string SaveImageToFile(Bitmap bitmap, SaveSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Filename))
        {
            throw new ArgumentException("Filename cannot be null or empty");
        }
        
        if (!Directory.Exists(settings.FilePath))
        {
            Directory.CreateDirectory(settings.FilePath);
        }
        
        #pragma warning disable CA1416
        bitmap.Save(Path.Combine(settings.FilePath, $"{settings.Filename}.{settings.Format}"), settings.ImageFormat);
        #pragma warning restore CA1416
        Console.WriteLine($"Tag cloud visualization saved to: {Path.GetFullPath(Path.Combine(settings.FilePath, $"{settings.Filename}.{settings.Format}"))}");

        return Path.Combine(settings.FilePath, $"{settings.Filename}.{settings.Format}");
    }
}