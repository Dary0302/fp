using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Filters;

public class BoringWordsTextFilter(BoringWordsSettings boringWordsSettings, ITextReader textReader) : ITextFilter
{
    public IEnumerable<string> BoringWords => [..boringWords];

    private readonly HashSet<string> boringWords =
        textReader.ReadText(boringWordsSettings.Path).GetValueOrThrow().ToHashSet();

    public Result<IEnumerable<string>> ApplyFilter(IEnumerable<string> text)
    {
        return Result.Of(() => text)
            .Then(ExcludeBoringWords)
            .OnFail(error => Result.Fail<IEnumerable<string>>($"Couldn't rule out boring words: {error}"));
    }

    public void AddBoringWords(IEnumerable<string> words)
    {
        boringWords.UnionWith(words);
    }

    public void AddBoringWord(string word)
    {
        boringWords.Add(word);
    }

    public void RemoveBoringWord(string word)
    {
        boringWords.Remove(word);
    }

    public void RemoveBoringWords(IEnumerable<string> words)
    {
        boringWords.ExceptWith(words);
    }

    public void ClearBoringWords()
    {
        boringWords.Clear();
    }

    private IEnumerable<string> ExcludeBoringWords(IEnumerable<string> text) =>
        text.Where(word => !boringWords.Contains(word));
}