using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Visualizatiuons;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class RectangleVisualizatiuonTest
{
    private RectangleVisualizatiuon drawer;

    [SetUp]
    public void SetUp()
    {
        #pragma warning disable CA1416
        drawer = new RectangleVisualizatiuon(1500, 1500);
    }

    [Test]
    public void CreateImage_WhenListOfRectanglesIsNull_ThrowsArgumentException()
    {
        var action = () => drawer.CreateImage(null!);
        action.Invoke().Error.Should().Be("No elements to draw");
    }

    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(1, 0)]
    [TestCase(0, 1)]
    public void Constructor_OnInvalidArguments_ThrowsArgumentException(int width, int height)
    {
        var action = () => new RectangleVisualizatiuon(width, height);
        action.Should().Throw<ArgumentException>();
        #pragma warning restore CA1416
    }
}