using NPOI.XWPF.UserModel;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Readers;

public class DocxTextReader(TextReaderSettings settings) : ITextReader
{
    public Result<IEnumerable<string>> ReadText()
    {
        using var doc = new XWPFDocument(File.OpenRead(settings.Path));

        return Result.Of(() => doc.Paragraphs.Select(paragraph => paragraph.Text))
            .OnFail(error => Result.Fail<IEnumerable<string>>($"The file cannot be read: {error}"));
    }

    public Result<IEnumerable<string>> ReadText(string path)
    {
        using var doc = new XWPFDocument(File.OpenRead(path));

        return Result.Of(() => doc.Paragraphs.Select(paragraph => paragraph.Text))
            .OnFail(error => Result.Fail<IEnumerable<string>>($"The file cannot be read: {error}"));
    }
}