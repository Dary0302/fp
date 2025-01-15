using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Readers;

public class TxtTextReader(TextReaderSettings settings) : ITextReader
{
    public Result<IEnumerable<string>> ReadText()
    {
        return Result.Of(() => File.ReadLines(settings.Path, settings.Encoding))
            .OnFail(error => Result.Fail<IEnumerable<string>>($"The file cannot be read: {error}"));
    }

    public Result<IEnumerable<string>> ReadText(string path)
    {
        return Result.Of(() => File.ReadLines(path, settings.Encoding))
            .OnFail(error => Result.Fail<IEnumerable<string>>($"The file cannot be read: {error}"));
    }
}