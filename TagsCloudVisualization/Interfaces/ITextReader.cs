using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Interfaces;

public interface ITextReader
{
    public Result<IEnumerable<string>> ReadText();
    public Result<IEnumerable<string>> ReadText(string path);
}