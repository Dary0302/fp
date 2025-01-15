using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Interfaces;

public interface ITextFilter
{
    public Result<IEnumerable<string>> ApplyFilter(IEnumerable<string> text);
}