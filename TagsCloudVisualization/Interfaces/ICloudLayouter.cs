using System.Drawing;

namespace TagsCloudVisualization.Interfaces;

public interface ICloudLayouter
{
    public List<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
}