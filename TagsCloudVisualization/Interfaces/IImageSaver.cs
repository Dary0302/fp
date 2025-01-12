using System.Drawing;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Interfaces;

public interface IImageSaver
{
    public string SaveImageToFile(Bitmap bitmap, SaveSettings settings);
}