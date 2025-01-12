using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Generators;

public class BitmapGenerator(ICloudLayouter layouter, BitmapGeneratorSettings settings) : IBitmapGenerator
{
    public Bitmap GenerateBitmap(IEnumerable<TagWord> words)
    {
        #pragma warning disable CA1416
        var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(settings.Background);
        var brush = new SolidBrush(settings.WordsColor);

        foreach (var word in words)
        {
            var font = new Font(settings.FontFamily, word.FontSize);
            var size = graphics.MeasureString(word.Word, font);
            var rectangle = layouter.PutNextRectangle(size.ToSize());
            var position = new PointF(rectangle.X + (rectangle.Width - size.Width) / 2,
                rectangle.Y + (rectangle.Height - size.Height) / 2);

            graphics.DrawString(word.Word, font, brush, position);
            #pragma warning restore CA1416
        }

        return bitmap;
    }
}