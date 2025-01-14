using System.Drawing;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Interfaces;

public interface IRectangleDraftsman
{
    public Bitmap Bitmap { get; }
    public Result<None> CreateImage(IEnumerable<Rectangle> objects);
}