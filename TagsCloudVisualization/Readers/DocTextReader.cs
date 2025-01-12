using Spire.Doc;
using Spire.Doc.Documents;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Readers;

public class DocTextReader(TextReaderSettings settings) : ITextReader
{
    public IEnumerable<string> ReadText()
    {
        var document = new Document();

        document.LoadFromFile(settings.Path);

        return
            from Section section in document.Sections
            from Paragraph paragraph in section.Paragraphs
            select paragraph.Text;
    }

    public IEnumerable<string> ReadText(string path)
    {
        var document = new Document();

        document.LoadFromFile(path);

        return
            from Section section in document.Sections
            from Paragraph paragraph in section.Paragraphs
            select paragraph.Text;
    }
}