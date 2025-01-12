using System.Drawing;

namespace TagsCloudVisualization.Interfaces;

public interface IRectangleGenerator
{
    public IEnumerable<Rectangle> GenerateRectangles(int countRectangles);
}