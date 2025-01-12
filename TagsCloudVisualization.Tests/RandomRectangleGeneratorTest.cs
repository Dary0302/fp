using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Generators;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class RandomRectangleGeneratorTest
{
    [Test]
    public void GenerateRandomRectangles_ReturnGeneratedRectanglesNumberMatchesRequested()
    {
        var rectangles = new RandomRectangleGenerator().GenerateRectangles(10).ToList();

        rectangles.Count.Should().Be(10);
    }
    
    [Test]
    public void GenerateRandomRectangles_IsRandomRectangles_RectanglesAreGeneratedRandomly()
    {
        var rectangles1 = new RandomRectangleGenerator().GenerateRectangles(10).ToList();
        var rectangles2 = new RandomRectangleGenerator().GenerateRectangles(10).ToList();

        rectangles1.Should().NotBeEquivalentTo(rectangles2);
    }
}