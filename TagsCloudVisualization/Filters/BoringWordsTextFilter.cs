using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Filters;

public class BoringWordsTextFilter(BoringWordsSettings boringWordsSettings, ITextReader textReader) : ITextFilter
{
    public IEnumerable<string> BoringWords => [..boringWords];
    private readonly HashSet<string> boringWords = textReader.ReadText(boringWordsSettings.Path).ToHashSet();

    public IEnumerable<string> ApplyFilter(IEnumerable<string> text)
    {
        return text.Where(word => !boringWords.Contains(word));
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
}