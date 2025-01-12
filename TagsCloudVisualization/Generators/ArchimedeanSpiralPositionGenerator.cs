using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Generators;

public class ArchimedeanSpiralPositionGenerator(SpiralGeneratorSettings settings) : IPositionGenerator
{
    public Point Center => settings.Center;
    private double angle = settings.AngleOffset;
    private readonly double step = settings.SpiralStep;
    private readonly Point start = settings.Center;

    public Point GetNextPoint()
    {
        var x = (int)(start.X + step * angle * Math.Cos(angle));
        var y = (int)(start.Y + step * angle * Math.Sin(angle));
        angle += Math.PI / 40;

        return new Point(x, y);
    }
}