using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Generators;

public class TagCloudImageGenerator(
    IImageSaver saver,
    SaveSettings saveSettings,
    TextSettings textSettings,
    IFileReadersSelector fileReadersSelector,
    IBitmapGenerator bitmapGenerator,
    IEnumerable<ITextFilter> filters)
{
    public Result<None> GenerateCloud()
    {
        #pragma warning disable CA1416
        fileReadersSelector
            .SelectFileReader()
            .Then(textReader => textReader.ReadText())
            .Then(GetWordsFrequency)
            .Then(GetWords)
            .Then(bitmapGenerator.GenerateBitmap)
            #pragma warning restore CA1416
            .Then(bitmap => saver.SaveImageToFile(bitmap, saveSettings));

        return Result.Ok();
    }

    private IEnumerable<TagWord> GetWords(Dictionary<string, int> wordsFrequency)
    {
        var minWordCount = wordsFrequency.Values.Min();
        var maxWordCount = wordsFrequency.Values.Max();

        var words = wordsFrequency
            .Select(w => new TagWord(w.Key, GetFontSize(w.Value, minWordCount, maxWordCount)));
        return words;
    }

    private Dictionary<string, int> GetWordsFrequency(IEnumerable<string> text) =>
        filters
            .Aggregate(text, (word, filter) => filter.ApplyFilter(word).GetValueOrThrow())
            .GroupBy(w => w)
            .OrderByDescending(words => words.Count())
            .ToDictionary(words => words.Key, words => words.Count());

    private int GetFontSize(int frequencyCount, int minWordCount, int maxWordCount) =>
        textSettings.MinFontSize + (textSettings.MaxFontSize - textSettings.MinFontSize)
        * (frequencyCount - minWordCount) / (maxWordCount - minWordCount);
}