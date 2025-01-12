using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Filters;

public class LowerCaseTextFilter : ITextFilter
{
    public IEnumerable<string> ApplyFilter(IEnumerable<string> text)
    {
        return text.Select(word => word.ToLower());
    }
}