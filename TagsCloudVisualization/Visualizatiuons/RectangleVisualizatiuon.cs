using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Visualizatiuons;

public class RectangleVisualizatiuon : IRectangleDraftsman
{
    #pragma warning disable CA1416
    public Bitmap Bitmap => new(bitmap);
    private readonly Bitmap bitmap;
    private readonly Size shiftToBitmapCenter;

    public RectangleVisualizatiuon(int width, int height)
    {
        bitmap = new(width, height);
        shiftToBitmapCenter = new Size(bitmap.Width / 2, bitmap.Height / 2);
    }

    public Result<None> CreateImage(IEnumerable<Rectangle> rectangles)
    {
        if (rectangles == null)
        {
            return Result.Fail<None>("No elements to draw");
        }

        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        foreach (var r in rectangles)
        {
            var rectangle = new Rectangle(r.Location + shiftToBitmapCenter, r.Size);
            graphics.DrawRectangle(new Pen(Color.BlueViolet), rectangle);
        }
        #pragma warning restore CA1416

        return Result.Ok();
    }
}