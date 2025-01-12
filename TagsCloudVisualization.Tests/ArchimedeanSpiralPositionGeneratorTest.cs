using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Generators;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class ArchimedeanSpiralPositionGeneratorTest
{
    [TestCase(0, 0, 1)]
    [TestCase(2, 10, 2)]
    [TestCase(-1, -1, 2)]
    public void Constructor_WithCorrectParameters_NotThrow(int x, int y, double step)
    {
        var action = () => new ArchimedeanSpiralPositionGenerator(new(0, step, new(x, y)));
        action.Should().NotThrow<ArgumentException>();
    }

    [Test]
    public void GetNextPoint_WithCorrectParameters_ReturnsCorrectPoint()
    {
        var spiral = new ArchimedeanSpiralPositionGenerator(new(0, 1, new(0, 0)));
        spiral.GetNextPoint().Should().Be(new Point(0, 0));
        spiral.GetNextPoint().Should().Be(new Point((int)(Math.PI / 40 * Math.Cos(Math.PI / 40)),
            (int)(Math.PI / 40 * Math.Sin(Math.PI / 40))));
    }

    [Test]
    public void GetNextPoint_WithCorrectParameters_ReturnsCentralPoint()
    {
        var spiral = new ArchimedeanSpiralPositionGenerator(new(0, 1, new(1, 2)));
        spiral.GetNextPoint().Should().Be(new Point(1, 2));
    }
}