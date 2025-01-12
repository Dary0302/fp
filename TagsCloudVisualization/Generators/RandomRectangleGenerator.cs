using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Generators;

public class RandomRectangleGenerator : IRectangleGenerator
{
    public IEnumerable<Rectangle> GenerateRectangles(int countRectangles)
    {
        var random = new Random();
        
        for (var i = 0; i < countRectangles; i++)
        {
            var size = new Size(random.Next(20,100), random.Next(10, 40));
            yield return new Rectangle(new Point(), size);
        }
    }
}