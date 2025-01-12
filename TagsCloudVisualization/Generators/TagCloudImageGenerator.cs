using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;
using TagsCloudVisualization.Selectors;

namespace TagsCloudVisualization.Generators;

public class TagCloudImageGenerator(
    IImageSaver saver,
    SaveSettings saveSettings,
    TextSettings textSettings,
    IFileReadersSelector fileReadersSelector,
    IBitmapGenerator bitmapGenerator,
    IEnumerable<ITextFilter> filters)
{
    public void GenerateCloud()
    {
        var text = fileReadersSelector.SelectFileReader().ReadText();

        var wordsFrequency = filters
            .Aggregate(text, (word, filter) => filter.ApplyFilter(word))
            .GroupBy(w => w)
            .OrderByDescending(words => words.Count())
            .ToDictionary(words => words.Key, words => words.Count());

        var minWordCount = wordsFrequency.Values.Min();
        var maxWordCount = wordsFrequency.Values.Max();

        var words = wordsFrequency
            .Select(w => new TagWord(w.Key, GetFontSize(w.Value, minWordCount, maxWordCount)));

        var bitmap = bitmapGenerator.GenerateBitmap(words);
        saver.SaveImageToFile(bitmap, saveSettings);
    }

    private int GetFontSize(int frequencyCount, int minWordCount, int maxWordCount) =>
        textSettings.MinFontSize + (textSettings.MaxFontSize - textSettings.MinFontSize)
        * (frequencyCount - minWordCount) / (maxWordCount - minWordCount);
}