using System.Drawing;
using ICSharpCode.SharpZipLib;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Generators;

public class BitmapGenerator(ICloudLayouter layouter, BitmapGeneratorSettings settings) : IBitmapGenerator
{
    public Result<Bitmap> GenerateBitmap(IEnumerable<TagWord> words)
    {
        #pragma warning disable CA1416
        var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(settings.Background);
        var brush = new SolidBrush(settings.WordsColor);
        var results = from word in words
            let font = new Font(settings.FontFamily, word.FontSize)
            let size = graphics.MeasureString(word.Word, font)
            select layouter.PutNextRectangle(size.ToSize())
                .Then(rectangle => GetPosition(rectangle, size))
                .Then(CheckIsOutOfBoundsBitmap)
                .Then(position => graphics.DrawString(word.Word, font, brush, position));

        var combinedResult = Result.Combine(results);
        return combinedResult.IsSuccess ? Result.Ok(bitmap) : Result.Fail<Bitmap>(combinedResult.Error);
        #pragma warning restore CA1416
    }

    private static PointF GetPosition(Rectangle rectangle, SizeF size) =>
        new(rectangle.X + (rectangle.Width - size.Width) / 2, rectangle.Y + (rectangle.Height - size.Height) / 2);

    private Result<PointF> CheckIsOutOfBoundsBitmap(PointF position)
    {
        if (settings.ImageSize.Height >= Math.Abs(position.Y) && settings.ImageSize.Width >= Math.Abs(position.X))
        {
            return Result.Ok(position);
        }

        return Result.Fail<PointF>($"Word is out of bounds of bitmap with point: {position}");
    }
}