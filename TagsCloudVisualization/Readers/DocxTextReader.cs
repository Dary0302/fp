using NPOI.XWPF.UserModel;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Readers;

public class DocxTextReader(TextReaderSettings settings) : ITextReader
{
    public IEnumerable<string> ReadText()
    {
        using var doc = new XWPFDocument(File.OpenRead(settings.Path));

        return doc.Paragraphs.Select(paragraph => paragraph.Text);
    }

    public IEnumerable<string> ReadText(string path)
    {
        using var doc = new XWPFDocument(File.OpenRead(path));

        return doc.Paragraphs.Select(paragraph => paragraph.Text);
    }
}