using System.Drawing;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Interfaces;

public interface IImageSaver
{
    public Result<string> SaveImageToFile(Bitmap bitmap, SaveSettings settings);
}