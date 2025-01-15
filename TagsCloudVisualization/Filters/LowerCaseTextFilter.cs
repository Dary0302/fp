using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Filters;

public class LowerCaseTextFilter : ITextFilter
{
    public Result<IEnumerable<string>> ApplyFilter(IEnumerable<string> text)
    {
        return Result.Of(() => text)
            .Then(ToLowerCase)
            .OnFail(error => Result.Fail<IEnumerable<string>>($"Failed to apply ToLowerCase filter: {error}"));
    }

    private IEnumerable<string> ToLowerCase(IEnumerable<string> text) => text.Select(word => word.ToLower());
}