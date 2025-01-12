using System.Drawing;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Interfaces;

public interface IBitmapGenerator
{
    public Bitmap GenerateBitmap(IEnumerable<TagWord> words);
}