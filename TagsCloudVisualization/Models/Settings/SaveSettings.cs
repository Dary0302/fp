using System.Drawing.Imaging;

namespace TagsCloudVisualization.Models.Settings;

public record SaveSettings(string FilePath, string Filename, string Format)
{
    public ImageFormat ImageFormat => Format.ToLower() switch
    {
        #pragma warning disable CA1416
        "png" => ImageFormat.Png,
        "jpeg" => ImageFormat.Jpeg,
        "jpg" => ImageFormat.Jpeg,
        #pragma warning restore CA1416
        var _ => throw new ArgumentException($"Unknown format: {Format}")
    };
}