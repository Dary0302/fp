using System.Drawing;

namespace TagsCloudVisualization.Interfaces;

public interface IPositionGenerator
{
    public Point Center { get; }
    public Point GetNextPoint();
}