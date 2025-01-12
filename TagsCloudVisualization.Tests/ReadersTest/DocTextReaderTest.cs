using System.Text;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualizationTests.ReadersTest;

[TestFixture]
public class DocTextReaderTest
{
    private TextReaderSettings textReaderSettings;
    private DocTextReader docTextReader;
    
    [SetUp]
    public void SetUp()
    {
        textReaderSettings = new TextReaderSettings(@"TextTests\Doc.doc", Encoding.UTF8);
        docTextReader = new DocTextReader(textReaderSettings);
    }
    
    [Test]
    public void ReadText()
    {
        var result = docTextReader.ReadText();
        result.Should().BeEquivalentTo(new List<string> { "test", "read", "text", "from", "doc", "file" });
    }
}