using System.Drawing;

namespace TagsCloudVisualization.Interfaces;

public interface IRectangleDraftsman
{
    public Bitmap Bitmap { get; }
    public void CreateImage(IEnumerable<Rectangle> objects);
}