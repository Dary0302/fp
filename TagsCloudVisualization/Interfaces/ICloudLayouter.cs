using System.Drawing;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Interfaces;

public interface ICloudLayouter
{
    public List<Rectangle> Rectangles { get; }
    public Result<Rectangle> PutNextRectangle(Size rectangleSize);
}