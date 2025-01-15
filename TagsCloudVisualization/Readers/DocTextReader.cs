using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Readers;

public class DocTextReader(TextReaderSettings settings) : ITextReader
{
    public Result<IEnumerable<string>> ReadText()
    {
        var document = new Document();
        return Result.Of(() => document)
            .Then(_ => document.LoadFromFile(settings.Path))
            .OnFail(error => Result.Fail<IEnumerable<string>>($"The file cannot be read: {error}"))
            .Then(_ => SelectText(document));
    }

    public Result<IEnumerable<string>> ReadText(string path)
    {
        var document = new Document();

        return Result.Of(() => document)
            .Then(_ => document.LoadFromFile(path))
            .OnFail(error => Result.Fail<IEnumerable<string>>($"The file cannot be read: {error}"))
            .Then(_ => SelectText(document));
    }

    private IEnumerable<string> SelectText(IDocument document)
    {
        return
            from Section section in document.Sections
            from Paragraph paragraph in section.Paragraphs
            select paragraph.Text;
    }
}