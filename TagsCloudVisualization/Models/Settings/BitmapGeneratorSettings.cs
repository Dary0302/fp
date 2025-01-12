using System.Drawing;

namespace TagsCloudVisualization.Models.Settings;

public record BitmapGeneratorSettings(
    Size ImageSize,
    Color Background,
    Color WordsColor,
    FontFamily FontFamily);