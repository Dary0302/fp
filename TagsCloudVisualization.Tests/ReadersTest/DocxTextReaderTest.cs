using System.Text;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualizationTests.ReadersTest;

[TestFixture]
public class DocxTextReaderTest
{
    private TextReaderSettings textReaderSettings;
    private DocxTextReader docxTextReader;
    
    [SetUp]
    public void SetUp()
    {
        textReaderSettings = new TextReaderSettings(@"TextTests\Docx.docx", Encoding.UTF8);
        docxTextReader = new DocxTextReader(textReaderSettings);
    }
    
    [Test]
    public void ReadText()
    {
        var result = docxTextReader.ReadText();
        result.Should().BeEquivalentTo(new List<string> { "test", "read", "text", "from", "docx", "file" });
    }
}